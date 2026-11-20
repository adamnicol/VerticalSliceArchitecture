namespace VerticalSliceArchitecture.API.Configuration.Caching;

internal static class CachePolicy
{
    public const string Public = nameof(Public);
    public const string PerUser = nameof(PerUser);

    public const string PublicShortLived = nameof(PublicShortLived);
    public const string PerUserShortLived = nameof(PerUserShortLived);

    public const string PublicLongLived = nameof(PublicLongLived);
    public const string PerUserLongLived = nameof(PerUserLongLived);

    public const string NoCache = nameof(NoCache);
}
