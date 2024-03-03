using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Management_API.Middleware
{
    public static class ApplicationBuilderExceptions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    }
}