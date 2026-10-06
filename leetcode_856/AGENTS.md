# Repository Guidelines

## Project Structure & Module Organization

This repository is a nested .NET 10 console solution for LeetCode 856 (Score of Parentheses).

- `leetcode_856/Program.cs` contains the entry point and the three solution methods.
- `leetcode_856/leetcode_856.csproj` defines the executable project; `leetcode_856.sln` is the solution file.
- `README.md` contains the bilingual problem explanation, algorithm derivations, traces, and examples.
- `.vscode/` contains the build task and CoreCLR launch configuration.
- There is no separate assets or test directory. Generated `bin/` and `obj/` output should remain uncommitted.

## Build, Test, and Development Commands

Run these commands from the repository root:

```powershell
dotnet restore leetcode_856/leetcode_856.csproj
dotnet build leetcode_856/leetcode_856.csproj --nologo
dotnet run --project leetcode_856/leetcode_856.csproj
```

`dotnet build` verifies compilation. `dotnet run` executes the four sample inputs and prints `PASS` for each of the three implementations. In VS Code, `F5` uses `.vscode/tasks.json` to build and `.vscode/launch.json` to launch the nested project.

## Coding Style & Naming Conventions

Follow the repository `.editorconfig`: four spaces for C# indentation, braces for control blocks, file-scoped namespaces, and a final newline. Use PascalCase for types and public methods (for example, `ScoreOfParentheses`) and camelCase for locals, parameters, and test data. Keep algorithm comments and README explanations aligned with the code; avoid unrelated refactoring.

## Testing Guidelines

No formal test framework or coverage threshold is configured. Treat the cases in `Program.Main` as the regression smoke test, covering `()`, nesting, concatenation, and mixed nesting. When changing an algorithm, add a representative case there and confirm every method reports `PASS`.

## Commit & Pull Request Guidelines

Recent commits use concise, descriptive prefixes such as `feat:`, `fix:`, and `docs:`. Keep commits focused and use the same convention. Pull requests should summarize the algorithm or documentation change, list the commands run and their results, link an issue when applicable, and exclude generated build artifacts.
