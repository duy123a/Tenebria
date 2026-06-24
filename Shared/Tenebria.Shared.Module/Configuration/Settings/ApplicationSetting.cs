namespace Tenebria.Shared.Module.Configuration.Settings;

public class ApplicationSetting
{
    public virtual string SystemName { get; init; } = default!;
    public virtual string ServerName { get; init; } = default!;
    public virtual string Version { get; init; } = default!;
    public virtual RedisCacheSetting RedisCache { get; init; } = default!;
    public virtual DatabaseSetting Database { get; init; } = default!;
    public OpenApiSetting OpenApi { get; init; } = default!;
    public KeycloakSetting Keycloak { get; init; } = default!;
    public JWTTokenSetting JwtToken { get; init; } = default!;
    public JobSetting Job { get; init; } = default!;
    public StorageSetting Storage { get; init; } = default!;
    public LogSetting Log { get; init; } = default!;
}
