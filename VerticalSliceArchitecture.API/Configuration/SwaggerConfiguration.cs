using VerticalSliceArchitecture.API.Filters;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(x => x.FullName);
            options.OperationFilter<FeatureTagOperationFilter>();
        });
    }
}
