using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserList;

public record GetUserListRequest : PaginatedRequest;

public record GetUserListResponse : PaginatedResponse<UserDto>;

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
