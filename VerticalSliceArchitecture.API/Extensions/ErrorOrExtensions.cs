using Microsoft.AspNetCore.Http.HttpResults;
using Created = Microsoft.AspNetCore.Http.HttpResults.Created;

namespace VerticalSliceArchitecture.API.Extensions;

public static class ErrorOrExtensions
{
    public static Results<Ok<T>, ProblemHttpResult> ToResult<T>(this ErrorOr<T> result)
    {
        return result.IsError 
            ? GetProblemDetails(result)
            : TypedResults.Ok(result.Value);
    }

    public static Results<Created, ProblemHttpResult> ToResult(this ErrorOr<ErrorOr.Created> result)
    {
        return result.IsError
            ? GetProblemDetails(result)
            : TypedResults.Created();
    }

    public static Results<NoContent, ProblemHttpResult> ToResult(this ErrorOr<Updated> result)
    {
        return result.IsError
            ? GetProblemDetails(result)
            : TypedResults.NoContent();
    }

    private static ProblemHttpResult GetProblemDetails<T>(ErrorOr<T> result)
    {
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
