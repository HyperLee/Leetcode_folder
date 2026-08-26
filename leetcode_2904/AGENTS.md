# Repository Guidelines

## Project Structure & Module Organization

This repository is a nested SDK-style .NET 10 console project for LeetCode 2904.

- `leetcode_2904/Program.cs` — entry point, problem summary, and solution implementation.
- `leetcode_2904/leetcode_2904.csproj` — executable project targeting `net10.0`.
- `.vscode/launch.json` and `.vscode/tasks.json` — direct F5 launch and default build task.
- `docs/readme-template.md` — guidance for creating `README.md`; treat current code and output as the source of truth.

Build artifacts in `bin/` and `obj/` are generated and ignored.

## Build, Test, and Development Commands

Run from `C:\GitHubFolder\Leetcode_folder\leetcode_2904`:

```powershell
dotnet build .\leetcode_2904\leetcode_2904.csproj
dotnet run --project .\leetcode_2904\leetcode_2904.csproj
```

`dotnet build` compiles the project; `dotnet run` executes the current console demonstration. In VS Code, F5 uses CoreCLR and builds first with `build leetcode_2904`.

## Coding Style & Naming Conventions

Follow `.editorconfig`: spaces, four-space C# indentation, braces, and file-scoped namespaces. Use `PascalCase` for types, methods, and properties; `camelCase` for locals and parameters; prefix private fields with `_` and private static fields with `s_`. Keep changes focused in `Program.cs`, preserve existing XML documentation unless it is part of the requested change, and do not add a formatter or linter configuration that is not already present.

## Testing Guidelines

There is currently no separate test project or coverage gate. For every change, run the build and execute the console app. When adding solution logic, include deterministic sample cases and verify the printed results manually; document expected output in `README.md` when a README exists.

## Commit & Pull Request Guidelines

Recent history favors short imperative subjects such as `Add ...`, `Clarify ...`, and `Document ...`, with concise Chinese subjects also present. Keep commits focused; for example, `docs: add repository guide` or `Add leetcode 2904 solution`. PR descriptions should state the problem solved, files changed, and build/run results. Link an issue when one exists; screenshots are unnecessary for this console-only project.

## Security & Configuration

Do not commit secrets, local `.env` files, or generated output. Keep changes limited to this project and inspect `git status` before submission.
