using VerticalSliceArchitecture.API.Configuration;

namespace VerticalSliceArchitecture.API.Features.Users.CreateUser;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(Constants.MinimumPasswordLength);
    }
}
