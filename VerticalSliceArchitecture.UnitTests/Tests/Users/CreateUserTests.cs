using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.API.Features.Users.CreateUser;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.UnitTests.Tests.Users;

public class CreateUserTests
{
    [Fact]
    public async Task CreateUser()
    {
        var context = HandlerContextFactory.GetHandlerContext();
        var hasher = new PasswordHasher<User>();

        var handler = new Handler(context, hasher);
        var request = new Request("user@domain.com", "password");

        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        Assert.True(response.IsSuccess);

        var users = await context.Database.Users.ToListAsync(TestContext.Current.CancellationToken);
        
        var user = Assert.Single(users);

        Assert.Equal(request.Email, user.EmailAddress.Value);
        Assert.NotEqual(request.Password, user.Password);

        var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);
     
        Assert.Equal(PasswordVerificationResult.Success, result);      
    }
}
