using VerticalSliceArchitecture.API.Features.Users.GetUserById;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.UnitTests.Tests.Users;

public class GetUserByIdTests
{
    [Fact]
    public async Task GetUserById()
    {
        var user = new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = new ("user@domain.com"),
            Password = "password"
        };

        var context = HandlerContextFactory.GetHandlerContext();

        context.Database.Users.Add(user);
        await context.Database.SaveChangesAsync(TestContext.Current.CancellationToken);

        var handler = new Handler(context);
        var request = new Request(user.UserId);

        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(user.EmailAddress.Value, response.Value.Email);
    }
}