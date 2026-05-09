# Repository Guidelines

## Project Structure & Module Organization

This repository contains a small .NET console solution for LeetCode problem 142. The solution file is `leetcode_142.sln`; the only project currently lives in `leetcode_142/`.

- `leetcode_142/Program.cs`: application entry point and current solution workspace.
- `leetcode_142/leetcode_142.csproj`: .NET project targeting `net10.0` with nullable reference types and implicit usings enabled.
- `.editorconfig`: repository-wide formatting and C# style rules.

If the solution grows, keep production code in `leetcode_142/` and add tests in a separate project such as `leetcode_142.Tests/`.

## Build, Test, and Development Commands

- `dotnet restore leetcode_142.sln`: restore NuGet dependencies.
- `dotnet build leetcode_142.sln`: compile the solution and surface analyzer/style warnings.
- `dotnet run --project leetcode_142/leetcode_142.csproj`: run the console app locally.
- `dotnet test leetcode_142.sln`: run tests after a test project is added.
- `dotnet format leetcode_142.sln`: apply formatting from `.editorconfig` when the formatter is available.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C#, LF line endings, braces on new lines, and explicit types instead of `var` unless project rules change. Use PascalCase for types, methods, properties, events, and public fields. Use camelCase for parameters, local variables, and local constants. Private fields should use `_camelCase`.

Keep LeetCode solution code focused and readable. Prefer small helper methods for linked-list setup, traversal, and cycle detection rather than embedding all logic in `Main`.

## Testing Guidelines

No test project is present yet. When adding tests, prefer xUnit or NUnit in `leetcode_142.Tests/`, name files after the class or behavior under test, and include edge cases: empty list, one-node list, no cycle, cycle at head, and cycle in the middle. Run `dotnet test leetcode_142.sln` before opening a pull request.

## Commit & Pull Request Guidelines

Recent history uses concise Traditional Chinese commit summaries. Keep the first line specific and action-oriented, for example `新增 linked list cycle 測試案例` or `修正空節點輸入處理`. Pull requests should describe the algorithmic change, list tested cases or commands run, and link any related issue. Include screenshots only when console output or tooling changes need visual confirmation.

## Security & Agent Notes

Do not commit secrets, API keys, private prompts, or local environment values. Avoid broad deletion commands; delete only explicit single-path files when cleanup is required.
