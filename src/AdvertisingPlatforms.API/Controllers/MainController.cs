using AdvertisingPlatforms.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatforms.API.Controllers;

[ApiController]
public class MainController : ControllerBase
{
    protected OkObjectResult Ok<TObject>(TObject? @object = null)
    where TObject : class
    {
        return new OkObjectResult(new Envelope()
        {
            Result = @object,
            Errors = null,
            DateTime = DateTime.UtcNow,
        });
    }
}
