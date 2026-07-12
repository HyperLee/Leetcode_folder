# LeetCode 516 .NET 10 Migration Design

## Scope

Migrate only `leetcode_516/` from the legacy .NET Framework 4.8 project format to
an SDK-style `net10.0` console project. Preserve the existing two-dimensional
interval-DP solution and public API. The complete delivery includes a single
commit, pull request, squash merge, Issue #2 update, and post-merge verification.

## Code and API

`Program` remains the executable entry point and exposes:

```csharp
public static int LongestPalindromeSubseq(string s)
```

The method uses `dp[i, j]` for the longest palindromic subsequence in the inclusive
interval `s[i..j]`. Diagonal cells are initialized to 1. Matching endpoints add
two to the inner interval; non-matching endpoints keep the larger result obtained
by dropping either endpoint. The implementation does not write to the console.

## Acceptance Harness

`Main` runs ten deterministic checks: both official examples, the minimum valid
input, odd and complete palindromes, no repeated characters, all equal characters,
non-contiguous and regression cases, and a 1000-character upper-bound spot check.
Every check prints its input, expected value, actual value, and PASS/FAIL. A failed
check sets `Environment.ExitCode` to 1. Success ends with:
`Summary: 10/10 checks passed.`

## Documentation and Tooling

The problem root contains shared editor/Git settings, problem-root VS Code
configuration, `AGENTS.md`, a Traditional Chinese teaching README, the README
template, and these Superpowers design/plan artifacts. The README contains exactly
one `text` fence for the fresh execution transcript.

## TDD and Verification Evidence

The legacy baseline failed with `MSB3644` because this macOS environment lacks
.NET Framework 4.8 reference assemblies. After removing legacy project metadata,
the acceptance-first skeleton produced the intended `CS0103` error for the missing
`EvaluateCase` helper. Implementing the DP and harness then produced a clean .NET
10 build and ten passing checks.
