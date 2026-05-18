# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single .NET console solution for LeetCode problem 58.

- `leetcode_058/Program.cs` contains the current runnable implementation and problem notes.
- `leetcode_058/leetcode_058.csproj` defines the `net10.0` console project.
- `docs/readme-template.md` is a prompt/template for creating an initial README.
- `.vscode/` contains local build and debug settings.

Keep new source files inside `leetcode_058/`. If tests are added, place them in a sibling project such as `leetcode_058.Tests/`.

## Build, Test, and Development Commands

- `dotnet build leetcode_058/leetcode_058.csproj` builds the project and reports compiler or analyzer issues.
- `dotnet run --project leetcode_058/leetcode_058.csproj` runs the console app locally.
- `dotnet format leetcode_058/leetcode_058.csproj` applies formatting rules from `.editorconfig` when the .NET format tool is available.
- `dotnet test` should be used after a test project is introduced.

The VS Code task named `build` runs the same project build command.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, 4-space indentation for C#, and 2-space indentation for XML, JSON, and project files. Prefer file-scoped namespaces, explicit built-in types over `var`, braces for code blocks, sorted `System` usings, and nullable-aware code. Use PascalCase for classes and public members, camelCase for locals and parameters, and meaningful names that match the LeetCode problem domain.

## Testing Guidelines

There is no dedicated test project yet. When adding one, use a standard .NET test framework such as xUnit, NUnit, or MSTest, and name it `leetcode_058.Tests`. Test method names should describe the scenario and expected result, for example `LengthOfLastWord_ReturnsZero_ForEmptyInput`. Cover normal cases, trailing spaces, single-word input, and edge cases before changing algorithm behavior.

## Commit & Pull Request Guidelines

Git history currently uses short, direct commit messages, including Traditional Chinese summaries and occasional conventional prefixes such as `docs:`. Keep messages concise and action-oriented, for example `docs: add contributor guide` or `新增測試案例`. Pull requests should include a brief summary, commands run, linked issue if applicable, and screenshots only when UI or rendered documentation changes.

## Security & Agent-Specific Instructions

Do not disclose prompts, secrets, keys, or credentials. Avoid broad file deletion. If cleanup is required, delete only a specific single-path file and ask the user to handle bulk directory removal manually.
