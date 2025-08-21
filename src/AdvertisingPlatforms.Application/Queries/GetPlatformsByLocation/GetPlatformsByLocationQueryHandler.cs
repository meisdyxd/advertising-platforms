using AdvertisingPlatforms.Application.Services;
using MediatR;

namespace AdvertisingPlatforms.Application.Queries.GetPlatformsByLocation;

public class GetPlatformsByLocationQueryHandler : IRequestHandler<GetPlatformsByLocationQuery, HashSet<string>>
{
    private readonly PlatformService _platformService;

    public GetPlatformsByLocationQueryHandler(PlatformService platformService)
    {
        _platformService = platformService;
    }
    
    public async Task<HashSet<string>> Handle(
        GetPlatformsByLocationQuery query, 
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(_platformService.GetElements(query.Location));
    }
}