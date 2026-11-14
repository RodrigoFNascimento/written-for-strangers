# Architecture Decision Records

An ADR records **one** decision: the context, what we chose, and what it costs us. It's how
the *why* survives after the people who decided have moved on.

- ADRs are numbered and never deleted.
- An accepted ADR isn't edited to change the decision. Write a new one that **supersedes**
  it, and mark the old one `Superseded by NNNN`.
- Format: [Michael Nygard's](https://cognitect.com/blog/2011/11/15/documenting-architecture-decisions)
  (Title, Status, Context, Decision, Consequences). Copy [template.md](template.md).

| # | Decision | Status |
|---|---|---|
| [0001](0001-short-code-generation.md) | Generate short codes randomly, retry on collision | Accepted |
| [0002](0002-document-the-api-with-data-annotations.md) | Document the API with data annotations, not XML comments | Accepted |
