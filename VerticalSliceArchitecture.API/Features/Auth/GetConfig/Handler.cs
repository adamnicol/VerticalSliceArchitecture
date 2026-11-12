using Microsoft.Extensions.Options;

namespace VerticalSliceArchitecture.API.Features.Auth.GetConfig;

public class Handler(IOptions<AuthenticationSettings> settings) : IRequestHandler
{
    public async Task<ErrorOr<AuthenticationConfig>> HandleAsync(CancellationToken cancellationToken)
    {
        if (settings == null)
        {
            return Error.Unexpected("Not configured.");
        }

        return new AuthenticationConfig()
        {
            LocalLoginEnabled = settings.Value.LocalLoginEnabled,

            Providers = settings.Value.Providers.Select(x => new ExternalProvider()
            {
                Identifier = x.Identifier.ToLower(),
                DisplayName = x.DisplayName,
                LoginPath = $"/auth/login/{x.Identifier.ToLower()}"
            })
        };
    }
}
