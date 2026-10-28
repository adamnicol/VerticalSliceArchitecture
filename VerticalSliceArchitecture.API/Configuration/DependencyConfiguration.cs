using Microsoft.AspNetCore.Identity;
using System.Reflection;
using VerticalSliceArchitecture.API.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class DependencyConfiguration
{
    internal static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddRequestHandlers(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IHandlerContext, HandlerContext>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

    private static IServiceCollection AddRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        foreach (Type type in assembly.DefinedTypes)
        {
            if (type is { IsClass: true, IsAbstract: false } && type.IsAssignableTo(typeof(IRequestHandler)))
            {
                services.Add(ServiceDescriptor.Transient(type, type));
            }

            foreach (Type @interface in type.GetInterfaces())
            {
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                {
                    services.AddTransient(@interface, type);
                }
            }
        }

        return services;
    }
}
