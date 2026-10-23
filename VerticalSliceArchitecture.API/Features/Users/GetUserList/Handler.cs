using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserList;

public class Handler(IHandlerContext context) : RequestHandler(context)
{
    public async Task<ErrorOr<Response>> HandleAsync(
        Request request,
        CancellationToken cancellationToken)
    {
        var total = await Database.Users.CountAsync(cancellationToken);
        var users = await Database.Users
            .AsNoTracking()
            .OrderBy(user => user.UserId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new Response()
        {
            Results = users.Select(UserDto.Create),
            Total = total,
            Returned = users.Count,
        };
    }
}