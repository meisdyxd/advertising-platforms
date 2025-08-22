using AdvertisingPlatforms.Domain.Shared;

namespace AdvertisingPlatforms.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationErrorException ex)
        {
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
