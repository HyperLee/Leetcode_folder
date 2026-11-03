# Repository Guidelines

This repository contains a .NET 10 C# console solution for LeetCode 3736. Keep changes focused on the problem solution and its documentation.

## Project Structure

- `leetcode_3736/Program.cs` contains the console entry point and solution code.
- `leetcode_3736/leetcode_3736.csproj` defines the .NET 10 executable project with nullable reference types enabled.
- `docs/readme-template.md` is a template for creating the project README.
- `.editorconfig` sets repository-wide formatting preferences. There is no test or assets directory currently.

## Build, Run, and Format

Run these commands from the repository root:

```powershell
dotnet build .\leetcode_3736\leetcode_3736.csproj
dotnet run --project .\leetcode_3736\leetcode_3736.csproj
dotnet format .\leetcode_3736\leetcode_3736.csproj --verify-no-changes
```

Build compiles the project, run executes its console entry point, and format checks the configured .NET style without changing files.

## Coding Style

Use four spaces for C# indentation and place opening braces on a new line, following `.editorconfig`. Prefer file-scoped namespaces where appropriate. Use PascalCase for types and methods, and camelCase for parameters and local variables. Keep solution logic readable and document non-obvious algorithm choices or complexity.

## Testing

No automated test project or framework is configured. For algorithm changes, exercise representative cases, including boundary and duplicate-value cases. If adding automated coverage, place it in a clearly named test project and run it with `dotnet test <path-to-test-project>`.

## Commits and Pull Requests

Recent commits use short imperative subjects such as `Add LeetCode 462 project scaffold` and `Implement LeetCode 453 solution`. Follow that pattern. A pull request should state the problem ID and title, summarize the approach and complexity, and list the build, run, or test commands performed. Link a related issue when one exists; screenshots are unnecessary for this console project.
