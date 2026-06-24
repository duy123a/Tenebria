using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using System.Text;
using Tenebria.Shared.Module.Configuration;
using Tenebria.Shared.Module.Configuration.Settings;

namespace Tenebria.Shared.Infrastructure.Utils.Logging;

public static class SeriLogProvider
{
    private const string _template =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | " +
        "System={SystemName} | " +
        "Server={ServerName} | " +
        "ProcessId={ProcessId} | " +
        "Version={Version} | " +
        "LogType={LogType} | " +
        "LogDataType={LogDataType} | " +
        "Level=[{Level:u3}] | " +
        "ClientIp={ClientIp} | " +
        "Tenant={Tenant} | " +
        "User={LoginId} | " +
        "Id={RequestId} | " +
        "Method={Method} | " +
        "Elapsed={ElapsedTime} | " +
        "Message={Message} | " +
        "Exception={ExceptionMessage} | " +
        "ExceptionTrace={ExceptionTrace}{NewLine}";

    public static void Initialize(IHostEnvironment env, ILogEventEnricher eventEnricher)
    {
        var logConfiguration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.With(eventEnricher);

        if (env.EnvironmentName == ApplicationEnvironment.DEVELOPMENT)
        {
            logConfiguration.WriteTo.Console(outputTemplate: _template);

            // For debug only
#if DEBUG
            Console.OutputEncoding = Encoding.UTF8;
            logConfiguration.WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Sixteen);

            SelfLog.Enable(Console.Error);
#endif
        }
        else if (env.EnvironmentName == ApplicationEnvironment.STAGING)
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "log.txt");
            logConfiguration.WriteTo.File(logPath, outputTemplate: _template, rollingInterval: RollingInterval.Day);
        }

        var appInsightsConnectionString = AppSettingProvider.Instance.Log?.ApplicationInsightsConnectionString;
        if (!string.IsNullOrEmpty(appInsightsConnectionString))
        {
            var telemetryConfiguration = new TelemetryConfiguration
            {
                ConnectionString = appInsightsConnectionString
            };

            logConfiguration.WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces);
        }

        Log.Logger = logConfiguration.CreateLogger();
    }

    public static void LogInfo(this ILogger logger, string message)
    {
        logger.ForContext("Message", message)
            .Information(message);
    }

    public static void LogInfo(this ILogger logger, string message, TimeSpan elapsedTime)
    {
        logger.ForContext("Message", message)
                .ForContext("ElapsedTime", elapsedTime)
            .Information(message);
    }

    public static void LogError(this ILogger logger, string message, Exception ex)
    {
        logger.ForContext("Message", message)
                .ForContext("ExceptionMessage", ex.Message)
                .ForContext("ExceptionTrace", ex.StackTrace)
            .Error(ex, message);
    }

    public static void LogDebug(this ILogger logger, string message)
    {
        logger.ForContext("Message", message)
            .Debug(message);
    }

    public static void LogDebug(this ILogger logger, string message, Exception ex)
    {
        logger.ForContext("Message", message)
                .ForContext("ExceptionMessage", ex.Message)
                .ForContext("ExceptionTrace", ex.StackTrace)
           .Debug(ex, message);
    }
}
