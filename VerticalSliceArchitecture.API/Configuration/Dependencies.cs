using System.Reflection;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Configuration;

public static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddRequestHandlers(Assembly.GetCallingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());

        services.AddScoped<IHandlerContext, HandlerContext>();

        return services;
    }

    private static IServiceCollection AddRequestHandlers(this IServiceCollection services, Assembly assembly)
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
}
