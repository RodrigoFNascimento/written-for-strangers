namespace UrlShortener.Links;

/// <summary>
/// Produces candidate short codes. A candidate may already be taken;
/// <see cref="LinkShortener"/> handles collisions.
/// </summary>
public interface IShortCodeGenerator
{
    /// <summary>Returns a new candidate short code.</summary>
    string Next();
}
