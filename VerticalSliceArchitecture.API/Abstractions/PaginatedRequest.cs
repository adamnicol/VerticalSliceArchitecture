using System.ComponentModel.DataAnnotations;
using VerticalSliceArchitecture.API.Configuration;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public abstract record PaginatedRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; init; } = 1;

        [Range(1, Constants.MaximumPageSize)]
        public int PageSize { get; init; } = Constants.MaximumPageSize;
    }
}
