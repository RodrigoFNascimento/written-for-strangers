# Changelog

All notable changes to this project are documented here.
The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and versions
follow [Semantic Versioning](https://semver.org/).

## [Unreleased]

## [1.1.0] - 2026-11-14
### Added
- Tests that fail when an endpoint, parameter or contract property has no description in
  the OpenAPI document ([ADR 0002](docs/adr/0002-document-the-api-with-data-annotations.md)).

<!-- #region v1 -->
## [1.0.0] - 2026-11-01
### Added
- `POST /links` creates a short link for an absolute `http` or `https` URL.
- `GET /{code}` redirects to the link's target URL, or returns 404.
- OpenAPI document at `/openapi/v1.json`.

### Security
- Only `http` and `https` target URLs are accepted, so a short link can't hide a
  `javascript:` or `file:` URL.
<!-- #endregion -->
