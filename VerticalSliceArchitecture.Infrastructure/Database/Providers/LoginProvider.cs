using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.Enums;
using VerticalSliceArchitecture.Domain.ValueObjects;
using VerticalSliceArchitecture.Infrastructure.Database.Providers.Contracts;

namespace VerticalSliceArchitecture.Infrastructure.Database.Providers;

public class LoginProvider(DatabaseContext database) : ILoginProvider
{
    public async Task<User> GetUser(string externalId, EmailAddress email, CancellationToken ct)
    {
        var user = await database.Users
            .Where(user => user.ExternalId == externalId)
            .FirstOrDefaultAsync(ct);

        if (user == null)
        {
            user = new User()
            {
                UserId = Guid.NewGuid(),
                ExternalId = externalId,
                EmailAddress = email,
                Status = EntityStatus.Active,
            };

            database.Users.Add(user);

            await database.SaveChangesAsync(ct);
        }

        return user;
    }
}
