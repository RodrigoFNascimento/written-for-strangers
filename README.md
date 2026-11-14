# Written for Strangers: what open source can teach your team

Open-source projects are built by contributors who are strangers: they live in other time
zones, speak other languages, and leave whenever they want. Those projects survive because
the context lives in the repository, not in people's heads. A README gets you running,
CONTRIBUTING explains how the project works, decision records explain *why*, and checks on
every pull request enforce the rules. Any team can use the same practices. They help a new
intern, they help you six months from now, and they help AI agents, which arrive with zero
context every single time.

**Read the article:** [English](https://rodrigofnascimento.github.io/written-for-strangers/en/) · [Português (BR)](https://rodrigofnascimento.github.io/written-for-strangers/pt-br/)<br>
**Slides (pt-BR):** [rodrigofnascimento.github.io/written-for-strangers/slides](https://rodrigofnascimento.github.io/written-for-strangers/slides/)

## Examples

| Example | What it shows | Run |
|---|---|---|
| [01-model-repo](examples/01-model-repo) | A small URL shortener API with everything a contributor needs around the code: README, CONTRIBUTING, GLOSSARY, `docs/` with ADRs, CHANGELOG, AGENTS.md, issue and PR templates, CODEOWNERS, and CI that runs on every pull request. Missing XML docs fail the build. Missing OpenAPI descriptions fail the tests. | `dotnet test examples/01-model-repo/UrlShortener.slnx` |

The model repo is self-contained. Copy the folder as a starting point for your own
repository. To try the API, run `dotnet run --project examples/01-model-repo/src/UrlShortener`.

Run everything:

```bash
dotnet test examples/Examples.slnx
```

Requires the [.NET SDK](https://dotnet.microsoft.com/download) 10 or later.

## Repository layout

```
examples/   working .NET code shown in the talk, with tests
slides/     Slidev deck (pt-BR). From the repo root: `npm install`, then `npm run slides`
article/    the article (EN + pt-BR). From the repo root: `npm run article`
talk/       how this talk was prepared: brief and speaker script
```

## References

- Karl Fogel, [*Producing Open Source Software*](https://producingoss.com/): how open-source projects actually run, free online
- GitHub, [Open Source Guides](https://opensource.guide/): short, practical guides to contributing and maintaining
- Nadia Eghbal, *Working in Public: The Making and Maintenance of Open Source Software* (2020): who really maintains open source
- [InnerSource Commons](https://innersourcecommons.org/): applying open-source practices inside a company
- Michael Nygard, [Documenting Architecture Decisions](https://cognitect.com/blog/2011/11/15/documenting-architecture-decisions) (2011): the post that introduced ADRs
- [Keep a Changelog](https://keepachangelog.com/): a changelog format written for humans
- [Conventional Comments](https://conventionalcomments.org/): labels that make review feedback unambiguous
- Google, [Code Review Developer Guide](https://google.github.io/eng-practices/review/): what to look for in a review, and how to write comments
- [AGENTS.md](https://agents.md/): the open format for guiding coding agents

## License

- **Code** (`examples/`, build scripts): [MIT](LICENSE).
- **Article and slides** (`article/`, `slides/`): [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/).
  You can share and adapt them if you credit the author.

---

Made by [Rodrigo Nascimento](https://github.com/RodrigoFNascimento).
