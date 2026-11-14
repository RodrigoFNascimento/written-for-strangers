using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Links;

/// <summary>Creates links and resolves short codes back to their target URL.</summary>
public sealed class LinkShortener(ILinkStore store, IShortCodeGenerator codes)
{
    /// <summary>How many codes to try before giving up on a streak of collisions.</summary>
    public const int MaxAttempts = 5;

    /// <summary>
    /// Checks that <paramref name="url"/> is an absolute http or https URL.
    /// Other schemes (javascript:, file:, ...) are rejected so a short link can't be used
    /// to hide them from the person clicking it.
    /// </summary>
    public static bool IsValidTarget(string? url, [NotNullWhen(true)] out Uri? target)
    {
        target = Uri.TryCreate(url, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
                ? uri
                : null;
        return target is not null;
    }

    /// <summary>Creates a link with a new, unique short code.</summary>
    /// <exception cref="InvalidOperationException">
    /// Every attempt hit a code that was already taken.
    /// </exception>
    public Link Shorten(Uri target)
    {
        #region collision-retry
        for (var attempt = 1; attempt <= MaxAttempts; attempt++)
        {
            var link = new Link(codes.Next(), target);

            // Random codes can collide. Retrying is cheaper than coordinating
            // a shared counter (docs/adr/0001-short-code-generation.md).
            if (store.TryAdd(link))
            {
                return link;
            }
        }

        throw new InvalidOperationException(
            $"Could not find a free short code after {MaxAttempts} attempts.");
        #endregion
    }

    /// <summary>Returns the link for <paramref name="code"/>, or <c>null</c> if it doesn't exist.</summary>
    public Link? Resolve(string code) => store.Find(code);
}
