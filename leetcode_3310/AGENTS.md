# Repository Guidelines

This directory contains the .NET 10 console project for LeetCode 3310, “Remove Methods From Project.” Keep changes focused on the solution, executable examples, and supporting documentation.

## Project Structure & Module Organization

- `leetcode_3310/Program.cs` contains the namespace, problem statement comments, solution code, and `Main`.
- `leetcode_3310/leetcode_3310.csproj` is an SDK-style executable targeting `net10.0`.
- `.vscode/tasks.json` and `.vscode/launch.json` define the build task and CoreCLR debug profile.
- `docs/readme-template.md` guides creation of the exercise README. Generated `bin/` and `obj/` artifacts are local only and ignored.

## Build, Test, and Development Commands

Run from this directory:

```powershell
dotnet build .\leetcode_3310\leetcode_3310.csproj
dotnet run --project .\leetcode_3310\leetcode_3310.csproj
```

`dotnet build` compiles the project and reports diagnostics; `dotnet run` executes `Main`. VS Code F5 uses the same build task and launches `bin/Debug/net10.0/leetcode_3310.dll`. No separate test project or test script is currently present; use the run command as a smoke test and add deterministic expected-result cases with any new solution runner.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four spaces for C#, braces on their own lines, file-scoped namespaces, nullable reference types enabled, and no tabs. Use `PascalCase` for types and methods and `camelCase` for locals and parameters. Keep the existing bilingual problem statement comments intact; place solution explanations and examples around them rather than replacing source text. Avoid unrelated formatting churn.

## Testing Guidelines

There is no coverage threshold yet. For algorithm changes, exercise relevant edge cases such as minimal inputs, cycles, and cross-group invocations, then report the exact build/run commands and outcomes. If a formal test project is added, keep it under a clearly named `Tests/` project and use descriptive names such as `Method_WhenCondition_ExpectedResult`.

## Commit & Pull Request Guidelines

Recent commits use short imperative subjects such as `Add ...`, `Remove ...`, and `Clarify ...`; follow that style and keep each commit focused. Pull requests should explain the algorithm or documentation change, list verification commands and results, link an issue when one exists, and note changes to sample output or the README. Screenshots are unnecessary for console-only changes unless they show documentation or debugger behavior.
