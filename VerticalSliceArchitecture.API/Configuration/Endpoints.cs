using System.Reflection;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Configuration;

public static class Endpoints
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

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        foreach (IEndpoint endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
