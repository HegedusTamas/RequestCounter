using RequestCounter.DataAccess.DataContext;

namespace RequestConter.BusinessLogic;

public interface IRequestService
{
    Task CountRequestWithStoredProcedureAsync(string endpointName);

    Task<IEnumerable<RequestLog>> GetRequestLogAsync(DateTime from, DateTime to);

    Task<IEnumerable<RequestLog>> GetRequestLogAsync(string endPointName, DateTime from, DateTime to);
}