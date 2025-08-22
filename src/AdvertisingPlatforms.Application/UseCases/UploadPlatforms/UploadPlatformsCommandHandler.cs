using AdvertisingPlatforms.Application.Services;
using Microsoft.Extensions.Logging;
using MediatR;

namespace AdvertisingPlatforms.Application.UseCases.UploadPlatforms;

public class UploadPlatformsCommandHandler: IRequestHandler<UploadPlatformsCommand>
{
    private readonly PlatformService _platformService;
    private readonly ILogger<UploadPlatformsCommandHandler> _logger;

    public UploadPlatformsCommandHandler(
        PlatformService platformService,
        ILogger<UploadPlatformsCommandHandler> logger)
    {
        _platformService = platformService;
        _logger = logger;
    }
    
    public Task Handle(
        UploadPlatformsCommand command, 
        CancellationToken cancellationToken)
    {
        var filename = command.File.FileName;
        _logger.LogInformation("Started upload data from filename: {filename}", filename);

        var sr = new StreamReader(command.File.Stream);
        _platformService.UploadPlatforms(sr);

        _logger.LogInformation("Successfuly upload data from filename: {filename}", filename);
        return Task.CompletedTask;
    }
}