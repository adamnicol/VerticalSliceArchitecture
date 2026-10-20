using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.UnitTests.Common;

internal class DbContextFactory
{
    internal static DatabaseContext CreateContext()
    {
        var builder = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        return new DatabaseContext(builder.Options);
    }
}
