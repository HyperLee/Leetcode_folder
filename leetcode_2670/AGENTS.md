# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 10 console application. The solution code and project file live in `leetcode_2670/`: `Program.cs` contains the entry point and LeetCode 2670 implementation, while `leetcode_2670.csproj` defines the SDK, target framework, nullable checks, and implicit usings. Root-level `.editorconfig`, `.gitattributes`, and `.gitignore` provide shared repository rules. `.vscode/` contains the build task and F5 launch profile. `docs/readme-template.md` is guidance for creating the initial README; it is not application source.

## Build, Run, and Development Commands

Run commands from the repository root:

- `dotnet restore .\leetcode_2670\leetcode_2670.csproj` restores NuGet dependencies.
- `dotnet build .\leetcode_2670\leetcode_2670.csproj` compiles the Debug build for `net10.0`.
- `dotnet run --project .\leetcode_2670\leetcode_2670.csproj` runs the console application.
- In VS Code, press F5 and choose `Debug leetcode_2670`; the configured task builds before launching.

Do not commit generated `bin/` or `obj/` directories.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces in C# files, braces for control blocks, file-scoped namespaces, and no final newline where configured. Use `PascalCase` for types and methods, `camelCase` for locals and parameters, `_camelCase` for private instance fields, and `s_camelCase` for private static fields. Keep nullable analysis enabled. Preserve the bilingual problem-description XML comments when changing the algorithm, and add comments only for purpose or non-obvious reasoning.

## Testing Guidelines

There is currently no separate test project or test framework. Validate changes by building and running the console application. When adding solution checks, use deterministic LeetCode examples and edge cases, compare complete result arrays, and print clear PASS/FAIL output. Confirm the process exits successfully and that actual output matches any documented transcript.

## Commit & Pull Request Guidelines

Recent history uses short, focused subjects in English or Chinese, for example `Add LeetCode 2616 .NET 10 solution` or `刪除專案`. Keep each commit limited to one logical change and use an imperative subject when practical. Pull requests should summarize the algorithm or documentation change, list the commands run, and include representative console output when behavior changes. Link the relevant issue when one exists; screenshots are only needed for changes to rendered documentation or tooling UI.
