using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Task_Management_API.Data.Entity;

namespace Task_Management_API.Auth
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<TaskDependency>()
                .HasKey(td => td.DependencyId); 

            modelBuilder.Entity<TaskDependency>()
                .HasOne(td => td.TaskItem)
                .WithMany(t => t.Dependencies)
                .HasForeignKey(td => td.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskDependency>()
                .HasOne(td => td.DependentTaskItem)
                .WithMany()
                .HasForeignKey(td => td.DependentTaskItemId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskDependency> TaskDependency { get; set; }
    }
}