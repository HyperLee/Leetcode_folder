# Repository Guidelines

## Project Structure & Module Organization

This directory is the `leetcode_3069` problem module inside the parent `Leetcode_folder` Git checkout. Application code lives in `leetcode_3069/Program.cs`; the adjacent `leetcode_3069.csproj` defines a .NET 10 console application with nullable reference types and implicit usings enabled. Root-level `.editorconfig` and `.gitattributes` define formatting and text-file rules. `.vscode/tasks.json` and `.vscode/launch.json` provide the default build and CoreCLR debug workflows. `docs/readme-template.md` is guidance for creating future documentation, not runtime code. Generated `bin/` and `obj/` directories are ignored and must not be committed. There is currently no dedicated test or asset directory.

## Build, Test, and Development Commands

Run commands from this directory and always name the nested project explicitly:

```bash
dotnet restore leetcode_3069/leetcode_3069.csproj
dotnet build leetcode_3069/leetcode_3069.csproj --nologo
dotnet run --no-build --project leetcode_3069/leetcode_3069.csproj
dotnet format leetcode_3069/leetcode_3069.csproj --verify-no-changes --no-restore
```

Restore downloads dependencies; build compiles the executable; run exercises the current console entry point; format checks C# style without rewriting files. In VS Code, the default build task and `Debug leetcode_3069` launch configuration perform the same nested-project build. Bare `dotnet test` at this directory is invalid because no solution or test project exists here.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space C# indentation, two-space JSON and project-file indentation, braces for control blocks, file-scoped namespaces, and explicit built-in types instead of `var`. Use PascalCase for types and methods, camelCase for parameters and local variables, `_camelCase` for private instance fields, and `s_camelCase` for private static fields. Keep algorithm explanations focused and update XML documentation when public behavior changes.

## Testing Guidelines

No automated framework or coverage threshold is configured. Treat a clean build plus deterministic console cases as the current validation gate. Algorithm changes should cover minimum-size input, each comparison branch, and a representative example. If a test project is introduced, keep it beside the application as `leetcode_3069.Tests/` and use descriptive names such as `ResultArray_WhenFirstTailIsLarger_AppendsToFirst`.

## Commit & Pull Request Guidelines

The parent history mixes imperative English or Chinese subjects with scoped Conventional Commits. Prefer a concise form such as `docs(leetcode-3069): add repository guidelines`, and keep each commit focused on this module. Pull requests should explain the algorithm or documentation change, list verification commands and results, link the relevant issue, and call out any intentionally unchanged behavior. Screenshots are only useful for developer-tooling UI changes.
