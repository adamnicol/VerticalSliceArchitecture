using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class HealthCheckConfiguration
{
    internal static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services)
    {
        var builder = services.AddHealthChecks();

        builder.AddDbContextCheck<DatabaseContext>("database", tags: ["ready"]);

        return services;
    }

    internal static WebApplication MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains("ready")
        });

        app.MapHealthChecks("/health/detailed", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
