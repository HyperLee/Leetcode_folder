# Repository Guidelines

## Project Structure & Module Organization
The repository is intentionally small. The runnable code lives in `leetcode_189/`, with the entry point in `leetcode_189/Program.cs` and project settings in `leetcode_189/leetcode_189.csproj`. Reusable documentation templates live in `docs/`, currently `docs/readme-template.md`. Keep new solution files close to the project they belong to, and avoid scattering logic across the repo root.

## Build, Test, and Development Commands
Use the .NET CLI from the repository root:

- `dotnet build leetcode_189/leetcode_189.csproj` builds the console app and validates compilation.
- `dotnet run --project leetcode_189/leetcode_189.csproj` runs the current solution locally.
- `dotnet test path/to/YourTests.csproj` should be used once a test project is added; today there is no committed test project, so treat `dotnet build` as the minimum verification step.

## Coding Style & Naming Conventions
Follow the root `.editorconfig`. Use 4 spaces in `*.cs` files and 2 spaces in project or JSON files. Prefer file-scoped namespaces, explicit types instead of `var`, and braces on control-flow blocks. Use `PascalCase` for types and methods, `camelCase` for locals and parameters, `_camelCase` for private fields, and `s_camelCase` for private static fields. Keep the LeetCode problem link and a short problem summary at the top of each solution file when relevant.

## Testing Guidelines
No automated test framework is checked in yet. If you add tests, place them in a dedicated `*.Tests` project and run them by targeting that project file directly with `dotnet test`. Favor small deterministic cases that cover edge conditions such as empty arrays, single-element arrays, and large rotation counts.

## Commit & Pull Request Guidelines
Recent history mixes short Chinese summaries such as `新增解法` with Conventional Commit-style messages like `feat: add samples and README for leetcode 169`. Keep commits short, imperative, and scoped to one problem or documentation change. Pull requests should state the LeetCode problem number, summarize the approach, list verification commands run, and include console output or screenshots only when they clarify behavior.
