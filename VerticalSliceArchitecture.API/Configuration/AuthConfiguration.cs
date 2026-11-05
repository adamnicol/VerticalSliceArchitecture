using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class AuthConfiguration
{
    internal static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetRequiredSection(nameof(AppSettings.Authentication))
            .Get<AuthenticationSettings>();

        if (settings?.OpenIdConnect != null)
        {
            services.AddOpenIdConnect(settings.OpenIdConnect);
        }        
    }

    internal static void AddOpenIdConnect(this IServiceCollection services, OidcSettings settings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = settings.Authority;
                options.ClientId = settings.ClientId;
                options.ClientSecret = settings.ClientSecret;

                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            });
    }
}
