using Serilog.Core;
using Serilog.Events;
using Tenebria.Shared.Infrastructure.Utils.Context;
using Tenebria.Shared.Module.Configuration.Settings;
using static Tenebria.Shared.Infrastructure.Utils.Logging.LogConstant;

namespace Tenebria.Shared.Infrastructure.Utils.Logging;

public class ApiLogEventEnricher : ILogEventEnricher
{
    private readonly string? _systemName;
    private readonly string? _serverName;
    private readonly string? _version;
    private readonly int _processId;

    public ApiLogEventEnricher(ApplicationSetting cfg)
    {
        _systemName = cfg.SystemName;
        _version = cfg.Version;
        _serverName = cfg.ServerName ?? Environment.MachineName;
        _processId = Environment.ProcessId;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var httpContext = HttpContextProvider.Instance;
        var userContext = HttpContextProvider.UserContext;

        var tenant = userContext?.RealmName ?? string.Empty;
        var loginId = userContext?.LoginId ?? string.Empty;
        var clientIp = userContext?.ClientIpAddress ?? httpContext?.GetClientIp() ?? string.Empty;
        var requestId = httpContext?.TraceIdentifier ?? string.Empty;
        var method = string.Format("{0,-6}{1}", httpContext?.Request.Method, httpContext?.Request.Path + httpContext?.Request.QueryString);

        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("SystemName", _systemName));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("ServerName", _serverName));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("ProcessId", _processId));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("Version", _version));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("LogType", LogType.Auditlog));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("LogDataType", LogDataType.Data));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("ClientIp", clientIp));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("Tenant", tenant));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("LoginId", loginId));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("RequestId", requestId));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("Method", method));
    }
}
