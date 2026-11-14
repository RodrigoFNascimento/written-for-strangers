using System.Security.Cryptography;

namespace UrlShortener.Links;

/// <summary>
/// Generates random Base62 short codes. Why random and not sequential:
/// docs/adr/0001-short-code-generation.md.
/// </summary>
public sealed class RandomShortCodeGenerator : IShortCodeGenerator
{
    /// <summary>Number of characters in every generated code.</summary>
    public const int Length = 7;

    /// <summary>The characters a code is made of: Base62 (digits and letters).</summary>
    // Letters and digits only, so codes are URL-safe without any encoding.
    public const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    /// <inheritdoc />
    public string Next() => RandomNumberGenerator.GetString(Alphabet, Length);
}
