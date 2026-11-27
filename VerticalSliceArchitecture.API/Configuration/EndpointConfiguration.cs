using System.Reflection;
using VerticalSliceArchitecture.API.Configuration.Auth;

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
        var root = app
            .MapGroup(Constants.RoutePrefix)
            .RequireAuthorization();

        var admin = root.MapGroup("/admin")
            .RequiresRole(UserRoles.Administrator);

        foreach (IEndpoint endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
        {
            string @namespace = endpoint.GetType().Namespace ?? string.Empty;

            if (@namespace.Contains(nameof(Features.Admin)))
                endpoint.MapEndpoint(admin);
            else
                endpoint.MapEndpoint(root);
        }

        return app;
    }
}
