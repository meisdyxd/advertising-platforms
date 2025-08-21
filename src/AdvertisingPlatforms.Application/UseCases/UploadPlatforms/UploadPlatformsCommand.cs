using AdvertisingPlatforms.Contracts.Dtos;
using MediatR;

namespace AdvertisingPlatforms.Application.UseCases.UploadPlatforms;

public class UploadPlatformsCommand : IRequest
{
    public FileDto File { get; set; }

    public UploadPlatformsCommand(FileDto file)
    {
        File = file;
    }
}