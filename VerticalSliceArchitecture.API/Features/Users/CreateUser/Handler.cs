using Microsoft.AspNetCore.Identity;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Features.Users.CreateUser;

public class Handler(IHandlerContext context, IPasswordHasher<User> hasher) : RequestHandler(context)
{
    public async Task<ErrorOr<Created>> HandleAsync(Request req, CancellationToken ct)
    {
        var email = new EmailAddress(req.Email);

        if (await Database.Users.AnyAsync(user => user.EmailAddress == email, ct))
        {
            return Error.Conflict();
        }

        var user = new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = email,
        };

        user.PasswordHash = hasher.HashPassword(user, req.Password);

        Database.Users.Add(user);
        await Database.SaveChangesAsync(ct);

        Log.Information("New account created for {Email}", email);

        return Result.Created;
    }
}
