# Repository Guidelines

## Project Structure & Module Organization

Application code is under `leetcode_1160/`: `Program.cs` is the entry point, and `leetcode_1160.csproj` targets .NET 10 with nullable types and implicit usings enabled. Do not commit generated `bin/` or `obj/` directories. Planning documents and the initial README template belong in `docs/`; editor and debug configuration belongs in `.vscode/`. Root-level `.editorconfig`, `.gitattributes`, and `.gitignore` define formatting, line endings, and exclusions. There is currently no test project or asset directory.

## Build, Test, and Development Commands

Run commands from this repository root:

- `dotnet restore leetcode_1160/leetcode_1160.csproj` restores NuGet dependencies.
- `dotnet build leetcode_1160/leetcode_1160.csproj` compiles the console application and reports analyzer/compiler warnings.
- `dotnet run --project leetcode_1160/leetcode_1160.csproj` executes the sample runner locally.
- `dotnet format leetcode_1160/leetcode_1160.csproj --verify-no-changes` checks formatting without rewriting files.

Before submitting changes, run build and run sequentially so both commands do not compete for the same output files.

## Coding Style & Naming Conventions

Treat `.editorconfig` as the source of truth. Use four spaces for C# and two for project and JSON files, with braces on new lines. Use `PascalCase` for types and methods, `camelCase` for parameters and locals, `_camelCase` for private fields, and `s_camelCase` for private static fields. Retain the file-scoped `leetcode_1160` namespace and keep nullable warnings clean. Preserve the problem title, links, and XML comments on `Main`.

## Documentation Guidelines

Use `docs/readme-template.md` only when creating the repository's first `README.md`; stop if a README already exists. Keep documentation aligned with the current implementation, verified commands, and actual console output. Do not invent features, badges, assets, or test results. Preserve the project's established language, including Traditional Chinese where already used.

## Testing Guidelines

No automated test framework is configured yet. Validate changes with `dotnet build` and deterministic examples in `Main`, including the LeetCode examples and edge cases such as repeated letters or words that cannot be formed. If a test project is introduced, name it `leetcode_1160.Tests`, use test names such as `CountCharacters_ReturnsExpectedTotal`, and run it with `dotnet test`.

## Commit & Pull Request Guidelines

Recent history favors short, imperative messages and Conventional Commit prefixes. Prefer messages such as `feat: implement count characters solution`, `test: add repeated-letter cases`, or `docs: explain frequency counting`. Pull requests should summarize the approach, list verification commands and results, link the relevant issue when available, and include console output when runtime behavior changes. Keep each PR focused on one problem or documentation task.
