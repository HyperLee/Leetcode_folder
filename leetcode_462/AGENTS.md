# Repository Guidelines

## Project Structure

- `leetcode_462/` contains the .NET 10 console project and its solution code in `Program.cs`.
- `.vscode/` contains VS Code build and launch settings; `docs/readme-template.md` is a documentation aid.
- `.editorconfig` defines repository formatting preferences. There is currently no separate test project or asset directory.

## Build and Run

From the repository root, build and run the console project with:

```powershell
dotnet build .\leetcode_462\leetcode_462.csproj
dotnet run --project .\leetcode_462\leetcode_462.csproj
```

Build checks compilation. Run executes `Main` and is the available way to exercise the program; no test command is configured.

## Coding Style

Use four spaces for C# indentation and follow `.editorconfig`. Keep the existing `leetcode_462` namespace style; use PascalCase for types and methods, and camelCase for parameters and local variables. Nullable reference types and implicit usings are enabled in the project.

## Testing

No test framework or test project is present. When changing the solution, run the build and include representative input/output checks in the program or add a focused test project if the change warrants it. Record the checks you performed in the pull request.

## Commits and Pull Requests

Recent repository history uses short imperative commit subjects, such as `Add LeetCode 453 project scaffold` and `Implement LeetCode 453 solution`. Follow that pattern (for example, `Add LeetCode 462 solution`). Pull requests should summarize the algorithm and complexity, describe user-visible changes, and list validation commands and results. Link a related issue when one exists.
