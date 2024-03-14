using RequestCounter.WebApi.Middlewares;

namespace RequestCounter.WebApi.Pipeline;

public static class WebApiPipeline
{
    public static void ConfigureWebApi(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<RequestCountMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}
