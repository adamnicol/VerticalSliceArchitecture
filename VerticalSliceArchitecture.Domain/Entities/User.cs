using System.ComponentModel.DataAnnotations;
using VerticalSliceArchitecture.Domain.Abstractions;

namespace VerticalSliceArchitecture.Domain.Entities
{
    public class User : Entity, ISoftDelete
    {
        [Key]
        public required Guid UserId { get; set; }
        [MaxLength(255)]
        public required string EmailAddress { get; set; }
        [MaxLength(255)]
        public required string Password { get; set; }
    }
}
