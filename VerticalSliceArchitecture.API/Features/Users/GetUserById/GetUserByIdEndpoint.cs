using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserById;

public class GetUserByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id}", async (
            Guid id,
            GetUserByIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var request = new GetUserByIdRequest(id);
            var response = await handler.HandleAsync(request, cancellationToken);

            return response.ToResult();
        })
            .WithTags("Users")
            .CacheOutput();
    }
}