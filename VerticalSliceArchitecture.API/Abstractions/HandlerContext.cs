using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public sealed class HandlerContext : IHandlerContext
    {
        public DatabaseContext Database { get; }
        public ILogger<IHandlerContext> Logger { get; }

        public HandlerContext(
            DatabaseContext database, 
            ILogger<IHandlerContext> logger)
        {
            Database = database;
            Logger = logger;
        }
    }
}
