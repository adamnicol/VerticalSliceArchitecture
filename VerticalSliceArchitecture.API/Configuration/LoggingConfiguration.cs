using Serilog;

namespace VerticalSliceArchitecture.API.Configuration;

public static class LoggingConfiguration
{
    public static IServiceCollection AddLoggingConfig(this IServiceCollection services)
    {
        return services.AddSerilog((provider, config) =>
        {
            config.ReadFrom.Configuration(
                provider.GetRequiredService<IConfiguration>());
        });
    }
}
