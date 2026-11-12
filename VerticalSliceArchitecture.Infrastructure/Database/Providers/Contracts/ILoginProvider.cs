using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.Infrastructure.Database.Providers.Contracts;

public interface ILoginProvider
{
    Task<User> GetUser(string externalId, EmailAddress email, CancellationToken ct);
}
