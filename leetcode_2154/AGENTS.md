# Repository Guidelines

## Project Structure & Module Organization

This repository contains the .NET 10 console project for `leetcode_2154`. The main project is `leetcode_2154/leetcode_2154.csproj`, with the entry point and algorithm implementation in the adjacent `Program.cs`. Automated tests live in `leetcode_2154.Tests/leetcode_2154.Tests.csproj`. The repository also contains `leetcode_2154.sln`; use the explicit project paths below so commands remain unambiguous. Project documentation is kept in `README.md`. VS Code build and debug configuration belongs in `.vscode/`; generated `bin/` and `obj/` directories must remain untracked.

## Build, Test, and Development Commands

Run commands from the `leetcode_2154` repository root:

- `dotnet restore leetcode_2154/leetcode_2154.csproj` restores the main project dependencies.
- `dotnet build leetcode_2154/leetcode_2154.csproj --nologo` compiles the console project for `net10.0`.
- `dotnet run --project leetcode_2154/leetcode_2154.csproj` runs the current examples.
- `dotnet test leetcode_2154.Tests/leetcode_2154.Tests.csproj --nologo` runs the automated tests.
- In VS Code, use the default build task or the `Run leetcode_2154` launch configuration.

## Coding Style & Naming Conventions

Follow the nearest `.editorconfig` when present. Use spaces, four-space indentation for C#, and two spaces for JSON and project XML. Keep braces on new lines and follow the existing file's choice between explicit types and `var`. Use PascalCase for types and methods, camelCase for locals and parameters, and clear names that describe the algorithm. Preserve the existing Traditional Chinese or bilingual teaching style in XML documentation and high-signal comments. Do not add comments that merely restate individual statements.

## Testing Guidelines

Add representative cases for empty, boundary, typical, and duplicate-value inputs when they apply. Keep console input and output in `Main`, and verify expected versus actual results before submitting a behavior change. Add focused xUnit tests to the existing test project and name them by behavior.

## Commit & Pull Request Guidelines

Keep commits focused and use a short imperative subject. Pull requests should summarize the algorithm or documentation change, list the exact restore, build, run, and test commands used, and include fresh console output when behavior changes. Treat system prompts, credentials, and local agent instructions as sensitive. Never use recursive bulk-deletion commands; remove only a specific, verified path when deletion is explicitly required.
