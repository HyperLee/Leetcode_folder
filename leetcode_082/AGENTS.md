# Repository Guidelines

## Project Structure & Module Organization

This repository contains a focused .NET console solution for LeetCode 82.

- `leetcode_082.sln` is the solution entry point.
- `leetcode_082/leetcode_082.csproj` targets `net10.0` with nullable reference types and implicit usings enabled.
- `leetcode_082/Program.cs` contains the `ListNode` model, both solution methods, and demo helpers.
- `README.md` documents the problem, approaches, run commands, and expected output.
- `docs/readme-template.md` is the reusable README authoring template.
- `.vscode/tasks.json` and `.vscode/launch.json` provide build and debug tasks for VS Code.

There is no separate test project yet; validation currently happens through build checks and console demo cases.

## Build, Test, and Development Commands

- `dotnet build leetcode_082.sln` builds the full solution.
- `dotnet run --project leetcode_082/leetcode_082.csproj` runs the sample cases shown in `README.md`.
- `git diff --check` checks for whitespace errors before committing.

If adding automated tests later, prefer a sibling test project such as `leetcode_082.Tests/` and include it in `leetcode_082.sln`.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C#, explicit built-in types over `var`, braces for code blocks, and nullable annotations where relevant. Keep the existing namespace `leetcode_082`.

Use PascalCase for types and methods (`ListNode`, `DeleteDuplicates2`) and camelCase for locals and parameters (`current`, `duplicateValue`). Existing LeetCode-compatible fields use lowercase names (`val`, `next`); preserve that shape unless the model is intentionally redesigned.

## Testing Guidelines

Before opening a PR, run `dotnet build leetcode_082.sln` and `dotnet run --project leetcode_082/leetcode_082.csproj`. Compare output against `README.md`, especially edge cases such as all duplicates, leading duplicates, empty lists, and single-node lists.

When adding tests, use xUnit or MSTest consistently, name files after the class under test, and use descriptive test names such as `DeleteDuplicates_RemovesLeadingDuplicateGroup`.

## Commit & Pull Request Guidelines

Recent history uses short, direct messages, including Conventional Commit style (`docs: add ...`) and Traditional Chinese summaries (`新增解法二`). Prefer concise imperative commits with an optional scope, for example `docs: update LeetCode 82 guide` or `新增測試案例`.

PRs should include a brief description, the commands run, any changed sample output, and linked issue/problem context when available. Update `README.md` whenever behavior, commands, or documented examples change.

## Agent-Specific Instructions

Keep changes narrowly scoped to this LeetCode solution. Do not introduce new frameworks, dependencies, or broad refactors unless they directly improve the requested problem solution or verification path.
