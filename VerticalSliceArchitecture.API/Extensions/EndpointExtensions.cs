using VerticalSliceArchitecture.API.Filters;

namespace VerticalSliceArchitecture.API.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}
