using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VerticalSliceArchitecture.Domain.Abstractions;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.Enums;
using VerticalSliceArchitecture.Infrastructure.Database.Extensions;

namespace VerticalSliceArchitecture.Infrastructure.Database
{
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

            foreach (var type in builder.Model.GetEntityTypes()
                .Where(x => typeof(ISoftDelete).IsAssignableFrom(x.ClrType)))
            {
                var method = typeof(DatabaseContext)
                    .GetMethod(nameof(SoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)?
                    .MakeGenericMethod(type.ClrType);

                method?.Invoke(null, new[] { builder });
            }
        }

        private static void SoftDeleteFilter<TEntity>(ModelBuilder builder) where TEntity : class, ISoftDelete
        {
            builder.Entity<TEntity>().HasQueryFilter(e => e.Status != EntityStatus.Deleted);
        }
    }
}
