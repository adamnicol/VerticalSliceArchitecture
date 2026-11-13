using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Configuration.Auth.Providers;
using VerticalSliceArchitecture.API.Utilities;

namespace VerticalSliceArchitecture.API.Configuration.Auth;

internal static class Configuration
{
    internal static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetSection(nameof(AppSettings.Authentication))
            .Get<AuthenticationSettings>();

        settings = Guard.AgainstNull(settings, "Authentication not configured.");
        
        services.AddAuthentication(settings);
    }

    private static void AddAuthentication(this IServiceCollection services, AuthenticationSettings settings)
    {
        var auth = services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        auth.AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.SlidingExpiration = true;

            options.Events.OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });

        var providers = settings.Providers.Where(x => x.Enabled);

        foreach (OidcSettings provider in providers)
        {
            string identifier = provider.Identifier.ToLowerInvariant();
            
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
                options.MapInboundClaims = true;

                options.EventsType = typeof(OidcEvents);
            });
        }
    }
}
