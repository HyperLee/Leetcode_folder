# Repository Guidelines

## Project Structure & Module Organization

This checkout contains one nested SDK-style .NET console project for LeetCode 1477. The outer directory stores contributor and configuration files: `.vscode/`, `docs/readme-template.md`, `.editorconfig`, `.gitattributes`, `.gitignore`, and this guide. Application source and project metadata live in `leetcode_1477/Program.cs` and `leetcode_1477/leetcode_1477.csproj`. There is currently no dedicated test project or asset directory. Keep generated `bin/` and `obj/` output uncommitted.

## Build, Test, and Development Commands

Run these commands from the outer directory:

```powershell
dotnet restore .\leetcode_1477\leetcode_1477.csproj
dotnet build .\leetcode_1477\leetcode_1477.csproj --nologo
dotnet run --project .\leetcode_1477\leetcode_1477.csproj
```

Restore downloads SDK dependencies; build compiles the `net10.0` project; run executes the console entry point. VS Code `F5` uses `.vscode/launch.json`, invokes the `build leetcode_1477` pre-launch task, and passes no command-line arguments.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four spaces for C#, two spaces for JSON and project files, spaces instead of tabs, file-scoped namespaces, and the configured final-newline and line-ending rules. Use PascalCase for types and methods, camelCase for locals and parameters, and `_camelCase` for private fields. Keep nullable reference types and implicit usings enabled. Preserve the problem-summary XML documentation and URLs when changing `Program.cs`, updating them only when the source behavior or problem reference changes.

## Testing Guidelines

No automated test framework or coverage threshold is configured yet. At minimum, run `dotnet build` and `dotnet run` for every change. When implementing the algorithm, add deterministic console cases covering successful pairs, non-overlap boundaries, and the `-1` no-solution path; show expected and actual results in the change description. A future test project should use descriptive names such as `FindTwoNonOverlappingSubarrays_ReturnsMinimumLength`.

## Commit & Pull Request Guidelines

Recent history uses concise imperative subjects such as `Add ...`, `Document ...`, `Adjust ...`, and `Explain ...`. Use a specific subject, for example `Add LeetCode 1477 solution`. Pull requests should explain the approach and affected paths, list validation commands and results, and call out any changed documentation or behavior. Link an issue when one exists; screenshots are unnecessary for this console-only project. Exclude generated `bin/` and `obj/` files.