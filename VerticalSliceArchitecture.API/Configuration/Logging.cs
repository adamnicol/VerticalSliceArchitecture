using Serilog;

namespace VerticalSliceArchitecture.API.Configuration;

public static class Logging
{
    public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSerilog((services, config) =>
        {
            config.ReadFrom.Configuration(configuration);
        });    
    }
}
