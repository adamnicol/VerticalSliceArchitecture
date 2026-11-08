namespace VerticalSliceArchitecture.API.Features.Auth.GetConfig;

public class AuthenticationConfig
{
    public bool LocalLoginEnabled { get; set; }
    public IEnumerable<ExternalProvider> Providers { get; set; } = [];
}

public class ExternalProvider
{
    public required string Identifier { get; set; }
    public required string DisplayName { get; set; }
    public required string LoginPath { get; set; }
}
