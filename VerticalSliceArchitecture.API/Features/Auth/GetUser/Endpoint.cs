namespace VerticalSliceArchitecture.API.Features.Auth.GetUser;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/user", async (
            Handler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(cancellationToken);
            return response.ToResult();
        });
    }
}