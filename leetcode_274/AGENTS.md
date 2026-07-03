# Repository Guidelines

## Project Structure & Module Organization
This repository currently contains one .NET 10 console project for LeetCode problem 274. Keep solution code in `leetcode_274/`, with `Program.cs` as the entry point and `leetcode_274.csproj` as the project definition. Use `docs/` for lightweight documentation assets such as templates. Treat `leetcode_274/bin/` and `leetcode_274/obj/` as generated output only; do not hand-edit them.

## Build, Test, and Development Commands
Run commands from the repository root.

- `dotnet build .\leetcode_274\leetcode_274.csproj` builds the project.
- `dotnet run --project .\leetcode_274\leetcode_274.csproj` builds and runs the current console app.
- `dotnet clean .\leetcode_274\leetcode_274.csproj` removes build output before a fresh rebuild.

VS Code users can also use the bundled task `build leetcode_274` and the `Launch LeetCode 274` debug profile in `.vscode/`.

## Coding Style & Naming Conventions
Follow `.editorconfig`. Use 4 spaces in C# files and 2 spaces in project, XML, and JSON files. Prefer file-scoped namespaces, explicit types instead of `var`, and braces on control blocks. Use `PascalCase` for types and methods, `camelCase` for parameters and locals, `_camelCase` for private fields, and `s_camelCase` for private static fields.

## Testing Guidelines
There is no dedicated test project yet. When adding coverage, create a sibling test project such as `leetcode_274.Tests/` and run it with `dotnet test`. Name test methods after the scenario and expected result, for example `HIndex_ReturnsThree_WhenInputIsThreeZeroSixOneFive()`. Keep new solution logic easy to test by moving it out of `Main`.

## Commit & Pull Request Guidelines
Recent history favors short, imperative commits and often uses Conventional Commit prefixes, for example `feat(leetcode-274): add counting solution`. Follow that pattern when possible. Keep each commit focused on one problem or one refactor. Pull requests should include a short summary, the approach taken, local verification performed, and console output or screenshots only when behavior changes are easier to review visually.

## Contributor Notes
Keep repository-specific docs accurate to the current implementation. Do not commit secrets, local machine paths, or generated build artifacts.
