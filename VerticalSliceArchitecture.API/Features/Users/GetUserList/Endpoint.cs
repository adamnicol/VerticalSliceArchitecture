using VerticalSliceArchitecture.API.Configuration.Auth;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserList;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (
            [AsParameters] Request request,
            Handler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(request, cancellationToken);
            return response.ToResult();
        })
            .RequiresRole(UserRoles.Administrator);
    }
}