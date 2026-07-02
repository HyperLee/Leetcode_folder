# Repository Guidelines

## Project Structure & Module Organization
This repository currently contains one .NET console project in `leetcode_268/`. The main implementation lives in `leetcode_268/Program.cs`, and the project file is `leetcode_268/leetcode_268.csproj`. Shared documentation helpers live in `docs/`, including `docs/readme-template.md`. Build outputs are generated under `leetcode_268/bin/` and `leetcode_268/obj/`; treat those as generated artifacts, not source files.

## Build, Test, and Development Commands
Use the project-level .NET CLI commands from the repository root:

- `dotnet build leetcode_268/leetcode_268.csproj` builds the console app and validates compilation.
- `dotnet run --project leetcode_268/leetcode_268.csproj` runs the current solution harness in `Program.cs`.
- `dotnet clean leetcode_268/leetcode_268.csproj` removes local build artifacts before a fresh rebuild.

There is no solution file or dedicated test project checked in yet, so keep commands scoped to the project file.

## Coding Style & Naming Conventions
Follow the root `.editorconfig`. Key rules in this repo are:

- Use spaces, with 4-space indentation in C# and 2 spaces in JSON/XML-style files.
- Prefer file-scoped namespaces, explicit types instead of `var`, and braces on their own lines.
- Use PascalCase for types and methods, camelCase for locals and parameters, `_camelCase` for private fields, and `s_camelCase` for private static fields.

Keep solution methods small and problem-focused. If `Program.cs` starts mixing multiple concerns, extract helper classes into the same project folder.

## Testing Guidelines
No automated test project exists yet. For logic changes, verify with `dotnet build` at minimum and add runnable examples in `Program.cs` when helpful. If you introduce broader reusable logic, add a dedicated test project before merging and run it with `dotnet test`.

## Commit & Pull Request Guidelines
Recent history uses short, imperative, problem-scoped messages such as `feat(leetcode-260): add single number iii demo` and `Add Single Number III solutions`. Follow that style: include the LeetCode problem number when relevant and keep each commit focused on one exercise or cleanup.

Pull requests should summarize the problem being solved, the approach taken, and the commands you ran to verify the change. Console-output screenshots are optional; include them only when output formatting is the main change.
