using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tenebria.Shared.Module.Consts;

namespace Tenebria.Api.OpenApi;

public class LangCdHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = TenebriaRequestConst.HTTP_LANG_KEY,
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String
            },
            Description = $"Language code"
        });
    }
}
