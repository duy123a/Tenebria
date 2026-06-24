namespace Tenebria.Shared.Module.Configuration.Settings;

public class JWTTokenSetting
{
    public string IssuerSigningKey { get; init; } = default!;

    public string Issuer { get; init; } = default!;

    public string Audience { get; init; } = default!;

    public int TokenLifespan { get; init; }
}
