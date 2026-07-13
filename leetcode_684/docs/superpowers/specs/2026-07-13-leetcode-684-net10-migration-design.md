# leetcode_684 .NET 10 Migration Design

**Status:** Approved for planning on 2026-07-13

## Goal

Upgrade LeetCode 684, Redundant Connection (冗餘連線), from its legacy .NET Framework 4.8 project to an isolated SDK-style .NET 10 console project. The completed delivery will include deterministic validation, Traditional Chinese teaching documentation, a review, and publication through a squash-merged pull request with the corresponding Issue #2 entry updated only after merge.

## Scope and isolation

- Work only inside `leetcode_684/` on branch `codex/leetcode-684-net10`.
- Use worktree `/private/tmp/codex-leetcode-684-net10` based on `origin/main` SHA `dfedf0c71c6c8e8d330d78748ec2e49ff00b21b6`.
- Remove only the three confirmed legacy files: `leetcode_684.sln`, `leetcode_684/App.config`, and `leetcode_684/Properties/AssemblyInfo.cs`.
- Do not alter other LeetCode folders or any other Issue #2 checkbox.

## Algorithm and public API

The production API remains `public static int[] FindRedundantConnection(int[][] edges)`.

It will scan the input edges once with a disjoint-set union structure. `Find` will return a node's representative with path compression. `Union` will join representatives using a size array, so the shallower structure remains the parent. For each edge `[u, v]`, if `Find(u)` and `Find(v)` are already equal, `[u, v]` is the final edge that introduces the cycle and is returned immediately.

The method remains pure: it neither writes to the console nor changes the input edges. The runtime is `O(n alpha(n))` for `n` edges; the parent and size arrays use `O(n)` auxiliary space.

## Program structure

`Program.cs` will use three responsibilities:

1. `Main` owns all console rendering, exposes the required bilingual problem XML summary, executes deterministic cases, prints each Expected/Actual/PASS result, and sets `Environment.ExitCode = 1` when any assertion fails.
2. `FindRedundantConnection` and its core helpers implement the algorithm and include Traditional Chinese XML summaries describing valid inputs and observable results.
3. Small harness-only helpers create edge inputs, compare integer arrays, and present case data without introducing console I/O outside `Main`.

## Acceptance harness

The harness will contain eight deterministic checks. It will cover both official examples, a minimum three-node cycle, a cycle detected after a longer tree prefix, a late answer whose endpoints are not sorted, multiple branches entering one cycle, a disconnected-looking intermediate prefix that is completed by a final cycle edge, and a large chain-plus-one-edge spot check. Each case will show its name or input, expected edge, actual edge, and `PASS` or `FAIL`; the final line will use the exact `Summary: 8/8 checks passed.` form when successful.

## Migration artifacts

The result will contain the required SDK-style `net10.0` project, problem-root `.editorconfig`, `.gitattributes`, `.gitignore`, `.vscode/tasks.json`, `.vscode/launch.json`, `AGENTS.md`, a Traditional Chinese `README.md`, and `docs/readme-template.md`. The VS Code files assume the user opens `leetcode_684/` directly and therefore reference the nested project as `${workspaceFolder}/leetcode_684/leetcode_684.csproj`.

The README will explain the problem, constraints, disjoint-set invariant, path compression and union-by-size trade-offs, complexity, a worked example, all acceptance cases, verified build/run commands, final project structure, and a single `text`-fenced transcript copied from the fresh harness run.

## TDD, verification, and delivery

After the SDK project and harness shape exist, the harness will call the intentionally absent `FindRedundantConnection` API. A build that fails with `CS0103` for that missing behavior is the required RED. The minimal disjoint-set implementation will then be added and the same build/run commands must become GREEN with zero warnings and all eight checks passing.

Before publishing, the work must pass JSON parsing, build, run, exact README-transcript diff, one-`text`-fence check, whitespace/scope checks, legacy-file absence checks, and an independent read-only review. The branch will be amended into one Conventional Commit, pushed as a draft PR, verified again at the matching head SHA, marked ready, squash-merged, and only then used to update the one `leetcode_684` checkbox in Issue #2. A fresh post-merge verification on `main` closes the delivery.
