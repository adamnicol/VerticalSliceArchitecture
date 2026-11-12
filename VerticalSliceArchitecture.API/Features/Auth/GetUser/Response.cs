namespace VerticalSliceArchitecture.API.Features.Auth.GetUser;

public class Response
{
    public Guid UserId { get; set; }
    public string? EmailAddress { get; set; }
    public bool IsAdministrator { get; set; }
}
