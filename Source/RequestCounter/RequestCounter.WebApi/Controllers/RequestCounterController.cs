using Microsoft.AspNetCore.Mvc;
using RequestConter.BusinessLogic;
using RequestCounter.DataAccess.DataContext;

namespace RequestCounter.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestCounterController : ControllerBase
{
    private readonly IRequestService _requestService;

    public RequestCounterController(
        IRequestService requestService)
    {
        _requestService = requestService;
    }

    [Route("GetRequestLog")]
    [HttpGet]
    public async Task<IEnumerable<RequestLog>> GetRequestLogAsync(DateTime from, DateTime to)
    {
        return await _requestService.GetRequestLogAsync(from, to);
    }
}