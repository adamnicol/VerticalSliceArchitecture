using VerticalSliceArchitecture.Infrastructure.Database.Queries;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserList;

public class Handler(IHandlerContext context) : RequestHandler(context)
{
    public async Task<ErrorOr<Response>> HandleAsync(Request req, CancellationToken ct)
    {
        var total = await Database.Users.Active().CountAsync(ct);
        var users = await Database.Users
            .AsNoTracking()
            .Active()
            .OrderBy(user => user.UserId)
            .Skip((req.Page - 1) * req.PageSize)
            .Take(req.PageSize)
            .ToListAsync(ct);

        return new Response()
        {
            Results = users.Select(UserDto.Create),
            Total = total,
            Returned = users.Count,
        };
    }
}