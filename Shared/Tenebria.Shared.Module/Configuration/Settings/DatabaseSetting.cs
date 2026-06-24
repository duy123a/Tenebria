namespace Tenebria.Shared.Module.Configuration.Settings;

public class DatabaseSetting
{
    public string MasterConnectionString { get; init; } = default!;
    public string SlaveConnectionString { get; init; } = default!;
}
