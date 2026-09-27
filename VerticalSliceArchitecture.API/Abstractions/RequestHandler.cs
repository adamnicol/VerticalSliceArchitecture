using VerticalSliceArchitecture.Infrastructure.Database;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public abstract class RequestHandler(IHandlerContext context) : IRequestHandler
    {
        protected DatabaseContext Database { get; } = context.Database;
        protected ILogger<RequestHandler> Logger { get; } = context.Logger;
    }
}
