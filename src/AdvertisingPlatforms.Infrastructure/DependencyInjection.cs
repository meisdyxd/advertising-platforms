using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Infrastructure.PlatformsTree;
using AdvertisingPlatforms.Infrastructure.PlatformsTree.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisingPlatforms.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services
            .ConfigueOptions(configuration)
            .AddSingleton<IPlatformTree, PlatformTree>();

        return services;
    }

    public static IServiceCollection ConfigueOptions(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddOptions<PlatformTreeOptions>()
            .Bind(configuration)
            .ValidateOnStart();

        return services;
    }
}
