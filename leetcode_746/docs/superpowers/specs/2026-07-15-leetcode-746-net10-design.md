# LeetCode 746 .NET 10 Migration Design

## Goal

Upgrade only `leetcode_746/` to an SDK-style .NET 10 console project and complete
the repository's full review, pull-request, merge, and Issue #2 workflow.

## Design

- Preserve the existing teaching progression: an `O(n)` dynamic-programming array
  implementation and an `O(1)` rolling-state implementation.
- Keep both public methods pure. They return the minimum cost without modifying
  `cost` or writing to the console.
- Make `Main` the deterministic acceptance boundary. Eight cases validate both
  methods and both input-preservation guarantees for 32 total checks.
- Treat the problem folder as the VS Code workspace root and keep every tracked
  change inside `leetcode_746/`.
- Use a Traditional Chinese README whose unique `text` fence is copied from a
  fresh verified run.

## Delivery

Use one commit on `codex/leetcode-746-net10`, obtain an independent read-only
review, open a draft PR, verify its head and checks, squash merge it, then update
only the `leetcode_746` checkbox in Issue #2 and verify `leetcode_767` remains
unchecked.
