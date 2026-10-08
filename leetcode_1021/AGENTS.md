# Repository Guidelines

## Project Structure & Module Organization

This challenge is a single .NET 10 console application. `leetcode_1021/Program.cs` contains the entry point and solution code; `leetcode_1021/leetcode_1021.csproj` defines the executable project. `.vscode/launch.json` and `.vscode/tasks.json` configure debugging and the build task. `docs/readme-template.md` is guidance for creating an initial README. There is currently no separate test or asset directory.

## Build, Test, and Development Commands

- `dotnet build .\leetcode_1021\leetcode_1021.csproj` compiles the application.
- `dotnet run --project .\leetcode_1021\leetcode_1021.csproj` builds and runs it.
- In VS Code, use the `C#: leetcode_1021` launch configuration; it runs the `build leetcode_1021` task first.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C#, place opening braces on their own lines, and keep `using` directives outside the namespace with system namespaces first. File-scoped namespaces are preferred. Use PascalCase for types and methods, and camelCase for parameters and local variables. No separate formatter or linter is configured.

## Testing Guidelines

No test project, test framework, or coverage threshold is configured. When implementing a solution, check representative and boundary inputs through the console entry point. If automated tests are introduced, keep them in a dedicated test project and document its run command.

## Commit & Pull Request Guidelines

Recent history in the enclosing Git repository uses concise imperative subjects such as `Add`, `Document`, and `Clarify`, without a fixed prefix. Follow that style and include the LeetCode number when it helps identify the change. Pull requests should describe the problem and approach, note related issues when applicable, and report the build/run commands and results. Include sample inputs or output for behavior that is not clear from the code; screenshots are unnecessary for this console project.
