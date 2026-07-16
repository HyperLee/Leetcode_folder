# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep both pure dynamic
programming solutions, the bilingual problem XML summary, and the deterministic
acceptance harness in `leetcode_1218/Program.cs`. The nested
`leetcode_1218/leetcode_1218.csproj` defines the executable. From this folder,
run:

```bash
dotnet build leetcode_1218/leetcode_1218.csproj --nologo
dotnet run --no-build --project leetcode_1218/leetcode_1218.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1218` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main`.

Keep `LongestSubsequence(int[] arr, int difference)` and
`LongestSubsequence2(int[] arr, int difference)` pure: neither method writes to
the console or modifies `arr`. Both process elements in input order and maintain
the invariant that the state for value `x` is the best valid subsequence length
ending at `x`. The first API uses dictionary state; the second uses the problem's
bounded `[-10000, 10000]` value range. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print every
case's input, each method's expected and actual value, PASS/FAIL, and
`Summary: 16/16 checks passed.` on success with exit code 0. This project has no
separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_1218` and stage only `leetcode_1218/` during
publishing. Keep commits and pull requests scoped to this folder.
