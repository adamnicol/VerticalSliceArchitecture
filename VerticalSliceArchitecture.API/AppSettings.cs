using System.ComponentModel.DataAnnotations;

namespace VerticalSliceArchitecture.API;

public class ConnectionStrings
{
    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = string.Empty;
}
