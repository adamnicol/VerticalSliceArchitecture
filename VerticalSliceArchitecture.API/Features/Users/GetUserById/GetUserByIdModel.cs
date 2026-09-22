namespace VerticalSliceArchitecture.API.Features.Users.GetUserById
{
    public record GetUserByIdRequest(Guid UserId);

    public record GetUserByIdResponse(Guid UserId, string Email);
}
