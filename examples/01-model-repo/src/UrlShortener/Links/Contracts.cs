using System.ComponentModel;

namespace UrlShortener.Links;

// API contracts are documented with [Description], because that is what ends up in the
// OpenAPI document. They're internal, so XML doc comments (for code readers) aren't required.
// Why: docs/adr/0002-document-the-api-with-data-annotations.md.

#region contracts
[Description("A request to shorten a URL.")]
internal sealed record CreateLinkRequest(
    [property: Description("The absolute http or https URL the short link redirects to.")]
    string Url);

[Description("A link that was just created.")]
internal sealed record LinkResponse(
    [property: Description("The short code. Case-sensitive.")]
    string Code,
    [property: Description("The full short URL to share.")]
    string ShortUrl,
    [property: Description("The URL the short link redirects to.")]
    string TargetUrl);
#endregion
