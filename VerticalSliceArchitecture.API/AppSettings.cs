using System.ComponentModel.DataAnnotations;

namespace VerticalSliceArchitecture.API;

public sealed class AppSettings
{
    public bool MaintenanceMode { get; set; } = false;

    public ConnectionStrings ConnectionStrings { get; set; } = default!;
}

public sealed class ConnectionStrings
{
    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = string.Empty;
}
