using AdvertisingPlatforms.Application.Services;
using Microsoft.Extensions.Logging;
using MediatR;

namespace AdvertisingPlatforms.Application.Queries.GetPlatformsByLocation;

public class GetPlatformsByLocationQueryHandler : IRequestHandler<GetPlatformsByLocationQuery, HashSet<string>>
{
    private readonly PlatformService _platformService;
    private readonly ILogger<GetPlatformsByLocationQueryHandler> _logger;

    public GetPlatformsByLocationQueryHandler(
        PlatformService platformService,
        ILogger<GetPlatformsByLocationQueryHandler> logger)
    {
        _platformService = platformService;
        _logger = logger;
    }
    
    public async Task<HashSet<string>> Handle(
        GetPlatformsByLocationQuery query, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get platforms by location: {location}", query.Location);
        return await Task.FromResult(_platformService.GetElements(query.Location));
    }
}