using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.Enums;

namespace VerticalSliceArchitecture.Infrastructure.Database.Queries;

public static class UserQueries
{
    public static IQueryable<User> Active(this IQueryable<User> users)
    {
        return users.Where(x => x.Status == EntityStatus.Active);
    }
}
