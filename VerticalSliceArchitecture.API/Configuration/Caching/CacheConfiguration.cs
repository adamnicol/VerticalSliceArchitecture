using VerticalSliceArchitecture.API.Configuration.Caching.Policies;

namespace VerticalSliceArchitecture.API.Configuration.Caching;

internal static class CacheConfiguration
{
    internal static IServiceCollection AddCachePolicies(this IServiceCollection services)
    {
        return services.AddOutputCache(options =>
        {
            options.AddPolicy(CachePolicy.Public, new PublicCachePolicy(TimeSpan.FromMinutes(5)));
            options.AddPolicy(CachePolicy.PerUser, new PerUserCachePolicy(TimeSpan.FromMinutes(5)));

            options.AddPolicy(CachePolicy.PublicShortLived, new PublicCachePolicy(TimeSpan.FromSeconds(30)));
            options.AddPolicy(CachePolicy.PerUserShortLived, new PerUserCachePolicy(TimeSpan.FromSeconds(30)));

            options.AddPolicy(CachePolicy.PublicLongLived, new PublicCachePolicy(TimeSpan.FromHours(1)));
            options.AddPolicy(CachePolicy.PerUserLongLived, new PerUserCachePolicy(TimeSpan.FromHours(1)));

            options.AddPolicy(CachePolicy.NoCache, builder => builder.NoCache());
        });
    }
}
