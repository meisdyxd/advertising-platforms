using AdvertisingPlatforms.Domain.Shared;

namespace AdvertisingPlatforms.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
        catch (ValidationErrorException ex)
        {
            _logger.LogError(ex, "Handled error, with message: {message}", ex.Message);
            var envelope = new Envelope
            {
                Result = null,
                Errors = ex.Errors,
                DateTime = DateTime.UtcNow
            };
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(envelope);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Handled error, with message: {message}", ex.Message);
            var envelope = new Envelope
            {
                Result = null,
                Errors = [ex.Message],
                DateTime = DateTime.UtcNow
            };
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
