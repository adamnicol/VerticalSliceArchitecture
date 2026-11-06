using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using VerticalSliceArchitecture.API.Abstractions;

namespace TVerticalSliceArchitecture.API.Features.Auth.Login;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/login", () =>
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/",
            };

            return Results.Challenge(properties, [OpenIdConnectDefaults.AuthenticationScheme]);
        })
            .AllowAnonymous();
    }
}