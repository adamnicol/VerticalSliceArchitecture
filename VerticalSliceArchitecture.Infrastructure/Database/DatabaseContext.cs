using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
    }
}
