namespace VerticalSliceArchitecture.API.Configuration;

public static class ErrorConfiguration
{
    public static IServiceCollection AddErrorConfig(this IServiceCollection services)
    {
        return services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Instance = ctx.HttpContext.Request.Path;
            };
        });
    }
}
