using VerticalSliceArchitecture.API.Middleware;

namespace VerticalSliceArchitecture.API.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this WebApplication app)
    {
        return app.UseMiddleware<HeadersMiddleware>();
    }

    public static IApplicationBuilder UseCsrfProtection(this WebApplication app)
    {
        return app.UseMiddleware<CsrfMiddleware>();
    }

    public static IApplicationBuilder UseMaintenanceMode(this WebApplication app)
    {
        return app.UseMiddleware<MaintenanceMiddleware>();
    }
}
