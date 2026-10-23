using VerticalSliceArchitecture.API.Filters;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(x => x.FullName);
            x.OperationFilter<FeatureTagOperationFilter>();
        });
    }
}
