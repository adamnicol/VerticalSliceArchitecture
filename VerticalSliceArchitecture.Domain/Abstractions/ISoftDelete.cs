using VerticalSliceArchitecture.Domain.Enums;

namespace VerticalSliceArchitecture.Domain.Abstractions
{
    public interface ISoftDelete
    {
        public EntityStatus Status { get; set; }
    }
}
