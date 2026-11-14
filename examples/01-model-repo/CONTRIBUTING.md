# Contributing

Thanks for helping! This file is how we work here. It's written for someone who has never
talked to any of us, and that includes AI agents (who also read [AGENTS.md](AGENTS.md)).

## Before you write code

- **Bugs and small fixes:** open a pull request directly, or an issue if you're not sure
  it's a bug.
- **New features or behavior changes:** open an issue first, using the feature template.
  Agreeing on the *what* before the *how* saves everyone a rewritten PR.
- **Changing a past decision:** check [docs/adr/](docs/adr/) first. If an ADR covers it,
  propose a new ADR that supersedes it; don't just change the code.

## Setting up

```bash
dotnet test        # must pass before you start, and before you open a PR
```

That's it: no database, no secrets, no services to run.

## Making a change

1. Branch from `main`: `fix/<short-description>` or `feature/<short-description>`.
2. Keep the PR **small and about one thing**. Two unrelated fixes = two PRs.
3. Add or update tests. A bug fix starts with a test that fails without the fix.
4. Update the docs **in the same PR**:
   - public C# types and members → XML doc comments (the build fails without them)
   - API endpoints, parameters and contracts → `[Description]`, `.WithSummary()` and
     `.WithDescription()` (the tests fail without them; see [docs/api.md](docs/api.md))
   - behavior or setup changes → `README.md` or the relevant file in `docs/`
   - user-visible changes → a line under `Unreleased` in [CHANGELOG.md](CHANGELOG.md)
   - new terms → [GLOSSARY.md](GLOSSARY.md)
5. Run `dotnet format` so formatting never comes up in review.

## What CI checks on every pull request

The pull request can't be merged until all of these pass:

| Check | Command |
|---|---|
| Builds with no warnings | `dotnet build -warnaserror` |
| Formatting | `dotnet format --verify-no-changes` |
| Tests, including API documentation tests | `dotnet test` |

Run them locally first; it's faster than waiting for CI.

## Review

- Every PR needs one approval from a [code owner](.github/CODEOWNERS).
- Reviewers review the code, not the person: "this method…", not "you…".
- Reviewers label their comments so you know what's blocking
  ([Conventional Comments](https://conventionalcomments.org/)):
  - `issue (blocking):` must be fixed before merging
  - `suggestion:` worth considering, your call
  - `nitpick (non-blocking):` tiny, feel free to ignore
  - `question:` I don't understand something yet
- The author resolves a comment thread once it's addressed; the reviewer re-approves.

<!-- #region spikes -->
## Spikes leave a doc behind

If you had to investigate how something works before you could change it, write down what
you learned: a file in [docs/](docs/), a new ADR, or a GLOSSARY entry, in the same PR.
Nobody should have to pay for the same investigation twice.
<!-- #endregion -->
