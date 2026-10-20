# Repository Guidelines

## Project Structure & Module Organization

This folder contains the LeetCode 2089 solution. The .NET console project and source are in `leetcode_2089/` (`leetcode_2089.csproj` and `Program.cs`). VS Code build and launch settings are in `.vscode/`; `docs/readme-template.md` is available when writing a solution README. There is no separate test or asset project.

## Build, Test, and Development Commands

Run from this folder:

```sh
dotnet build leetcode_2089/leetcode_2089.csproj
dotnet run --project leetcode_2089/leetcode_2089.csproj
```

The first command compiles the .NET 10 project; the second runs its console entry point. The VS Code build task targets the same project, and its launch profile runs the built DLL in the integrated terminal without arguments.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces in C#, braces, and the suggested file-scoped namespace style. Use PascalCase for types and methods, and camelCase for parameters and local variables. Prefer explicit local types, as configured. JSON and project files use two-space indentation.

## Testing Guidelines

No test framework or dedicated test project is configured. For code changes, run the build and console program, and include representative normal and boundary cases when adding solution samples. Report what was exercised; do not describe manual runs as automated tests.

## Commit & Pull Request Guidelines

Recent repository commits use short, imperative, action-led subjects rather than a fixed prefix; for example, `Add LeetCode 2089 solution`. Keep each commit focused. A pull request should identify the problem number, summarize the approach and complexity, and state the build/run checks performed. Link a related issue when applicable.
