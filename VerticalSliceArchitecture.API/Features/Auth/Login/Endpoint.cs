using Microsoft.AspNetCore.Authentication;
using VerticalSliceArchitecture.API.Configuration;

namespace VerticalSliceArchitecture.API.Features.Auth.Login;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/login/{provider}", (string provider) =>
        {
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,
                RedirectUri = Constants.LoginRedirectUrl,
            };

            return Results.Challenge(properties, [provider]);
        })
            .AllowAnonymous();
    }
}