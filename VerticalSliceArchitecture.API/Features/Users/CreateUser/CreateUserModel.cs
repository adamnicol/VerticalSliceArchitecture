namespace VerticalSliceArchitecture.API.Features.Users.CreateUser
{
    public record CreateUserRequest(string Email, string Password);

    public record CreateUserResponse(Guid UserId);
}
