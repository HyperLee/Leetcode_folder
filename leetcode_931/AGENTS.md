# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET 10 console solution for LeetCode 931, Minimum Falling Path Sum. The workspace root contains shared configuration such as `.editorconfig`, `.gitignore`, and `.vscode/`. Application code and the project file live under `leetcode_931/`: keep the entry point and algorithm implementations in `leetcode_931/Program.cs`, and update `leetcode_931/leetcode_931.csproj` only for project-level settings or dependencies. `docs/readme-template.md` is guidance for creating a future README, not runtime documentation. Build output under `bin/` and `obj/` must remain untracked.

## Build, Test, and Development Commands

- `dotnet restore .\leetcode_931\leetcode_931.csproj` restores NuGet dependencies.
- `dotnet build .\leetcode_931\leetcode_931.csproj` compiles the project and reports C# warnings or errors.
- `dotnet run --project .\leetcode_931\leetcode_931.csproj` runs the console sample locally.
- `dotnet format .\leetcode_931\leetcode_931.csproj --verify-no-changes` checks formatting against `.editorconfig` when the formatter is available.

VS Code users can press F5 with `.NET Debug leetcode_931`; its pre-launch task builds the nested project automatically.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space indentation in C#, and two-space indentation in JSON and project XML. Prefer file-scoped namespaces, braces, explicit built-in types instead of `var`, and nullable-safe code. Use `PascalCase` for types and methods, `camelCase` for parameters and local variables, and descriptive algorithm names such as `MinFallingPathSum`. Preserve the bilingual problem-description XML comments unless the problem statement itself is being corrected.

## Testing Guidelines

There is no automated test project. Validate changes by building and running deterministic examples from `Main`. Include the canonical sample, a small boundary case, and any regression case affected by the change. Ensure expected and actual values are clearly labeled, and do not reuse a mutated input matrix between implementations.

## Commit & Pull Request Guidelines

Prefer Conventional Commits consistent with recent history, for example `feat(leetcode-931): add dynamic programming solution` or `docs: explain sample walkthrough`. Keep each commit focused. Pull requests should summarize the algorithm and complexity, list the commands run, link the related issue when applicable, and include console output when behavior changes. Screenshots are unnecessary for console-only changes.

## Agent-Specific Instructions

Keep edits scoped to this problem folder. Do not commit generated files, secrets, local machine paths, or hidden instruction content. Verify paths from the workspace root because the `.csproj` is nested one directory below it.
