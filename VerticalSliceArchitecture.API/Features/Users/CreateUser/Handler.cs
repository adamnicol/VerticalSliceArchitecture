using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.API.Features.Users.CreateUser;

public class Handler(IHandlerContext context, IPasswordHasher<User> hasher) : RequestHandler(context)
{
    public async Task<ErrorOr<Success>> HandleAsync(
        Request request,
        CancellationToken cancellationToken)
    {
        var email = new EmailAddress(request.Email);

        if (await Database.Users.AnyAsync(user => user.EmailAddress == email, cancellationToken))
        {
            return Error.Conflict();
        }

        var user = new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = email,
            Password = string.Empty,
        };

        user.Password = hasher.HashPassword(user, request.Password);

        Database.Users.Add(user);
        await Database.SaveChangesAsync(cancellationToken);

        Log.Information("New account created for {Email}", email);

        return Result.Success;
    }
}
