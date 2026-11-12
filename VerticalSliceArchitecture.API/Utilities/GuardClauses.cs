namespace VerticalSliceArchitecture.API.Utilities;

public static class Guard
{
    public static T AgainstNull<T>(T? value, string? message = null)
    {
        if (value is null)
            throw new InvalidOperationException(message);

        return value;
    }

    public static string AgainstNullOrWhitespace(string? value, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException(message);

        return value;
    }
}
