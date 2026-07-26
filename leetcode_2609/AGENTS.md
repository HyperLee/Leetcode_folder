# Repository Guidelines

## Project Structure & Module Organization

This repository is a focused .NET 10 console solution for LeetCode 2609. The executable project is under `leetcode_2609/`: `Program.cs` contains the entry point and solution code, while `leetcode_2609.csproj` defines the SDK and target framework. Workspace-level conventions live in `.editorconfig`; `.vscode/tasks.json` and `.vscode/launch.json` provide build and F5 debugging. Use `docs/readme-template.md` when adding or refreshing the teaching-oriented `README.md`. Generated `bin/` and `obj/` directories must remain untracked.

## Build, Run, and Development Commands

- `dotnet restore leetcode_2609/leetcode_2609.csproj` restores project dependencies.
- `dotnet build leetcode_2609/leetcode_2609.csproj` compiles the project and reports analyzer/compiler warnings.
- `dotnet run --project leetcode_2609/leetcode_2609.csproj` runs the console demonstration.
- `dotnet format leetcode_2609/leetcode_2609.csproj --verify-no-changes` checks formatting against `.editorconfig`.

From VS Code, press `Ctrl+Shift+B` for the default build task or `F5` to build and launch `bin/Debug/net10.0/leetcode_2609.dll`.

## Coding Style & Naming Conventions

Use four spaces in C# and two spaces in JSON/XML project files. Follow file-scoped namespaces, braces for control blocks, and explicit types instead of `var`, as configured in `.editorconfig`. Name types and methods in `PascalCase`, locals and parameters in `camelCase`, private fields `_camelCase`, and private static fields `s_camelCase`. Preserve the bilingual English/Traditional Chinese problem summary and add comments only for algorithm intent or non-obvious invariants.

## Testing Guidelines

No automated test project currently exists. Add deterministic sample and edge cases to the console runner, including empty/minimum behavior, all-zero/all-one input, and multiple balanced groups. Print expected versus actual results with a clear `PASS`/`FAIL` summary. Before submitting, run both `dotnet build` and `dotnet run`; do not treat compilation alone as behavioral verification.

## Commit & Pull Request Guidelines

Recent history uses concise problem-title subjects and short typed commits such as `chore: stop tracking generated files`. Prefer an imperative subject, optionally `feat:`, `fix:`, `docs:`, or `chore:`, and keep each commit focused. Pull requests should explain the algorithm and complexity, list commands run, link the LeetCode problem or issue, and include updated console output when behavior changes. Screenshots are unnecessary for console-only changes.
