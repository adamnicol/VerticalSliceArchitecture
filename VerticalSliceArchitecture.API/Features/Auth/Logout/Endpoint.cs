using Microsoft.AspNetCore.Authentication;
using VerticalSliceArchitecture.API.Configuration;

namespace VerticalSliceArchitecture.API.Features.Auth.Logout;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/logout", () =>
        {
            return Results.SignOut(new AuthenticationProperties()
            {
                RedirectUri = Constants.LogoutRedirectUrl,
            });
        });
    }
}