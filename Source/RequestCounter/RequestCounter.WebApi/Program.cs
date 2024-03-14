using RequestCounter.WebApi.DependencyInjection;
using RequestCounter.WebApi.Pipeline;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureWebApi(builder.Configuration);

WebApplication app = builder.Build();

app.ConfigureWebApi();

app.Run();