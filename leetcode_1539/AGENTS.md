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
problem summary and the Traditional Chinese XML summary for `FindKthPositive`.

Keep `public static int FindKthPositive(int[] arr, int k)` console-free and
input-preserving. It uses the approved sequential enumeration with a forward-only array
index. Do not add behavior outside LeetCode's valid-input contract. `Main` alone owns
acceptance output.

## Testing & Git Scope

The executable harness has nine cases and eighteen checks. Each case verifies the answer
and that the input array is unchanged. Success ends with
`Summary: 18/18 checks passed.` and exit code 0. Git metadata is at the parent repository
root; commits and pull requests must remain scoped to `leetcode_1539/`.
