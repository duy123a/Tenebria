using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Tenebria.Api.OpenApi;
using Tenebria.Shared.Module.Configuration;
using Tenebria.Shared.Module.Configuration.Settings;

namespace Tenebria.Api
{
    public static class Startup
    {
        private const string _cors = "AllowAll";

        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_cors, policy =>
                {
                    policy.SetIsOriginAllowed(_ => true);
                    policy.AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // Get Application Setting
            var appConfig = AppSettingProvider.Instance;

            // TODO: Setting Kestrel Server Limit (Json / Raw stream bytes)
            // services.Configure<KestrelServerOptions>(options => options.Limits.MaxRequestBodySize = ??);

            // Setting Form Options (Mime Multipart / IFormFile)
            var maxFileSize = appConfig.Storage.FileUploadOptions.MaxSizeMB * 1024 * 1024;
            services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = maxFileSize);

            if (appConfig.OpenApi.EnableApiDocument)
            {
                services.AddSwaggerGen(new List<string>
                {
                    Path.Combine(AppContext.BaseDirectory, $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml"),
                });
            }

            return services;
        }

        public static void ConfigurePipeline(this WebApplication app, ApplicationSetting settings)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            // HTTPS redirection
            app.UseHttpsRedirection();

            // Static files, routing, CORS
            app.UseRouting();
            app.UseCors(_cors);

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Middleware Rate Limiter
            app.UseRateLimiter();

            // Map endpoints
            app.MapControllers();

            // Open api document
            if (settings.OpenApi.EnableApiDocument)
            {
                app.AddOpenApiDocument(settings.OpenApi);
            }
        }
    }
}
