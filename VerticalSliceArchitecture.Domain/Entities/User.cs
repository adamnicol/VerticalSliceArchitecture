using VerticalSliceArchitecture.Domain.Abstractions;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.Domain.Entities
{
    public class User : AuditableEntity, ISoftDelete
    {
        [Key]
        public required Guid UserId { get; set; }
        [MaxLength(255)]
        public required Email EmailAddress { get; set; }
        [MaxLength(255)]
        public required string Password { get; set; }
    }
}
