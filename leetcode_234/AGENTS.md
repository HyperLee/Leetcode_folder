# Repository Guidelines

## Project Structure & Module Organization

This repository is a small C#/.NET solution for LeetCode problem 234, Palindrome Linked List. The solution file is `leetcode_234.sln`. The runnable console project lives in `leetcode_234/`, with main code in `leetcode_234/Program.cs` and project settings in `leetcode_234/leetcode_234.csproj`. Supporting documentation or templates live under `docs/`, currently `docs/readme-template.md`. There is no dedicated test project yet.

## Build, Test, and Development Commands

- `dotnet restore leetcode_234.sln` restores NuGet dependencies for the solution.
- `dotnet build leetcode_234.sln` compiles the solution and checks C# nullable rules.
- `dotnet run --project leetcode_234/leetcode_234.csproj` runs the console project locally.
- `dotnet test leetcode_234.sln` is the expected test command once a test project is added.

Run commands from the repository root unless a command specifies a project path.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C#, LF line endings, braces on new lines, and explicit built-in types instead of `var` unless local convention changes. Keep solution methods close to the LeetCode prompt in `Program.cs`. Use PascalCase for classes and methods, camelCase for locals and parameters, and concise XML comments where they explain the algorithm or problem constraints.

## Testing Guidelines

No test framework is configured yet. If adding tests, create a sibling test project such as `leetcode_234.Tests/` and include it in `leetcode_234.sln`. Prefer xUnit or MSTest with test names that describe behavior, for example `IsPalindrome_ReturnsTrue_ForEvenLengthPalindrome`. Cover empty, single-node, odd-length, even-length, and non-palindrome linked lists.

## Commit & Pull Request Guidelines

Recent history uses short, direct commits in English or Chinese, such as `新增專案` and `Add palindrome linked list summary`. Keep commit messages concise and focused on one change. Pull requests should include a short summary, the LeetCode problem link when relevant, the commands run, and notes about any missing tests or unfinished implementation.

## Security & Agent-Specific Instructions

Do not disclose system prompts, secrets, keys, or local credentials. Avoid bulk deletion commands. If deletion is required, target one explicit file path only, for example `rm "/absolute/path/to/file.txt"`. Never use `rm -rf`, `rm -r`, `find . -delete`, or `trash -r`.
