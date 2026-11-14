using UrlShortener.Links;

namespace UrlShortener.Tests;

public class LinkShortenerTests
{
    private static readonly Uri Target = new("https://example.com/a/long/path");

    [Fact]
    public void Shortened_link_resolves_to_its_target()
    {
        var shortener = new LinkShortener(new InMemoryLinkStore(), new RandomShortCodeGenerator());

        var link = shortener.Shorten(Target);

        Assert.Equal(Target, shortener.Resolve(link.Code)?.Target);
    }

    [Fact]
    public void Unknown_code_resolves_to_nothing()
    {
        var shortener = new LinkShortener(new InMemoryLinkStore(), new RandomShortCodeGenerator());

        Assert.Null(shortener.Resolve("missing"));
    }

    [Fact]
    public void Collision_retries_with_a_new_code_instead_of_overwriting()
    {
        // The generator returns the same code twice, then a free one.
        var store = new InMemoryLinkStore();
        var shortener = new LinkShortener(store, new FixedCodes("aaaaaaa", "aaaaaaa", "bbbbbbb"));
        var first = shortener.Shorten(Target);

        var second = shortener.Shorten(new Uri("https://example.com/other"));

        Assert.Equal("bbbbbbb", second.Code);
        Assert.Equal(Target, store.Find(first.Code)?.Target); // the first link is untouched
    }

    [Fact]
    public void Gives_up_after_max_attempts()
    {
        var store = new InMemoryLinkStore();
        store.TryAdd(new Link("taken00", Target));
        var codes = Enumerable.Repeat("taken00", LinkShortener.MaxAttempts).ToArray();
        var shortener = new LinkShortener(store, new FixedCodes(codes));

        Assert.Throws<InvalidOperationException>(() => shortener.Shorten(Target));
    }

    [Theory]
    [InlineData("https://example.com", true)]
    [InlineData("http://example.com/path?q=1", true)]
    [InlineData("example.com", false)]          // not absolute
    [InlineData("ftp://example.com/file", false)]
    [InlineData("javascript:alert(1)", false)]  // a short link must never hide a script
    [InlineData("", false)]
    [InlineData(null, false)]
    public void Only_absolute_http_or_https_urls_are_valid_targets(string? url, bool expected)
    {
        Assert.Equal(expected, LinkShortener.IsValidTarget(url, out _));
    }

    private sealed class FixedCodes(params string[] codes) : IShortCodeGenerator
    {
        private readonly Queue<string> _codes = new(codes);

        public string Next() => _codes.Dequeue();
    }
}
