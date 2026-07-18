# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The nested executable is
`leetcode_1539/leetcode_1539.csproj`. From this folder, run:

```bash
dotnet build leetcode_1539/leetcode_1539.csproj --nologo
dotnet run --no-build --project leetcode_1539/leetcode_1539.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1539` VS Code configuration.
There is no root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow, explicit
types, PascalCase public members, and camelCase locals. Preserve the bilingual XML
problem summary and the Traditional Chinese XML summaries for both solution methods.

Keep `public static int FindKthPositive(int[] arr, int k)` and
`public static int FindKthPositive2(int[] arr, int k)` console-free and input-preserving.
The first uses sequential enumeration; the second uses the approved half-open lower-bound
binary search. Do not add behavior outside LeetCode's valid-input contract. `Main` alone
owns acceptance output.

## Testing & Git Scope

The executable harness has nine cases and thirty-six checks. It runs both methods against
independent clones and verifies each answer and input array. Success ends with
`Summary: 36/36 checks passed.` and exit code 0. Git metadata is at the parent repository
root; commits and pull requests must remain scoped to `leetcode_1539/`.
