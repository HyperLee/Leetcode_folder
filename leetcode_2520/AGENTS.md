# Repository Guidelines

## Project Structure & Module Organization

The repository root contains shared configuration and contributor documentation. The runnable .NET 10 console application lives in `leetcode_2520/`: `Program.cs` contains the entry point and algorithm work, while `leetcode_2520.csproj` defines the SDK and target framework. VS Code build and debug profiles are under `.vscode/`. Use `docs/readme-template.md` only when creating the project’s initial `README.md`. Generated `bin/` and `obj/` directories are build artifacts and must not be committed.

## Build, Run, and Development Commands

- `dotnet restore .\leetcode_2520\leetcode_2520.csproj` restores project dependencies.
- `dotnet build .\leetcode_2520\leetcode_2520.csproj` compiles the console application for `net10.0`.
- `dotnet run --project .\leetcode_2520\leetcode_2520.csproj` runs the current sample harness.
- `dotnet format .\leetcode_2520\leetcode_2520.csproj --verify-no-changes` checks formatting against `.editorconfig`.

From VS Code, press F5 with **Debug leetcode_2520**. The launch profile runs the default `build leetcode_2520` task first.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space indentation for C#, and Allman-style braces. Use `PascalCase` for classes and methods, `camelCase` for parameters and local variables, and keep the existing `leetcode_2520` namespace. Preserve nullable reference types and implicit usings. Add XML documentation for the problem statement and non-obvious algorithm reasoning; avoid comments that merely repeat the code.

## Testing Guidelines

There is currently no separate automated test project or coverage threshold. Validate algorithm changes with deterministic cases in `Main` or a focused helper, including official examples and relevant edge cases. Print expected and actual values clearly, then run both `dotnet build` and `dotnet run`. If a test project is introduced later, name tests by behavior, such as `CountDigits_ReturnsTwo_For1248`.

## Commit & Pull Request Guidelines

Recent history favors concise Conventional Commit subjects with a LeetCode scope, for example `feat(leetcode-2520): implement digit divisor count`. Use `fix:`, `docs:`, or `chore:` when appropriate, and keep each commit focused. Pull requests should explain the algorithm and complexity, link the relevant issue when available, and include the exact validation commands and observed results. Add screenshots only for changes with meaningful visual impact.
