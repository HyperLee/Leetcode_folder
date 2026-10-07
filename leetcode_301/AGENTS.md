# Repository Guidelines

## Project Structure & Module Organization

This folder contains the LeetCode 301 solution project. The .NET 10 console app is in `leetcode_301/`: `Program.cs` is the entry point and solution file, and `leetcode_301.csproj` defines the project. `docs/readme-template.md` is the documentation starter. `.vscode/` contains the build task and F5 launch configuration; `.editorconfig` and `.gitattributes` define source and text-file conventions.

## Build, Test, and Development Commands

Run commands from this folder:

```powershell
dotnet build .\leetcode_301\leetcode_301.csproj --nologo
dotnet run --project .\leetcode_301\leetcode_301.csproj
```

The first command compiles the project; the second runs the console app. In VS Code, F5 runs the configured build task before launching the app with no arguments.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces in C# files, braces for blocks, PascalCase for types and members, and camelCase for locals and parameters. Prefer file-scoped namespaces, as used by the project. Keep solution logic in the console project and avoid adding dependencies without a clear need.

## Testing Guidelines

There is no separate test project, framework, or coverage threshold in this folder. Run the app after changes and add deterministic checks when implementing solution behavior. Cover valid and invalid parenthesis strings, repeated parentheses, empty input, and cases with multiple minimum-removal answers.

## Commit & Pull Request Guidelines

Use a short imperative commit subject, matching the history's `Initialize leetcode_301 .NET scaffold` style. A pull request should summarize the algorithm or behavior change, list build and execution checks, and link a related issue when applicable. Run `git diff --check` before submitting; no pull request template is present.
