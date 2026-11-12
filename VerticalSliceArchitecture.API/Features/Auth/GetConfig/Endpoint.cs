namespace VerticalSliceArchitecture.API.Features.Auth.GetConfig;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/config", async (
            Handler handler, 
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(cancellationToken);
            return response.ToResult();
        })
            .AllowAnonymous();
    }
}