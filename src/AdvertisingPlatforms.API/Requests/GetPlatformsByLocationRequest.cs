using AdvertisingPlatforms.Application.Queries.GetPlatformsByLocation;

namespace AdvertisingPlatforms.API.Requests;

public record GetPlatformsByLocationRequest(string LocationPath)
{
    public GetPlatformsByLocationQuery ToCommand()
    {
        return new GetPlatformsByLocationQuery(LocationPath);
    }
}