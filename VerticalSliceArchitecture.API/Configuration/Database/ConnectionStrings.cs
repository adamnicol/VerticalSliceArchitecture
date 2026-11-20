using System.ComponentModel.DataAnnotations;

namespace VerticalSliceArchitecture.API.Configuration.Database;

public sealed class ConnectionStrings
{
    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = string.Empty;
}
