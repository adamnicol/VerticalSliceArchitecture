using System.Security.Claims;
using VerticalSliceArchitecture.API.Configuration.Auth.Claims;

namespace VerticalSliceArchitecture.API.Configuration.Auth;

public class UserContext : IUserContext
{
    public Guid UserId { get; }
    public string? EmailAddress { get; }
    public bool Administrator { get; }

    public UserContext(IHttpContextAccessor context)
    {
        var user = context.HttpContext?.User;

        if (user != null)
        {
            UserId = user.GetGuid(CustomClaims.UserId);
            EmailAddress = user.GetClaim(ClaimTypes.Email);
            Administrator = user.IsInRole(UserRoles.Administrator);
        }
    }
}
