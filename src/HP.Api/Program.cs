
using System.Text.Json.Serialization;
using System.Text.Json;
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

      builder.Services.AddControllers().AddJsonOptions(o =>
      {
        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // ignore default values, including null for nullable types        
        o.JsonSerializerOptions.WriteIndented = true;
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        o.JsonSerializerOptions.AllowTrailingCommas = true;
        o.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
      });

      builder.Services.AddOpenApi();

      builder.Services.AddHealthChecks();
      builder.Services.AddMemoryCache();

      builder.Services.AddHostedService<CacheService>();

      builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

      builder.Services.AddOutputCache(options =>
      {
        options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(10)));        
      });

      var app = builder.Build();

      app.UseOutputCache();

      app.MapOpenApi().CacheOutput();

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
