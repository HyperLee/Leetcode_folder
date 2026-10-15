# Repository Guidelines

## Project Structure & Module Organization

- `leetcode_2913/Program.cs` contains the console entry point and the LeetCode solution.
- `leetcode_2913/leetcode_2913.csproj` is the .NET 10 project. `.vscode/` contains the debugger and build-task configuration; `docs/readme-template.md` guides initial README creation.
- No separate test project is currently tracked.

## Build, Test, and Development Commands

- `dotnet build .\leetcode_2913\leetcode_2913.csproj` compiles the project.
- `dotnet run --project .\leetcode_2913\leetcode_2913.csproj` runs the console application.
- In VS Code, F5 uses the configured .NET Core launch profile and runs the Debug build task first.

## Coding Style & Naming Conventions

- Follow `.editorconfig`: use four spaces in C# and two spaces in JSON, XML, and project files. Use PascalCase for types and methods, and camelCase for parameters and local variables.
- Keep the solution focused in `Program.cs` and follow its XML documentation style for problem context. No separate formatter or linter is configured.

## Testing Guidelines

- There is no dedicated test framework or test command. For algorithm changes, check the problem examples and boundary cases, then build the project to catch compile errors. Do not treat a successful build as proof of algorithm correctness.

## Commit & Pull Request Guidelines

- Recent history uses concise imperative subjects, such as `Add LeetCode 2913 project scaffold` and `Clarify factor-pair loop condition`. Use a short, action-led commit subject.
- In a pull request, describe the problem and approach, report sample and edge-case checks, and link a related issue when one exists. No PR template is tracked.
