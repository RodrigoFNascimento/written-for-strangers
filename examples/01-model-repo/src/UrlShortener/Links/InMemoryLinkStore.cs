using System.Collections.Concurrent;

namespace UrlShortener.Links;

/// <summary>
/// Keeps links in memory. Links are lost on restart, which is fine for this sample
/// (see docs/architecture.md).
/// </summary>
public sealed class InMemoryLinkStore : ILinkStore
{
    // Ordinal: short codes are case-sensitive ("abc" and "ABC" are different links).
    private readonly ConcurrentDictionary<string, Link> _links = new(StringComparer.Ordinal);

    /// <inheritdoc />
    public bool TryAdd(Link link) => _links.TryAdd(link.Code, link);

    /// <inheritdoc />
    public Link? Find(string code) => _links.GetValueOrDefault(code);
}
