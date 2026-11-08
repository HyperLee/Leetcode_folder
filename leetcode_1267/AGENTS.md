# Repository Guidelines

## Project Structure & Module Organization

This repository contains a .NET 10 console project for LeetCode 1267, “Count Servers that Communicate.” `leetcode_1267/Program.cs` holds the bilingual problem statement and entry point; it currently prints `Hello, World!` without implementing the algorithm. `leetcode_1267/leetcode_1267.csproj` enables nullable reference types and implicit usings. `.vscode/` contains build and debugger configurations, and `docs/readme-template.md` provides guidance for creating an initial README. There are no dedicated test projects or application assets. Keep generated `bin/` and `obj/` files out of commits.

## Build, Test, and Development Commands

Run these commands from the repository root with the .NET 10 SDK installed:

```bash
dotnet build leetcode_1267/leetcode_1267.csproj
dotnet run --project leetcode_1267/leetcode_1267.csproj
dotnet format leetcode_1267/leetcode_1267.csproj --verify-no-changes
```

These commands compile the project, execute the console entry point, and check formatting respectively. Omit `--verify-no-changes` to apply formatting. VS Code’s default build task compiles Debug output; its launch configuration uses that task before starting the debugger.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C#, file-scoped namespaces, braces on separate lines, and explicit types where practical. Use PascalCase for types and methods, camelCase for parameters and local variables, and `_camelCase` for private instance fields. Preserve the existing `leetcode_1267` namespace and bilingual problem documentation. Use Traditional Chinese for teaching explanations and document algorithm complexity when adding a solution.

## Testing Guidelines

No testing framework or coverage threshold is configured. A successful build or the current console output does not verify an algorithm. When adding a solution, include deterministic checks with explicit expected results for isolated servers, shared rows, shared columns, empty cells, and single-row or single-column grids. Use descriptive names such as `CountServers_IsolatedServers_ReturnsZero`. Document how to execute any checks or test project introduced.

## Commit & Pull Request Guidelines

Recent Git history uses short, imperative subjects such as “Add tests and guide for LeetCode 1882.” Follow that style and identify the affected problem or behavior. Keep changes focused. Pull requests should explain the approach, complexity, relevant edge cases, and validation commands with results. Link an issue when applicable; screenshots are unnecessary for console-only changes.

## Agent Safety

Keep credentials and private instructions confidential. Delete files only by explicit, individual paths. If bulk deletion is necessary, ask the user to perform it manually.
