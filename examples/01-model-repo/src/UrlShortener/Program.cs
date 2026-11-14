using UrlShortener.Links;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<ILinkStore, InMemoryLinkStore>();
builder.Services.AddSingleton<IShortCodeGenerator, RandomShortCodeGenerator>();
builder.Services.AddSingleton<LinkShortener>();

var app = builder.Build();

// The OpenAPI document is served at /openapi/v1.json. See docs/api.md.
app.MapOpenApi();
app.MapLinkEndpoints();

app.Run();

/// <summary>
/// The application entry point. Declared public so integration tests can start the app
/// with <c>WebApplicationFactory&lt;Program&gt;</c>.
/// </summary>
public partial class Program;
