# Repository Guidelines

## Project Structure & Module Organization

This workspace contains one .NET console application. Keep problem code and its runnable entry point in `leetcode_215/Program.cs`; its project definition is `leetcode_215/leetcode_215.csproj` (targeting `net10.0`). Keep reusable documentation and contributor notes in `docs/`. Do not edit generated `leetcode_215/bin/` or `leetcode_215/obj/` output.

## Build, Run, and Development Commands

Run these commands from this directory:

```bash
dotnet build leetcode_215/leetcode_215.csproj
dotnet run --project leetcode_215/leetcode_215.csproj
```

The first command restores and compiles the console project; the second runs its sample harness. There is no solution file or test project here, so `dotnet test` at the workspace root fails with `MSB1003`. Use the explicit project path rather than a bare `dotnet build` or `dotnet test` command.

## Coding Style & Naming Conventions

Follow the parent `.editorconfig`: use four spaces in C#, braces for control blocks, explicit local types, and nullable-safe code. Use PascalCase for types, methods, and properties; use camelCase for parameters and local variables. Prefer a file-scoped namespace for new files and keep `using` directives outside it. Preserve the bilingual XML problem description in `Program.cs`; put algorithm notes and sample behavior in adjacent, focused comments rather than rewriting the original statement.

## Testing Guidelines

No automated test framework is configured. Before proposing a change, run the build and console command above, and cover normal, boundary, and duplicate-value cases in the runnable sample harness when implementation begins. If a test project is added later, name it `leetcode_215.Tests` and use behavior-oriented test names such as `FindKthLargest_ReturnsValueForDuplicateNumbers`.

## Commit and Pull Request Guidelines

Recent history uses concise Chinese summaries and focused Conventional Commit-style messages such as `feat: add samples and documentation for leetcode 209`. Keep each commit limited to one problem or documentation change. Pull requests should state the algorithm or documentation change, link the relevant LeetCode prompt when applicable, and list the validation commands and results. Do not commit secrets, machine-specific paths, or generated build artifacts.
