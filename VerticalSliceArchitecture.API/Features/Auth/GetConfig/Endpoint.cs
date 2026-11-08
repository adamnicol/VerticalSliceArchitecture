using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Auth.GetConfig;

internal class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/config", (Handler handler) =>
        {
            var response = handler.Handle();
            return response.ToResult();
        })
            .AllowAnonymous();
    }
}