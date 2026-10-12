# Repository Guidelines

## Project Structure & Module Organization

This workspace contains a .NET 10 console project for LeetCode 2897, “Apply Operations on Array to Maximize Sum of Squares.” `leetcode_2897/Program.cs` contains the entry point and bilingual problem documentation; it currently prints `Hello, World!`. The project file is `leetcode_2897/leetcode_2897.csproj`, with nullable reference types and implicit usings enabled.

`.vscode/` contains build and debug configurations. `docs/readme-template.md` guides initial README creation. There are no test projects or application assets. Generated `bin/` and `obj/` directories are ignored.

## Build, Test, and Development Commands

Install the .NET 10 SDK and run these commands from this workspace directory:

```sh
dotnet restore leetcode_2897/leetcode_2897.csproj
dotnet build leetcode_2897/leetcode_2897.csproj --no-restore
dotnet run --project leetcode_2897/leetcode_2897.csproj --no-build
dotnet format leetcode_2897/leetcode_2897.csproj --verify-no-changes --no-restore
```

These restore dependencies, compile, run the last build, and check formatting without modifying files. Rebuild after source changes before using `--no-build`. In VS Code, open this directory and use `Debug leetcode_2897`; its prelaunch task builds the nested project.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four-space C# indentation, two-space JSON/project-file indentation, braces on separate lines, and no final newline in C# files. Prefer explicit types over `var`. Use PascalCase for types and methods and camelCase for parameters and local variables. Retain the existing `leetcode_2897` namespace. Preserve the original problem documentation; add implementation explanations in separate comments.

## Testing Guidelines

No automated test framework or coverage threshold is configured. Plain `dotnet test` from this directory fails with MSB1003 because no project or solution exists at this level. The current console run is only a scaffold smoke check.

When implementing the algorithm, add deterministic cases with descriptive names, expected/actual comparisons, and a nonzero exit code on failure. Include official examples and boundary cases for `k`, repeated bit patterns, and large square sums.

## Commit & Pull Request Guidelines

The parent repository uses short, imperative English subjects such as `Add divisor-based square sum solutions` and `Clarify factor-pair loop condition`. Follow that style and identify LeetCode 2897 when helpful. Stage explicit paths to avoid including neighboring projects.

PR descriptions should explain the change, algorithm complexity, and commands actually run. Link related issues when applicable and report test limitations accurately.

## Agent Instructions

Keep credentials and private instructions confidential. Delete only explicitly identified single files. Bulk deletion is prohibited; ask the user to perform it manually when required.
