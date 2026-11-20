using VerticalSliceArchitecture.API.Configuration.RateLimiting.Policies;

namespace VerticalSliceArchitecture.API.Configuration.RateLimiting;

internal static class RateLimitConfiguration
{
    internal static IServiceCollection AddRateLimiters(this IServiceCollection services)
    {
        return services.AddRateLimiter(options =>
        {
            options.AddPolicy<string, LoginPolicy>(RateLimitPolicy.Login);

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });
    }
}
