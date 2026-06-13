# Repository Guidelines

## Project Structure & Module Organization
This repository contains one .NET console project for LeetCode problem 191. The main source lives in `leetcode_191/`, with the entry point in `leetcode_191/Program.cs` and project settings in `leetcode_191/leetcode_191.csproj`. Shared documentation helpers live in `docs/`, and local VS Code build/debug tasks are stored in `.vscode/`.

## Build, Test, and Development Commands
Use the project file directly because there is no solution file at the repository root.

- `dotnet build leetcode_191/leetcode_191.csproj` builds the console app for `net10.0`.
- `dotnet run --project leetcode_191/leetcode_191.csproj` runs the current sample implementation.
- `dotnet test <test-project.csproj>` is the expected test command once a dedicated test project exists; `dotnet test` at the root does not work yet because no solution or test project is present.

## Coding Style & Naming Conventions
Follow `.editorconfig` exactly: use 4 spaces in C# files and 2 spaces in JSON, XML, and project files. Prefer explicit types over `var`, file-scoped namespaces, and PascalCase for types and methods. Keep nullable-safe code enabled, and preserve concise XML documentation when adding problem statements, links, or bilingual notes above solution code.

## Testing Guidelines
There is no automated test project yet, so contributors should validate changes with `dotnet build` and `dotnet run` before opening a PR. If you add tests, place them in a separate project such as `leetcode_191.Tests/`, mirror the production namespace, and name tests after the target behavior, for example `HammingWeight_Returns3_ForBinary1011`.

## Commit & Pull Request Guidelines
Recent history shows short, focused commits, sometimes in Chinese and sometimes with Conventional Commit prefixes such as `feat:`. Keep commit messages brief, imperative, and scoped to one change, ideally mentioning the problem number. PRs should include a short summary, the commands you ran, the expected console output or behavior change, and links to the relevant LeetCode prompt. Screenshots are only needed for documentation or editor-config changes that benefit from visual proof.

## Security & Repo Hygiene
Do not commit secrets, machine-specific paths, or SDK-generated noise outside the project. Keep repository-level guidance files current when workflow or structure changes.
