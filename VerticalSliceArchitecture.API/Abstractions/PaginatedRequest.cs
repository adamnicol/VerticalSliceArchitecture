using System.ComponentModel.DataAnnotations;
using VerticalSliceArchitecture.API.Configuration;

namespace VerticalSliceArchitecture.API.Abstractions
{
    public abstract record PaginatedRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; init; }

        [Range(1, Constants.MaxPageSize)]
        public int PageSize { get; init; }
    }
}
