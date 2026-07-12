# Repository Guidelines

## Project Structure

This folder contains one SDK-style .NET 10 console project. The nested
`leetcode_589/leetcode_589.csproj` defines the executable, and
`leetcode_589/Program.cs` contains the pure `Preorder` API plus the deterministic
acceptance harness. `.vscode/` assumes this problem folder is opened directly.

There is no formal test project. The console harness is the verification entry
point and all console output must remain in `Main`.

## Build and Run

Run from this problem folder:

```bash
dotnet build leetcode_589/leetcode_589.csproj --nologo
dotnet run --no-build --project leetcode_589/leetcode_589.csproj
```

The expected result is `Summary: 8/8 checks passed.` with exit code 0. Do not
use a bare `dotnet build` or `dotnet test` from this folder.

## Coding Contract

Keep `public static IList<int> Preorder(Node? root)` pure. A null root returns
an empty sequence; a non-null root is visited before each child, and children
are visited in their original order. `PreorderVisit` must not write to the
console. Follow `.editorconfig`, use four-space C# indentation, and preserve
the bilingual XML problem documentation and high-signal algorithm comments.

The acceptance harness covers both official examples, null and leaf inputs,
child ordering, repeated calls, a 1000-node chain, and traversal invariants.

## Git Workflow

Git metadata lives at the parent repository root. Review scoped changes with
`git diff --check -- leetcode_589` and stage only the `leetcode_589/` paths.
Use the scoped commit subject `feat(leetcode-589): migrate project to .NET 10`.
Do not modify another LeetCode problem as part of this migration.
