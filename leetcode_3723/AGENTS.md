# Repository Guidelines

## Project Structure & Module Organization

This workspace contains the LeetCode 3723 console solution. Keep its source and project file in `leetcode_3723/`; `.vscode/` contains the launch and build-task settings, and `docs/readme-template.md` guides initial README creation. Shared editor and Git settings live at the workspace root. There is no separate test or asset directory.

## Build, Test, and Development Commands

Run these commands from the workspace root:

```powershell
dotnet build .\leetcode_3723\leetcode_3723.csproj
dotnet run --project .\leetcode_3723\leetcode_3723.csproj
```

The build targets .NET 10; `dotnet run` starts the console app. VS Code F5 uses the checked-in launch profile and build task. No test project is configured, so add focused tests or deterministic sample checks when implementing the solution.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C# and two spaces for JSON and project files. Use PascalCase for types and methods, and camelCase for parameters and local variables. Keep braces and the existing namespace and XML problem-summary style.

## Testing Guidelines

There is no test framework or coverage threshold yet. Check official examples and boundary cases, including impossible sums and ties, with focused tests or sample checks. A successful build confirms compilation, not algorithm correctness.

## Commit & Pull Request Guidelines

Recent commits use concise imperative subjects such as `Add LeetCode 2913 project scaffold` and `Document SumCounts loop walkthrough`. Keep commits focused and action-led. Pull requests should explain the problem and approach, report representative and boundary-case checks, and link a related issue when one exists.
