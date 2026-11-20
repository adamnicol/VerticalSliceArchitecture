using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Net.Http.Headers;

namespace VerticalSliceArchitecture.API.Configuration.Caching.Policies;

internal sealed class PublicCachePolicy(TimeSpan cacheTime) : IOutputCachePolicy
{
    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var request = context.HttpContext.Request;

        if (!HttpMethods.IsGet(request.Method))
        {
            return ValueTask.CompletedTask;
        }

        context.EnableOutputCaching = true;
        context.AllowCacheLookup = true;
        context.AllowCacheStorage = true;
        context.AllowLocking = true;
        context.ResponseExpirationTimeSpan = cacheTime;

        context.CacheVaryByRules.QueryKeys = "*";

        return ValueTask.CompletedTask;
    }

    public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var response = context.HttpContext.Response;

        if (response.StatusCode != StatusCodes.Status200OK ||
            response.Headers.ContainsKey(HeaderNames.SetCookie))
        {
            context.AllowCacheStorage = false;
        }

        return ValueTask.CompletedTask;
    }
}