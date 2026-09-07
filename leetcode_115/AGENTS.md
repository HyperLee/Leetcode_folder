# Repository Guidelines

## Project Structure & Module Organization

This repository is a small SDK-style .NET 10 console project for LeetCode 115. Keep algorithm source in `leetcode_115/Program.cs`; `leetcode_115/leetcode_115.csproj` defines the build target. `.vscode/launch.json` and `.vscode/tasks.json` provide direct F5/build integration. `docs/readme-template.md` is a template for an initial README, not runtime documentation. Build artifacts under `bin/` and `obj/` are generated and ignored. There is currently no separate test project or asset directory.

## Build, Test, and Development Commands

Run these commands from the repository root:

```powershell
dotnet build .\leetcode_115\leetcode_115.csproj --nologo
dotnet run --project .\leetcode_115\leetcode_115.csproj --no-build
```

The first command compiles the project; the second executes the current `Main` entry point. In VS Code, F5 invokes the `build leetcode_115` task and launches an integrated terminal with no arguments. If project inputs change, build first or omit `--no-build`.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C#, braces, file-scoped namespaces, and explicit built-in types rather than `var`. Use PascalCase for types and methods, and camelCase for parameters and local variables. Keep public methods documented with XML comments, and preserve the bilingual problem statement when changing `NumDistinct`. No repository formatter or linter is configured; use IDE formatting and review the diff.

## Testing Guidelines

No test framework or coverage threshold is configured. For algorithm changes, run the build and exercise deterministic cases covering exact matches, repeated characters, an empty target, and `s.Length < t.Length`. Keep temporary examples isolated from the production method. If a change warrants automated tests, add a focused test project and document its command in the pull request.

## Commit & Pull Request Guidelines

Recent commits use concise imperative messages such as `Add DP solution NumDistinct to Program.cs` and `Initialize leetcode_115 .NET project`. Follow the `<Verb> <focused change>` pattern and keep each commit logically scoped. Pull requests should explain intent, list affected paths, and include exact validation commands and results. Link an issue when applicable; screenshots are unnecessary for console-only changes. Do not commit generated `bin/` or `obj/` output.
