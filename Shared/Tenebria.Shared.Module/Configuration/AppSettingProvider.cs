using Tenebria.Shared.Module.Configuration.Settings;

namespace Tenebria.Shared.Module.Configuration;

public static class AppSettingProvider
{
    private static ApplicationSetting? _appCfg;

    public static void Initialize(ApplicationSetting appCfg)
    {
        _appCfg = appCfg;
    }

    public static ApplicationSetting Instance => _appCfg ?? throw new NullReferenceException();

    public static string MasterRealm => Instance.Keycloak.MasterRealm;
}
