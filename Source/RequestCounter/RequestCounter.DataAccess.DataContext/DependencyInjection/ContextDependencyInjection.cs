using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RequestCounter.DataAccess.DataContext.DependencyInjection;

public static class ContextDependencyInjection
{
    public static void ConfigureContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RequestContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("RequestLog")));
    }
}