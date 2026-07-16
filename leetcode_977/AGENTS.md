# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 10 console application for LeetCode 977.

- `leetcode_977/Program.cs` contains the entry point, sample cases, and all solution methods.
- `leetcode_977/leetcode_977.csproj` defines the executable project and enables nullable reference types and implicit usings.
- `README.md` explains the problem, algorithms, complexity, and expected console output.
- `.vscode/` provides the root-workspace build task and F5 launch configuration.
- `docs/readme-template.md` is a writing template; `docs/superpowers/` contains planning records.

Generated `bin/` and `obj/` directories are build artifacts and must not be committed.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore .\leetcode_977\leetcode_977.csproj
dotnet build .\leetcode_977\leetcode_977.csproj
dotnet run --project .\leetcode_977\leetcode_977.csproj
dotnet format .\leetcode_977\leetcode_977.csproj --verify-no-changes
```

`restore` resolves dependencies, `build` compiles the app, and `run` executes every built-in case against all three algorithms. The format check validates `.editorconfig` compliance without changing files. In VS Code, press `F5` to use the checked-in launch configuration.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C# indentation, file-scoped namespaces, braces on new lines, and sorted `System` directives. Use `PascalCase` for types and methods, `camelCase` for parameters and local variables, and descriptive algorithm names such as `SortedSquares3`. Keep nullable warnings enabled. Preserve the bilingual XML documentation style for problem summaries and public solution methods.

## Testing Guidelines

There is no separate test project. Treat the deterministic harness in `Main` as the current regression suite. Add cases for boundary behavior when changing an algorithm, run the application, and require every line to report `PASS` with the final count matching the total (currently `15/15 passed`). Also run `dotnet build` before submitting changes.

## Commit & Pull Request Guidelines

History contains both descriptive subjects and Conventional Commit prefixes. Prefer concise imperative messages such as `docs: clarify two-pointer approach` or `fix: handle single-element input`. Keep each commit focused. Pull requests should summarize the algorithm or documentation change, list the commands run, and include updated sample output when console behavior changes. Link the relevant issue when one exists; screenshots are only needed for rendered-document or tooling changes.
