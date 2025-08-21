using MediatR;

namespace AdvertisingPlatforms.Application.Queries.GetPlatformsByLocation;

public class GetPlatformsByLocationQuery : IRequest<HashSet<string>>
{
    public string Location { get; set; }

    public GetPlatformsByLocationQuery(string location)
    {
        Location = location;
    }
}