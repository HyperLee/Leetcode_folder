# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep both pure dynamic-
programming solutions, the bilingual problem XML summary, and the deterministic
acceptance harness in `leetcode_746/Program.cs`. The nested
`leetcode_746/leetcode_746.csproj` defines the executable. From this folder,
run:

```bash
dotnet build leetcode_746/leetcode_746.csproj --nologo
dotnet run --no-build --project leetcode_746/leetcode_746.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_746` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main`.

Keep both `MinCostClimbingStairs` methods pure: they return the minimum cost to
reach the virtual top without writing to the console or changing the input.
For every virtual step `i`, the minimum cost is the smaller cost of arriving
from `i - 1` after paying `cost[i - 1]` or from `i - 2` after paying
`cost[i - 2]`. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print every
case's input, expected value, actual value, PASS/FAIL, and
`Summary: 32/32 checks passed.` on success with exit code 0. This project has
no separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_746` and stage only `leetcode_746/`. Keep commits
and pull requests scoped to this folder.
