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

        if (settings != null)
        {
            AddOpenIdConnect(services, settings.Oidc);
        }
    }

    internal static void AddOpenIdConnect(IServiceCollection services, OidcSettings settings)
    {
        var builder = services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        builder.AddCookie(options =>
        {
            options.Events.OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });
        
        builder.AddOpenIdConnect(options =>
        {
            options.Authority = settings.Authority;
            options.ClientId = settings.ClientId;
            options.ClientSecret = settings.ClientSecret;
            options.CallbackPath = settings.CallbackPath ?? "/signin-oidc";

            options.ResponseType = OpenIdConnectResponseType.Code;
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            options.UsePkce = true;
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.RequireHttpsMetadata = true;
        });
    }
}
