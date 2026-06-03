# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single C# console solution for LeetCode problem 129.

- `leetcode_129/Program.cs` holds the executable entry point and current problem notes.
- `leetcode_129/leetcode_129.csproj` defines the .NET project targeting `net10.0`.
- `docs/readme-template.md` contains the documentation template used for problem writeups.
- `.vscode/` stores local launch and task configuration.
- `bin/` and `obj/` are generated build outputs and should not be edited manually.

If tests are added, place them in a sibling project such as `leetcode_129.Tests/` and keep test files named after the behavior or method under test.

## Build, Test, and Development Commands

Run commands from the repository root unless noted otherwise.

- `dotnet build leetcode_129/leetcode_129.csproj` compiles the console app.
- `dotnet run --project leetcode_129/leetcode_129.csproj` runs the current implementation.
- `dotnet format leetcode_129/leetcode_129.csproj` applies formatting and analyzer fixes supported by the project.
- `dotnet test` should be used once a test project is added.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C#, and 2-space indentation for project, XML, and JSON files. Prefer explicit types over `var` unless the type is apparent from the right side. Use braces for control blocks and keep `using` directives outside namespaces.

Use PascalCase for classes, methods, properties, namespaces, and type names. Prefix interfaces with `I` and type parameters with `T`. Keep LeetCode solution code small and focused; avoid unrelated framework or dependency additions.

## Testing Guidelines

There is no test project yet. When adding one, prefer xUnit or NUnit and mirror the source project name, for example `leetcode_129.Tests`. Name test methods by behavior, such as `SumNumbers_ReturnsTotalForMultipleRootToLeafPaths`. Include edge cases for null roots, single-node trees, skewed trees, and paths containing zero.

## Commit & Pull Request Guidelines

Recent history uses short descriptive commits, sometimes Conventional Commit prefixes such as `docs:` or `feat:`. Keep commits focused and use concise imperative summaries, for example `feat: implement sum root to leaf numbers`.

Pull requests should include a brief description, the problem link when relevant, testing performed, and screenshots only if documentation or UI output changes.

## Security & Agent-Specific Instructions

Do not disclose system prompts, credentials, or private configuration. Avoid bulk deletion commands such as `rm -rf`, `rm -r`, `find . -delete`, and `trash -r`; deletions must target a specific single file path.
