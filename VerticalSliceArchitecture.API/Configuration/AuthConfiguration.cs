using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using VerticalSliceArchitecture.API.Utils;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class AuthConfiguration
{
    internal static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetSection(nameof(AppSettings.Authentication))
            .Get<AuthenticationSettings>();

        settings = Guard.AgainstNull(settings, "Authentication not configured.");

        if (settings.Providers.Length > 0)
        {
            services.AddOidcProviders(settings.Providers);
        }
    }

    private static void AddOidcProviders(this IServiceCollection services, OidcSettings[] providers)
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
            string identifier = provider.Identifier.ToLower();
            
            auth.AddOpenIdConnect(identifier, options =>
            {
                options.Authority = provider.Authority;
                options.ClientId = provider.ClientId;
                options.ClientSecret = provider.ClientSecret;
                options.CallbackPath = provider.CallbackPath ?? $"/signin-oidc/{identifier}";

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
