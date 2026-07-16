# Repository Guidelines

## Project Structure & Commands

This problem folder contains one .NET 10 console project. Keep the top-right
staircase solution, bilingual problem XML summary, and deterministic acceptance
harness in `leetcode_1351/Program.cs`. The nested
`leetcode_1351/leetcode_1351.csproj` defines the executable. From this problem
root, run:

```bash
dotnet build leetcode_1351/leetcode_1351.csproj --nologo
dotnet run --no-build --project leetcode_1351/leetcode_1351.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1351` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: this folder has
no root project, solution, or formal test project.

## Coding Style & API Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary immediately above `Main`
and the Traditional Chinese summary on the public API.

Keep exactly `public static int CountNegatives(int[][] grid)`. The method must
remain console-free and must not mutate `grid`. Start at the top-right cell. A
negative value proves every value below it in that column is negative, so add
`grid.Length - row` and move left; a non-negative value proves every value to
its left in that row is non-negative, so move down. Do not add behavior outside
the official valid-input contract. `Main` alone owns console output and process
failure status.

## Verification & Git Scope

The executable harness is the verification mechanism. It prints eight cases,
each with Case, Input, Expected, Actual, and PASS/FAIL. Success must end with
`Summary: 8/8 checks passed.` and exit code 0; any failure must set
`Environment.ExitCode = 1`.

Git metadata lives at the parent repository root. Review only this exercise
with `git diff --check -- leetcode_1351`, stage only `leetcode_1351/`, and keep
commits and pull requests scoped to that folder. Before publishing, parse both
`.vscode` JSON files, rebuild, rerun all eight checks, compare the README
transcript with fresh output, and confirm the three legacy files remain absent.
