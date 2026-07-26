# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 10 console application. The executable project is under `leetcode_2558/`; `Program.cs` holds the entry point and LeetCode solution code, while `leetcode_2558.csproj` defines the target framework and compiler defaults. Root-level `.vscode/` files provide the checked-in build and F5 debug configuration. Use `docs/readme-template.md` as a structural guide when creating or revising the root `README.md`. Generated `bin/` and `obj/` directories are ignored and must not be committed.

## Build, Run, and Development Commands

- `dotnet restore leetcode_2558/leetcode_2558.csproj` restores project dependencies.
- `dotnet build leetcode_2558/leetcode_2558.csproj` compiles the app and reports compiler or nullable warnings.
- `dotnet run --project leetcode_2558/leetcode_2558.csproj` runs the fixed console examples locally.
- In VS Code, press `Ctrl+Shift+B` for the default build task or `F5` to launch `leetcode_2558.dll` in the integrated terminal.
- `dotnet format leetcode_2558/leetcode_2558.csproj --verify-no-changes` checks formatting without rewriting files.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space indentation for C#, two spaces for JSON and project XML, braces for code blocks, and file-scoped namespaces where practical. Use `PascalCase` for types and methods, `camelCase` for locals and parameters, `_camelCase` for private instance fields, and `s_camelCase` for private static fields. Keep solution methods focused, preserve existing public method names, and comment the algorithmic reason or complexity—not obvious syntax.

## Testing Guidelines

There is currently no separate automated test project or coverage threshold. Add deterministic cases to the console runner, including the problem examples and edge cases such as one pile, repeated maximums, and `k = 0`. Print expected and actual values with a clear PASS/FAIL result. Before submitting, run both `dotnet build` and `dotnet run`; update documentation transcripts when console output changes.

## Commit & Pull Request Guidelines

Recent history primarily uses short Conventional Commit-style subjects, for example `feat(leetcode-2558): add priority-queue solution` or `docs: add usage notes`. Keep each commit scoped to one logical change. Pull requests should explain the approach, list verification commands and results, and link the relevant issue. Include console output for behavior changes; screenshots are only needed for tooling or visual changes.

## Security & Configuration

Do not commit secrets, `.env` files, IDE user settings, or generated artifacts. Keep dependency and target-framework changes explicit in the project file.
