using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Features.Users.GetUserById;

internal class GetUserByIdHandler(IHandlerContext context) : RequestHandler(context)
{
    public async Task<ErrorOr<GetUserByIdResponse>> HandleAsync(
        GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        User? user = await Database.Users.FirstOrDefaultAsync(user => user.UserId == request.UserId, cancellationToken);
        if (user == null)
        {
            return Error.NotFound();
        }

        var response = new GetUserByIdResponse(user.UserId, user.EmailAddress);

        return response;
    }
}