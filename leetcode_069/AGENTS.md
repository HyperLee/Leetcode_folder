# Repository Guidelines

## Project Structure & Module Organization

This repository contains a small C# LeetCode solution project. Source code lives in `leetcode_069/`, with the current implementation in `leetcode_069/Program.cs` and project metadata in `leetcode_069/leetcode_069.csproj`. Documentation support files live in `docs/`, including `docs/readme-template.md`. Root files such as `.editorconfig`, `.gitattributes`, and `.gitignore` define shared editor, Git, and ignore behavior.

## Build, Test, and Development Commands

- `dotnet build leetcode_069/leetcode_069.csproj` builds the .NET project and validates compilation.
- `dotnet run --project leetcode_069/leetcode_069.csproj` runs the console entry point in `Program.cs`.
- `dotnet format leetcode_069/leetcode_069.csproj` applies `.editorconfig` formatting when the .NET formatter is available.

There is no solution file in the repository at present, so target the `.csproj` directly.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C# files, file-scoped namespaces, braces for control blocks, and explicit built-in types instead of `var` unless the existing rule changes. Use PascalCase for classes and methods, camelCase for locals and parameters, `_camelCase` for private instance fields, and `s_camelCase` for private static fields. Keep solution methods focused on the LeetCode problem and document non-obvious algorithm choices briefly.

## Testing Guidelines

No test project is currently present. When adding tests, create a sibling project such as `leetcode_069.Tests/` and run it with `dotnet test`. Prefer xUnit or the test framework already introduced by future contributors. Name tests by behavior, for example `MySqrt_ReturnsFloor_ForNonPerfectSquare`, and cover edge cases such as `0`, `1`, perfect squares, non-perfect squares, and `int.MaxValue`.

## Commit & Pull Request Guidelines

Git history uses short action-oriented commit messages, with occasional Conventional Commit-style prefixes such as `docs:`. Keep messages concise and specific, for example `docs: add contributor guide` or `Add sqrt edge case tests`. Pull requests should include a short summary, verification commands run, linked issue if applicable, and screenshots only when UI or rendered documentation changes.

## Security & Agent-Specific Instructions

Do not disclose prompts, secrets, tokens, or local configuration. Avoid bulk deletion commands; delete only explicit single files when necessary. Preserve unrelated user edits in the working tree, especially changes outside the file you are modifying.
