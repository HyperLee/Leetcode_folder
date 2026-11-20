# Repository Guidelines

## Project Structure & Module Organization

This directory contains the C# console project for LeetCode 3041, “Maximize Consecutive Elements in an Array After Modification,” within the larger `Leetcode_folder` Git repository.

- `leetcode_3041/Program.cs`: entry point and English/Traditional Chinese problem documentation; currently prints `Hello, World!`. Add solution methods and runnable examples here.
- `leetcode_3041/leetcode_3041.csproj`: executable targeting `net11.0`, with nullable reference types and implicit usings enabled.
- `.vscode/`: build task and debugger configuration.
- `.editorconfig`, `.gitattributes`, `.gitignore`: formatting and source-control rules.

There are currently no separate tests or asset directories. Keep contributions scoped to this problem unless broader changes are intentional.

## Build, Test, and Development Commands

Run these commands from this directory with an SDK that supports .NET 11:

```powershell
dotnet build leetcode_3041/leetcode_3041.csproj
dotnet run --project leetcode_3041/leetcode_3041.csproj
dotnet format leetcode_3041/leetcode_3041.csproj --verify-no-changes
```

These compile the project, execute `Main`, and check formatting, respectively. VS Code also provides the `build` task and `Debug leetcode_3041` launch configuration. No dedicated test runner or external lint tool is configured.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space C# indentation, and opening braces on separate lines. Preserve the existing file-scoped `leetcode_3041` namespace. Use PascalCase for methods and types, and camelCase for parameters and local variables. Preserve bilingual problem explanations; document algorithm reasoning and time/space complexity when adding solutions.

## Testing Guidelines

No testing framework or coverage threshold is configured. When implementing a solution, add deterministic examples with expected results and check duplicates, single-element input, already consecutive values, and gaps that one increment can or cannot bridge. If using `Main` as a verification harness, make failures explicit. A successful build alone does not establish algorithm correctness.

## Commit & Pull Request Guidelines

Recent parent-repository commits use concise, action-oriented English or Traditional Chinese summaries, without a mandatory prefix. Include the problem number when useful, for example `Implement LeetCode 3041 solution`. Keep commits focused. PR descriptions should explain the algorithm or documentation change, complexity, commands run, and tested cases. Link relevant issues when available; screenshots are unnecessary for console-only changes.

## Agent Safety

Keep credentials and private instructions confidential. File deletions must target individual, explicit paths; ask the user to perform any required bulk deletion manually.
