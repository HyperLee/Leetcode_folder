# Repository Guidelines

## Project Structure

This problem root contains one nested .NET 10 console project:
`leetcode_530/leetcode_530.csproj`. The implementation, `TreeNode` model,
bilingual problem XML summary, and deterministic acceptance harness live in
`leetcode_530/Program.cs`. `.vscode/` is configured for opening this problem
folder directly. `docs/readme-template.md` is a reusable README starting point,
not a replacement for the problem-specific README.

## Build and Run

From this problem root, run:

```bash
dotnet build leetcode_530/leetcode_530.csproj --nologo
dotnet run --no-build --project leetcode_530/leetcode_530.csproj
```

From the parent repository root, use the explicit nested project path:

```bash
dotnet build leetcode_530/leetcode_530/leetcode_530.csproj --nologo
dotnet run --no-build --project leetcode_530/leetcode_530/leetcode_530.csproj
```

There is no formal test project. The executable acceptance harness is the test
mechanism and must finish with `Summary: 8/8 checks passed.` and exit code 0.

## Coding and Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit local types, PascalCase for public methods, and camelCase for locals
and parameters. Keep LeetCode-compatible `TreeNode.val`, `TreeNode.left`, and
`TreeNode.right` fields.

`public static int GetMinimumDifference(TreeNode root)` is pure: it returns the
minimum difference and never writes to the console. The implementation uses
recursive in-order traversal. Because a BST's in-order sequence is increasing,
only the current value and the previous visited value need to be compared.
Per-call state must remain local; do not restore static `pre` or `ans` fields.
`Main` owns all acceptance output.

## Acceptance Harness

The harness covers both official examples, the minimum valid input, zero and
100,000 boundaries, a non-parent/child in-order neighbor case, an irregular
multi-level BST, and a 10,000-node balanced BST spot check. Every case prints
its name, Expected, Actual, and PASS/FAIL. Any failure sets
`Environment.ExitCode = 1`.

## Git and Review

Git metadata lives at the parent repository root. Review scoped changes with
`git diff --check -- leetcode_530` and stage only `leetcode_530/`. There is no
formal test project to run with `dotnet test`. Commits and pull requests must
remain limited to this problem folder. The migration commit subject is
`feat(leetcode-530): migrate project to .NET 10`.
