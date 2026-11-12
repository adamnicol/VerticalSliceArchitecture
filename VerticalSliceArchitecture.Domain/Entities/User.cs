using VerticalSliceArchitecture.Domain.Abstractions;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.Domain.Entities
{
    public class User : AuditableEntity, ISoftDelete
    {
        [Key]
        public required Guid UserId { get; set; }
        [MaxLength(255)]
        public required EmailAddress EmailAddress { get; set; }
        [MaxLength(255)]
        public string? PasswordHash { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
