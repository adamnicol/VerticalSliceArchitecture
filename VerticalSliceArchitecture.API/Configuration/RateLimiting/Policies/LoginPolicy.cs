using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace VerticalSliceArchitecture.API.Configuration.RateLimiting.Policies;

internal class LoginPolicy : IRateLimiterPolicy<string>
{
    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => HandleRejectedAsync;

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        var key = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var options = new SlidingWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromMinutes(1),
            SegmentsPerWindow = 6,
            QueueLimit = 0
        };

        return RateLimitPartition.GetSlidingWindowLimiter(key, _ => options);
    }

    private ValueTask HandleRejectedAsync(OnRejectedContext context, CancellationToken cancellationToken)
    {
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            int seconds = (int)retryAfter.TotalSeconds;
            context.HttpContext.Response.Headers.RetryAfter = seconds.ToString();
        }

        return ValueTask.CompletedTask;
    }
}
