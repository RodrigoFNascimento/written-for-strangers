# 0001. Generate short codes randomly, retry on collision

- **Status:** Accepted
- **Date:** 2026-11-01

## Context
Every link needs a short code that is unique, short enough to type, and safe in a URL.
Short codes are public: anyone can try `/aaaaaaa`, `/aaaaaab`… We don't want people to
discover other people's links by counting.

<!-- #region decision -->
## Decision
Codes are **7 random Base62 characters** (`0-9`, `A-Z`, `a-z`), generated with a
cryptographically secure random number generator. If a code is already taken, generate a
new one and try again, up to 5 times.
<!-- #endregion -->

## Options considered
- **Sequential IDs encoded in Base62** (`1`, `2`, … → `1`, `2`, … `10`): shortest codes and no
  collisions, but trivially guessable, and it needs a shared counter once there's more than
  one instance.
- **Hash of the target URL** (first characters of a SHA-256): the same URL always gets the same
  code, but truncated hashes still collide, and different users shortening the same URL would
  share a link. Codes could also reveal which URLs were shortened.
- **GUIDs:** unique without coordination, but 32+ characters is not "short".

## Consequences
- Codes can't be guessed by counting.
- No coordination between instances: any instance can create a link.
- Collisions are possible, so the store must reject duplicates atomically (`ILinkStore.TryAdd`),
  and `LinkShortener` must retry.
- 62⁷ ≈ 3.5 trillion possible codes keeps collisions rare for any realistic number of links.
  If that ever changes, a new ADR supersedes this one.
- Shortening the same URL twice creates two different links.
