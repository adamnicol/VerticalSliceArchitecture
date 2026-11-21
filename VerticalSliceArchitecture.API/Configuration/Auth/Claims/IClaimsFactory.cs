using System.Security.Claims;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Configuration.Auth.Claims;

public interface IClaimsFactory
{
    IEnumerable<Claim> CreateClaims(User user);
}
