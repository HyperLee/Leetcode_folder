# Repository Guidelines

## Project Structure & Module Organization

The repository is a focused .NET 10 console solution for LeetCode 930. The application lives in `leetcode_930/`: `Program.cs` contains the entry point, sample-case runner, and both algorithm implementations, while `leetcode_930.csproj` defines the executable target. Root-level `README.md` explains the approaches and expected output. `docs/readme-template.md` is the reusable documentation template. VS Code build and debug settings are stored in `.vscode/`; generated `bin/` and `obj/` directories must remain untracked.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore .\leetcode_930\leetcode_930.csproj
dotnet build .\leetcode_930\leetcode_930.csproj
dotnet run --project .\leetcode_930\leetcode_930.csproj
dotnet format .\leetcode_930\leetcode_930.csproj --verify-no-changes
```

`restore` resolves SDK dependencies, `build` compiles the project, and `run` executes all built-in sample cases. The formatter command checks compliance with `.editorconfig`. In VS Code, press F5 to use the `.NET Debug leetcode_930` launch configuration; its pre-launch task builds the project automatically.

## Coding Style & Naming Conventions

Follow the root `.editorconfig`: C# uses four-space indentation, spaces rather than tabs, and standard .NET spacing and brace conventions. Use PascalCase for types, methods, and properties; camelCase for parameters and locals; `_camelCase` for private instance fields; and `I`-prefixed PascalCase for interfaces. Keep nullable-reference checks and implicit usings enabled. Preserve the bilingual XML documentation around the problem and add short comments only where an algorithmic invariant is not obvious.

## Testing Guidelines

There is currently no separate test project. `Main` is the regression harness: each case must exercise both `NumSubarraysWithSum` and `NumSubarraysWithSum2`, print `PASS`, and finish with `Overall: PASS` and exit code 0. Add edge cases for zero goals, all-zero arrays, and single-element inputs when changing either algorithm. Always run both `dotnet build` and `dotnet run` before submitting.

## Commit & Pull Request Guidelines

Recent history mixes brief summaries with Conventional Commit subjects. Prefer the clearer scoped form, for example `feat(leetcode-930): add prefix-sum edge cases` or `docs(leetcode-930): clarify sliding window`. Keep commits focused. Pull requests should describe the algorithm or documentation change, list verification commands and results, and link the relevant issue when one exists. Include screenshots only for visible editor or documentation-rendering changes; console output is sufficient for algorithm changes.
