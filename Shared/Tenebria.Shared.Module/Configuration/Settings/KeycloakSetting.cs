namespace Tenebria.Shared.Module.Configuration.Settings;

public class KeycloakSetting
{
    public string Issuer { get; init; } = string.Empty;
    public string MasterRealm { get; init; } = string.Empty;
    public string ApiUrl { get; init; } = string.Empty;
    public string ForwardUrl { get; init; } = string.Empty;
    public string SigninAcsUrl { get; init; } = string.Empty;
    public string SignoutAcsUrl { get; init; } = string.Empty;
}
