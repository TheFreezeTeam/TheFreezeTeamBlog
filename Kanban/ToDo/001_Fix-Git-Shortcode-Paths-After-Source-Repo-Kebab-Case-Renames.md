# Task 001: Fix Git shortcode paths after source-repo kebab-case renames

## Description

Four posts pull their body from a TimeWarpEngineering repo at build time via the `Git` shortcode
(`Source/TheFreezeTeam.com/Shortcodes/GitShortCode.cs`, GitHub contents API). Those repos renamed
their folders and files to lowercase kebab-case during 2025 (timewarp-nuru on 2025-09-30). The GitHub
contents API is case-sensitive, so every one of those shortcode paths now returns 404. The live site
still shows the posts only because it has not been rebuilt since the renames; the next push to `master`
will replace each body with the shortcode's `#Error: GitHub API request failed with status 404` text.

Verified 2026-10-08 with `GET https://api.github.com/repos/{owner}/{repo}/contents/{path}`:

| Post stub (under `Source/TheFreezeTeam.com/input/posts/steven-t-cramer/`) | Current shortcode path (404) | Correct path (200) |
|---|---|---|
| `2025/07/2025-07-28-nuru-cli-framework-launch.md` | `timewarp-nuru` `Documentation/Posts/Blogs/2025-07-28-nuru-cli-framework-launch.md` | `documentation/posts/blogs/2025-07-28-nuru-cli-framework-launch.md` |
| `2024/12/2024-02-TimeWarp-State-11-Release.md` | `timewarp-state` `Documentation/Blogs/2024-02-TimeWarp-State-11-Release.md` | `documentation/blogs/2024-02-time-warp-state-11-release.md` |
| `2023/02/date-time-service.md` | `timewarp-architecture` `TimeWarp.Architecture/Source/Common/Common.Infrastructure/Services/DateTimeService.cs` | `source/foundation/foundation-infrastructure/services/date-time-service.cs` |
| `2023/02/date-time-service.md` | `timewarp-architecture` `TimeWarp.Architecture/Tests/Common/Common.Infrastructure.Tests/DateTimeService_Tests.cs` | `tests/foundation/foundation-infrastructure-tests/date-time-service-tests.cs` |

The fifth shortcode (`TheFreezeTeam/TheFreezeTeamBlog` `Readme.md`) still resolves.

## Requirements

- Each shortcode path above updated to the lowercase path that returns 200 today.
- The `date-time-service` post uses `RegionName` arguments; confirm the named regions still exist in the
  renamed `.cs` files (the architecture repo may have changed region names along with file names).
- No other content change in the stubs.

## Checklist

### Implementation
- [ ] Update the four `<?! Git ... /?>` paths
- [ ] Re-verify every shortcode path in `input/posts` returns 200 from the GitHub contents API
- [ ] Local Statiq build renders the Nuru, State 11, and DateTimeService posts with real bodies, no `#Error` text
### Documentation
- [ ] Note in `Readme.md` (or `.ai/other/blog.md`) that shortcode paths are case-sensitive and must track
      source-repo renames
### Review
- [ ] Code Review
- [ ] Deployed site shows the three posts correctly after the `master` push

## Notes

- Root cause is upstream, not here: the source repos' kebab-case rename tasks did not search for
  external consumers of the old paths. A later improvement is a CI check in this repo that probes
  every shortcode path so a rename breaks the build instead of the live site.
- Filed from the timewarp-nuru 3.0 release work (nuru task 482/483 context) on 2026-10-08.

## Progress

- 2026-10-15: Nuru 2025 post path fixed (contents API and raw both 200), merged ahead of the Nuru 3.0 post. The State 11 and DateTimeService paths are still open.
