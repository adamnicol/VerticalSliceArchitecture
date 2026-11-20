using Microsoft.Extensions.Options;

namespace VerticalSliceArchitecture.API.Middleware;

public class MaintenanceMiddleware(RequestDelegate next, IOptionsMonitor<AppSettings> settings)
{
    public async Task Invoke(HttpContext context)
    {
        if (settings.CurrentValue.MaintenanceMode)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Down for maintenance");
            return;
        }

        await next(context);
    }
}