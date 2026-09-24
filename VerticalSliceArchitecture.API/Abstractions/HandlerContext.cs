using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public sealed class HandlerContext : IHandlerContext
    {
        public DatabaseContext Database { get; }
        public HttpClient HttpClient { get; }
        public ILogger<IHandlerContext> Logger { get; }

        public HandlerContext(
            DatabaseContext database,
            HttpClient httpClient,
            ILogger<IHandlerContext> logger)
        {
            Database = database;
            Logger = logger;
            HttpClient = httpClient;
        }
    }
}
