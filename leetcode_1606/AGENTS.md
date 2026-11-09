# Repository Guidelines

## Project Structure & Module Organization

This directory contains a C# console project for LeetCode 1606, “Find Servers That Handled Most Number of Requests,” within the larger `Leetcode_folder` Git repository.

- `leetcode_1606/Program.cs`: entry point and bilingual English/Traditional Chinese problem description. Currently, `Main` only prints `Hello, World!`; the solution is not implemented.
- `leetcode_1606/leetcode_1606.csproj`: .NET 10 executable with nullable reference types and implicit usings enabled.
- `docs/readme-template.md`: instructions for creating an initial README.
- `.editorconfig`, `.gitignore`, and `.gitattributes`: formatting and source-control settings; `.vscode/` contains editor tasks and launch settings.

There is currently no dedicated test project or asset directory. Keep changes scoped to this problem directory.

## Build, Test, and Development Commands

Run commands from this directory with the .NET 10 SDK installed:

```powershell
dotnet restore leetcode_1606/leetcode_1606.csproj
dotnet build leetcode_1606/leetcode_1606.csproj
dotnet run --project leetcode_1606/leetcode_1606.csproj
dotnet format leetcode_1606/leetcode_1606.csproj --verify-no-changes
```

These restore dependencies, compile, run the console application, and check formatting respectively. Use the explicit project path because this directory has no solution file.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four spaces for C#, braces on separate lines, and file-scoped namespaces. Preserve the existing `leetcode_1606` namespace. Use PascalCase for types and methods, camelCase for parameters and local variables, and `_camelCase` for private instance fields. Preserve bilingual problem documentation and explain algorithm invariants and time/space complexity when adding a solution.

## Testing Guidelines

No testing framework or coverage threshold is configured. A successful build or the current greeting does not establish algorithm correctness. When implementing the solution, add repeatable checks for official examples, one server, dropped requests, wraparound assignment, tied busiest servers, and completion exactly at arrival time. Compare returned server IDs without assuming order. Name checks descriptively, for example `BusiestServers_AllServersBusy_DropsRequest`, and document their execution command.

## Commit & Pull Request Guidelines

Recent repository commits use concise imperative subjects, such as `Add dictionary-based server counting solution`; follow that pattern. Keep commits focused. Pull requests should identify problem 1606, describe algorithm and complexity changes, and list validation commands and results. Link a related issue when applicable. Exclude generated `bin/` and `obj/` output and unrelated problem changes.
