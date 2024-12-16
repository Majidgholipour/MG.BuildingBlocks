
using MG.BuildingBlock.Application.Features;
using Microsoft.AspNetCore.Mvc;

namespace MG.BuildingBlock.Api.Controlles;

[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController() : ControllerBase
{
    protected IActionResult ApiResult(Result result)
    {
        return Ok(result);
    }
}
