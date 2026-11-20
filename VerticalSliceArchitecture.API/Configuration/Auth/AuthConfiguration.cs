using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Configuration.Auth.Providers;

namespace VerticalSliceArchitecture.API.Configuration.Auth;

internal static class AuthConfiguration
{
    private const string DefaultScheme = "Any";

    internal static void AddAuthentication(this IServiceCollection services, IConfigurationManager configuration)
    {
        var settings = configuration
            .GetRequiredSection(nameof(AppSettings.Authentication))
            .Get<AuthenticationSettings>();

        if (settings != null)
        {
            services.AddAuthentication(settings);
        }
    }

    private static void AddAuthentication(this IServiceCollection services, AuthenticationSettings settings)
    {
        var auth = services.AddAuthentication(options =>
        {
            options.DefaultScheme = DefaultScheme;
        });

        auth.AddCookie(settings);
        auth.AddJwtBearer(settings);
        auth.AddExternalProviders(settings);

        auth.AddPolicyScheme(DefaultScheme, DefaultScheme, options =>
        {
            options.ForwardDefaultSelector = context =>
            {
                return GetPolicyScheme(context, settings);
            };
        });
    }

    private static void AddCookie(this AuthenticationBuilder auth, AuthenticationSettings settings)
    {
        auth.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.SlidingExpiration = true;

            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });
    }

    private static void AddExternalProviders(this AuthenticationBuilder auth, AuthenticationSettings settings)
    {
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

                options.Scope.Add("openid");
                options.Scope.Add("email");
                options.Scope.Add("profile");

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

    private static void AddJwtBearer(this AuthenticationBuilder auth, AuthenticationSettings settings)
    {
        if (settings.JwtBearer.Enabled)
        {
            auth.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = settings.JwtBearer.Authority;
                options.Audience = settings.JwtBearer.Audience;
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.MapInboundClaims = true;

                options.EventsType = typeof(JwtEvents);
            });
        }
    }

    private static string GetPolicyScheme(HttpContext context, AuthenticationSettings settings)
    {
        if (settings.JwtBearer.Enabled)
        {
            string authorization = context.Request.Headers.Authorization.ToString();

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return JwtBearerDefaults.AuthenticationScheme;
            }
        }

        return CookieAuthenticationDefaults.AuthenticationScheme;
    }
}

