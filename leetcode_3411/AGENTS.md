# Repository Guidelines

## Project Structure

This folder contains the C# console project for LeetCode 3411, Maximum Subarray With Equal Products. The parent Git repository also contains other LeetCode projects.

- `leetcode_3411/Program.cs` contains the console entry point and bilingual problem summary.
- `leetcode_3411/leetcode_3411.csproj` targets `net10.0` with nullable reference types and implicit usings enabled.
- `Problem_Description.md` records the problem statement, examples, and constraints in Traditional Chinese.
- `.vscode/` contains the build task and debug configuration. Keep generated `bin/` and `obj/` directories out of commits.

## Build and Run

Use the .NET 10 SDK. Run these commands from the directory containing this guide.

- `dotnet build leetcode_3411/leetcode_3411.csproj` restores dependencies and compiles the console app.
- `dotnet run --project leetcode_3411/leetcode_3411.csproj` builds and runs the console entry point.
- In VS Code, open this folder and select **Debug leetcode_3411** to build and launch under the debugger.

## Coding Style

Follow `.editorconfig`. Use four spaces in C# and two in project XML and JSON. Put opening braces on their own line, prefer explicit local types, and use file-scoped namespaces. Use PascalCase for types and methods and camelCase for parameters and locals.

## Testing

The project has no automated test suite or coverage target. The current console entry point prints `Hello, World!`. When adding or changing a solution, run the examples in `Problem_Description.md` and check boundary cases such as a two-element array, all ones, and repeated factors. Record the expected and actual lengths.

## Commits and Pull Requests

Parent repository history favors short, action-oriented subjects such as `Add LeetCode 1352 problem description`. Include the problem number when it clarifies scope. In a pull request, summarize the change, list the checks you ran, and link any related issue. For algorithm changes, describe the approach and give its time and space complexity.
