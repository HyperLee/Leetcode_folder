# Repository Guidelines

## Project Structure

This folder contains one SDK-style .NET 10 console project. The nested
`leetcode_590/leetcode_590.csproj` defines the executable, and
`leetcode_590/Program.cs` contains the pure `Postorder` API plus the deterministic
acceptance harness. `.vscode/` assumes this problem folder is opened directly.

There is no formal test project. The console harness is the verification entry
point, and all console output must remain in `Main`.

## Build and Run

Run from this problem folder:

```bash
dotnet build leetcode_590/leetcode_590.csproj --nologo
dotnet run --no-build --project leetcode_590/leetcode_590.csproj
```

The expected result is `Summary: 8/8 checks passed.` with exit code 0. Do not
use a bare `dotnet build` or `dotnet test` from this folder.

## Coding Contract

Keep `public static IList<int> Postorder(Node? root)` pure. A null root returns
an empty sequence; a non-null node is appended after every child subtree, and
children are visited in their original order. `PostorderVisit` must not write to
the console. Follow `.editorconfig`, use four-space C# indentation, preserve the
bilingual XML problem documentation, and keep the postorder invariant comment.

The acceptance harness covers both official examples, null and leaf inputs,
child ordering, repeated calls, the 1000-node height boundary, and traversal
invariants.

## Git Workflow

Git metadata lives at the parent repository root. Review scoped changes with
`git diff --check -- leetcode_590` and stage only the `leetcode_590/` paths.
Use the scoped commit subject `feat(leetcode-590): migrate project to .NET 10`.
Do not modify another LeetCode problem as part of this migration.
