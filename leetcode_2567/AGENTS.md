# Repository Guidelines

## Project Structure & Module Organization

This directory contains a C# console project for LeetCode 2567, “Minimum Score by Changing Two Elements,” within the parent Leetcode repository.

- `leetcode_2567/Program.cs`: entry point, problem summary, and solution methods `MinimizeSum` and `MinimizeSum2`.
- `leetcode_2567/leetcode_2567.csproj`: .NET 11 executable with implicit usings and nullable reference types enabled.
- `PROBLEM.md`: Traditional Chinese problem explanation and official examples.
- `.vscode/`: build task and .NET debugger configuration.
- `.editorconfig`: formatting and naming rules. Generated `bin/` and `obj/` directories are build artifacts; keep them out of commits.

There are currently no separate test projects or application assets.

## Build, Test, and Development Commands

Run commands from this directory with the .NET 11 SDK installed:

```powershell
dotnet build leetcode_2567/leetcode_2567.csproj
dotnet run --project leetcode_2567/leetcode_2567.csproj
dotnet format leetcode_2567/leetcode_2567.csproj --verify-no-changes
```

These compile the project, execute `Main`, and check formatting, respectively. `Main` currently prints `Hello, World!`; running it does not validate the solution methods. In VS Code, select `Debug leetcode_2567` to use the configured build-and-debug workflow.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C#, file-scoped namespaces, PascalCase method and type names, and camelCase parameters and local variables. Preserve the existing `leetcode_2567` namespace. Keep alternative algorithms in clearly identified methods and explain their approach, complexity, and input mutation behavior. Preserve bilingual problem references and use Traditional Chinese for additions to `PROBLEM.md`.

## Testing Guidelines

No automated test framework or coverage threshold is configured. For algorithm changes, explicitly invoke each affected method and verify official examples (`[1,4,7,8,5]` → `3`, `[1,4,3]` → `0`), duplicates, sorted/reversed inputs, and boundary values. Use fresh array copies when comparing methods because sorting mutates input. If adding tests, use descriptive names such as `MinimizeSum_ThreeElements_ReturnsZero` and document their execution command.

## Commit & Pull Request Guidelines

Recent commits use short, imperative descriptions such as `Add LeetCode 2567 project scaffold` and `Clarify allowed replacement values`. Follow that style and keep changes focused on this problem. PRs should describe the change, explain algorithm and complexity impacts, and report validation commands and results. Link relevant issues when available; include screenshots only for visual changes.

## Agent Safety

Preserve existing user edits. Keep credentials and private instructions out of source and logs. Delete only explicitly identified individual files; ask the user to perform any required bulk deletion manually.
