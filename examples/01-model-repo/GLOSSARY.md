# Glossary

The words this project uses, and what they mean **here**. If you need a new term in code,
docs or an issue, add it here in the same PR.

| Term | Meaning | In code |
|---|---|---|
| **Link** | A short code together with the URL it redirects to. | `Link` |
| **Short code** | The 7-character Base62 identifier at the end of a short URL, e.g. `aZ3kP9q`. Case-sensitive. | `Link.Code` |
| **Short URL** | The full URL people share: the API's address + `/` + short code. | `LinkResponse.ShortUrl` |
| **Target URL** | The original, long URL a link redirects to. Must be absolute `http` or `https`. | `Link.Target` |
| **Shorten** | Create a new link for a target URL. Always creates a new code, even for a URL shortened before. | `LinkShortener.Shorten` |
| **Follow** / **resolve** | Look up a short code. *Follow* is the HTTP redirect; *resolve* is the lookup behind it. | `FollowLink`, `LinkShortener.Resolve` |
| **Collision** | A newly generated short code that's already taken. Handled by retrying ([ADR 0001](docs/adr/0001-short-code-generation.md)). | `LinkShortener.MaxAttempts` |

## Words we don't use

| Avoid | Say instead | Why |
|---|---|---|
| *slug* | short code | "Slug" usually means a readable, human-chosen path (`/my-blog-post`). Our codes are random. |
| *alias* | short code | We don't support custom codes; "alias" suggests we do. |
| *hash* | short code | Codes aren't derived from the URL ([ADR 0001](docs/adr/0001-short-code-generation.md)). |
