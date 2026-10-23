using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserById;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id}", async (
            [AsParameters] Request request,
            Handler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(request, cancellationToken);
            return response.ToResult();
        })
            .WithTags("Users")
            .CacheOutput();
    }
}