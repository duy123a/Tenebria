namespace Tenebria.Shared.Module.Configuration.Settings;

public class RedisCacheSetting
{
    public string ConnectionString { get; init; } = string.Empty;
    public int? DefaultExpirationMinutes { get; init; }
    public bool ClearCacheOnStartup { get; init; } = true;
    public Dictionary<string, bool> Tables { get; init; } = new();
}
