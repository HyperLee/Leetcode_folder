# Repository Guidelines

## Project Structure & Module Organization

This directory is one problem module inside the parent LeetCode repository. The executable project lives in `leetcode_2029/`: `Program.cs` contains the three Stone Game IX implementations and the console acceptance harness, while `leetcode_2029.csproj` targets .NET 10. `README.md` explains the algorithm and records expected output. `docs/readme-template.md` is guidance for initial README creation. Repository-level `.editorconfig` and `.vscode/` files define formatting and local build/debug behavior. There is no separate test project or asset directory.

## Build, Test, and Development Commands

Run commands from this directory and always name the nested project explicitly:

```bash
dotnet restore leetcode_2029/leetcode_2029.csproj
dotnet build leetcode_2029/leetcode_2029.csproj --no-restore
dotnet run --project leetcode_2029/leetcode_2029.csproj --no-build
dotnet format leetcode_2029/leetcode_2029.csproj --verify-no-changes --no-restore
```

Restore dependencies before the first build. The run command executes seven fixtures against three solutions. The format command is a non-mutating style check. Do not use bare `dotnet test` here: this folder has no solution or project at its root, so it returns `MSB1003`.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space C# indentation, two-space project/XML indentation, braces on new lines, file-scoped namespaces, and explicit types instead of `var`. Use PascalCase for types and methods (`StoneGameIX3`), camelCase for locals and parameters (`candidateCount`), and descriptive test-case names. Keep nullable reference types enabled. Preserve bilingual XML problem documentation and focused Traditional Chinese teaching comments.

## Testing Guidelines

The console harness is the current acceptance suite. Add deterministic boundary and regression cases to `testCases`; every solution must print matching `Expected`, `Actual`, and `PASS` values. A failure must continue to produce a nonzero process exit. Before submitting, require `dotnet build`, a fresh run ending in `21/21` (or the updated total), formatter verification, and `git diff --check -- AGENTS.md`.

## Commit & Pull Request Guidelines

Recent history mixes concise Chinese subjects with Conventional Commit style. Prefer a scoped imperative subject such as `fix(leetcode-2029): correct game logic`; keep each commit limited to this module. Pull requests should summarize the algorithm or documentation change, list commands run, link the relevant issue, and include updated console output when behavior or fixtures change. Screenshots are unnecessary for this console-only project.
