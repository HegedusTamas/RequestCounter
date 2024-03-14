using Microsoft.AspNetCore.Mvc;
using RequestCounter.WebApi.Attributes;

namespace RequestCounter.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    public ActorController()
    {
    }

    [CountRequest]
    [Route("ActorEndPointOne")]
    [HttpGet]
    public IActionResult ActorEndPointOne()
    {
        return Ok();
    }

    [CountRequest]
    [Route("ActorEndPointTwo")]
    [HttpGet]
    public IActionResult ActorEndPointTwo()
    {
        return Ok();
    }

    [CountRequest]
    [Route("ActorEndPointThree")]
    [HttpGet]
    public IActionResult ActorEndPointThree()
    {
        return Ok();
    }
}