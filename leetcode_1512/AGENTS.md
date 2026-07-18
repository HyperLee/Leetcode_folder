# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The nested executable is
`leetcode_1512/leetcode_1512.csproj`. From this folder, run:

```bash
dotnet build leetcode_1512/leetcode_1512.csproj --nologo
dotnet run --no-build --project leetcode_1512/leetcode_1512.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1512` VS Code configuration.
There is no root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow, explicit
types, PascalCase public members, and camelCase locals. Preserve the bilingual XML
problem summary and the Traditional Chinese XML summaries for both public methods.

Keep `public static int NumIdenticalPairs(int[] nums)` and
`public static int NumIdenticalPairs2(int[] nums)` console-free and input-preserving.
The first enumerates every `i < j` pair; the second records prior frequencies in a
dictionary. Do not add behavior outside LeetCode's valid-input contract. `Main` alone
owns acceptance output.

## Testing & Git Scope

The executable harness has nine checks. Each case creates independent inputs for both
APIs and passes only when both answers are expected and both arrays are unchanged.
Success ends with `Summary: 9/9 checks passed.` and exit code 0. Git metadata is at the
parent repository root; commits and pull requests must remain scoped to `leetcode_1512/`.
