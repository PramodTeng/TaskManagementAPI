using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Task_Management_API.Exceptions;

using KeyNotFoundException = Task_Management_API.Exceptions.KeyNotFoundException;
using NotImplemenetedException = Task_Management_API.Exceptions.NotImplemenetedException;
using UnauthorizedAccessException = Task_Management_API.Exceptions.UnauthorizedAccessException;




namespace Task_Management_API.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;

    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);

        }
        catch (Exception ex)
        {

            await HanldeExceptionAsync(context, ex);

        }
    }


    private static Task HanldeExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        string message = "";


        var exceptionType = ex.GetType();
        if (exceptionType == typeof(BadRequestException))
        {
            message = ex.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = ex.StackTrace;

        }
        else if (exceptionType == typeof(NotImplemenetedException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotImplemented;
            stackTrace = ex.StackTrace;

        }
        else if (exceptionType == typeof(KeyNotFoundException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = ex.StackTrace;

        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            message = ex.Message;
            status = HttpStatusCode.Unauthorized;
            stackTrace = ex.StackTrace;

        }
        else
        {
            message = ex.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = ex.StackTrace;

        }
        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        return context.Response.WriteAsync(exceptionResult);
    }
}


