# Repository Guidelines

## Project Structure & Module Organization

This directory contains a C# console project for LeetCode 2747, **Count Zero Request Servers**.

- `leetcode_2747/Program.cs`: entry point and bilingual problem description. Currently, `Main` only prints `Hello, World!`; the algorithm is not implemented.
- `leetcode_2747/leetcode_2747.csproj`: .NET 10 executable with nullable reference types and implicit usings enabled.
- `docs/readme-template.md`: instructions for initial README creation.
- `.editorconfig`: formatting and naming rules; `.vscode/`: editor launch and task settings.

There is currently no test project or asset directory. Keep algorithm code in the existing project and exclude generated `bin/` and `obj/` files from commits.

## Build, Test, and Development Commands

Run these commands from this directory with the .NET 10 SDK installed:

```powershell
dotnet build ./leetcode_2747/leetcode_2747.csproj
dotnet run --project ./leetcode_2747/leetcode_2747.csproj
dotnet format ./leetcode_2747/leetcode_2747.csproj --verify-no-changes
```

These commands compile the project, execute `Main`, and check formatting against configured rules, respectively. No separate linting tool is configured.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four-space C# indentation, two-space project/XML and JSON indentation, braces on separate lines, and file-scoped namespaces. Prefer explicit variable types. Use PascalCase for types and methods, camelCase for parameters and local variables, and the existing `leetcode_2747` namespace. Preserve the English and Traditional Chinese problem documentation. Explain algorithm invariants and time/space complexity when adding solutions.

## Testing Guidelines

No testing framework or coverage threshold is configured. When implementing the solution, add repeatable checks with expected outputs and document how to execute them. Cover both inclusive interval boundaries, repeated requests from one server, unsorted logs and queries, and windows containing zero or all active servers. Confirm results retain the original query order. A successful build alone does not validate algorithm correctness.

## Commit & Pull Request Guidelines

Recent enclosing-repository commits use short, action-oriented subjects, such as `Add distance value solutions`; English and Chinese both appear. Follow this style and keep changes focused on this problem. Pull requests should explain the approach, complexity, and verification results, link relevant issues when available, and identify any remaining limitations. Screenshots are unnecessary for console-only changes.
