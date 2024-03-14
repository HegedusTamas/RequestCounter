using RequestCounter.DataAccess.DataContext;

namespace RequestCounter.DataAccess.Repository;

public interface IRequestLogRepository : IRepository<RequestLog>
{
    Task UpdateApiRequestCountAsync(string endpointName);
}