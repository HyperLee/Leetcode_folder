# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET 10 console solution for LeetCode 1657. Application code lives in `leetcode_1657/`: `Program.cs` contains the problem statement, entry point, and solution code, while `leetcode_1657.csproj` defines the executable project. Root-level `.vscode/` files provide the F5 build and debug workflow. Treat `bin/` and `obj/` as generated output; do not edit or commit them. There is currently no separate test project or asset directory.

## Build, Test, and Development Commands

Run commands from the repository root:

- `dotnet restore .\leetcode_1657\leetcode_1657.csproj` restores NuGet dependencies.
- `dotnet build .\leetcode_1657\leetcode_1657.csproj` compiles the project and reports warnings or errors.
- `dotnet run --project .\leetcode_1657\leetcode_1657.csproj` runs the current console entry point.
- `dotnet format .\leetcode_1657\leetcode_1657.csproj --verify-no-changes` checks standard .NET formatting without modifying files.

In VS Code, press F5 to use `.vscode/launch.json`; its build task targets the nested project automatically.

## Coding Style & Naming Conventions

Use four-space indentation, braces on new lines, and nullable-safe C#. Name classes and methods with `PascalCase`; use `camelCase` for parameters and local variables. Keep solution methods focused and name alternative approaches clearly, such as `CloseStrings2`. Preserve the bilingual English/Traditional Chinese XML problem summary in `Program.cs`. Prefer descriptive identifiers over single letters except for conventional loop indexes.

## Testing Guidelines

No automated test framework or coverage threshold is configured. Add deterministic sample cases to `Main`, including typical, edge, and false-result cases. Print expected and actual values with an obvious `PASS` or `FAIL` result. Before submitting, run both `dotnet build` and `dotnet run`; new test infrastructure should only be added when explicitly requested.

## Commit & Pull Request Guidelines

Recent history uses short Chinese summaries and Conventional Commit-style subjects. Prefer a concise imperative subject, for example `feat: add CloseStrings implementation` or `docs: clarify frequency comparison`. Keep each commit scoped to one logical change. Pull requests should explain the approach, list verification commands and results, and link the relevant issue when available. Include screenshots only when output or debugger behavior is materially affected.
