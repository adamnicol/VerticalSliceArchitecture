namespace VerticalSliceArchitecture.API.Configuration.Caching;

internal static class CachePolicy
{
    /// <summary>
    /// Disables caching. The response will not be cached. 
    /// </summary>
    public const string NoCache = nameof(NoCache);

    /// <summary>
    /// All users will receive the same cached response. Medium cache length.
    /// </summary>
    public const string Shared = nameof(Shared);

    /// <summary>
    /// Each user will see a different cached response. Medium cache length.
    /// </summary>
    public const string PerUser = nameof(PerUser);

    /// <summary>
    /// All users will receive the same cached response. Short cache length.
    /// </summary>
    public const string SharedShortLived = nameof(SharedShortLived);

    /// <summary>
    /// Each user will see a different cached response. Short cache length.
    /// </summary>
    public const string PerUserShortLived = nameof(PerUserShortLived);

    /// <summary>
    /// All users will receive the same cached response. Long cache length.
    /// </summary>
    public const string SharedLongLived = nameof(SharedLongLived);

    /// <summary>
    /// Each user will see a different cached response. Long cache length.
    /// </summary>
    public const string PerUserLongLived = nameof(PerUserLongLived);
}
