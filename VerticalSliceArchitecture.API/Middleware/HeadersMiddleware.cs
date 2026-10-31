namespace VerticalSliceArchitecture.API.Middleware;

public class HeadersMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var headers = context.Response.Headers;

        headers.Append("X-Frame-Options", "DENY");
        headers.Append("X-Content-Type-Options", "nosniff");
        headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        headers.Append("Content-Security-Policy", "default-src 'self';");
        headers.Append("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");

        await next(context);
    }
}