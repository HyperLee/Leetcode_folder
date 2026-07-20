# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 10 console application. The outer directory is the workspace root; application code and the SDK-style project file are under `leetcode_1642/`:

- `leetcode_1642/Program.cs` contains the entry point, problem statement, and solution code.
- `leetcode_1642/leetcode_1642.csproj` targets `net10.0` with nullable reference types and implicit usings enabled.
- `.vscode/` contains the default build task and direct F5 debug profile.
- `bin/` and `obj/` are generated outputs. Do not edit or commit them.

There is no separate test project or asset directory. Keep new algorithms close to `Program` unless the project grows enough to justify additional focused `.cs` files.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore .\leetcode_1642\leetcode_1642.csproj
dotnet build .\leetcode_1642\leetcode_1642.csproj
dotnet run --project .\leetcode_1642\leetcode_1642.csproj
dotnet format .\leetcode_1642\leetcode_1642.csproj --verify-no-changes
```

`restore` resolves dependencies, `build` compiles the app, `run` executes `Main`, and `format --verify-no-changes` checks standard .NET formatting. In VS Code, press F5 to use the `Debug leetcode_1642` profile.

## Coding Style & Naming Conventions

Use four-space indentation, braces on new lines, and file-scoped namespaces. Follow standard C# naming: `PascalCase` for types and methods, `camelCase` for parameters and locals. Keep nullable warnings enabled and avoid unnecessary `using` directives. Preserve the bilingual English/Traditional Chinese XML problem summary when editing `Program.cs`. Name alternative solutions descriptively or with the established numeric suffix pattern, such as `FurthestBuilding2`.

## Testing Guidelines

No automated test framework or coverage threshold is configured. Add deterministic sample cases to `Main`, including typical, boundary, and resource-exhaustion scenarios, and print clear expected-versus-actual or PASS/FAIL results. Before submitting, run `dotnet build`, then `dotnet run`; verify the complete console output manually.

## Commit & Pull Request Guidelines

History is brief and uses concise subjects, including `chore: stop tracking generated files` and Chinese summaries. Prefer an imperative Conventional Commit subject such as `feat: add heap-based solution` or `docs: clarify complexity`. Keep each commit focused. Pull requests should explain the algorithm, time and space complexity, validation cases, and any changed console output. Link the relevant issue when available; screenshots are only needed for visible tooling or output changes.
