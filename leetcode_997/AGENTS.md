# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 10 console application for LeetCode 997, **Find the Town Judge**. The workspace root holds contributor tooling, while application code is nested under `leetcode_997/`:

- `leetcode_997/Program.cs` contains the entry point, bilingual problem notes, and solution methods.
- `leetcode_997/leetcode_997.csproj` defines the executable project and enables nullable reference types and implicit usings.
- `.vscode/` contains the build task and `coreclr` launch profile.
- `bin/` and `obj/` are generated outputs; do not edit or commit them.

There is currently no test project or separate assets directory.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore .\leetcode_997\leetcode_997.csproj
dotnet build .\leetcode_997\leetcode_997.csproj
dotnet run --project .\leetcode_997\leetcode_997.csproj
dotnet format .\leetcode_997\leetcode_997.csproj --verify-no-changes
```

`restore` resolves dependencies, `build` compiles the solution, `run` executes the console demo, and `format --verify-no-changes` checks formatting. In VS Code, press F5 to use `.vscode/launch.json`; its pre-launch task builds the project first.

## Coding Style & Naming Conventions

Use four-space indentation and standard C# formatting. Use PascalCase for types and methods, camelCase for parameters and local variables, and descriptive names for graph concepts. Keep nullable warnings enabled. Preserve the English and Traditional Chinese problem summary in XML documentation, and document non-obvious algorithm choices without narrating every line.

## Testing Guidelines

No automated test framework is configured. Until one is added, exercise representative cases from `Main`, including a valid judge, no judge, and the single-person case, then run `dotnet build` and `dotnet run`. If adding tests, create a sibling `leetcode_997.Tests` project, use xUnit, and name tests by behavior, such as `FindJudge_ReturnsMinusOne_WhenNoJudgeExists`.

## Commit & Pull Request Guidelines

Recent history favors short, imperative summaries, sometimes with Conventional Commit prefixes such as `docs:` and `chore:`. Prefer messages like `feat: add town judge examples` or `test: cover missing judge case`. Keep each commit focused. Pull requests should explain the approach, list validation commands and results, link the relevant issue when available, and include console output when runtime behavior changes. Screenshots are only needed for changes to developer tooling or visible UI.
