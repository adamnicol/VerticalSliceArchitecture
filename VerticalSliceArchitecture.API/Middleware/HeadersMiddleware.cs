namespace VerticalSliceArchitecture.API.Middleware;

public class HeadersMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var headers = context.Response.Headers;

        headers["X-Frame-Options"] = "DENY";
        headers["X-Content-Type-Options"] = "nosniff";
        headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        headers["Content-Security-Policy"] = "default-src 'self'";
        headers["Cache-Control"] = "no-cache, no-store, max-age=0, must-revalidate";

        await next(context);
    }
}