using Microsoft.OpenApi.Models;

namespace AdvertisingPlatforms.API.Registrations;

public static class SwaggerRegistration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Advertising Platforms", Version = "v1" });
        });

        return services;
    }

    public static WebApplication UseSwaggerCustom(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
