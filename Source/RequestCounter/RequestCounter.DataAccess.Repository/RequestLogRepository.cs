using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RequestCounter.DataAccess.DataContext;
using System.Data;

namespace RequestCounter.DataAccess.Repository;

public class RequestLogRepository : Repository<RequestLog>, IRequestLogRepository
{
    private readonly IConfiguration _configuration;

    public RequestLogRepository(
        RequestContext context,
        IConfiguration configuration)
        : base(context)
    {
        _configuration = configuration;
    }

    private string? GetConnectionString()
    {
        return _configuration.GetConnectionString("RequestLog");
    }

    public async Task UpdateApiRequestCountAsync(string endpointName)
    {
        using (var connection = new SqlConnection(GetConnectionString()))
        {
            await connection.OpenAsync();

            using (var transaction = await connection.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {
                    await connection.ExecuteAsync("IncrementRequestCounter", new { endpointName }, transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollBackException)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", rollBackException.GetType());
                        Console.WriteLine("  Message: {0}", rollBackException.Message);
                    }

                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}