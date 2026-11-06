using System.ComponentModel.DataAnnotations;

namespace VerticalSliceArchitecture.API;

public sealed class AppSettings
{
    public bool MaintenanceMode { get; set; } = false;

    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public AuthenticationSettings Authentication { get; set; } = new();
}

public sealed class ConnectionStrings
{
    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = string.Empty;
}

public sealed class AuthenticationSettings
{
    public OidcSettings Oidc { get; set; } = new();
}

public sealed class OidcSettings
{
    [Required(AllowEmptyStrings = false)]
    public string Authority { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    public string ClientId { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    public string ClientSecret { get; set; } = string.Empty;
    public string? CallbackPath {  get; set; }
}