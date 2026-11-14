# URL Shortener

A tiny HTTP API that turns long URLs into short codes and redirects them back.

> This is the **model repository** for the talk *Written for Strangers: what open source can
> teach your team*. The API is deliberately small; the point is everything around it: how a
> repo can give every contributor (a stranger, a new teammate, or an AI agent) the context
> they need without a meeting.

## What it does

```http
POST /links      { "url": "https://example.com/a/very/long/path" }
→ 201 Created    { "code": "aZ3kP9q", "shortUrl": "http://localhost:5206/aZ3kP9q", ... }

GET /aZ3kP9q
→ 302 Found      Location: https://example.com/a/very/long/path
```

## Quick start

You need the [.NET 10 SDK](https://dotnet.microsoft.com/download). Nothing else: links are
stored in memory.

```bash
dotnet test                              # build and run all tests
dotnet run --project src/UrlShortener    # start the API on http://localhost:5206
```

Then try it:

```bash
curl -i -X POST http://localhost:5206/links -H "Content-Type: application/json" -d '{"url":"https://example.com"}'
```

The OpenAPI document is at `http://localhost:5206/openapi/v1.json`.

## Where to go next

| I want to… | Read |
|---|---|
| understand the words used here | [GLOSSARY.md](GLOSSARY.md) |
| make a change | [CONTRIBUTING.md](CONTRIBUTING.md) |
| understand how it's built | [docs/architecture.md](docs/architecture.md) |
| know *why* something is the way it is | [docs/adr/](docs/adr/) |
| see what changed between versions | [CHANGELOG.md](CHANGELOG.md) |
| point an AI agent at this repo | [AGENTS.md](AGENTS.md) |

## Project layout

```
src/UrlShortener/          the API
  Links/                   everything about links: endpoints, contracts, logic, storage
tests/UrlShortener.Tests/  unit, API and API-documentation tests
docs/                      one file per topic, plus decision records in docs/adr/
.github/                   issue/PR templates, code owners, CI
```
