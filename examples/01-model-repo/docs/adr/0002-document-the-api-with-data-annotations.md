# 0002. Document the API with data annotations, not XML comments

- **Status:** Accepted
- **Date:** 2026-11-14

## Context
The API has two kinds of readers. Developers **calling** the API read the OpenAPI document
(or a UI generated from it). Developers **changing** the code read the source and IDE
tooltips. ASP.NET Core can fill the OpenAPI document from data annotations, from endpoint
metadata, or from XML doc comments.

We want API docs that are easy to spot in review and impossible to forget.

## Decision
- The OpenAPI document is described **only** with `[Description]` (on contracts and
  parameters) and `.WithSummary()` / `.WithDescription()` (on endpoints).
- XML doc comments are for code readers. Every public type and member has one; the build
  enforces it (`GenerateDocumentationFile` + warnings as errors).
- API contracts are `internal`, so they don't need XML comments and there's a single source
  for their API description.
- `OpenApiDocumentationTests` fails the build when any operation, parameter or contract
  property is undocumented.

## Options considered
- **XML comments as the source for OpenAPI:** one kind of comment for everything, but the
  same text then has to serve both audiences, and it's harder to see in a diff whether a
  change affects the public API docs.
- **No enforcement, rely on review:** reviewers forget; the check doesn't.

## Consequences
- An undocumented endpoint can't be merged.
- Two places to write docs, each with a clear audience.
- New contracts must be added to `IsOurContract` in the documentation test.
