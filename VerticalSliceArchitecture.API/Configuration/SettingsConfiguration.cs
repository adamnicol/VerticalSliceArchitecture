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
            .BindConfiguration(nameof(AppSettings.ConnectionStrings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<AuthenticationSettings>()
            .BindConfiguration(nameof(AppSettings.Authentication))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
