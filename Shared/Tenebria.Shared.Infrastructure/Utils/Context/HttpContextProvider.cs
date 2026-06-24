using Microsoft.AspNetCore.Http;
using Tenebria.Shared.Module.Configuration;
using Tenebria.Shared.Module.Consts;

namespace Tenebria.Shared.Infrastructure.Utils.Context;

public static class HttpContextProvider
{
    private static IHttpContextAccessor? _httpContextAccessor;

    public static void Initialize(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }

    public static HttpContext? Instance => _httpContextAccessor?.HttpContext;

    public static IUserContext? UserContext =>
        Instance?.Items[TenebriaRequestConst.HTTP_CONTEXT_KEY] as IUserContext;

    public static string GetClientIp(this HttpContext context)
    {
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            return forwardedFor.Split(',')[0];
        }

        // Fallback to RemoteIpAddress
        return context.Connection.RemoteIpAddress?.ToString() ?? "-";
    }
}
