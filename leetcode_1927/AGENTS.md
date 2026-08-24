# Repository Guidelines

## Project Structure & Module Organization

This checkout contains one .NET 10 console project for LeetCode 1927 (Sum Game). The implementation and entry point are in `leetcode_1927/Program.cs`; project metadata is in `leetcode_1927/leetcode_1927.csproj`. VS Code automation lives in `.vscode/launch.json` and `.vscode/tasks.json`. `docs/readme-template.md` is the repository’s template for a future README. There is currently no separate test, asset, or solution directory.

## Build, Test, and Development Commands

Run commands from this directory and name the nested project explicitly:

```bash
dotnet restore leetcode_1927/leetcode_1927.csproj
dotnet build leetcode_1927/leetcode_1927.csproj --nologo
dotnet run --project leetcode_1927/leetcode_1927.csproj --no-build
```

Restore dependencies, build the project, then run the already-built binary. VS Code’s default build task and `coreclr` launch configuration use the same nested project and output path. `dotnet test --nologo` is not applicable until a solution or test project is added; from this directory it returns `MSB1003`.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four-space indentation for C#, spaces instead of tabs, file-scoped namespaces, braces for blocks, and explicit built-in types instead of `var`. Use PascalCase for types and methods, camelCase for parameters and locals, and `_camelCase` for private fields. Keep XML documentation and algorithm comments focused, accurate, and consistent with the existing bilingual problem description.

## Testing Guidelines

No automated test framework or coverage threshold is configured. For every change, run the build and the executable smoke test above, and inspect the output for the intended result. If tests are introduced, keep them in a clearly named test project and document its explicit `dotnet test <path>` command here.

## Commit & Pull Request Guidelines

Recent history mixes Conventional Commit prefixes such as `feat(leetcode-3622): ...`, `fix(leetcode-2029): ...`, and `docs(...)` with concise English or Traditional Chinese subjects. Prefer `type(scope): imperative summary`, for example `docs(leetcode-1927): add contributor guidance`. Pull requests should explain the algorithm or documentation change, list validation commands and results, link a related issue when available, and keep unrelated working-tree edits out of the change.
