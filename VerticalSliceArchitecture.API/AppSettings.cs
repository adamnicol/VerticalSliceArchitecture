using System.ComponentModel.DataAnnotations;

namespace VerticalSliceArchitecture.API;

public sealed class AppSettings
{
    public bool MaintenanceMode { get; set; } = false;

    public ConnectionStrings ConnectionStrings { get; set; } = default!;
    public AuthenticationSettings Authentication { get; set; } = default!;
}

public sealed class ConnectionStrings
{
    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = default!;
}

public sealed class AuthenticationSettings
{
    public OidcSettings OpenIdConnect { get; set; } = default!;
}

public sealed class OidcSettings
{
    [Required(AllowEmptyStrings = false)]
    public string Authority { get; set; } = default!;
    [Required(AllowEmptyStrings = false)]
    public string ClientId { get; set; } = default!;
    [Required(AllowEmptyStrings = false)]
    public string ClientSecret { get; set; } = default!;
}