using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public sealed record HandlerContext(DatabaseContext Database, Serilog.ILogger Log) : IHandlerContext;
}
