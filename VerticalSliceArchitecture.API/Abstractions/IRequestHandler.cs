namespace VerticalSliceArchitecture.API.Abstractions
{
    public interface IRequestHandler;

    public interface IRequestHandler<TRequest, TResponse> : IRequestHandler
    {
        Task<ErrorOr<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
