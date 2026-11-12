using System.Reflection;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class EndpointConfiguration
{
    internal static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        services.AddEndpoints(assembly);
        services.AddRequestHandlers(assembly);
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }

    private static void AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        foreach (Type type in assembly.DefinedTypes)
        {
            if (type.IsClass && type.IsAssignableTo(typeof(IEndpoint)))
            {
                services.Add(ServiceDescriptor.Transient(typeof(IEndpoint), type));
            }
        }
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

    internal static WebApplication MapEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup(Constants.RoutePrefix)
            .RequireAuthorization();

        foreach (IEndpoint endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
        {
            endpoint.MapEndpoint(group);
        }

        return app;
    }
}
