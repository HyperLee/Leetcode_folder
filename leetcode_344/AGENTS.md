# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET console solution for LeetCode problem 344.

- `leetcode_344.slnx` is the solution entry point.
- `leetcode_344/leetcode_344.csproj` defines the executable project targeting `net10.0` with nullable reference types and implicit usings enabled.
- `leetcode_344/Program.cs` contains the current program code.
- `.editorconfig` defines formatting and C# style rules.

There is no dedicated test project or asset directory at the moment. If tests are added, place them in a sibling project such as `leetcode_344.Tests/`.

## Build, Test, and Development Commands

Run commands from the repository root.

```powershell
dotnet build leetcode_344.slnx
```

Builds the solution and reports compiler or analyzer issues.

```powershell
dotnet run --project leetcode_344/leetcode_344.csproj
```

Runs the console application.

```powershell
dotnet test
```

Runs tests once a test project is added to the solution. Until then, use `dotnet build` as the primary validation command.

## Coding Style & Naming Conventions

Follow `.editorconfig` as the source of truth. Use spaces for indentation, 4 spaces in C# files, and 2 spaces in project/XML/JSON-style files. Prefer file-scoped namespaces, braces for code blocks, sorted `System` usings first, and explicit types instead of `var` unless the codebase changes that convention.

Use PascalCase for classes, methods, and public members; camelCase for local variables and parameters. Keep solution methods focused and readable, with short comments only where they clarify algorithmic intent.

## Testing Guidelines

No automated tests are currently present. For new algorithm work, add focused unit tests covering normal cases, edge cases, and LeetCode examples. Name test methods after the behavior under test, for example `ReverseString_ReversesInPlace` or `ReverseString_HandlesEmptyInput`.

If a test project is introduced, prefer a standard .NET test framework such as xUnit, NUnit, or MSTest and include it in `leetcode_344.slnx` so `dotnet test` works from the root.

## Commit & Pull Request Guidelines

Recent commits are short, direct summaries, often using verbs equivalent to add, adjust, or delete. Keep that style: one concise subject line, imperative when possible, such as `Add two-pointer solution` or `Adjust comments for reverse string`.

For pull requests, include a brief description of the change, the validation command run, and any relevant LeetCode case coverage. Link related issues when available. Screenshots are not required for this console-only project.
