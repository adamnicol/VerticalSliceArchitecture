using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("Test");
            }
        }
    }
}
