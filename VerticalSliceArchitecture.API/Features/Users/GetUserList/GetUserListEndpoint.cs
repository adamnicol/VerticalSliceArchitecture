using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserList;

public class GetUserListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (
            int page,
            int pageSize,
            GetUserListHandler handler,
            CancellationToken cancellationToken) =>
        {
            var request = new GetUserListRequest(page, pageSize);
            var response = await handler.HandleAsync(request, cancellationToken);

            return response.ToResult();
        })
            .WithTags("Users");
    }
}