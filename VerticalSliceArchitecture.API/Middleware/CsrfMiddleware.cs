using Microsoft.AspNetCore.Antiforgery;

namespace VerticalSliceArchitecture.API.Middleware;

public class CsrfMiddleware(RequestDelegate next, ICsrfProtection csrf)
{
    public async Task Invoke(HttpContext context)
    {
        if (context.GetEndpoint() != null)
        {
            var result = await csrf.ValidateAsync(context);

            if (!result.IsAllowed)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }

        await next(context);
    }
}