using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.ValueObjects;
using VerticalSliceArchitecture.Infrastructure.Database.Converters;

namespace VerticalSliceArchitecture.Infrastructure.Database.Extensions;

internal static class ModelConfigurationBuilderExtensions
{
    internal static ModelConfigurationBuilder AddValueObjectConversions(
        this ModelConfigurationBuilder builder)
    {
        builder.Properties<Email>()
            .HaveConversion<EmailConverter>();

        return builder;
    }
}
