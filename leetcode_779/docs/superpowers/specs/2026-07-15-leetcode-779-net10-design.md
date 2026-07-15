# LeetCode 779 .NET 10 Migration Design

## Goal

Upgrade `leetcode_779` from the legacy .NET Framework 4.8 project shape to a
self-contained SDK-style .NET 10 console project while preserving the public
recursive solution API and adding deterministic, executable evidence.

## Design

- Keep `public static int KthGrammar(int n, int k)` as the only solution API.
- Use the row invariant that the first half equals the previous row and the
  second half is its complement. Map `k` to the previous row recursively and
  XOR by `1` only when crossing into the second half.
- Keep the solution pure. `Main` owns all console output and executes ten fixed
  checks covering official examples, boundaries, complement regressions, and
  the maximum valid row.
- Use problem-root VS Code and documentation paths. There is no formal test
  project; the acceptance harness is the executable verification boundary.
- Publish through one scoped commit, an independently reviewed draft PR,
  squash merge, and an exact post-merge Issue #2 checkbox update.

## Error and Boundary Policy

Inputs follow the official constraint `1 <= n <= 30` and
`1 <= k <= 2^(n - 1)`. The migration does not define exceptions or fallback
values for invalid inputs. A harness failure sets `Environment.ExitCode = 1`.
