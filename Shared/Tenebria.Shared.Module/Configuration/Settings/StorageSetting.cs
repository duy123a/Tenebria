namespace Tenebria.Shared.Module.Configuration.Settings;

public class StorageSetting
{
    public string ConnectionString { get; init; } = default!;
    public FileUploadOptions FileUploadOptions { get; init; } = default!;
}

public class FileUploadOptions
{
    public uint MaxSizeMB { get; init; } = 10;
    public string[] AllowedExtensions { get; init; } = [".*"];
}
