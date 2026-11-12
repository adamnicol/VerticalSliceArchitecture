namespace VerticalSliceArchitecture.API.Abstractions;

public interface IUserContext
{
    Guid UserId { get; }
    string? EmailAddress { get; }
    bool Administrator { get; }
}
