# Repository Guidelines

## Project Structure & Module Organization

This directory contains one C# console project for LeetCode 134, Gas Station. The main source file is `leetcode_134/Program.cs`, and the project file is `leetcode_134/leetcode_134.csproj`. Keep problem notes, templates, and contributor-facing documentation under `docs/`. VS Code configuration lives in `.vscode/`. Build outputs under `leetcode_134/bin/` and `leetcode_134/obj/` are generated artifacts and should not be edited or committed.

## Build, Test, and Development Commands

- `dotnet restore leetcode_134/leetcode_134.csproj`: restore NuGet dependencies for the project.
- `dotnet build leetcode_134/leetcode_134.csproj`: compile the project and report C# analyzer warnings.
- `dotnet run --project leetcode_134/leetcode_134.csproj`: run the console app locally.
- `dotnet format leetcode_134/leetcode_134.csproj`: apply formatting rules from `.editorconfig` when the formatter is available.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C#, and 2-space indentation for project, XML, and JSON files. Prefer file-scoped namespaces and top-level statements where they fit the project style. Use PascalCase for types, methods, properties, and public fields; use camelCase for parameters and locals; use `_camelCase` for private instance fields and `s_camelCase` for private static fields. Keep LeetCode solution methods small, deterministic, and named after the expected platform signature, such as `CanCompleteCircuit`.

## Testing Guidelines

No automated test project is currently committed. For behavioral changes, add focused tests before broad refactors. Prefer a sibling test project such as `leetcode_134.Tests/` using xUnit, NUnit, or MSTest, with test files named after the method under test, for example `CanCompleteCircuitTests.cs`. Run tests with `dotnet test` once a solution file or test project exists. Include edge cases from LeetCode examples plus boundary cases such as empty totals, impossible routes, and single-station inputs.

## Commit & Pull Request Guidelines

Recent history uses short Chinese summaries and occasional Conventional Commit prefixes, for example `feat: document and verify dfs bfs solutions for leetcode 129`. Keep commits concise and action-oriented; use prefixes such as `feat:`, `fix:`, or `docs:` when helpful. Pull requests should include a short description, the LeetCode problem number, commands run, and any test coverage added or intentionally omitted.

## Security & Agent-Specific Instructions

Do not commit `.env`, credentials, local secrets, generated binaries, or prompt/configuration content that should remain private. Do not use bulk deletion commands such as `rm -rf`, `rm -r`, `find . -delete`, or `trash -r`; delete only specific single-path files when cleanup is required.
