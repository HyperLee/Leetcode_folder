# Repository Guidelines

## Project Structure & Module Organization
This repository contains one .NET console project for LeetCode 205. The main source lives in `leetcode_205/`: `Program.cs` is the entry point and solution file, and `leetcode_205.csproj` targets `net10.0`. Shared tooling lives at the repo root in `.editorconfig`, `.gitignore`, `.gitattributes`, and `.vscode/`. Use `docs/readme-template.md` as the starting point for problem writeups. There is no separate test project or assets folder today.

## Build, Test, and Development Commands
Use explicit project paths from the repository root because there is no solution file here.

- `dotnet build leetcode_205/leetcode_205.csproj` builds the console app and matches the default VS Code build task.
- `dotnet run --project leetcode_205/leetcode_205.csproj` runs `Main` for a quick behavior check.
- `dotnet test` at the repo root does not work yet because no solution or test project exists there (`MSB1003`).

For local debugging, `.vscode/launch.json` starts `leetcode_205/bin/Debug/net10.0/leetcode_205.dll` after running the `build leetcode_205` task.

## Coding Style & Naming Conventions
Follow `.editorconfig` exactly: use 4 spaces in C# files and 2 spaces in JSON, XML, and project files. Keep file-scoped namespaces, nullable-aware code, and braces on control blocks. Prefer explicit types over `var` unless the type is obvious from the right-hand side. Use PascalCase for types and methods, for example `Program`, `IsIsomorphic`, or `RunSamples`.

## Testing Guidelines
There is no automated test harness yet. Validate changes with `dotnet build` and `dotnet run`, and add representative sample cases in `Main` when algorithm behavior changes. If you add a real test project later, place it beside `leetcode_205/` and run it with an explicit path such as `dotnet test leetcode_205.Tests/leetcode_205.Tests.csproj`.

## Commit & Pull Request Guidelines
Recent history mixes short Chinese summaries with Conventional Commit-style messages such as `feat: ...`. Keep commits focused and imperative, ideally mentioning the problem number, for example `feat: add isomorphic string sample cases`. If you open a pull request, include a short summary, the commands you ran, and any expected console output changes. Run `git diff --check` before submitting to catch whitespace issues.
