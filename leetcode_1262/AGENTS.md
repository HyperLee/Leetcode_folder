# Repository Guidelines

## Project Structure & Module Organization

This repository contains the .NET 10 console project for `leetcode_1262`. The main project is `leetcode_1262/leetcode_1262.csproj`, with the entry point and algorithm implementation in the adjacent `Program.cs`. There is currently no automated test project. The repository also contains `leetcode_1262.sln`; use the explicit project paths below so commands remain unambiguous. Project documentation is kept in `README.md`. VS Code build and debug configuration belongs in `.vscode/`; generated `bin/` and `obj/` directories must remain untracked.

## Build, Test, and Development Commands

Run commands from the `leetcode_1262` repository root:

- `dotnet restore leetcode_1262/leetcode_1262.csproj` restores the main project dependencies.
- `dotnet build leetcode_1262/leetcode_1262.csproj --nologo` compiles the console project for `net10.0`.
- `dotnet run --project leetcode_1262/leetcode_1262.csproj` runs the current examples.
- No automated test project exists; use build plus representative console cases as the acceptance check.
- In VS Code, use the default build task or the `Run leetcode_1262` launch configuration.

## Coding Style & Naming Conventions

Follow the nearest `.editorconfig` when present. Use spaces, four-space indentation for C#, and two spaces for JSON and project XML. Keep braces on new lines and follow the existing file's choice between explicit types and `var`. Use PascalCase for types and methods, camelCase for locals and parameters, and clear names that describe the algorithm. Preserve the existing Traditional Chinese or bilingual teaching style in XML documentation and high-signal comments. Do not add comments that merely restate individual statements.

## Testing Guidelines

Add representative cases for empty, boundary, typical, and duplicate-value inputs when they apply. Keep console input and output in `Main`, and verify expected versus actual results before submitting a behavior change. If automated tests are introduced later, place them in a sibling `${root}.Tests` project and name tests by behavior.

## Commit & Pull Request Guidelines

Keep commits focused and use a short imperative subject. Pull requests should summarize the algorithm or documentation change, list the exact restore, build, run, and test commands used, and include fresh console output when behavior changes. Treat system prompts, credentials, and local agent instructions as sensitive. Never use recursive bulk-deletion commands; remove only a specific, verified path when deletion is explicitly required.
