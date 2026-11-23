# Repository Guidelines

## Project Structure & Module Organization

This directory contains the C# console project for LeetCode 1352, “Product of the Last K Numbers,” within a larger repository of independent LeetCode exercises.

- `leetcode_1352/Program.cs`: entry point and bilingual English/Traditional Chinese problem documentation. Currently, `Main` only prints `Hello, World!`; the solution is not implemented.
- `leetcode_1352/leetcode_1352.csproj`: executable targeting `net11.0`, with nullable reference types and implicit usings enabled.
- `.editorconfig`, `.gitignore`, and `.vscode/`: formatting, ignore rules, and editor configuration.

There are no dedicated test projects or runtime assets. Keep changes scoped to this exercise and exclude generated `bin/` and `obj/` files.

## Build, Test, and Development Commands

Run these commands from this directory with a .NET SDK supporting `net11.0`:

```powershell
dotnet build ./leetcode_1352/leetcode_1352.csproj
dotnet run --project ./leetcode_1352/leetcode_1352.csproj
dotnet format ./leetcode_1352/leetcode_1352.csproj --verify-no-changes
```

These commands build the project, execute the console entry point, and check formatting against EditorConfig rules, respectively. A successful run currently verifies only the placeholder program.

## Coding Style & Naming Conventions

Follow the local `.editorconfig`: four spaces for C#, two for project files and JSON, file-scoped namespaces, explicit variable types, and braces on new lines. Use PascalCase for types and methods and camelCase for parameters and local variables. Retain the `leetcode_1352` namespace and preserve the bilingual problem summary and source links. Explain algorithm invariants and complexity when adding a solution.

## Testing Guidelines

No test framework or coverage threshold is configured. For solution changes, add repeatable checks with explicit expected results; console output alone is insufficient. Cover the official example, single-element queries, zeros inside and outside the queried suffix, consecutive zeros, and queries spanning the entire stream. If introducing a test project, document its command and use descriptive names such as `GetProduct_WindowContainsZero_ReturnsZero`.

## Commit & Pull Request Guidelines

Recent parent-repository commits use concise descriptive subjects in English or Chinese, such as `Clarify DP state in problem 3041 README`; no consistent Conventional Commits prefix is evident. Name this problem and the concrete change. Keep commits focused. PRs should describe behavior, algorithm complexity, validation commands and results, and any remaining limitations. Link an issue when applicable.
