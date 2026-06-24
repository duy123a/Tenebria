namespace Tenebria.Shared.Module.Configuration.Settings;

public class OpenApiSetting
{
    public bool EnableApiDocument { get; init; }
    public string WebOpenApiUrl { get; init; } = string.Empty;
    public string KeycloakOpenApiUrl { get; init; } = string.Empty;
}
