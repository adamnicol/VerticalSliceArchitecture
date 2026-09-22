using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        foreach (IEndpoint endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
