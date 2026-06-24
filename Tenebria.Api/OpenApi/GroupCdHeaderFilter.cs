using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tenebria.Shared.Module.Consts;

namespace Tenebria.Api.OpenApi;

public class GroupCdHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = TenebriaRequestConst.HTTP_GROUP_KEY,
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String
            },
            Description = $"Role name"
        });
    }
}
