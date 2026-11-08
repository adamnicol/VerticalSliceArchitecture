using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var authentication = configuration
            .GetRequiredSection(nameof(AppSettings.Authentication))
            .Get<AuthenticationSettings>();

        return services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(x => x.FullName);
            options.OperationFilter<FeatureTagFilter>();

            foreach (var provider in authentication?.Providers ?? [])
            {
                options.AddProvider(provider);
            }
        });
    }

    private static void AddProvider(this SwaggerGenOptions options, OidcSettings provider)
    {
        string basePath = provider.Authority.TrimEnd('/');
        string authPath = provider.AuthorizationPath?.TrimStart('/') ?? "authorize";
        string tokenPath = provider.TokenPath?.TrimStart('/') ?? "oauth/token";

        options.AddSecurityDefinition(provider.Identifier, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Description = provider.DisplayName,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{basePath}/{authPath}"),
                    TokenUrl = new Uri($"{basePath}/{tokenPath}"),
                }
            }
        });
    }

    private class FeatureTagFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var namespaceName = context.MethodInfo?.DeclaringType?.Namespace;
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                var segments = namespaceName.Split('.');
                if (segments.Length > 1)
                {
                    var tag = segments[segments.Length - 2];

                    operation.Tags?.Clear();
                    operation.Tags?.Add(new OpenApiTagReference(tag));
                }
            }
        }
    }
}
