using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Configuration.Database;

namespace VerticalSliceArchitecture.API;

public sealed class AppSettings
{
    public bool MaintenanceMode { get; set; } = false;

    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public AuthenticationSettings Authentication { get; set; } = new();
}
