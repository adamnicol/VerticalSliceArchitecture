using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        // Don't delete
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
