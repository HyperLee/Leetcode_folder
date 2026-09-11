# Repository Guidelines

## Project Structure & Module Organization

This repository is a small nested .NET 10 console project. The executable project and source are under `leetcode_3483/`; `Program.cs` contains the LeetCode 3483 entry point and problem documentation, while `leetcode_3483.csproj` defines the SDK-style project. VS Code build/debug settings live in `.vscode/`, and `docs/readme-template.md` is the template for a future README. There is currently no separate test project or asset directory.

## Build, Test, and Development Commands

Run these commands from the repository root:

```powershell
dotnet build .\leetcode_3483\leetcode_3483.csproj --nologo
dotnet run --project .\leetcode_3483\leetcode_3483.csproj --no-build
```

The first command restores and compiles the project; the second runs the already-built console application. In VS Code, press F5 to use `.vscode/launch.json`, which builds through the `build leetcode_3483` task and launches with no arguments.

## Coding Style & Naming Conventions

Follow the repository `.editorconfig`: use four spaces for C# indentation, braces for control blocks, file-scoped namespaces, and a final newline only where existing file conventions allow it. Use PascalCase for types and methods, camelCase for locals and parameters, and `_camelCase` for private fields. Keep XML comments and bilingual problem links accurate. No separate formatter or linter is configured; `dotnet build` is the baseline compiler check.

## Testing Guidelines

No automated test framework or coverage threshold is configured. Validate algorithm changes with deterministic cases in the console runner, including duplicate digits, leading-zero candidates, and different final-digit parity. Run both commands above and record meaningful output in the change description when behavior changes.

## Commit & Pull Request Guidelines

Recent history uses short imperative subjects such as `Add ...`, `Explain ...`, `Fix ...`, and `Expand ...`. Keep commits focused and use the same style, for example `Add 3483 solution cases`. PRs should summarize the algorithm or documentation change, list validation commands and results, link an issue when applicable, and call out any intentionally preserved LeetCode signatures or comments. Do not include unrelated generated files or broad formatting changes.

## Agent-Specific Instructions

Keep changes scoped to this project, preserve the original problem statement/XML documentation unless intentionally updating it, and inspect the worktree before editing. Do not overwrite unrelated user changes.
