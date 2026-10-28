using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VerticalSliceArchitecture.Infrastructure.Database;
using VerticalSliceArchitecture.Infrastructure.Database.Interceptors;

namespace VerticalSliceArchitecture.API.Configuration;

internal static class DatabaseConfiguration
{
    internal static IServiceCollection AddDatabases(this IServiceCollection services)
    {
        return AddMainDatabase(services);
    }

    private static IServiceCollection AddMainDatabase(this IServiceCollection services) 
    {
        return services.AddDbContext<DatabaseContext>((sp, options) =>
        {
            var connectionStrings = sp.GetRequiredService<IOptions<ConnectionStrings>>();

            options.UseSqlServer(connectionStrings.Value.DefaultConnection, settings =>
            {
                settings.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });

            options.AddInterceptors(new SoftDeleteInterceptor());
            options.AddInterceptors(new AuditingInterceptor());
        });
    }

    internal static WebApplication RunMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        {
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            context.Database.Migrate();
        }

        return app;
    }
}
