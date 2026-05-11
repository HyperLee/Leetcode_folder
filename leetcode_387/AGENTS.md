# Repository Guidelines

## Project Structure & Module Organization

This repository contains a small C# console project for LeetCode problem 387.

- `leetcode_387/Program.cs` contains the entry point and problem notes.
- `leetcode_387/leetcode_387.csproj` defines the `net10.0` console application with nullable reference types and implicit usings enabled.
- `leetcode_387.slnx` exists at the root, but currently does not list projects. Prefer project-level `dotnet` commands unless the solution file is updated.
- `.editorconfig` defines formatting and C# style rules.

No dedicated test project or asset directory is present yet.

## Build, Test, and Development Commands

- `dotnet build leetcode_387/leetcode_387.csproj` builds the console app and validates compilation.
- `dotnet run --project leetcode_387/leetcode_387.csproj` runs the current implementation.
- `dotnet format leetcode_387/leetcode_387.csproj` applies formatting and analyzer fixes supported by the installed .NET SDK.
- `dotnet test` should be used after a test project is added to the repository.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces for indentation; C# files use 4 spaces. Prefer file-scoped namespaces, explicit types instead of `var`, nullable-aware code, and simple expression-bodied members only where they improve readability.

Use PascalCase for classes, methods, and public members. Use camelCase for local variables and parameters. Keep LeetCode solution methods focused and name them by approach when multiple implementations exist, for example `FirstUniqChar` and `FirstUniqCharSinglePointer`.

## Testing Guidelines

There is no test framework configured. When adding tests, create a sibling project such as `leetcode_387.Tests` and use a standard .NET framework such as xUnit, NUnit, or MSTest. Name test files after the unit under test, for example `ProgramTests.cs` or `SolutionTests.cs`.

Cover normal cases, repeated-character cases, empty strings, and no-unique-character inputs. Run tests with `dotnet test` before opening a pull request.

## Commit & Pull Request Guidelines

Recent history uses short action-oriented Chinese messages such as `新增...`, `調整: ...`, and `刪除`. Keep commits concise and focused on one change.

Pull requests should include a short summary, the LeetCode problem or issue being addressed, implementation notes for each approach added, and the commands run for verification. Include screenshots only when console output or documentation rendering is relevant.

