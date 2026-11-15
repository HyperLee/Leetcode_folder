# Repository Guidelines

## Project Structure

This folder contains the C# console project for LeetCode 1524, *Number of Sub-arrays With Odd Sum*. `leetcode_1524/Program.cs` contains the entry point and the bilingual problem summary. The project file, `leetcode_1524/leetcode_1524.csproj`, targets `net10.0` and enables nullable references and implicit usings. `.vscode/` contains the build task and debugger configuration. Keep generated `bin/` and `obj/` output out of commits.

## Build and Run

Use the .NET 10 SDK and run these commands from the directory containing this guide.

- `dotnet build leetcode_1524/leetcode_1524.csproj` restores dependencies and compiles the project.
- `dotnet run --project leetcode_1524/leetcode_1524.csproj` builds and runs the console app.
- In VS Code, select **Debug LeetCode 1524** to build and launch under the debugger.

## Coding Style

Follow `.editorconfig`. Use four spaces and put opening braces on their own lines in C#. Use two spaces for JSON and project XML. Prefer explicit local types, `PascalCase` for methods and types, and `camelCase` for parameters and local variables. Preserve the bilingual problem summary when editing `Program.cs`.

## Testing

The console app currently prints `Hello, World!`. Building and running it is a smoke check. When adding a solution, verify expected results with focused cases such as `[1, 3, 5]` → `4`, `[2, 4, 6]` → `0`, and `[1]` → `1`. For changes to the counting logic, include a large input that exercises the modulo requirement.

## Commits and Pull Requests

Follow the parent repository’s pattern of short, action-oriented subjects, such as `Add LeetCode 1524 solution`. Keep changes scoped to this exercise. In the pull request, summarize the change and list the commands and cases you verified. For algorithm changes, include time and space complexity. Link a related issue when one exists.
