using Serilog;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class LoggingConfiguration
{
    internal static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
    {
        return services.AddSerilog((provider, config) =>
        {
            config.ReadFrom.Configuration(
                provider.GetRequiredService<IConfiguration>());
        });
    }
}
