using VerticalSliceArchitecture.Domain.Abstractions;

namespace VerticalSliceArchitecture.Domain.Entities
{
    public class User : Entity
    {
        public required Guid UserId { get; set; }
        public required string EmailAddress { get; set; }
        public required string Password { get; set; }
    }
}
