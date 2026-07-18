# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The nested executable is
`leetcode_1502/leetcode_1502.csproj`. From this folder, run:

```bash
dotnet build leetcode_1502/leetcode_1502.csproj --nologo
dotnet run --no-build --project leetcode_1502/leetcode_1502.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1502` VS Code configuration.
There is no root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow, explicit
types, PascalCase public members, and camelCase locals. Preserve the bilingual XML
problem summary and Traditional Chinese `CanMakeArithmeticProgression` summary.

Keep `public static bool CanMakeArithmeticProgression(int[] arr)` console-free and
input-preserving. Copy the valid input, sort only the copy, and require every adjacent
difference to equal the first difference. Do not add behavior outside LeetCode's
valid-input contract. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness has ten checks. Each case copies its input before the API call
and passes only when both the boolean answer and complete input array match expectations.
Success ends with `Summary: 10/10 checks passed.` and exit code 0. Git metadata is at
the parent repository root; commits and pull requests must remain scoped to
`leetcode_1502/`.
