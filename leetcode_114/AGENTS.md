# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single .NET console project for LeetCode problem 114.

- `leetcode_114/` holds the application code and project file.
- `leetcode_114/Program.cs` is the current entry point and the main place for solution logic.
- `docs/` stores repository documentation and templates.
- `.vscode/` contains local build and debug task settings.
- `leetcode_114/bin/` and `leetcode_114/obj/` are generated build outputs and should not be edited manually.

## Build, Test, and Development Commands

Use the project file directly from the repository root:

- `dotnet build leetcode_114/leetcode_114.csproj` builds the console app.
- `dotnet run --project leetcode_114/leetcode_114.csproj` runs the current solution entry point.

VS Code users can also use the bundled tasks: `build leetcode_114` and `run leetcode_114`.

## Coding Style & Naming Conventions

Follow `.editorconfig` as the source of truth.

- Use 4 spaces for C# indentation.
- Keep namespaces file-scoped where practical.
- Prefer explicit types over `var` unless the type is obvious.
- Use `PascalCase` for types and methods, `camelCase` for parameters and locals, `_camelCase` for private fields, and `s_camelCase` for private static fields.
- Keep new solution helpers small and focused; if logic grows, split it into additional C# files under `leetcode_114/`.

## Testing Guidelines

There is no automated test project yet. For non-trivial logic, add a sibling test project such as `leetcode_114.Tests/` and run it with `dotnet test` after it exists. Name tests after the behavior they verify, for example `Flatten_ProducesPreorderRightChain`.

## Commit & Pull Request Guidelines

Recent history mixes short Traditional Chinese summaries with conventional-style messages such as `feat: ...`. Keep commits short, imperative, and scoped to one change. Examples:

- `feat: add iterative flatten solution`
- `補上 TreeNode 輔助方法`

Pull requests should include a short problem summary, the approach taken, commands run for verification, and sample output when console behavior changes.

## Repository Hygiene

Do not commit local editor files beyond the shared `.vscode` settings, and do not include generated `bin/` or `obj/` changes in reviews.
