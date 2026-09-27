using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserList;

internal class GetUserListHandler(IHandlerContext context) : RequestHandler(context)
{
    public async Task<ErrorOr<GetUserListResponse>> HandleAsync(
        GetUserListRequest request,
        CancellationToken cancellationToken)
    {
        var total = await Database.Users.CountAsync(cancellationToken);
        var users = await Database.Users
            .AsNoTracking()
            .OrderBy(user => user.UserId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new GetUserListResponse()
        {
            Items = users.Select(UserDto.Create),
            Total = total,
            Returned = users.Count,
        };
    }
}