namespace VerticalSliceArchitecture.API.Configuration;

internal static class SettingsConfiguration
{
    internal static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddOptions<AppSettings>()
            .BindConfiguration(string.Empty)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<ConnectionStrings>()
            .BindConfiguration(nameof(ConnectionStrings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
