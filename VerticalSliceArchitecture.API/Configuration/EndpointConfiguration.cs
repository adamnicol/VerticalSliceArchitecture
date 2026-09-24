using System.Reflection;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.API.Configuration;

public static class EndpointConfiguration
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

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(Constants.RoutePrefix);

        foreach (IEndpoint endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
        {
            endpoint.MapEndpoint(group);
        }

        return app;
    }
}
