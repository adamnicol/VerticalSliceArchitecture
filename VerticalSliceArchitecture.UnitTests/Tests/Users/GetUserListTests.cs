

using VerticalSliceArchitecture.API.Features.Users.GetUserList;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.UnitTests.Tests.Users;

public class GetUserListTests
{
    private readonly List<User> users = [
        new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = "user1@domain.com",
            Password = "password"
        },
        new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = "user2@domain.com",
            Password = "password"
        },
        new User()
        {
            UserId = Guid.NewGuid(),
            EmailAddress = "user3@domain.com",
            Password = "password"
        },
    ];

    [Fact]
    public async Task GetUserList()
    {
        var context = HandlerContextFactory.GetHandlerContext();

        await context.Database.Users.AddRangeAsync(users);
        await context.Database.SaveChangesAsync();

        var handler = new Handler(context);
        var request = new Request();

        var response = await handler.HandleAsync(request, default);

        Assert.Equal(3, response.Value.Returned);
    }
}
