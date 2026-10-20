using Serilog;
using VerticalSliceArchitecture.API.Abstractions;

namespace VerticalSliceArchitecture.UnitTests.Common;

internal class HandlerContextFactory
{
    public static IHandlerContext GetHandlerContext()
    {
        var database = DbContextFactory.CreateContext();
        var logger = new LoggerConfiguration().CreateLogger();

        return new HandlerContext(database, logger);
    }
}
