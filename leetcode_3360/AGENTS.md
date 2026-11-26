# Repository Guidelines

## Project Structure

This directory contains the C# console project for LeetCode 3360, Stone Removal Game. `leetcode_3360/Program.cs` holds the entry point and the English and Traditional Chinese problem notes. `leetcode_3360/leetcode_3360.csproj` targets `net11.0` with nullable reference types and implicit usings enabled. `.vscode/` contains the build task and debugger configuration. There are no separate assets or test projects.

## Build and Run

Run these commands from the directory containing this guide with the .NET 11 SDK installed.

- `dotnet build leetcode_3360/leetcode_3360.csproj` restores dependencies and compiles the project.
- `dotnet run --project leetcode_3360/leetcode_3360.csproj` builds and runs the console app. The current starter prints `Hello, World!`.
- In VS Code, select **Debug leetcode_3360** to build and launch under the debugger.

## Coding Style and Naming

Follow `.editorconfig`. Use four spaces for C# and two for JSON and project XML. Put opening braces on their own lines, prefer explicit local types and file-scoped namespaces, and keep the existing `leetcode_3360` namespace. Use PascalCase for methods and types and camelCase for locals and parameters. Keep game logic callable independently of console output so examples can exercise it directly.

## Testing Guidelines

The project has no automated test framework or coverage threshold. When changing the solution, run the problem examples and check the first two turn boundaries. Verify that `9` returns `false`, `10` and `18` return `true`, and `19` returns `false`. Report the cases and results in the pull request.

## Commits and Pull Requests

The parent repository uses short, descriptive commit subjects, such as `Document sliding window solution details`. Keep commits focused on one change. In a pull request, summarize what changed, include the verification commands and results, and link any related issue. For algorithm changes, include time and space complexity.
