using System.Reflection;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        return services.AddEndpoints(Assembly.GetExecutingAssembly());
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        foreach (Type type in assembly.DefinedTypes)
        {
            if (type.IsClass && type.IsAssignableTo(typeof(IEndpoint)))
            {
                services.Add(ServiceDescriptor.Transient(typeof(IEndpoint), type));
            }
        }

        return services;
    }

    public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
    {
        return services.AddRequestHandlers(Assembly.GetExecutingAssembly());
    }

    public static IServiceCollection AddRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        foreach (Type type in assembly.DefinedTypes)
        {
            if (type.IsClass && type.IsAssignableTo(typeof(IRequestHandler)))
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

    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IHandlerContext, HandlerContext>();

        return services;
    }
}
