using AdvertisingPlatforms.Application.UseCases.UploadPlatforms;
using AdvertisingPlatforms.Contracts.Dtos;

namespace AdvertisingPlatforms.API.Requests;

public record UploadPlatformRequest(IFormFile File)
{
    public UploadPlatformsCommand ToCommand(FileDto file)
    {
        return new UploadPlatformsCommand(file);
    }
}