# Repository Guidelines

## Project Structure & Module Organization

This repository is a compact .NET console project for LeetCode 290, Word Pattern. Source lives in `leetcode_290/Program.cs`; the project file is `leetcode_290/leetcode_290.csproj` and targets `net10.0` with nullable reference types and implicit usings enabled. The root `.vscode/` folder contains one-click build/debug configuration, and `docs/readme-template.md` is available for future first-time README generation. Build artifacts stay under `leetcode_290/bin/` and `leetcode_290/obj/`.

## Build, Test, and Development Commands

- `dotnet build leetcode_290/leetcode_290.csproj` builds the nested console project.
- `dotnet run --project leetcode_290/leetcode_290.csproj` runs the current sample harness; at present it prints `Hello, World!`.
- `dotnet test leetcode_290/leetcode_290.csproj` currently has no assertions because there is no test project.
- `dotnet test` from the repository root fails with `MSB1003` because the root does not contain a solution or project file.

Use the VS Code configuration `Debug leetcode_290` to build and launch the compiled `net10.0` DLL from the integrated terminal.

## Coding Style & Naming Conventions

Follow `.editorconfig`: C# files use 4-space indentation, braces on new lines, file-scoped namespaces where practical, and explicit built-in types instead of `var` unless a future change updates the convention. Use PascalCase for types, methods, properties, events, enums, and local functions; use camelCase for locals and parameters; use `_camelCase` for private fields and `s_camelCase` for private static fields. Keep XML comments that contain the original bilingual problem statement intact unless the task explicitly asks to revise them.

## Testing Guidelines

There is no dedicated unit-test project yet. For small algorithm updates, add clear sample cases to `Main` or a local sample runner and verify them with `dotnet run --project leetcode_290/leetcode_290.csproj`. If adding formal tests later, prefer a sibling `leetcode_290.Tests` project and document the exact `dotnet test` command.

## Commit & Pull Request Guidelines

Recent history mixes Conventional Commit style, such as `feat(leetcode-260): add single number iii demo`, with short Chinese maintenance messages. Prefer concise, imperative commits; use `feat(leetcode-290): ...` or `docs(leetcode-290): ...` when the scope is clear. Pull requests should summarize the algorithm or documentation change, list verified commands, and mention any intentional sample-output changes.
