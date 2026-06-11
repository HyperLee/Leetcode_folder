# Repository Guidelines

## Project Structure & Module Organization
This repository is a small .NET solution for LeetCode problem 169. The runnable project lives in `leetcode_169/`, with the entry point in `leetcode_169/Program.cs` and project settings in `leetcode_169/leetcode_169.csproj`. Use `docs/` for authoring templates and supporting notes, not runtime code. Treat `leetcode_169/bin/` and `leetcode_169/obj/` as generated output; do not edit them manually. VS Code build and debug tasks are stored in `.vscode/`.

## Build, Test, and Development Commands
Run commands from the repository root.

- `dotnet build leetcode_169/leetcode_169.csproj` builds the console app and validates compilation.
- `dotnet run --project leetcode_169/leetcode_169.csproj` runs the current solution locally.
- `dotnet format leetcode_169/leetcode_169.csproj` applies SDK-based formatting when available in your environment.

The existing VS Code task `build leetcode_169` runs the same project build command.

## Coding Style & Naming Conventions
Follow `.editorconfig`. Use spaces for indentation; C# files use 4-space indents. Prefer file-scoped namespaces, explicit types over `var`, and clear PascalCase names for classes and methods. Keep solution methods small and focused. When adding helper methods for algorithms, use descriptive names such as `FindMajorityElement` instead of generic names like `Solve`.

## Testing Guidelines
There is no dedicated test project yet. For now, verify every change with `dotnet build` and `dotnet run --project leetcode_169/leetcode_169.csproj`. If you add tests, place them in a separate test project under `tests/` and use names such as `MajorityElement_ReturnsExpectedValue_WhenInputHasClearMajority`.

## Commit & Pull Request Guidelines
Recent history uses short, imperative commit titles, often in Traditional Chinese, with occasional Conventional Commit prefixes such as `feat:`. Keep commit messages concise and specific to one change, for example `新增 Boyer-Moore 解法` or `feat: add sample cases`. Pull requests should summarize the algorithm, note time and space complexity, list validation commands you ran, and include sample input/output when behavior changes.

## Repository Hygiene
Keep machine-specific output out of commits. Avoid editing generated files, and do not add secrets, personal tokens, or local-only paths to source or docs.
