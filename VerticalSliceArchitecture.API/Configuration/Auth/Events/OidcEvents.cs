using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Authentication;
using System.Security.Claims;
using VerticalSliceArchitecture.API.Configuration.Auth.Claims;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.Enums;
using VerticalSliceArchitecture.Infrastructure.Database.Providers.Contracts;

namespace VerticalSliceArchitecture.API.Configuration.Auth.Providers;

public class OidcEvents(ILoginProvider loginProvider) : OpenIdConnectEvents
{
    public override async Task TokenValidated(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;
        if (identity == null)
        {
            throw new AuthenticationException("Missing claims identity");
        }

        var email = identity.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new AuthenticationException("Missing email address claim");
        }

        var emailAddress = new EmailAddress(email);
        var cancellationToken = context.HttpContext.RequestAborted;
        var identifier = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrWhiteSpace(identifier))
        {
            User? user = await loginProvider.GetUser(identifier, emailAddress, cancellationToken);

            if (user?.Status == EntityStatus.Active)
            {
                identity.AddClaims(ClaimsFactory.CreateClaims(user));
            }
        }
    }
}