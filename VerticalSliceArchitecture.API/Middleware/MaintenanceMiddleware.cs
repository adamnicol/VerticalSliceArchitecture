using Microsoft.Extensions.Options;

namespace VerticalSliceArchitecture.API.Middleware;

public class MaintenanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptionsMonitor<AppSettings> _settings;

    public MaintenanceMiddleware(RequestDelegate next, IOptionsMonitor<AppSettings> settings)
    {
        _next = next;
        _settings = settings;
    }

    public async Task Invoke(HttpContext context)
    {
        if (_settings.CurrentValue.MaintenanceMode)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("The service is currently undergoing maintenance");
            return;
        }

        await _next(context);
    }
}