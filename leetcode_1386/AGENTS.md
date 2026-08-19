# Repository Guidelines

## Project Structure

This directory contains one .NET 10 console project for LeetCode 1386, Cinema Seat Allocation. The implementation and entry point are in `leetcode_1386/Program.cs`; project metadata is in `leetcode_1386/leetcode_1386.csproj`. The checked-in `.vscode/tasks.json` and `.vscode/launch.json` provide the default build and `coreclr` debug workflows. `docs/readme-template.md` is the template for future project documentation. There is currently no test project or asset directory; generated `bin/` and `obj/` files are ignored.

## Build, Run, and Development Commands

Run these commands from this directory, passing the nested project explicitly:

```bash
dotnet restore leetcode_1386/leetcode_1386.csproj
dotnet build leetcode_1386/leetcode_1386.csproj
dotnet run --project leetcode_1386/leetcode_1386.csproj
```

After a successful build, `dotnet run --project leetcode_1386/leetcode_1386.csproj --no-build` is a quick smoke check. In VS Code, use the default `build leetcode_1386` task or the `Debug leetcode_1386` launch configuration.

## Coding Style and Naming

Follow `.editorconfig`: use spaces and four-space indentation in C#, two-space indentation in JSON, braces for control blocks, and file-scoped namespaces. Prefer explicit types over `var`. Use PascalCase for classes, methods, and properties; camelCase for parameters and local variables; and `_camelCase` for private fields. Keep the bilingual problem statement and links in XML documentation accurate when editing `Program.cs`.

## Testing Guidelines

No automated test framework or coverage threshold is configured yet. Treat a clean `dotnet build` and a successful `dotnet run` smoke check as the current validation. If you add a test project or deterministic console harness, document expected versus actual results and run the relevant explicit project command; use `dotnet test` once a test project exists.

## Commit and Pull Request Guidelines

History contains concise Chinese messages and Conventional Commit messages such as `feat(leetcode-3471): ...` and `docs(leetcode-2029): ...`. Prefer a focused Conventional Commit, for example `docs(leetcode-1386): add repository guidelines`. Pull requests should explain the change, list validation commands and results, link a related issue when applicable, and avoid unrelated edits or generated artifacts.
