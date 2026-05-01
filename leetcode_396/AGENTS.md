# Repository Guidelines

## Project Structure & Module Organization
This repository is a small .NET solution for LeetCode problem 396. The solution file is `leetcode_396.sln`. Application code lives in [`leetcode_396/`](./leetcode_396), with the main entry point and algorithm implementation in `Program.cs` and project settings in `leetcode_396.csproj`. Root-level documentation lives in `README.md`. Repository automation and prompt metadata are stored under `.github/`.

## Build, Test, and Development Commands
- `dotnet build leetcode_396.sln`: restore and build the solution.
- `dotnet run --project leetcode_396/leetcode_396.csproj`: run the console app and execute the built-in sample cases.
- `dotnet test leetcode_396.sln`: check for test projects. At present, this completes without running tests because no separate test project exists.

Use the console output from `dotnet run` as the current validation path before submitting changes.

## Coding Style & Naming Conventions
Follow `.editorconfig`. Key rules in this repo:
- Use 4 spaces in `*.cs` files and 2 spaces in solution/project JSON or XML files.
- Use PascalCase for namespaces, classes, and methods.
- Use camelCase for locals and parameters.
- Keep braces and spacing consistent with standard C# formatter output.

Prefer clear method names such as `MaxRotateFunction`, and keep problem-specific logic close to the executable example when the repo remains single-problem scoped.

## Testing Guidelines
There is no dedicated test project yet. Add coverage by either:
- extending the sample case array in `Program.cs`, or
- creating a proper test project such as `leetcode_396.Tests/`.

If you add formal tests later, use descriptive names like `MaxRotateFunction_Returns26_ForSampleInput` and make `dotnet test` part of the required validation flow.

## Commit & Pull Request Guidelines
Recent history uses Conventional Commit prefixes, especially `feat:`. Continue with concise messages such as `feat: add edge case for negative values` or `fix: correct rotation recurrence`.

Pull requests should include:
- a short summary of the algorithm or refactor,
- the validation performed, for example `dotnet build` and `dotnet run`,
- updated `README.md` notes when behavior or explanation changes.
