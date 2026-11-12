using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.Enums;
using VerticalSliceArchitecture.Domain.ValueObjects;
using VerticalSliceArchitecture.Infrastructure.Database.Providers.Contracts;

namespace VerticalSliceArchitecture.Infrastructure.Database.Providers;

public class LoginProvider(DatabaseContext database) : ILoginProvider
{
    public async Task<User> GetUser(EmailAddress email, CancellationToken cancellationToken)
    {
        var user = await database.Users
            .Where(user => user.EmailAddress == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            string username = email.Value.Substring(0, email.Value.IndexOf('@'));

            user = new User()
            {
                UserId = Guid.NewGuid(),
                EmailAddress = email, 
                Status = EntityStatus.Active,
            };

            database.Users.Add(user);

            await database.SaveChangesAsync(cancellationToken);
        }

        return user;
    }
}
