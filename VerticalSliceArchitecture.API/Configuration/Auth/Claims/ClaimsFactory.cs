using System.Security.Claims;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Configuration.Auth.Claims;

public static class ClaimsFactory
{
    public static IEnumerable<Claim> CreateClaims(User user)
    {
        var claims = new List<Claim>(); 

        claims.Add(new(CustomClaims.UserId, user.UserId.ToString()));

        if (user.Administrator)
        {
            claims.Add(new(ClaimTypes.Role, UserRoles.Administrator));
        }

        return claims;
    }
}
