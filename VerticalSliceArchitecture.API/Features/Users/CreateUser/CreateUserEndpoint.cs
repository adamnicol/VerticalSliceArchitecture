using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.API.Filters;

namespace VerticalSliceArchitecture.API.Features.Users.CreateUser;

public class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/create", async (
            CreateUserRequest request,
            CreateUserHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(request, cancellationToken);
            return response.ToResult();
        })
            .WithTags("Users")
            .AddEndpointFilter<ValidationFilter<CreateUserRequest>>()
            .AllowAnonymous();
    }
}