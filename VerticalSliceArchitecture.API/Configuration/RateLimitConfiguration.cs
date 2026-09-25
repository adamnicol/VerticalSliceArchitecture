using System.Threading.RateLimiting;

namespace VerticalSliceArchitecture.API.Configuration;

public static class RateLimitConfiguration
{
    public static IServiceCollection AddRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(RateLimiting)).Get<RateLimiting>() 
            ?? new RateLimiting();

        return services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                GetSlidingWindowLimiter(context, settings));
        });
    }

    private static RateLimitPartition<string> GetSlidingWindowLimiter(HttpContext context, RateLimiting settings)
    {
        var partitionKey = context.User.Identity?.Name 
            ?? context.Connection.RemoteIpAddress?.ToString() 
            ?? context.Request.Headers.Host.ToString();

        var options = new SlidingWindowRateLimiterOptions
        {
            PermitLimit = settings.PermitLimit,
            QueueLimit = settings.QueueLimit,
            Window = TimeSpan.FromSeconds(settings.WindowInSeconds),
            SegmentsPerWindow = settings.SegmentsPerWindow,
            AutoReplenishment = true
        };

        return RateLimitPartition.GetSlidingWindowLimiter(partitionKey, _ => options);
    }
}
