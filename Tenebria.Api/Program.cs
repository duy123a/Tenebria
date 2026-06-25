using Serilog;
using Tenebria.Api;
using Tenebria.Shared.Infrastructure;
using Tenebria.Shared.Infrastructure.Utils.Logging;
using Tenebria.Shared.Module.Configuration;
using Tenebria.Shared.Module.Configuration.Settings;

var builder = WebApplication.CreateBuilder(args);

// Load application setting
var appSetting = builder.Configuration.Get<ApplicationSetting>()!;

// Set static app setting
AppSettingProvider.Initialize(appSetting);

// Configure Serilog
SeriLogProvider.Initialize(builder.Environment, new ApiLogEventEnricher(appSetting));
Log.Logger.LogInfo("Tenebria application will now start.");

// Web API services
builder.Services.AddWebApiServices(builder.Environment);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.ConfigurePipeline(appSetting);

await app.Services.InitializeSharedModule();

app.Run();
