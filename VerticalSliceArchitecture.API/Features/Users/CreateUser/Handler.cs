using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.API.Features.Users.CreateUser;

public class Handler(IHandlerContext context, IPasswordHasher<User> hasher) : RequestHandler(context)
{
    public async Task<ErrorOr<Success>> HandleAsync(Request req, CancellationToken ct)
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
            Password = string.Empty,
        };

        user.Password = hasher.HashPassword(user, req.Password);

        Database.Users.Add(user);
        await Database.SaveChangesAsync(ct);

        Log.Information("New account created for {Email}", email);

        return Result.Success;
    }
}
