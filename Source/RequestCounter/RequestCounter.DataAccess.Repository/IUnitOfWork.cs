namespace RequestCounter.DataAccess.Repository;

public interface IUnitOfWork : IDisposable
{
    IRequestLogRepository RequestLogRepository { get; set; }

    Task<int> CompleteAsync();
}
