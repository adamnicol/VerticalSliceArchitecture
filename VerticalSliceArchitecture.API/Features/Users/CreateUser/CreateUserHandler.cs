using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Features.Users.CreateUser;

internal class CreateUserHandler(IHandlerContext ctx) : IRequestHandler
{
    public async Task<ErrorOr<CreateUserResponse>> HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        string email = request.Email.ToLower().Trim();

        if (await ctx.Database.Users.AnyAsync(user => user.EmailAddress == email, cancellationToken))
        {
            return Error.Conflict();
        }

        var user = new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = email,
            Password = request.Password,
        };

        await ctx.Database.Users.AddAsync(user, cancellationToken);
        await ctx.Database.SaveChangesAsync(cancellationToken);

        ctx.Logger.LogInformation("New account created for {Email}", email);

        var response = new CreateUserResponse(user.UserId);

        return response;
    }
}
