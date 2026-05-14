# Repository Guidelines

## Project Structure & Module Organization
This repository is a small .NET console solution for LeetCode problem 19. The solution entry is [`leetcode_019.slnx`](/C:/GitHubFolder/Leetcode_folder/leetcode_019/leetcode_019.slnx). Application code lives under [`leetcode_019/`](/C:/GitHubFolder/Leetcode_folder/leetcode_019/leetcode_019), with the main implementation in [`leetcode_019/Program.cs`](/C:/GitHubFolder/Leetcode_folder/leetcode_019/leetcode_019/Program.cs) and project settings in [`leetcode_019/leetcode_019.csproj`](/C:/GitHubFolder/Leetcode_folder/leetcode_019/leetcode_019/leetcode_019.csproj). There is no dedicated `tests/` directory yet.

## Build, Test, and Development Commands
Use the .NET CLI from the repository root.

- `dotnet build leetcode_019.slnx` builds the solution and validates compile-time errors.
- `dotnet run --project leetcode_019/leetcode_019.csproj` runs the console app locally.
- `dotnet format` applies SDK formatting rules and `.editorconfig` preferences.
- `dotnet test` should be used after a test project is added; it currently has no test target to execute.

## Coding Style & Naming Conventions
Follow `.editorconfig` exactly. Use 4-space indentation in C# and 2 spaces in project and JSON files. Prefer file-scoped namespaces, explicit types instead of `var`, and braces on control blocks. Use `PascalCase` for types and methods, `camelCase` for parameters and locals, `_camelCase` for private fields, and `s_camelCase` for private static fields. Keep problem notes and algorithm comments short and directly tied to the implementation.

## Testing Guidelines
This repo does not currently include automated tests. If you add coverage, create a sibling test project such as `leetcode_019.Tests/` and name test files after the target method, for example `RemoveNthFromEndTests.cs`. Cover normal cases, removing the head, and single-node edge cases. Run `dotnet test` before opening a PR.

## Commit & Pull Request Guidelines
Recent history favors short, imperative commit messages, often in Traditional Chinese, for example `新增附錄 參考解法`, `調整:加入註解`, and `docs: add leetcode 014 explanation and demos`. Keep commits focused on one change. PRs should include a concise summary, the problem or behavior changed, sample input/output when logic changes, and screenshots only if console output or documentation rendering is relevant.

## Security & Agent Notes
Do not commit secrets, tokens, or system prompts. Respect the repository rule that bulk deletion commands are banned; only delete explicitly named single files when necessary.
