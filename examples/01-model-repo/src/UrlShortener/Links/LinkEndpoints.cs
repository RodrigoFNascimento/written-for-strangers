using System.ComponentModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UrlShortener.Links;

// Maps the HTTP endpoints for creating and following links.
internal static class LinkEndpoints
{
    public static IEndpointRouteBuilder MapLinkEndpoints(this IEndpointRouteBuilder app)
    {
        #region endpoints
        app.MapPost("/links", Create)
            .WithName("CreateLink")
            .WithSummary("Shorten a URL")
            .WithDescription("Creates a new short code that redirects to the given URL.");

        app.MapGet("/{code}", Follow)
            .WithName("FollowLink")
            .WithSummary("Follow a short link")
            .WithDescription("Redirects (302) to the URL behind the short code.");
        #endregion

        return app;
    }

    private static Results<Created<LinkResponse>, ValidationProblem> Create(
        CreateLinkRequest request, LinkShortener shortener, HttpRequest http)
    {
        if (!LinkShortener.IsValidTarget(request.Url, out var target))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                ["url"] = ["Must be an absolute http or https URL."],
            });
        }

        var link = shortener.Shorten(target);
        var shortUrl = $"{http.Scheme}://{http.Host}/{link.Code}";

        return TypedResults.Created(shortUrl, new LinkResponse(link.Code, shortUrl, link.Target.ToString()));
    }

    private static Results<RedirectHttpResult, NotFound> Follow(
        [Description("The short code of an existing link.")] string code,
        LinkShortener shortener) =>
        shortener.Resolve(code) is { } link
            ? TypedResults.Redirect(link.Target.ToString())
            : TypedResults.NotFound();
}
