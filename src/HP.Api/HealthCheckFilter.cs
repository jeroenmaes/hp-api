﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HP.Api
{
  public class HealthChecksFilter : IDocumentFilter
  {
    public const string HealthCheckEndpoint = @"/api/health";

    public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
    {
      var pathItem = new OpenApiPathItem();
      var operation = new OpenApiOperation();

      operation.Tags.Add(new OpenApiTag { Name = "Health" });

      var properties = new Dictionary<string, OpenApiSchema>
            {
                { "status", new OpenApiSchema() { Type = "string" } }
            };

      var response = new OpenApiResponse();
      response.Description = "Success";
      response.Content.Add("application/json", new OpenApiMediaType
      {        
        Schema = new OpenApiSchema
        {
          Type = "object",
          AdditionalPropertiesAllowed = true,
          Properties = properties
        }
      });

      operation.Responses.Add("200", response);
      pathItem.AddOperation(OperationType.Get, operation);
      openApiDocument?.Paths.Add(HealthCheckEndpoint, pathItem);
    }
  }
}
