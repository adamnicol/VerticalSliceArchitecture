namespace VerticalSliceArchitecture.API.Configuration.Caching;

public static class CachePolicy
{
    public const string Public = nameof(Public);
    public const string PerUser = nameof(PerUser);

    public const string PublicShortLived = nameof(PublicShortLived);
    public const string PerUserShortLived = nameof(PerUserShortLived);
}
