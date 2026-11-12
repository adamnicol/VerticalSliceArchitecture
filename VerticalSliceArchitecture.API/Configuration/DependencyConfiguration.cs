using Microsoft.AspNetCore.Identity;
using System.Reflection;
using VerticalSliceArchitecture.API.Configuration.Auth;
using VerticalSliceArchitecture.API.Configuration.Auth.Providers;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Infrastructure.Database.Providers;
using VerticalSliceArchitecture.Infrastructure.Database.Providers.Contracts;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class DependencyConfiguration
{
    internal static void AddDependencies(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        services.AddRequestHandlers(assembly);
        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<OidcEvents>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IHandlerContext, HandlerContext>();
        services.AddScoped<ILoginProvider, LoginProvider>();

        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    private static void AddRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        foreach (Type type in assembly.DefinedTypes)
        {
            if (type is { IsClass: true, IsAbstract: false } && type.IsAssignableTo(typeof(IRequestHandler)))
            {
                services.Add(ServiceDescriptor.Transient(type, type));
            }
        }
    }
}
