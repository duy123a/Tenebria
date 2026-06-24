using Microsoft.OpenApi;
using Tenebria.Shared.Module.Configuration.Settings;

namespace Tenebria.Api.OpenApi;

public static class OpenApiConfiguration
{
    public static IServiceCollection AddSwaggerGen(this IServiceCollection services, List<string> openApiFiles)
    {
        services.AddEndpointsApiExplorer();

        // Swagger UI
        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tenebria.Api" });
                options.CustomSchemaIds(s => s.FullName?.Replace("+", "."));
                options.SupportNonNullableReferenceTypes();

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme.<br><br>
                          Enter 'Bearer' [space] and then your token in the text input below.
                          <br><br>Example: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                    });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement()
                {
                    [new("Bearer", document)] = []
                });

                // Custom header
                options.OperationFilter<LangCdHeaderFilter>();
                options.OperationFilter<GroupCdHeaderFilter>();

                foreach (string file in openApiFiles)
                {
                    options.IncludeXmlComments(file, true);
                }
            });

        return services;
    }

    public static void AddOpenApiDocument(this WebApplication app, OpenApiSetting cfg)
    {
        app.UseSwagger();

        GenOpenApiDocument(app, "Tenebria", cfg.WebOpenApiUrl, "swagger");
        GenOpenApiDocument(app, "Keycloak", cfg.KeycloakOpenApiUrl, "keycloak");
    }

    private static void GenOpenApiDocument(this WebApplication app, string openApiName, string openApiUrl, string mainRouter)
    {
        if (string.IsNullOrEmpty(openApiUrl))
            return;

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(openApiUrl, openApiName);
            options.RoutePrefix = mainRouter;
        });

        app.UseReDoc(options =>
        {
            options.SpecUrl(openApiUrl);
            options.RoutePrefix = $"{mainRouter}/redoc";
        });
    }
}
