using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public interface IHandlerContext
    {
        DatabaseContext Database { get; }
        HttpClient HttpClient { get; }
        ILogger<IHandlerContext> Logger { get; }
    }
}
