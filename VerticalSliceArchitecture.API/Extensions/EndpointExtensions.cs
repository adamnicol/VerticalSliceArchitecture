using VerticalSliceArchitecture.API.Filters;

namespace VerticalSliceArchitecture.API.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder RequiresRole(this RouteHandlerBuilder builder, string role)
    {
        return builder.RequireAuthorization(policy => policy.RequireRole(role));
    }

    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}
