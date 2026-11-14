# API

The API describes itself: run the app and open `/openapi/v1.json`. This page explains how
that document is produced and kept complete.

## Endpoints

| Method | Path | Result |
|---|---|---|
| `POST` | `/links` | `201` with the new link, or `400` if the URL isn't a valid target |
| `GET` | `/{code}` | `302` redirect to the target URL, or `404` |

## How the API is documented

Two audiences, two tools ([ADR 0002](adr/0002-document-the-api-with-data-annotations.md)):

| Who reads it | What we write | Where it shows up |
|---|---|---|
| Developers **consuming** the API | `[Description]` on contracts and parameters, `.WithSummary()` / `.WithDescription()` on endpoints | The OpenAPI document |
| Developers **reading** the code | XML doc comments (`///`) on public types and members | IDE tooltips |

## How it stays complete

Nobody has to remember to check. `OpenApiDocumentationTests` loads the OpenAPI document and
fails if any operation lacks a summary or description, or any parameter or contract property
lacks a description. For example, removing one `[Description]` fails CI with:

```
LinkResponse.shortUrl has no [Description].
```

If you add a new contract, add its schema name to `IsOurContract` in that test.
