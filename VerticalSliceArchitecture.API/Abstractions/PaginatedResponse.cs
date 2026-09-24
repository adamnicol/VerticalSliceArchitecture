namespace VerticalSliceArchitecture.API.Abstractions
{
    public record PaginatedResponse<T>()
    {
        public required IEnumerable<T> Items { get; init; }
        public required int Total { get; init; }
        public required int Returned { get; init; }
    }
}
