using VerticalSliceArchitecture.API.Features.Users.CreateUser;

namespace VerticalSliceArchitecture.UnitTests.Tests.Users;

public class CreateUserTests
{
    [Fact]
    public async Task CreateUser()
    {
        var context = HandlerContextFactory.GetHandlerContext();

        var handler = new Handler(context);
        var request = new Request("user@domain.com", "password");

        var response = await handler.HandleAsync(request, default);

        Assert.True(response.IsSuccess);

        var count = context.Database.Users.Count();

        Assert.Equal(1, count);
    }
}
