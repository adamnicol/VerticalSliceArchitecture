using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceArchitecture.Domain.Entities;
using VerticalSliceArchitecture.Domain.Enums;

namespace VerticalSliceArchitecture.Infrastructure.Database.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.UserId);

        builder.HasIndex(user => user.ExternalId).IsUnique();
        builder.HasIndex(user => user.EmailAddress).IsUnique();

        builder.HasQueryFilter(user => user.Status != EntityStatus.Deleted);
    }
}