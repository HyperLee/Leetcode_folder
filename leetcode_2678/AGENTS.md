# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project for LeetCode 2678.
`leetcode_2678/Program.cs` contains the bilingual problem statement, the pure
solution API, and the deterministic acceptance harness. The nested
`leetcode_2678/leetcode_2678.csproj` defines the executable. `.vscode/` contains
the direct build and debug configuration, while `docs/readme-template.md` is
only a template for creating future README files. Treat `.vs/`, `bin/`, and
`obj/` as generated output; do not edit or commit them.

## Build, Run, and Development Commands

Run commands from this folder using the explicit nested project path:

```powershell
dotnet build .\leetcode_2678\leetcode_2678.csproj --nologo
dotnet run --no-build --project .\leetcode_2678\leetcode_2678.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_2678`; its
pre-launch task builds the nested project. Do not use bare `dotnet build` or
`dotnet test`: there is no project at this folder's root and no formal test
project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, and camelCase for locals and parameters.
Preserve the bilingual English/Traditional Chinese XML problem summary above
`Main`.

Keep `public static int CountSeniors(string[] details)` pure. Each valid record
has length 15, and the age is encoded by the digits at indexes 11 and 12.
Count only ages strictly greater than 60; gender and seat number must not affect
the result. The solution returns the count without writing to the console, and
`Main` alone owns acceptance output. Follow the LeetCode input contract instead
of inventing invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the verification mechanism. It covers both
official examples, the 60/61 boundary, ages 00 and 99, all three gender markers,
and a 100-record upper-bound case. Each of the eight checks prints input,
expected value, actual value, and PASS/FAIL. Require a clean build,
`Summary: 8/8 checks passed.`, and exit code 0.

## Commits and Pull Requests

Git metadata lives in the parent repository. From that root, inspect scoped
changes with `git diff --check -- leetcode_2678` and `git status --short`.
Stage only `leetcode_2678/`. A suitable commit subject is
`feat(leetcode-2678): migrate project to .NET 10`. Pull requests should describe
the fixed age indexes, the strict `> 60` boundary, O(n) time and O(1) auxiliary
space, and the verified 8/8 harness result.
