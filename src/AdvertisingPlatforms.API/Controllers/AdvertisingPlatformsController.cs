using AdvertisingPlatforms.API.Processors;
using AdvertisingPlatforms.API.Requests;
using AdvertisingPlatforms.Application.Queries.GetPlatformsByLocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatforms.API.Controllers;

[ApiController]
[Route("api/platforms")]
public class AdvertisingPlatformsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdvertisingPlatformsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{*locationPath}")]
    public async Task<IActionResult> Get(
        [FromRoute] string locationPath)
    {
        var query = new GetPlatformsByLocationQuery(locationPath);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(
        [FromForm] UploadPlatformRequest request,
        CancellationToken cancellationToken)
    {
        await using var processor = new FormFileProcessor();
        var file =  processor.Process(request.File);
        var command = request.ToCommand(file);
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
}