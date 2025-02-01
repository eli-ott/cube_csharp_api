using System.Data;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MonApi.API.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyName = "ApiKey";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the current route is the Swagger documentation to bypass the api key verification
        var isSwagger = context.Request.Path.ToString().ToLower().Contains("swagger");

        if (!isSwagger)
        {
            // Check if the api key was provided in the headers
            if (!context.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey))
            {
                throw new AuthenticationException("Api Key was not provided.");
            }

            var apiKey = Environment.GetEnvironmentVariable("API_KEY") ??
                         throw new InvalidExpressionException("Can't find API_KEY in environment variable.");

            if (!apiKey.Equals(extractedApiKey))
            {
                throw new AuthenticationException("Unauthorized client. API key invalid.");
            }
        }

        await _next(context);
    }
}