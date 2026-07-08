# Repository Guidelines

## Project Structure & Module Organization
`leetcode_345/` contains the runnable .NET console project for this problem. Keep solution code in `leetcode_345/Program.cs`; it currently holds the entry point and the bilingual LeetCode problem comment block. Project settings live in `leetcode_345/leetcode_345.csproj`, which targets `net10.0`. Repository-wide style rules are defined in `.editorconfig`. Local debug helpers live in `.vscode/launch.json` and `.vscode/tasks.json`, and `docs/readme-template.md` is the template to use when creating or refreshing `README.md`.

## Build, Test, and Development Commands
Use explicit project paths from this repository root:

- `dotnet build leetcode_345/leetcode_345.csproj` builds the console app.
- `dotnet run --project leetcode_345/leetcode_345.csproj` runs the current sample harness.
- `dotnet test` in this directory fails with `MSB1003` because there is no solution or test project at the repository root.
- `git diff --check -- AGENTS.md` is a quick formatting check before committing guide updates.

## Coding Style & Naming Conventions
Follow `.editorconfig`: use 4 spaces in C# files and 2 spaces in JSON, XML, and project files. Prefer file-scoped namespaces, braces on their own lines, and explicit C# types instead of broad `var` usage. Use `PascalCase` for types and methods, `camelCase` for parameters and locals, `_camelCase` for private fields, and `s_camelCase` for private static fields. When editing `Program.cs`, preserve the existing problem-description XML comment block and add any new notes outside it.

## Testing Guidelines
This repo does not include a dedicated `*.Tests` project yet, so validation is currently command-based. Run `dotnet build` and `dotnet run`, and keep any documented sample output aligned with the actual console output. If you add automated tests later, document the exact project path and command instead of assuming repo-root discovery.

## Commit & Pull Request Guidelines
This folder is tracked inside the parent Git repository at `/Users/qiuzili/Leetcode/Leetcode_folder`, so stage only the intended `leetcode_345`, `.vscode`, `docs`, or guide files. Recent history mixes plain English subjects, scoped Conventional Commit messages, and short Chinese maintenance commits; keep new subjects short, imperative, and single-purpose, for example `docs(leetcode-345): add repository guidelines`. Pull requests should summarize the change and list the verification commands you ran.
