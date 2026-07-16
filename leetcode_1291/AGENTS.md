# Repository Guidelines

## Project Structure & Commands

This problem folder contains one .NET 10 console project. Keep the pure
sequential-digit generator, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_1291/Program.cs`. The nested
`leetcode_1291/leetcode_1291.csproj` defines the executable. From this problem
root, run:

```bash
dotnet build leetcode_1291/leetcode_1291.csproj --nologo
dotnet run --no-build --project leetcode_1291/leetcode_1291.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1291` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: this folder
has no root project, solution, or formal test project.

## Coding Style & API Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary immediately above
`Main` and the Traditional Chinese public-API summary.

Keep exactly `public static IList<int> SequentialDigits(int low, int high)`.
Generate each number by appending the next larger digit, return only values in
the inclusive range, and sort the result in ascending order. Do not print from
the solution or add behavior for inputs outside the official constraints.
`Main` alone owns all console output and process failure status.

## Verification & Git Scope

The executable harness is the verification mechanism. It must print each
case's name, Input, Expected, Actual, and PASS/FAIL, finish with
`Summary: 8/8 checks passed.`, and exit 0. A failure must set
`Environment.ExitCode = 1`.

Git metadata lives at the parent repository root. Review only this exercise
with `git diff --check -- leetcode_1291`, stage only `leetcode_1291/`, and keep
commits and pull requests scoped to that folder. Before publishing, parse both
`.vscode` JSON files, rebuild, rerun all 8 checks, compare the README transcript
with fresh output, and confirm the three legacy files remain absent.
