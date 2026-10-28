using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserById;

public class Handler(IHandlerContext context) : RequestHandler(context)
{
    public async Task<ErrorOr<Response>> HandleAsync(
        Request request,
        CancellationToken cancellationToken)
    {
        User? user = await Database.Users.FirstOrDefaultAsync(user => user.UserId == request.UserId, cancellationToken);
        if (user == null)
        {
            return Error.NotFound();
        }

        var response = new Response(user.UserId, user.EmailAddress.Value);

        return response;
    }
}