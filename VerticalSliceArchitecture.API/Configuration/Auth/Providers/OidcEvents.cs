using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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

        if (identity == null || !identity.IsAuthenticated)
        {
            context.Fail("User is not authenticated.");
            return;
        }

        var email = identity.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrWhiteSpace(email))
        {
            context.Fail("Missing email address claim.");
            return;
        }

        var emailAddress = new EmailAddress(email);
        var cancellationToken = context.HttpContext.RequestAborted;

        User? user = await loginProvider.GetUser(emailAddress, cancellationToken);

        if (user?.Status == EntityStatus.Active)
        {
            identity.AddClaims(ClaimsFactory.CreateClaims(user));
        }
    }
}