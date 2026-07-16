# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single .NET 10 console application for LeetCode 1043, Partition Array for Maximum Sum.

- `leetcode_1043/Program.cs` contains the entry point, problem documentation, sample runner, and solution methods.
- `leetcode_1043/leetcode_1043.csproj` defines the executable project with nullable reference types and implicit usings enabled.
- `.vscode/tasks.json` provides the default VS Code build task for the nested project.
- `leetcode_1043/bin/` and `leetcode_1043/obj/` are generated build outputs; do not edit or commit them.

Keep algorithm code close to the small console harness unless the project grows enough to justify separate source or test files.

## Build, Test, and Development Commands

Run these commands from the repository root:

```powershell
dotnet restore .\leetcode_1043\leetcode_1043.csproj
dotnet build .\leetcode_1043\leetcode_1043.csproj
dotnet run --project .\leetcode_1043\leetcode_1043.csproj
```

`restore` resolves dependencies, `build` compiles the .NET 10 application, and `run` executes the console demo. In VS Code, `Ctrl+Shift+B` invokes the checked-in build task.

## Coding Style & Naming Conventions

Use four-space indentation and place braces on new lines. Keep nullable analysis enabled. Use `PascalCase` for classes and methods, and `camelCase` for parameters and local variables. Prefer descriptive algorithm names such as `MaxSumAfterPartitioning` over numbered or abbreviated names. Preserve the bilingual English and Traditional Chinese XML problem summary when editing `Program.cs`.

## Testing Guidelines

No dedicated test project or testing framework is configured. Add deterministic sample and edge cases to the console harness, including minimum-length arrays, `k = 1`, and `k` equal to the array length. Compare actual results with explicit expected values, then run both `dotnet build` and `dotnet run`. If a test project is introduced, name it `leetcode_1043.Tests` and use behavior-oriented test names such as `MaxSumAfterPartitioning_KIsOne_ReturnsOriginalSum`.

## Commit & Pull Request Guidelines

History mixes plain descriptions with Conventional Commit prefixes; prefer concise imperative Conventional Commits, for example `feat: implement dynamic programming solution` or `docs: explain partition recurrence`. Keep commits focused. Pull requests should summarize the approach, state time and space complexity, list verification commands, and link the relevant issue. Include console output when behavior or sample cases change; screenshots are unnecessary for code-only changes.
