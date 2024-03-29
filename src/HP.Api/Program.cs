﻿
using HealthChecks.UI.Client;
using HP.Api.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(options => { options.DocumentFilter<HealthChecksFilter>(); });
      builder.Services.AddHealthChecks();
      builder.Services.AddMemoryCache();

      builder.Services.AddHostedService<CacheService>();

      builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

      var app = builder.Build();

      app.UseSwagger();
      app.UseSwaggerUI();

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.MapHealthChecks("/v1/health", new HealthCheckOptions
      {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });

      app.Run();
    }
  }
}
