using RequestCounter.DataAccess.DataContext;
using RequestCounter.DataAccess.Repository;

namespace RequestConter.BusinessLogic;

public class RequestService : IRequestService
{
    private readonly IUnitOfWork _unitOfWork;

    public RequestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RequestLog>> GetRequestLogAsync(DateTime from, DateTime to)
    {
        return await _unitOfWork.RequestLogRepository.FindAsync(r => r.Date >= from.Date && r.Date.Date <= to);
    }
   
    public async Task<IEnumerable<RequestLog>> GetRequestLogAsync(string endPointName, DateTime from, DateTime to)
    {
        return await _unitOfWork.RequestLogRepository.FindAsync(
            r => endPointName == r.EndPointName
            && r.Date >= from.Date && r.Date.Date <= to);
    }    

    public async Task CountRequestWithStoredProcedureAsync(string endpointName)
    {
        await _unitOfWork.RequestLogRepository.UpdateApiRequestCountAsync(endpointName);
    }
}