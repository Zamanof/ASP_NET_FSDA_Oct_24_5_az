using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace ASP_NET_11._Validation.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception occurred while processing request");

        context.Response.ContentType = "application/problem+json";

        ProblemDetails problem;
        int statusCode;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                problem = CreateValidationProblemDetails(context, validationException, statusCode);
                break;

            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                problem = CreateProblemDetails(
                    context,
                    statusCode,
                    "Resource not found",
                    exception.Message);
                break;

            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                problem = CreateProblemDetails(
                    context,
                    statusCode,
                    "Invalid request",
                    exception.Message);
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                problem = CreateProblemDetails(
                    context,
                    statusCode,
                    "An unexpected error occurred",
                    "An unexpected error occurred while processing your request.");
                break;
        }

        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(problem);

        await context.Response.WriteAsync(json);
    }

    private static ProblemDetails CreateProblemDetails(
        HttpContext context,
        int statusCode,
        string title,
        string? detail = null)
    {
        return new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = title,
            Status = statusCode,
            Detail = detail,
            Instance = context.Request.Path
        };
    }

    private static ProblemDetails CreateValidationProblemDetails(
        HttpContext context,
        ValidationException exception,
        int statusCode)
    {
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        var problem = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7807#section-3.1",
            Title = "One or more validation errors occurred.",
            Status = statusCode,
            Detail = "See the 'errors' property for more details.",
            Instance = context.Request.Path
        };

        problem.Extensions["errors"] = errors;

        return problem;
    }
}
