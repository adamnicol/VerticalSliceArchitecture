namespace VerticalSliceArchitecture.API.Configuration;

public static class SettingsConfiguration
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services)
    {
        services.AddOptions<AppSettings>()
            .BindConfiguration(string.Empty)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<ConnectionStrings>()
            .BindConfiguration(nameof(ConnectionStrings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<RateLimiting>()
            .BindConfiguration(nameof(RateLimiting))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
