# Repository Guidelines

## Project Structure & Module Organization
This repository is a small .NET console solution for LeetCode problem 27. Keep source code in `leetcode_027/`, with the main implementation in `leetcode_027/Program.cs` and project settings in `leetcode_027/leetcode_027.csproj`. Use `docs/` for templates and planning notes, and keep editor/task settings in `.vscode/`. Treat `leetcode_027/bin/` and `leetcode_027/obj/` as generated output, not hand-edited source.

## Build, Test, and Development Commands
Use the existing .NET CLI workflow:

- `dotnet build leetcode_027/leetcode_027.csproj` builds the console project.
- `dotnet run --project leetcode_027/leetcode_027.csproj` runs the current demo entry point and should print the expected sample output.
- In VS Code, the default build task is `build leetcode_027`, and the launch profile `.NET Debug leetcode_027` uses that task automatically.

## Coding Style & Naming Conventions
Follow `.editorconfig` exactly. Use 4 spaces for C# indentation and 2 spaces for JSON, XML, and project files. Prefer file-scoped namespaces, explicit types instead of `var` when the type is not obvious, and braces on control blocks. Use PascalCase for types and methods, camelCase for parameters and locals, `_camelCase` for private fields, and `s_camelCase` for private static fields.

## Testing Guidelines
There is no dedicated test project in this repository yet. Until one is added, validate changes with `dotnet build` and `dotnet run`, then compare behavior against the LeetCode examples documented in code comments. If you add tests, place them in a separate sibling project such as `leetcode_027.Tests` and name test methods after the scenario they verify.

## Commit & Pull Request Guidelines
Recent history mixes short Chinese summaries (`新增解法`, `調整行數`) with Conventional Commit style (`feat: add ...`). Keep commit subjects to one line, imperative, and specific to the problem being solved. For pull requests, include the target LeetCode problem, a short summary of the algorithm or refactor, manual verification steps, and console output screenshots only when the runtime output changes.
