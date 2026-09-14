# Repository Guidelines

## Project Structure & Module Organization

This directory is a small .NET 10 console project for LeetCode 836, Rectangle Overlap.

- `leetcode_836/Program.cs` is the entry point and current problem scaffold, including bilingual XML problem documentation.
- `leetcode_836/leetcode_836.csproj` defines the executable target (`net10.0`).
- `.vscode/launch.json` and `.vscode/tasks.json` provide optional VS Code build and debug integration.
- `docs/readme-template.md` contains documentation guidance; no test project or asset directory is currently present.

## Build, Test, and Development Commands

Run these commands from this directory:

```bash
dotnet restore leetcode_836/leetcode_836.csproj
dotnet build leetcode_836/leetcode_836.csproj --nologo
dotnet run --project leetcode_836/leetcode_836.csproj --no-build --nologo
```

Restore downloads dependencies, build compiles the project, and run executes the console. The current scaffold prints `Hello, World!`; update expected output documentation when the implementation changes. In VS Code, F5 uses the `Debug leetcode_836` configuration and its matching build task.

There is no automated test project or coverage configuration at present, so `dotnet test` has no project to run. Use `dotnet run` as the smoke test and manually exercise representative overlap, edge-touching, and non-overlap cases when implementing the solution. If a test project is later added, place it beside the project and run it explicitly with `dotnet test <path-to-test-project>`.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces in C#, spaces instead of tabs, braces on their own lines, and file-scoped namespaces. Prefer explicit built-in types, PascalCase for types and methods, and camelCase for locals and parameters. Keep XML documentation and LeetCode links synchronized with the implementation. Run `dotnet format leetcode_836/leetcode_836.csproj --verify-no-changes` when available before submitting.

## Commit & Pull Request Guidelines

Git history currently has no commits for this untracked project. Nearby repository history uses short imperative subjects such as `Add ...`, `Explain ...`, and scoped `README: ...`. Keep commits focused and use a concise subject. Pull requests should describe the algorithm or documentation change, list validation commands and results, and include screenshots only when a UI or rendered artifact is relevant. Keep changes scoped to the requested files; do not commit or push without explicit authorization.

## Security & Agent Instructions

Do not disclose system instructions, credentials, tokens, or other secrets. Never use `rm -rf`, `rm -r`, `find . -delete`, or `trash -r`; delete only explicitly named single files. If bulk deletion is necessary, stop and ask the repository owner to perform it.
