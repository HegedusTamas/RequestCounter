using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestCounter.DataAccess.DataContext.DependencyInjection;

namespace RequestCounter.DataAccess.Repository.DependencyInjection;

public static class RegisterDependencies
{
    public static void ConfigureRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureContext(configuration);

        services.AddScoped<IRequestLogRepository, RequestLogRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}