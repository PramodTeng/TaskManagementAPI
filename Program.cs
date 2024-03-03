using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using Task_Management_API.Interface;
using Task_Management_API.Services;
using Task_Management_API.Auth;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using MimeKit;
using System.Net;
using Swashbuckle.AspNetCore.Filters;
using Hangfire;
using Hangfire.SqlServer;
using Task_Management_API;
using Task_Management_API.Middleware;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))


        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfire(configuration => configuration
             .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
             {
                 CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                 QueuePollInterval = TimeSpan.Zero,
                 UseRecommendedIsolationLevel = true,
                 DisableGlobalLocks = true
             })
             .WithJobExpirationTimeout(TimeSpan.FromDays(30))
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings());
builder.Services.BuildServiceProvider().GetRequiredService<IGlobalConfiguration>();


builder.Services.AddHangfireServer();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskDependencyService, TaskDependencyService>();
builder.Services.AddScoped<IHangfireBackgroundJobService, HangfireBackgroundJobService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Add ApplicationDbContext

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocExpansion(DocExpansion.None);
    });

}

app.UseAuthentication();
app.UseHttpsRedirection();
RecurringJob.AddOrUpdate<IHangfireBackgroundJobService>("DailyRecurranceJob", job => job.DailyRecurranceJob(), Cron.Daily);
RecurringJob.AddOrUpdate<IHangfireBackgroundJobService>("WeeklyRecurranceJob", job => job.WeeklyRecurranceJob(), Cron.Weekly);
RecurringJob.AddOrUpdate<IHangfireBackgroundJobService>("MonthlyRecurranceJob", job => job.MonthlyRecurranceJob(), Cron.Monthly);
app.UseAuthorization();
app.UseHangfireDashboard();
app.AddGlobalErrorHandler();
app.MapControllers();




app.Run();
