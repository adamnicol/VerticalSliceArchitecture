namespace VerticalSliceArchitecture.Domain.ValueObjects;

public class EmailAddress
{
    public string Value { get; }

    public override string ToString() => Value;

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
        {
            throw new ArgumentException($"Invalid email address: {value}");
        }

        Value = value.Trim().ToLowerInvariant();
    }
}
