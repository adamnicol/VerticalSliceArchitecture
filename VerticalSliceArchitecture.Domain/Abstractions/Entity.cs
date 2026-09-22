using VerticalSliceArchitecture.Domain.Enums;

namespace VerticalSliceArchitecture.Domain.Abstractions
{
    public class Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public EntityStatus Status { get; set; }
    }
}
