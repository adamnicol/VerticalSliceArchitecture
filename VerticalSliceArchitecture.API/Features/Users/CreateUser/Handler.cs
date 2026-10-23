using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Features.Users.CreateUser;

public class Handler(IHandlerContext context) : RequestHandler(context)
{
    public async Task<ErrorOr<Success>> HandleAsync(
        Request request,
        CancellationToken cancellationToken)
    {
        string email = request.Email.ToLower().Trim();

        if (await Database.Users.AnyAsync(user => user.EmailAddress == email, cancellationToken))
        {
            return Error.Conflict();
        }

        var user = new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = email,
            Password = request.Password,
        };

        await Database.Users.AddAsync(user, cancellationToken);
        await Database.SaveChangesAsync(cancellationToken);

        Log.Information("New account created for {Email}", email);

        return Result.Success;
    }
}
