# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET console project for LeetCode 3870. The outer folder contains shared configuration and tooling: `.editorconfig`, `.gitattributes`, `.gitignore`, `.vscode/`, and `docs/readme-template.md`. The executable project is nested under `leetcode_3870/`, with `Program.cs` and `leetcode_3870.csproj`. Build artifacts belong under that nested project’s `bin/` and `obj/` directories; there is currently no separate test or asset directory.

## Build, Test, and Development Commands

Run these commands from the repository root:

```powershell
dotnet build .\leetcode_3870\leetcode_3870.csproj --nologo
dotnet run --project .\leetcode_3870\leetcode_3870.csproj
```

`dotnet build` compiles the `net10.0` project with nullable reference types and implicit usings enabled. `dotnet run` executes the console entry point; it currently requires no input. In VS Code, press F5 to use `Run leetcode_3870`, which builds first and launches with `args: []` in the integrated terminal. No automated test project or coverage command exists yet, so use a clean build and a representative console run as the smoke test.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C# indentation, braces for control blocks, and file-scoped namespaces. Use PascalCase for types and methods, camelCase for locals and parameters, and explicit types instead of `var` unless a future rule or surrounding code clearly favors otherwise. Keep nullable annotations enabled. The problem summary in `Program.cs` is bilingual; keep English and Traditional Chinese documentation synchronized when changing it, and preserve the no-input entry-point behavior unless the task explicitly requires input handling.

## Testing Guidelines

For algorithm changes, keep sample cases deterministic and document expected results. If unit tests are introduced, place them in a separate test project and name test methods after the behavior they verify. Run the relevant `dotnet test <test-project>.csproj` command in addition to the build and console smoke test.

## Commit & Pull Request Guidelines

Nearby LeetCode history uses short imperative subjects such as `Add ...`, `Improve ...`, and `README: add ...`. Follow that style. Pull requests should explain the problem or behavior changed, summarize the algorithm and complexity when applicable, list validation commands and results, and mention documentation or VS Code configuration changes. Link an issue when one exists; screenshots are normally unnecessary for this console-only project.

## Security & Configuration Tips

Do not commit secrets, machine-specific settings, or generated `bin/` and `obj/` contents. Review command-line and file-path changes carefully before sharing a branch.
