using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Infrastructure.Database.Extensions;

namespace VerticalSliceArchitecture.Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.AddValueObjectConversions();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
