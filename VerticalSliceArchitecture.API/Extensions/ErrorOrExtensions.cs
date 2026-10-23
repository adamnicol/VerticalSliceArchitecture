using Microsoft.AspNetCore.Http.HttpResults;

namespace VerticalSliceArchitecture.API.Extensions;

public static class ErrorOrExtensions
{
    public static Results<Ok<T>, ProblemHttpResult> ToResult<T>(this ErrorOr<T> result)
    {
        if (!result.IsError)
        {
            return TypedResults.Ok(result.Value);
        }

        int statusCode = result.FirstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return TypedResults.Problem(statusCode: statusCode);
    }
}
