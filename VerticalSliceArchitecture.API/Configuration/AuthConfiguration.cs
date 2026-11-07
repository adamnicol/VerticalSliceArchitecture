using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class AuthConfiguration
{
    internal static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetRequiredSection(nameof(AppSettings.Authentication))
            .Get<AuthenticationSettings>();

        if (settings?.Providers.Length > 0)
        {
            services.AddExternalProviders(settings.Providers);
        }
    }

    private static void AddExternalProviders(this IServiceCollection services, OidcSettings[] providers)
    {
        var auth = services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        auth.AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Events.OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });

        foreach (var provider in providers)
        {
            auth.AddOpenIdConnect(provider.Name.ToLower(), options =>
            {
                options.Authority = provider.Authority;
                options.ClientId = provider.ClientId;
                options.ClientSecret = provider.ClientSecret;
                options.CallbackPath = provider.CallbackPath ?? $"/signin-oidc/{provider.Name.ToLower()}";

                options.Scope.Add("email");

                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.UsePkce = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = true;
            });
        }
    }
}
