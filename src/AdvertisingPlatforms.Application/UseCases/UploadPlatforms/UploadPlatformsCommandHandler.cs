using AdvertisingPlatforms.Application.Services;
using MediatR;

namespace AdvertisingPlatforms.Application.UseCases.UploadPlatforms;

public class UploadPlatformsCommandHandler: IRequestHandler<UploadPlatformsCommand>
{
    private readonly PlatformService _platformService;

    public UploadPlatformsCommandHandler(PlatformService platformService)
    {
        _platformService = platformService;
    }
    
    public Task Handle(
        UploadPlatformsCommand command, 
        CancellationToken cancellationToken)
    {
        var filename = command.File.FileName;
        if (Path.GetExtension(filename) != ".txt")
            throw new Exception("Invalid file extension");
        
        var sr = new StreamReader(command.File.Stream);
        _platformService.UploadPlatforms(sr);
        
        return Task.CompletedTask;
    }
}