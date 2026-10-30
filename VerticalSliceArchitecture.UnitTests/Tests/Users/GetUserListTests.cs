

using VerticalSliceArchitecture.API.Features.Users.GetUserList;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.UnitTests.Tests.Users;

public class GetUserListTests
{
    private readonly List<User> users = [
        new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = new ("user1@domain.com"),
            Password = "password"
        },
        new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = new ("user2@domain.com"),
            Password = "password"
        },
        new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = new ("user3@domain.com"),
            Password = "password"
        },
    ];

    [Fact]
    public async Task GetUserList()
    {
        var context = HandlerContextFactory.GetHandlerContext();

        context.Database.Users.AddRange(users);
        await context.Database.SaveChangesAsync(TestContext.Current.CancellationToken);

        var handler = new Handler(context);
        var request = new Request();

        var response = await handler.HandleAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(3, response.Value.Returned);
    }
}
