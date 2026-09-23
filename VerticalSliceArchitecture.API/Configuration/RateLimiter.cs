using System.Threading.RateLimiting;

namespace VerticalSliceArchitecture.API.Configuration;

public static class RateLimiter
{
    private class RateLimiterSettings
    {
        public int PermitLimit { get; set; } = 100;
        public int WindowInSeconds { get; set; } = 60;
        public int SegmentsPerWindow { get; set; } = 6;
        public int QueueLimit { get; set; } = 0;
    }

    public static IServiceCollection AddRateLimiter(this IServiceCollection services, ConfigurationManager configuration)
    {
        var settings = configuration.GetSection("RateLimiter").Get<RateLimiterSettings>()
            ?? new RateLimiterSettings();

        return services.AddRateLimiter((options) =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                GetSlidingWindowLimiter(context, settings));
        });
    }

    private static RateLimitPartition<string> GetSlidingWindowLimiter(HttpContext context, RateLimiterSettings settings)
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
