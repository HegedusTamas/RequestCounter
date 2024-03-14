using RequestCounter.DataAccess.DataContext;

namespace RequestCounter.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly RequestContext _context;

    public UnitOfWork(RequestContext context,
        IRequestLogRepository requestLogRepository)
    {
        _context = context;

        RequestLogRepository = requestLogRepository;
    }

    public IRequestLogRepository RequestLogRepository { get; set; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
