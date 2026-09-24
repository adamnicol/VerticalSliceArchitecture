using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Database;
using VerticalSliceArchitecture.Infrastructure.Database.Interceptors;

namespace VerticalSliceArchitecture.API.Configuration;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException("Connection string not set");
            
        return AddDbContext<DatabaseContext>(services, connectionString);
    }

    public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, string connectionString) 
        where TContext : DbContext
    {
        return services.AddDbContext<TContext>(options =>
        {
            options.UseSqlServer(connectionString, settings =>
            {
                settings.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });

            options.AddInterceptors(new SoftDeleteInterceptor());
            options.AddInterceptors(new AuditingInterceptor());
        });
    }

    public static WebApplication RunMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        {
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            context.Database.Migrate();
        }

        return app;
    }
}
