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
        
        var sr = new StreamReader(command.File.Stream);
        _platformService.UploadPlatforms(sr);
        
        return Task.CompletedTask;
    }
}