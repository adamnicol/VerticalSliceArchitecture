namespace VerticalSliceArchitecture.API.Features.Auth.GetUser;

public class Handler(IUserContext user) : IRequestHandler
{
    public async Task<ErrorOr<Response>> HandleAsync(CancellationToken cancellationToken)
    {
        return new Response()
        {
            UserId = user.UserId,
            EmailAddress = user.EmailAddress,
            Administrator = user.Administrator,
        };
    }
}
