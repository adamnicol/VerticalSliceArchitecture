using System.Diagnostics;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class ErrorConfiguration
{
    internal static IServiceCollection AddErrorConfiguration(this IServiceCollection services)
    {
        return services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Instance = ctx.HttpContext.Request.Path;
                ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.TraceId.ToString();
            };
        });
    }
}
