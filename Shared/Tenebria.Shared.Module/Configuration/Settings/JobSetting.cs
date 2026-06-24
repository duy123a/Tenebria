namespace Tenebria.Shared.Module.Configuration.Settings;

public enum JobDbMode
{
    Read = 0,
    Write = 1
}

public class JobSetting
{
    public string ApiUrl { get; init; } = default!;
    public JobDbMode Mode { get; init; } = JobDbMode.Write;
    public string QueueName { get; init; } = default!;
    public int QueueTimeoutSec { get; init; } = 3;
    public int MaxRetryCount { get; init; } = 3;
    public string[] RegisterAssemblies { get; init; } = Array.Empty<string>();
}
