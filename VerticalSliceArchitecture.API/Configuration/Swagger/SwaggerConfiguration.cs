using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Configuration.Swagger.Filters;

namespace VerticalSliceArchitecture.API.Configuration.Swagger;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var authentication = configuration
            .GetSection(nameof(AppSettings.Authentication))
            .Get<AuthenticationSettings>();

        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Vertical Slice",
            });

            options.CustomSchemaIds(x => x.FullName);
            options.OperationFilter<FeatureTagFilter>();

            foreach (var provider in authentication?.Providers ?? [])
            {
                if (provider.Enabled)
                {
                    options.AddOidcProvider(provider);
                }
            }
        });
    }

    private static void AddOidcProvider(this SwaggerGenOptions options, OidcSettings provider)
    {
        string basePath = provider.Authority.TrimEnd('/');
        string? authPath = provider.AuthorizationPath?.TrimStart('/');
        string? tokenPath = provider.TokenPath?.TrimStart('/');

        options.OperationFilter<SecurityRequirementFilter>(provider.Identifier);
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
}
