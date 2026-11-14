using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace UrlShortener.Tests;

public class LinkApiTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    // Don't follow redirects: the redirect itself is what we're testing.
    private readonly HttpClient _client = factory.CreateClient(
        new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

    [Fact]
    public async Task Created_link_redirects_to_the_target()
    {
        var created = await _client.PostAsJsonAsync("/links", new { url = "https://example.com/docs" });
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        var body = await created.Content.ReadFromJsonAsync<CreatedLink>();

        var followed = await _client.GetAsync($"/{body!.Code}");

        Assert.Equal(HttpStatusCode.Redirect, followed.StatusCode);
        Assert.Equal(new Uri("https://example.com/docs"), followed.Headers.Location);
    }

    [Fact]
    public async Task Invalid_url_is_rejected()
    {
        var response = await _client.PostAsJsonAsync("/links", new { url = "javascript:alert(1)" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Unknown_code_is_not_found()
    {
        var response = await _client.GetAsync("/nope123");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private sealed record CreatedLink(string Code, string ShortUrl, string TargetUrl);
}
