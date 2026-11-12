using Microsoft.AspNetCore.Authentication;

namespace VerticalSliceArchitecture.API.Features.Auth.Login;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/login/{provider}", (string provider) =>
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/",
            };

            return Results.Challenge(properties, [provider]);
        })
            .AllowAnonymous();
    }
}