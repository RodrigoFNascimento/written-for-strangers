namespace UrlShortener.Links;

/// <summary>
/// A short code and the URL it redirects to. See GLOSSARY.md for the terms.
/// </summary>
/// <param name="Code">The short code, unique per link. Case-sensitive.</param>
/// <param name="Target">The absolute http or https URL the link redirects to.</param>
public sealed record Link(string Code, Uri Target);
