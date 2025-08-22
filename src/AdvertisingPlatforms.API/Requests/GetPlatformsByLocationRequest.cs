using AdvertisingPlatforms.Application.Queries.GetPlatformsByLocation;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatforms.API.Requests;

public record GetPlatformsByLocationRequest(string LocationPath)
{
    public GetPlatformsByLocationQuery ToCommand()
    {
        return new GetPlatformsByLocationQuery(LocationPath);
    }
}