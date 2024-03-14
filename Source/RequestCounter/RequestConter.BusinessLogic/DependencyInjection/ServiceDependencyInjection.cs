using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestCounter.DataAccess.Repository.DependencyInjection;

namespace RequestConter.BusinessLogic.DependencyInjection;

public static class ServiceDependencyInjection
{
    public static void ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureRepositories(configuration);

        services.AddScoped<IRequestService, RequestService>();
    }
}