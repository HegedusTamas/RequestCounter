using RequestConter.BusinessLogic;
using RequestCounter.WebApi.Attributes;

namespace RequestCounter.WebApi.Middlewares;

public class RequestCountMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCountMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IRequestService requestService)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint?.Metadata.GetMetadata<CountRequestAttribute>() != null)
        {
            await requestService.CountRequestWithStoredProcedureAsync(endpoint.DisplayName);
        }

        await _next(context);
    }
}
