using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RequestConter.BusinessLogic;
using RequestCounter.DataAccess.DataContext;
using RequestCounter.DataAccess.Repository;

namespace Test.BusinessLogic;

public class Tests
{
    private IConfiguration _configuration;

    private DbContextOptions<RequestContext> _contextOptions;

    [SetUp]
    public void Setup()
    {
        _configuration = InitConfiguration();

        _contextOptions = new DbContextOptionsBuilder<RequestContext>()
               .UseSqlServer(_configuration.GetConnectionString("RequestLog"))
               .EnableSensitiveDataLogging()
            .Options;
    }

    [Test]
    public async Task CountRequestWithStoredProcedureAsync()
    {
        string endpointName = $"/api/testendpoint/{GenerateRandomString(5)}";

        const int executionCount = 10000;

        var tasks = new List<Task>();

        for (int executionIndex = 0; executionIndex < executionCount; executionIndex++)
        {
            IRequestService requestServiceToCountRequest = InitializeRequestService();

            tasks.Add(Task.Factory.StartNew(async () => await requestServiceToCountRequest.CountRequestWithStoredProcedureAsync(endpointName)));
        }

        try
        {
            Task.WaitAll(tasks.ToArray());

            IRequestService requestServiceToRetrieve = InitializeRequestService();

            var request = await requestServiceToRetrieve.GetRequestLogAsync(endpointName, DateTime.Today, DateTime.Today);

            Assert.True(request.Single().RequestCount == executionCount);
        }
        catch (AggregateException e)
        {
            Console.WriteLine("\nThe following exceptions have been thrown by WaitAll():");
            for (int innerExceptionCounter = 0; innerExceptionCounter < e.InnerExceptions.Count; innerExceptionCounter++)
            {
                Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[innerExceptionCounter].ToString());
            }
        }
    }

    private IConfiguration InitConfiguration()
    {
        return new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
            .Build();
    }

    private IRequestService InitializeRequestService()
    {
        RequestContext context = new RequestContext(_contextOptions);

        IRequestLogRepository repository = new RequestLogRepository(context, _configuration);

        IUnitOfWork unitOfWork = new UnitOfWork(context, repository);

        return new RequestService(unitOfWork);
    }

    public string GenerateRandomString(int length)
    {
        var random = new Random();

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}