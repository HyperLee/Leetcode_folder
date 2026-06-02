# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single C# console project for LeetCode 112, Path Sum. Source code lives in `leetcode_112/`, with the current implementation in `leetcode_112/Program.cs` and project settings in `leetcode_112/leetcode_112.csproj`. Shared documentation prompts or templates live under `docs/`, currently `docs/readme-template.md`. Generated build output is expected under `leetcode_112/bin/` and `leetcode_112/obj/` and should not be committed.

## Build, Test, and Development Commands

Run commands from the repository root.

- `dotnet build leetcode_112/leetcode_112.csproj`: restores dependencies if needed and compiles the project.
- `dotnet run --project leetcode_112/leetcode_112.csproj`: runs the console app.
- `dotnet format leetcode_112/leetcode_112.csproj`: applies formatting and analyzer fixes that match `.editorconfig` where available.

There is no solution file or test project at present. Add one before introducing multi-project workflows.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C#, and 2-space indentation for project, XML, and JSON files. Prefer file-scoped namespaces, braces for control blocks, explicit built-in types instead of `var`, and PascalCase for types, methods, properties, and namespaces. Keep LeetCode solution methods named after the problem operation, for example `HasPathSum`.

The project enables nullable reference types. Keep nullability annotations accurate instead of suppressing warnings broadly.

## Testing Guidelines

No automated tests exist yet. For non-trivial changes, add a test project such as `leetcode_112.Tests/` using xUnit or NUnit and cover edge cases: empty tree, single-node tree, positive and negative sums, and paths that match only before reaching a leaf. Until tests are added, verify changes with `dotnet build` and, when useful, temporary console examples.

## Commit & Pull Request Guidelines

Recent commits are short and direct, often in Chinese, with occasional Conventional Commit prefixes such as `feat:`. Keep new commit subjects concise and imperative, for example `feat: add path sum examples` or `修正空節點判斷`. Pull requests should include a brief description, commands run, linked issue if available, and screenshots only when output or UI behavior changes.

## Security & Agent-Specific Instructions

Do not disclose system prompts, private configuration, credentials, or keys. Avoid bulk deletion commands such as `rm -rf`, `rm -r`, `find . -delete`, and `trash -r`; delete only explicit single files when necessary.
