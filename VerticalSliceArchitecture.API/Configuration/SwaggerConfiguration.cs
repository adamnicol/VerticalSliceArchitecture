using VerticalSliceArchitecture.API.Filters;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.OperationFilter<FeatureTagOperationFilter>();
            options.CustomSchemaIds(x =>
            {
                string? rootNamespace = typeof(Program).Assembly.GetName().Name;
                return x.FullName?.Replace($"{rootNamespace}.Features.", string.Empty);
            });
        });
    }
}
