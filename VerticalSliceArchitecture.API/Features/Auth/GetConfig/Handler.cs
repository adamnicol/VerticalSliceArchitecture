using Microsoft.Extensions.Options;
using VerticalSliceArchitecture.API.Configuration;
using VerticalSliceArchitecture.API.Configuration.Auth;

namespace VerticalSliceArchitecture.API.Features.Auth.GetConfig;

public class Handler(IOptions<AuthenticationSettings> settings) : IRequestHandler
{
    public async Task<ErrorOr<AuthenticationConfig>> HandleAsync(CancellationToken cancellationToken)
    {
        if (settings == null)
        {
            return Error.Unexpected("Not configured.");
        }

        var providers = settings.Value.Providers.Where(p => p.Enabled);

        return new AuthenticationConfig()
        {
            LocalLoginEnabled = settings.Value.LocalLoginEnabled,
            LocalLoginPath = $"{Constants.RoutePrefix}/auth/login",

            Providers = providers.Select(x => new ExternalProvider()
            {
                Identifier = x.Identifier.ToLower(),
                DisplayName = x.DisplayName,
                LoginPath = $"{Constants.RoutePrefix}/auth/login/{x.Identifier.ToLowerInvariant()}"
            })
        };
    }
}
