using System.ComponentModel.DataAnnotations;

public sealed class AppSettings
{
    public bool MaintenanceMode { get; set; } = false;

    public ConnectionStrings ConnectionStrings { get; set; } = default!;
    public RateLimiting? RateLimiting { get; set; }
}

public sealed class ConnectionStrings
{
    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = string.Empty;
}

public sealed class RateLimiting
{
    public int PermitLimit { get; set; } = 100;
    public int WindowInSeconds { get; set; } = 60;
    public int SegmentsPerWindow { get; set; } = 6;
    public int QueueLimit { get; set; } = 0;
}