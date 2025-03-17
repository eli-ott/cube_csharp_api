using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MonApi.Shared.Exceptions;


namespace MonApi.API.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        private readonly bool _isDevelopment;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _isDevelopment = env.IsDevelopment();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the unhandled exception
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                ArgumentException => (StatusCodes.Status400BadRequest, "Invalid argument provided."),

                KeyNotFoundException => (StatusCodes.Status404NotFound, "The requested resource was not found."),

                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access."),

                SoftDeletedException => (StatusCodes.Status410Gone, "The requested resource has been deleted."),

                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var developerDetails = _isDevelopment ? exception.ToString() : null;

            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = message,
                ExceptionMessage = exception.Message,
                Details = developerDetails
            };

            // Return the JSON response
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }

}
