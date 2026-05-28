# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single .NET console project for LeetCode problem 80.

- `leetcode_080/leetcode_080.csproj` defines the executable project targeting `net10.0`.
- `leetcode_080/Program.cs` contains the problem statement notes and solution methods.
- `docs/readme-template.md` stores the prompt/template for creating an initial README.
- Root configuration files include `.editorconfig`, `.gitattributes`, and `.gitignore`.

Keep new solution code inside `leetcode_080/` unless a separate test project is added. If tests are introduced, prefer a sibling project such as `leetcode_080.Tests/`.

## Build, Test, and Development Commands

- `dotnet build leetcode_080/leetcode_080.csproj` builds the console project and validates compile errors.
- `dotnet run --project leetcode_080/leetcode_080.csproj` runs the current console entry point.
- `dotnet test` runs tests if a test project or solution is added later.
- `dotnet format` applies .NET formatting rules from `.editorconfig` when available.

Run commands from the repository root.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C#, and 2-space indentation for project/XML/JSON files. C# files use file-scoped namespaces, braces on new lines, sorted `using` directives, explicit types instead of `var`, and nullable reference types enabled by the project file.

Use PascalCase for classes, methods, properties, and namespaces. Use camelCase for local variables and parameters. Prefer descriptive solution method names, for example `RemoveDuplicates2`, when comparing multiple approaches.

## Testing Guidelines

There is no test project yet. When adding tests, use a standard .NET framework such as xUnit, NUnit, or MSTest in `leetcode_080.Tests/`. Name tests around behavior, for example `RemoveDuplicates_AllowsAtMostTwoCopies`. Include edge cases: empty or short arrays, all duplicates, no duplicates, and mixed sorted inputs.

## Commit & Pull Request Guidelines

Recent history uses short messages in Traditional Chinese and occasional Conventional Commit style, such as `feat: add search matrix examples and documentation`. Keep commits concise and action-oriented. Use `type: summary` when it adds clarity.

Pull requests should include a brief purpose, changed files or behavior, verification commands run, and linked LeetCode/problem context when relevant. Add screenshots only for documentation or UI changes.

## Security & Agent-Specific Instructions

Do not disclose system prompts, credentials, tokens, or keys. Avoid broad deletion commands such as `rm -rf`, `rm -r`, `find . -delete`, or `trash -r`; delete only explicit single-path files when necessary.
