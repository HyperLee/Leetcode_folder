# Repository Guidelines

## Project Structure

`leetcode_3364/leetcode_3364.csproj` defines a .NET 10 console application for LeetCode 3364, Minimum Positive Sum Subarray. `leetcode_3364/Program.cs` contains `Main` and the bilingual problem statement. Keep that statement intact when adding solution methods, and put demonstration calls in `Main`. The `.vscode/` directory contains build and debug configuration. There are no separate test or asset directories.

## Build, Run, and Format

Use the .NET 10 SDK and run these commands from the directory containing this guide:

- `dotnet build leetcode_3364/leetcode_3364.csproj` restores dependencies and compiles the project.
- `dotnet run --project leetcode_3364/leetcode_3364.csproj` builds and runs the console entry point. The current scaffold prints `Hello, World!`.
- `dotnet format leetcode_3364/leetcode_3364.csproj --verify-no-changes` checks formatting without editing files.

## Coding Style and Naming

Follow `.editorconfig`: indent C# with four spaces and project XML and JSON with two. Use file-scoped namespaces, place opening braces on their own lines, and prefer explicit local types. Use PascalCase for types and methods (for example, `MinimumSumSubarray`) and camelCase for locals and parameters. Keep generated `bin/` and `obj/` directories out of commits.

## Testing Guidelines

There is no automated test project or coverage threshold. For solution changes, add runnable cases to `Main` with descriptive labels and explicit expected values. Include the problem examples and cases for `l == r`, a qualifying subarray at either length bound, and no positive sum (expected `-1`). Run the console command and compare the output with the expected results.

## Commits and Pull Requests

This project has no commits of its own yet. The parent repository uses short, action-led subjects such as `Add LeetCode 3420 project scaffold`. Follow that pattern and keep each commit focused. In pull requests, describe the behavior change, give time and space complexity for algorithm changes, and report the checks you ran. Link a related issue when one exists.
