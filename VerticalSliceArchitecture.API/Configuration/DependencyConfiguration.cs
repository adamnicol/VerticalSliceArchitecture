using Microsoft.AspNetCore.Identity;
using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Configuration.Auth.Providers;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Infrastructure.Database.Providers;
using VerticalSliceArchitecture.Infrastructure.Database.Providers.Contracts;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class DependencyConfiguration
{
    internal static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<OidcEvents>();

        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IHandlerContext, HandlerContext>();
        services.AddScoped<ILoginProvider, LoginProvider>();

        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}
