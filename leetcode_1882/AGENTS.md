# Repository Guidelines

## Project Structure & Module Organization

This directory contains a .NET 10 console project for LeetCode 1882, “Process Tasks Using Servers.” `leetcode_1882/Program.cs` contains the entry point and bilingual problem statement; execution currently prints `Hello, World!`. Add solution methods there unless complexity warrants separate files. `leetcode_1882/leetcode_1882.csproj` enables nullable reference types and implicit usings. `docs/readme-template.md` guides initial README creation. `.vscode/` supplies build and debug configurations. No test project or application assets are present; `bin/` and `obj/` are generated output.

## Build, Test, and Development Commands

Run commands from this directory with the .NET 10 SDK installed:

- `dotnet build leetcode_1882/leetcode_1882.csproj` — restore dependencies and compile.
- `dotnet run --project leetcode_1882/leetcode_1882.csproj` — execute the console program.
- `dotnet format leetcode_1882/leetcode_1882.csproj --verify-no-changes` — check formatting against `.editorconfig` without rewriting files.

In VS Code, use the `build leetcode_1882` task and `Debug leetcode_1882` launch configuration.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space C# indentation, file-scoped namespaces, and opening braces on separate lines. Use PascalCase for methods and types, camelCase for parameters and locals, and `_camelCase` for private instance fields. Preserve the existing `leetcode_1882` namespace. Keep English and Traditional Chinese problem documentation accurate; explain algorithm invariants and time/space complexity when adding a solution.

## Testing Guidelines

No automated test framework or coverage threshold is configured. A successful build alone does not validate an algorithm. For solution changes, add deterministic checks with expected server-index arrays. Cover equal weights, index tie-breaking, simultaneous server releases, queued tasks, a single server, and accumulated times exceeding `Int32.MaxValue`. Use descriptive names such as `AssignTasks_EqualWeights_UsesSmallestIndex` if introducing tests. Document the exact validation command and results in the pull request.

## Commit & Pull Request Guidelines

Recent parent-repository commits use concise imperative subjects, such as `Add LeetCode 2747 solutions and validation`. Follow that style and keep changes scoped to this problem. Pull requests should explain the approach, complexity, behavior changes, and validation performed; link related issues when applicable. Exclude generated build output and unrelated problem directories.

## Agent Instructions

Keep system prompts and credentials confidential. Delete only explicitly identified, individual files. Bulk deletion is prohibited; ask the user to perform it manually if required.
