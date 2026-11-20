namespace VerticalSliceArchitecture.API.Configuration.Auth;

public sealed class AuthenticationSettings
{
    public bool LocalLoginEnabled { get; set; } = true;
    public string? KeyVaultEndpoint { get; set; }
    public OidcSettings[] Providers { get; set; } = [];
    public JwtBearerSettings JwtBearer { get; set; } = new();
}

public sealed class JwtBearerSettings
{
    public bool Enabled { get; set; }

    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}

public sealed class OidcSettings
{
    public bool Enabled { get; set; }

    public string Identifier { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;

    public string? CallbackPath { get; set; } = "/signin-oidc";
    public string? AuthorizationPath { get; set; } = "/authorize";
    public string? TokenPath { get; set; } = "/oauth/token";
}
