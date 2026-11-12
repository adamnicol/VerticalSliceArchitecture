using System.Security.Claims;

namespace VerticalSliceArchitecture.API.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string? GetClaim(this ClaimsPrincipal principal, string claimType)
    {
        return principal?.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
    }

    public static Guid GetGuid(this ClaimsPrincipal principal, string claimType)
    {
        return Guid.TryParse(principal.GetClaim(claimType), out Guid value) ? value : Guid.Empty;
    }
}
