using Microsoft.AspNetCore.Authentication;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Auth.Logout;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/logout", () =>
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/",
            };

            return Results.SignOut(properties);
        });
    }
}