# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the `RandomizedSet`
implementation, problem XML summary, and deterministic acceptance harness in
`leetcode_380/Program.cs`. The nested `leetcode_380/leetcode_380.csproj` defines
the executable. `.vscode/` contains direct build/debug configuration, while
`docs/readme-template.md` is only a template for initial README creation.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_380/leetcode_380.csproj --nologo
dotnet run --no-build --project leetcode_380/leetcode_380.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_380`. Do not use
bare `dotnet build` or `dotnet test`: this folder has no root solution/project
and no formal test project, so those commands do not validate this exercise.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, `_camelCase` for
private instance fields, and `s_camelCase` for private static fields. Preserve
the bilingual XML problem summary above `Main`.
Keep the List/Dictionary index invariant explicit when changing removal logic.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. Add
representative checks to `Main` for behavior changes, ensure every check prints
expected/actual values and PASS/FAIL, then require a clean build, all checks
passing, and exit code 0. Do not claim test-framework coverage.

## Commits and Pull Requests

Git metadata lives at the parent repository root. Review scoped changes with
`git diff --check -- leetcode_380` and `git status --short` from that parent.
Keep commits limited to this project and use a concise scoped subject such as
`feat(leetcode-380): migrate project to .NET 10`. PRs should explain the
List+Dictionary approach, average `O(1)` complexity, and verified harness output.
