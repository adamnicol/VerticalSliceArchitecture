namespace VerticalSliceArchitecture.API.Extensions;

public static class ErrorOrExtensions
{
    public static IResult ToResult<T>(this ErrorOr<T> result)
    {
        if (!result.IsError)
        {
            return Results.Ok(result.Value);
        }

        return result.FirstError.Type switch
        {
            ErrorType.NotFound => Results.NotFound(),
            ErrorType.Validation => Results.BadRequest(),
            ErrorType.Conflict => Results.Conflict(),
            ErrorType.Unauthorized => Results.Unauthorized(),
            ErrorType.Forbidden => Results.Forbid(),
            _ => Results.InternalServerError()
        };
    }
}
