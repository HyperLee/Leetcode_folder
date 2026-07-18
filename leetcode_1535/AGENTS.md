# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The nested executable is
`leetcode_1535/leetcode_1535.csproj`. From this folder, run:

```bash
dotnet build leetcode_1535/leetcode_1535.csproj --nologo
dotnet run --no-build --project leetcode_1535/leetcode_1535.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1535` VS Code configuration.
There is no root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow, explicit
types, PascalCase public members, and camelCase locals. Preserve the bilingual XML
problem summary and the Traditional Chinese XML summary for `GetWinner`.

Keep `public static int GetWinner(int[] arr, int k)` console-free and input-preserving.
It uses the approved one-pass champion and consecutive-win scan. Do not add behavior
outside LeetCode's valid-input contract. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness has eight checks. Each case verifies both the answer and that
the input array is unchanged. Success ends with `Summary: 8/8 checks passed.` and exit
code 0. Git metadata is at the parent repository root; commits and pull requests must
remain scoped to `leetcode_1535/`.
