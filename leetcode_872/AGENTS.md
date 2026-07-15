# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep the pure `LeafSimilar`
solution, bilingual problem XML summary, fixture helpers, and deterministic
acceptance harness in `leetcode_872/Program.cs`. The nested
`leetcode_872/leetcode_872.csproj` defines the executable. From this folder, run:

```bash
dotnet build leetcode_872/leetcode_872.csproj --nologo
dotnet run --no-build --project leetcode_872/leetcode_872.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_872` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no root
project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main` and the
Traditional Chinese XML summaries on the two solution methods.

Keep `public static bool LeafSimilar(TreeNode root1, TreeNode root2)` pure. It
must collect separate left-to-right leaf sequences and compare them without
writing to the console or mutating either tree. `CollectLeaves` must recognize a
leaf only when both children are null and recurse left before right. `Main` alone
owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print each case's
input, expected value, actual value, PASS/FAIL, and
`Summary: 9/9 checks passed.` on success with exit code 0. This project has no
separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_872` and stage only `leetcode_872/`. Keep commits
and pull requests scoped to this folder.
