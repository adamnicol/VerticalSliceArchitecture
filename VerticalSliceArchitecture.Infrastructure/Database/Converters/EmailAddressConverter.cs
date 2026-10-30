using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VerticalSliceArchitecture.Domain.ValueObjects;

namespace VerticalSliceArchitecture.Infrastructure.Database.Converters
{
    public sealed class EmailAddressConverter()
        : ValueConverter<EmailAddress, string>(x => x.Value, x => new EmailAddress(x));
}
