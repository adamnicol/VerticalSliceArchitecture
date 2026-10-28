using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.Infrastructure.Database.Converters
{
    public sealed class EmailConverter()
        : ValueConverter<Email, string>(x => x.Value, x => new Email(x));
}
