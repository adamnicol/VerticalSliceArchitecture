using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.Infrastructure.Database.Providers.Contracts;

public interface ILoginProvider
{
    Task<User> GetUser(EmailAddress email, CancellationToken cancellationToken);
}
