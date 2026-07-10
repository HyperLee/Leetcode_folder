# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the three solutions,
bilingual problem XML summary, and deterministic acceptance harness in
`leetcode_389/Program.cs`. The nested `leetcode_389/leetcode_389.csproj` defines
the executable. `.vscode/` contains direct build/debug configuration, while
`docs/readme-template.md` is only a template for initial README creation.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_389/leetcode_389.csproj --nologo
dotnet run --no-build --project leetcode_389/leetcode_389.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_389`. Do not use
bare `dotnet build` or `dotnet test`: this folder has no root solution/project
and no formal test project, so those commands do not validate this exercise.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary above `Main`.

Keep all three public comparison solutions distinct:

- `FindTheDifference` uses sorting.
- `FindTheDifference2` removes matches from a mutable list.
- `FindTheDifference3` uses XOR cancellation.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. Add
representative cases to `Main` for behavior changes, run every case against all
three methods, and ensure each check prints expected/actual values and
PASS/FAIL. Require a clean build, all checks passing, and exit code 0. Do not
claim test-framework coverage.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From that root, review scoped
changes with `git diff --check -- leetcode_389` and `git status --short`, then
stage only `leetcode_389/`. Use a concise scoped commit subject such as
`feat(leetcode-389): migrate project to .NET 10`. Pull requests should explain
the repaired duplicate-safe list removal, compare all three complexities, and
include the verified 18/18 harness result.
