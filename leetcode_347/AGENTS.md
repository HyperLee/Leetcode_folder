# Repository Guidelines

## Project Structure & Module Organization

This repository folder contains one .NET 10 console project. Keep problem-specific
code in `leetcode_347/Program.cs`; its XML documentation preserves the LeetCode
347 statement in English and Traditional Chinese. Add solution helpers and the
runnable sample harness there unless the project is deliberately expanded.
`leetcode_347/leetcode_347.csproj` defines the executable. `.vscode/` supplies
the direct build and debug configuration, while `docs/readme-template.md` is
only a template for an initial README.

## Build, Run, and Development Commands

Run commands from this repository folder, using the explicit nested project path:

```bash
dotnet build leetcode_347/leetcode_347.csproj --nologo
dotnet run --no-build --project leetcode_347/leetcode_347.csproj
```

The first command restores and compiles; the second runs the current sample after
a successful build. In VS Code, use the `Debug leetcode_347` launch configuration.
Do not use bare `dotnet build` or `dotnet test` here: this folder has no solution
or project file at its root, so MSBuild reports `MSB1003`.

## Coding Style & Naming Conventions

Follow the root `.editorconfig`: use four spaces in C#, braces for control-flow
blocks, explicit types rather than `var`, and file-scoped namespaces where
appropriate. Use PascalCase for types, methods, and properties; camelCase for
parameters and locals; and `_camelCase` for private instance fields. Keep the
problem description XML comment intact when modifying `Program.cs`, and name
solution methods after their behavior (for example, `TopKFrequent`).

## Testing Guidelines

There is currently no test project or test framework. Validate changes with a
clean build and representative sample cases in `Main`, checking both values and
edge cases. Do not describe `dotnet test` as a test suite or claim coverage. If
you add tests, introduce a clearly named test project and document its framework
and exact command in the same change.

## Commits and Pull Requests

The parent repository history uses concise imperative messages, with both
conventional-style (`feat(leetcode-287): ...`) and plain English/Chinese entries.
Prefer a scoped imperative message such as `feat(leetcode-347): add bucket-count
solution`. Keep commits focused. PRs should state the approach and complexity,
show the sample output used for validation, and note documentation or debug-setup
changes; link an issue when one exists.
