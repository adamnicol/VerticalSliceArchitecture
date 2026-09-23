namespace VerticalSliceArchitecture.API.Configuration;

public static class Options
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services)
    {
        services.AddOptions<ConnectionStrings>()
            .BindConfiguration(nameof(ConnectionStrings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
