using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tenebria.Shared.Infrastructure.Utils.Context;

namespace Tenebria.Shared.Infrastructure
{
    public static class Startup
    {
        public static async Task InitializeSharedModule(this IServiceProvider serviceProvider)
        {
            HttpContextProvider.Initialize(serviceProvider.GetRequiredService<IHttpContextAccessor>());
        }
    }
}
