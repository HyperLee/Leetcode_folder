# Repository Guidelines

## Project Structure

This folder contains a C# console project for LeetCode 3105, Longest Strictly Increasing or Strictly Decreasing Subarray. `leetcode_3105/Program.cs` contains the entry point and the bilingual problem statement. Keep the problem statement intact when adding or revising a solution. `leetcode_3105/leetcode_3105.csproj` targets `net10.0`. `.vscode/` holds the build and debug configurations. There are no separate test or asset directories.

## Build and Run

Install the .NET 10 SDK and run these commands from the directory containing this guide:

- `dotnet build leetcode_3105/leetcode_3105.csproj` restores dependencies and compiles the project.
- `dotnet run --project leetcode_3105/leetcode_3105.csproj` builds and runs the console app. The starter currently prints `Hello, World!`.
- In VS Code, open this folder and press **F5** with **Debug LeetCode 3105** selected to build and launch the Debug executable with the C# debugger.

## Coding Style

Follow `.editorconfig`: use four spaces for C# and two spaces for project XML and JSON. Match `Program.cs` by using a file-scoped namespace and placing braces on their own lines. Use PascalCase for methods and camelCase for local variables. Choose names that explain what a counter measures, such as `increasingLength`.

## Testing

There is no test framework or coverage threshold yet. Build and run the project after changes. When adding a solution, include runnable cases with descriptive labels and explicit expected lengths. Cover a single element, equal adjacent values, strictly increasing and decreasing arrays, and a change in direction. Equality breaks a strictly monotonic run; `[1, 2, 2, 3]` should return `2`.

## Commits and Pull Requests

The parent LeetCode repository uses short, action-oriented commit subjects, often naming the problem or method. Follow that pattern, for example `Add linear scan for LeetCode 3105`. In a pull request, describe the change, list the validation you ran, and link a related issue if one exists. Keep generated `bin/` and `obj/` files out of commits.
