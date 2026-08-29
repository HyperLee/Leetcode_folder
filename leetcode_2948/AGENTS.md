# Repository Guidelines

## Project Structure & Module Organization

This directory contains a single .NET 10 console project for LeetCode 2948, “Make Lexicographically Smallest Array by Swapping Elements.” Keep the solution entry point in `leetcode_2948/Program.cs` and project metadata in `leetcode_2948/leetcode_2948.csproj`. Supporting documentation templates belong in `docs/`; `docs/readme-template.md` describes the repository’s expected initial README workflow. There is currently no dedicated test or asset directory. Generated `bin/` and `obj/` folders are ignored and should not be committed.

## Build, Run, and Development Commands

Run these commands from the current directory:

```bash
dotnet restore leetcode_2948/leetcode_2948.csproj
dotnet build leetcode_2948/leetcode_2948.csproj --no-restore
dotnet run --project leetcode_2948/leetcode_2948.csproj
```

Restore resolves SDK dependencies, build compiles the project, and run executes the console program. Use `dotnet format leetcode_2948/leetcode_2948.csproj --verify-no-changes --no-restore` to check formatting after C# changes. `dotnet test` is not currently applicable because this project has no solution or test project.

## Coding Style & Naming Conventions

Follow the parent `.editorconfig`: use four spaces for C# indentation, braces, and file-scoped namespaces. Keep nullable reference types and implicit usings enabled in the project file. Use PascalCase for types and methods and camelCase for parameters and local variables. Keep XML documentation accurate; escape XML-sensitive operators such as `<=` as `&lt;=` inside documentation comments.

## Testing Guidelines

No automated test framework or coverage threshold is configured. Treat a successful build followed by a direct console run as the current smoke test, and exercise representative edge cases when changing the algorithm. If a test project is introduced, document its command and conventions here before making `dotnet test` part of the required workflow.

## Commit & Pull Request Guidelines

Recent history uses short, imperative, sentence-case subjects such as `Add ...`, `Rename ...`, and `Clarify ...`; no strict Conventional Commits prefix is established. Keep commits focused. Pull requests should summarize the algorithm or documentation change, list validation commands and results, link a related issue when applicable, and exclude generated output. Screenshots are unnecessary for this console project unless documentation later adds a visual interface.
