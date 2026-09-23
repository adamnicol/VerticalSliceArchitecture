using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public sealed class HandlerContext : IHandlerContext
    {
        public DatabaseContext Database { get; }
        public IConfiguration Configuration { get; }
        public ILogger<IHandlerContext> Logger { get; }

        public HandlerContext(
            DatabaseContext database, 
            IConfiguration configuration, 
            ILogger<IHandlerContext> logger)
        {
            Database = database;
            Configuration = configuration;
            Logger = logger;
        }
    }
}
