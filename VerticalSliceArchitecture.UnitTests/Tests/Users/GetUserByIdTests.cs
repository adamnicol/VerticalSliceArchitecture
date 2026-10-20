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
            EmailAddress = "user@domain.com",
            Password = "password"
        };

        var context = HandlerContextFactory.GetHandlerContext();

        await context.Database.Users.AddAsync(user);
        await context.Database.SaveChangesAsync();

        var handler = new GetUserByIdHandler(context);
        var request = new GetUserByIdRequest(user.UserId);

        var response = await handler.HandleAsync(request, default);

        Assert.Equal(user.EmailAddress, response.Value.Email);
    }
}