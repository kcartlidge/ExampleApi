using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExampleApi.Swagger
{
    /// <summary>Attribute for adding a correlation ID to API requests.</summary>
    public class CorrelationIdAttribute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();
            var example = new Microsoft.OpenApi.Any.OpenApiString(Guid.NewGuid().ToString());
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "correlation-id",
                In = ParameterLocation.Header,
                Example = example,
                AllowEmptyValue = false,
                Required = true,
                Description = "An ID which is passed into nested calls enabling them to be grouped together.",
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
