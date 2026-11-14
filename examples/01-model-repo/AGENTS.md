# AGENTS.md

Instructions for AI coding agents working in this repository. Humans: the same rules are in
[CONTRIBUTING.md](CONTRIBUTING.md), with more explanation.

## Read first
- [GLOSSARY.md](GLOSSARY.md): use these terms in code, comments and docs. Never "slug",
  "alias" or "hash" for a short code.
- [docs/architecture.md](docs/architecture.md): how the code is organized.
- [docs/adr/](docs/adr/): decisions already made. Don't "fix" something an ADR explains;
  if you think a decision is wrong, say so instead of changing it.

## Commands
- Build: `dotnet build -warnaserror`
- Test: `dotnet test`
- Format: `dotnet format` (CI runs `dotnet format --verify-no-changes`)

All three must succeed before you consider a task done.

## Conventions
- .NET 10, C# latest, nullable enabled, file-scoped namespaces.
- Everything about links lives in `src/UrlShortener/Links/`. Don't add layers or folders
  for a change this size.
- Public types and members need XML doc comments; the build fails without them.
- API contracts are `internal` records documented with `[Description]`; endpoints use
  `.WithSummary()` and `.WithDescription()`. Don't document the API with XML comments.
- Comments explain *why*, not *what*.
- Tests: xUnit, one behavior per test, names like `Unknown_code_is_not_found`.

## Boundaries
- Don't add NuGet packages without an issue that asks for it.
- Don't change `.github/workflows/` or `Directory.Build.props` to make a check pass.
  Fix the code instead.
- Keep changes small and focused on the task. Mention anything else you noticed in your
  summary instead of fixing it.

## When you finish
Update docs in the same change (see "Making a change" in CONTRIBUTING.md), and add a line
under `Unreleased` in CHANGELOG.md for anything a user of the API would notice.
