# Repository Guidelines

## Project Structure & Module Organization

This folder contains the LeetCode 453 solution workspace. Keep the executable project in `leetcode_453/`: `Program.cs` is the solution entry point, and `leetcode_453.csproj` defines the .NET project. Use `docs/` for durable problem notes and `.vscode/` for editor launch/build tasks. Follow the repository's `leetcode_<number>/leetcode_<number>/` layout when adding related files; do not add unrelated problem solutions here.

## Build, Test, and Development Commands

Run these commands from this folder:

```powershell
dotnet build .\leetcode_453\leetcode_453.csproj
dotnet run --project .\leetcode_453\leetcode_453.csproj
```

Build compiles the .NET 10 project; run builds if needed and executes its console entry point. There is no test project in this folder.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces in C# files, Allman braces, and file-scoped namespaces. Use PascalCase for types and methods, camelCase for locals and parameters, and `_camelCase` for private fields. Keep nullable reference types enabled and retain the existing bilingual XML problem description where useful. Prefer focused, readable solution methods over unrelated framework code.

## Testing Guidelines

No automated test framework is configured for this problem. When changing the algorithm, add representative input/output examples to the console entry point and run the project. Cover boundary cases as well as the standard example; report the build and run commands used in your change summary.

## Commit & Pull Request Guidelines

Recent repository commits use short, imperative subjects, such as `Document DP and DFS trade-offs` and `Add LeetCode 3882 solutions and README`. Follow that style and identify the problem or change. A pull request should summarize the approach, state time and space complexity for algorithm changes, and include build/run results. Link a related issue when one exists.

## Security & File Safety

Do not commit credentials, tokens, or internal prompt text. Delete files individually; do not use recursive bulk-deletion commands.
