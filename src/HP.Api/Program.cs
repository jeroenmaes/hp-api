
using HealthChecks.UI.Client;
using HP.Api.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace HP.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddTransient<DataService>();
      builder.Services.AddControllers();
      
      builder.Services.AddOpenApi();

      builder.Services.AddHealthChecks();
      builder.Services.AddMemoryCache();

      builder.Services.AddHostedService<CacheService>();

      builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

      var app = builder.Build();

      app.MapOpenApi();

      app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "v1"); });

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.MapHealthChecks("/health", new HealthCheckOptions
      {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });

      app.Run();
    }
  }
}
