using AdvertisingPlatforms.API.Processors;
using AdvertisingPlatforms.API.Requests;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace AdvertisingPlatforms.API.Controllers;

[Route("api/platforms")]
public class AdvertisingPlatformsController : MainController
{
    private readonly IMediator _mediator;

    public AdvertisingPlatformsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{*LocationPath}")]
    public async Task<IActionResult> Get(
        [FromRoute] GetPlatformsByLocationRequest request,
        CancellationToken cancellationToken)
    {
        var query = request.ToCommand();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(
        [FromForm] UploadPlatformRequest request,
        CancellationToken cancellationToken)
    {
        await using var processor = new FormFileProcessor();
        var file = processor.Process(request.File);
        var command = request.ToCommand(file);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}