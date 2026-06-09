# Repository Guidelines

## Project Structure & Module Organization

This repository is a minimal .NET console solution for LeetCode problem 167. The main project lives in `leetcode_167/`. Keep executable code in `leetcode_167/Program.cs` until the solution grows enough to justify helper classes in the same folder. Project settings are defined in `leetcode_167/leetcode_167.csproj`, which currently targets `net10.0`. Documentation scaffolding lives in `docs/`, including `docs/readme-template.md`. Generated build output under `leetcode_167/bin/` and `leetcode_167/obj/` should remain uncommitted.

## Build, Test, and Development Commands

- `dotnet build leetcode_167/leetcode_167.csproj` restores dependencies and compiles the project.
- `dotnet run --project leetcode_167/leetcode_167.csproj` runs the console app locally.
- `dotnet test leetcode_167/leetcode_167.csproj` currently verifies restore/build only; there is no dedicated test project yet.

Run `dotnet build` before committing. Use `dotnet run` to check console output whenever problem logic changes.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces for indentation, 4 spaces in C# files, and 2 spaces in XML/JSON files. Prefer file-scoped namespaces, braces on new lines, and explicit types over `var` unless the type is truly obvious. Use PascalCase for types, methods, and properties. Keep namespaces aligned with folder and project names.

## Testing Guidelines

If you add tests, create a sibling test project such as `leetcode_167.Tests/`. Prefer xUnit or MSTest, name files after the target type or problem (`TwoSumIITests.cs`), and use descriptive test names such as `TwoSum_Returns1BasedIndexes`. Cover normal cases, edge indices, duplicate values, and negative numbers. Run `dotnet test` before opening a PR.

## Commit & Pull Request Guidelines

Recent commits use short, single-line subjects, mostly in Traditional Chinese, with occasional Conventional Commit prefixes such as `feat:`. Keep commit messages concise and imperative; include the problem number or scope when possible, for example `feat: add solution for leetcode 167` or `新增 167 題解法`.

PRs should explain the algorithm, note time/space complexity, list the commands you ran, and mention any documentation updates. Screenshots are usually unnecessary unless console output formatting changes.
