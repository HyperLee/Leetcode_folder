# Repository Guidelines

## Project Structure & Module Organization
This repository contains a single C# console solution for LeetCode 3660.
The root `leetcode_3660.sln` references the application project in `leetcode_3660/`.

- `leetcode_3660/Program.cs` is the current entry point and contains the solution methods plus console sample cases.
- `leetcode_3660/leetcode_3660.csproj` targets `net10.0` with nullable reference types and implicit usings enabled.
- `README.md` documents the problem and solution approach in Traditional Chinese.
- `.editorconfig` defines C# formatting, naming, and analyzer style rules.
- `.vscode/tasks.json` and `.vscode/launch.json` provide build, run, and debug shortcuts.

There is no dedicated test project in the current tree. If tests are added, prefer a sibling project such as `leetcode_3660.Tests/` and include it in `leetcode_3660.sln`.

## Build, Test, and Development Commands
Run commands from the repository root unless noted otherwise.

- `dotnet restore leetcode_3660.sln` restores NuGet dependencies.
- `dotnet build leetcode_3660.sln -c Debug` builds the solution.
- `dotnet run --project leetcode_3660/leetcode_3660.csproj -c Debug` runs the console app and prints the sample cases.
- `dotnet format leetcode_3660.sln` applies `.editorconfig` formatting when the SDK command is available.
- `dotnet test leetcode_3660.sln` runs tests after a test project has been added to the solution.

## C# Style & Maintainability
Follow `.editorconfig` and use the latest C# language version available with .NET 10, currently C# 14. Use 4-space indentation for C# and place opening braces on new lines for all code blocks.

Prefer explicit types over `var` unless the type is truly obvious from the right-hand side. Prefer file-scoped namespaces, single-line using directives, pattern matching, switch expressions, collection expressions, and `nameof` where they make the code clearer. Keep final `return` statements on their own line.

Keep solution methods small enough to reason about in one pass. Add clear, concise comments for each method that explain the algorithm or design decision, especially why a boundary condition or optimization is correct. Avoid comments that restate the code.

## Naming Conventions
Use PascalCase for namespaces, classes, methods, properties, enums, and public fields. Prefix interface names with `I`, for example `ISolver`, if interfaces are introduced.

Use camelCase for locals and parameters. Use `_camelCase` for private instance fields and `s_camelCase` for private static fields.

## Nullable Reference Types & Edge Cases
Keep nullable annotations accurate. Declare values non-nullable when they are required, validate nullable entry-point inputs before use, and do not silence nullable warnings without a specific reason.

Use `is null` and `is not null` instead of `== null` and `!= null`. Trust the type system; do not add redundant null checks when the compiler already proves a value is non-null.

Algorithm implementations should handle minimal inputs clearly, including empty arrays, single-element arrays, duplicate values, monotonic sequences, and boundary integer values when relevant.

## Documentation
Create XML documentation comments for public APIs. Include `<example>` and `<code>` blocks when an example clarifies expected input and output. For LeetCode solution methods, document the problem intent, algorithm idea, and time and space complexity.

Keep README updates aligned with the implemented solution. If a second solution method is added, explain how it differs from the primary method and why it is useful.

## Testing Guidelines
No coverage threshold is currently configured. For new algorithm work, add focused unit tests when practical, covering normal cases, edge cases, and malformed or minimal input. Name test methods descriptively, for example `MaxValue_ReturnsBlockMaximums_WhenArrayContainsDisconnectedIntervals`.

Keep test data small and readable. Do not add `Act`, `Arrange`, or `Assert` comments in tests. If a future test framework is introduced, document it here and keep tests runnable through `dotnet test leetcode_3660.sln`.

Until a test project exists, keep representative console sample cases in `Program.cs` so `dotnet run` can quickly compare solution variants.

## Dependencies
Avoid external dependencies for LeetCode algorithm solutions unless they are necessary. If a library is introduced, document its purpose in comments or README text and keep the dependency scoped to the project that needs it.

## Commit & Pull Request Guidelines
Recent history uses short Conventional Commit-style prefixes such as `feat:` and `fix:`, with concise Chinese descriptions. Continue that style, for example `feat: 新增邊界案例測試` or `fix: 修正空輸入處理`.

Pull requests should describe the behavior change, list verification commands run, and link any related issue. Include console output only when it clarifies changed behavior. Avoid committing generated `bin/` or `obj/` outputs.
