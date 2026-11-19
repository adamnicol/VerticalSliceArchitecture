using VerticalSliceArchitecture.API.Configuration.Caching.Policies;

namespace VerticalSliceArchitecture.API.Configuration.Caching;

public static class CacheConfiguration
{
    public static IServiceCollection AddCachePolicies(this IServiceCollection services)
    {
        return services.AddOutputCache(options =>
        {
            options.AddPolicy(CachePolicy.Public, new PublicCachePolicy(TimeSpan.FromMinutes(5)));
            options.AddPolicy(CachePolicy.PerUser, new PerUserCachePolicy(TimeSpan.FromMinutes(5)));

            options.AddPolicy(CachePolicy.PublicShortLived, new PublicCachePolicy(TimeSpan.FromSeconds(30)));
            options.AddPolicy(CachePolicy.PerUserShortLived, new PerUserCachePolicy(TimeSpan.FromSeconds(30)));
        });
    }
}
