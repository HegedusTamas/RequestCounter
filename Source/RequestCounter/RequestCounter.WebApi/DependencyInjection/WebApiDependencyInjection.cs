using RequestConter.BusinessLogic.DependencyInjection;

namespace RequestCounter.WebApi.DependencyInjection;

public static class WebApiDependencyInjection
{
    public static void ConfigureWebApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureServices(configuration);
    }
}