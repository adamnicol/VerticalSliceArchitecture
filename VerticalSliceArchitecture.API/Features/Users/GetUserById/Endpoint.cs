using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserById;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id}", async (
            Guid id,
            Handler handler,
            CancellationToken cancellationToken) =>
        {
            var request = new Request(id);
            var response = await handler.HandleAsync(request, cancellationToken);

            return response.ToResult();
        })
            .CacheOutput();
    }
}