# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the greedy solution,
bilingual XML problem summary, and deterministic acceptance harness in
`leetcode_435/Program.cs`. The nested `leetcode_435/leetcode_435.csproj`
defines the executable. `.vscode/` provides direct build/debug configuration,
and `docs/readme-template.md` is only a template for first-time README files.

## Build, Run, and Development Commands

Run commands from this outer `leetcode_435` folder with the explicit project
path:

```bash
dotnet build leetcode_435/leetcode_435.csproj --nologo
dotnet run --no-build --project leetcode_435/leetcode_435.csproj
```

Build before `--no-build`. In VS Code use `Debug leetcode_435`. Do not use bare
`dotnet build` or `dotnet test`: this folder has neither a root project nor a
formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals and parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML summary
immediately above `Main`.

`public static int EraseOverlapIntervals(int[][] intervals)` follows the valid
LeetCode interval contract and writes nothing to the console. It deliberately
sorts the supplied jagged array in place by end time. Keep the earliest ending
accepted interval; an interval beginning at that end is non-overlapping, while
an earlier start must be removed. `Main` must clone source inputs before every
call so the harness remains deterministic.

## Testing Guidelines

The executable acceptance harness is the verification mechanism. It checks
three official cases, an endpoint extreme, two greedy regressions, and two
100,000-item bounds cases for eight checks total. Require a clean build,
`Summary: 8/8 checks passed.`, and exit code 0. Do not claim test-framework
coverage because no separate test project exists.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From that root, review scoped
changes with `git diff --check -- leetcode_435` and `git status --short`, then
stage only `leetcode_435/`. Use `feat(leetcode-435): migrate project to .NET
10`. Pull requests should describe the earliest-end invariant, the `O(n log n)`
time complexity, and the verified 8/8 harness result.
