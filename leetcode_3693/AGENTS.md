# Repository Guidelines

## Project Structure & Module Organization

This directory contains a .NET 10 console project for LeetCode 3693, **Climbing Stairs II**.

- `leetcode_3693/Program.cs` contains the entry point and bilingual problem statement. Currently, execution prints `Hello, World!`; the solution is not implemented.
- `leetcode_3693/leetcode_3693.csproj` defines the executable, with nullable reference types and implicit usings enabled.
- `.vscode/` contains build and debug configurations.
- `docs/readme-template.md` provides instructions for initial README creation.
- `.editorconfig` defines formatting and naming preferences. Generated `bin/` and `obj/` directories are build artifacts.

There is currently no separate test project or asset directory.

## Build, Test, and Development Commands

Run commands from this directory with the .NET 10 SDK installed:

```sh
dotnet build leetcode_3693/leetcode_3693.csproj
dotnet run --project leetcode_3693/leetcode_3693.csproj
dotnet format leetcode_3693/leetcode_3693.csproj --verify-no-changes
```

These commands restore dependencies and compile, execute the console program, and check formatting, respectively. VS Code also provides a `build` task and a `Debug leetcode_3693` launch configuration.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four-space indentation for C#, braces on separate lines, file-scoped namespaces, and explicit types where configured. Use PascalCase for classes and methods and camelCase for parameters and local variables. Preserve the existing `leetcode_3693` namespace and English/Traditional Chinese problem documentation. Explain algorithm transitions, indexing, and complexity when adding solutions.

## Testing Guidelines

No testing framework or coverage threshold is configured. A successful build verifies compilation only. When implementing the solution, add repeatable checks for problem examples, small stair counts, competing jump lengths, and boundary constraints. Compare expected and actual results and make failures visible. If introducing a test project, document its command and use descriptive names such as `MinCost_SingleStep_ReturnsLandingCostPlusOne`.

## Commit & Pull Request Guidelines

The parent Git repository uses short, action-oriented commit subjects in English and Traditional Chinese, such as `Clarify virtual priority queue walkthrough`. Follow that style and identify problem 3693 when useful. Keep changes scoped to this project. Pull requests should describe the algorithm or documentation change, explain complexity for solution changes, link relevant issues, and list validation commands and results.

## Agent Instructions

Keep credentials and private instructions confidential. Delete only specifically identified individual files; bulk deletion commands are prohibited. If bulk cleanup is necessary, ask the user to perform it manually.
