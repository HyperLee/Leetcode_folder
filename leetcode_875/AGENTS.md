# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep the pure `MinEatingSpeed`
solution, bilingual problem XML summary, private hour-calculation helper, and
deterministic acceptance harness in `leetcode_875/Program.cs`. The nested
`leetcode_875/leetcode_875.csproj` defines the executable. From this folder, run:

```bash
dotnet build leetcode_875/leetcode_875.csproj --nologo
dotnet run --no-build --project leetcode_875/leetcode_875.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_875` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no root
project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main` and the
Traditional Chinese XML summaries on both major non-Main functions.

Keep `public static int MinEatingSpeed(int[] piles, int h)` pure and console-free.
It must perform lower-bound binary search over `[1, piles.Max()]` and return the
smallest speed whose required hours are at most `h`. `CalculateRequiredHours`
must use `((long)pile + speed - 1) / speed` with a `long` accumulator. Do not
mutate `piles` or add invalid-input behavior outside LeetCode's valid contract.
`Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print each case's
name, input, expected value, actual value, PASS/FAIL, and
`Summary: 9/9 checks passed.` on success with exit code 0. This project has no
separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_875` and stage only `leetcode_875/`. Keep commits
and pull requests scoped to this folder.
