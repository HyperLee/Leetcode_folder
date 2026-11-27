# Repository Guidelines

## Project Structure

This folder contains the C# console project for LeetCode 3946, Maximum Number of Items From Sale I. `leetcode_3946/Program.cs` contains the entry point and the English and Traditional Chinese problem statement in XML documentation. `leetcode_3946/leetcode_3946.csproj` targets `net11.0` and enables nullable reference types and implicit usings. `.vscode/` contains the build and debug configuration. There are currently no separate test or asset directories. Keep generated `bin/` and `obj/` files out of commits.

## Build and Run

Use the .NET 11 SDK and run these commands from the directory containing this guide.

- `dotnet build leetcode_3946/leetcode_3946.csproj` restores dependencies and compiles the project.
- `dotnet run --project leetcode_3946/leetcode_3946.csproj` builds and runs the console entry point. The starter program prints `Hello, World!`.
- In VS Code, select **Run leetcode_3946** to build and launch the debugger.

## Coding Style

Follow `.editorconfig`. Use four spaces for C# and two for project XML and JSON. Keep opening braces on their own lines, use a file-scoped namespace, and prefer explicit local types. Use `PascalCase` for types and methods and `camelCase` for parameters and local variables. Keep the existing `leetcode_3946` namespace and preserve the bilingual problem documentation when editing `Program.cs`.

## Testing

The project currently has no test runner or coverage threshold. For solution changes, verify the problem examples and check budgets that buy zero items or exactly one item. Include cases with duplicate factors and repeat purchases of the same item type. Give each case a descriptive name and record its expected item count.

## Commits and Pull Requests

The parent repository uses short, action-oriented subjects such as `Add LeetCode 3360 project scaffold`. Keep changes focused on problem 3946. In the pull request, explain the change and list the commands and cases you ran. For algorithm changes, include time and space complexity. Link related issues when available.
