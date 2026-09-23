using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public interface IHandlerContext
    {
        DatabaseContext Database { get; }
        IConfiguration Configuration { get; }
        ILogger<IHandlerContext> Logger { get; }
    }
}
