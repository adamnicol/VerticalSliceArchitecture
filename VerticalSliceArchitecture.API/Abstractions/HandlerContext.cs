using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public sealed class HandlerContext : IHandlerContext
    {
        public DatabaseContext Database { get; }
        public Serilog.ILogger Logger { get; }

        public HandlerContext(
            DatabaseContext database,
            Serilog.ILogger logger)
        {
            Database = database;
            Logger = logger;
        }
    }
}
