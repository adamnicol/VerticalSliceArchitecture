using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class HealthCheckConfiguration
{
    internal static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services)
    {
        var builder = services.AddHealthChecks();
        
        builder.AddSqlServer(sp => sp.GetRequiredService<IOptions<ConnectionStrings>>().Value.DefaultConnection, 
            name: "database");

        return services;
    }

    internal static WebApplication MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = (check) => false
        });

        app.MapHealthChecks("/health/db", new HealthCheckOptions
        {
            Predicate = (check) => check.Name == "database"
        });

        return app;
    }
}
