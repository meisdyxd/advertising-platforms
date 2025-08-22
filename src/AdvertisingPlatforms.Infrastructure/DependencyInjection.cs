using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Infrastructure.PlatformsTree;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisingPlatforms.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IPlatformTree, PlatformTree>();

        return services;
    }
}
