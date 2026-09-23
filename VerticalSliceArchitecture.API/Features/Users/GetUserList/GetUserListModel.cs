using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserList;

public record GetUserListRequest(int Page, int PageSize);

public record GetUserListResponse
{
    public required IEnumerable<UserDto> Users { get; set; }
    public required int Total { get; set; }
    public required int Returned { get; set; }
}

public class UserDto
{
    public required Guid UserId { get; set; }
    public required string EmailAddress { get; set; }

    public static UserDto Create(User user)
    {
        return new UserDto()
        {
            UserId = user.UserId,
            EmailAddress = user.EmailAddress,
        };
    }
}
