# Repository Guidelines

## Project Structure

This repository contains a .NET 10 console project for LeetCode 3882.

- `leetcode_3882/Program.cs` is the entry point and holds the problem description; implement the solution logic there.
- `leetcode_3882/leetcode_3882.csproj` defines the executable project, nullable checks, and implicit usings.
- `.editorconfig` records C# formatting preferences; `.vscode/` contains build and debug tasks.
- `docs/readme-template.md` is a template for creating the project README. There is currently no test project or separate assets directory.

## Build and Run

Run these from the repository root with the .NET 10 SDK installed:

```sh
dotnet build leetcode_3882/leetcode_3882.csproj
dotnet run --project leetcode_3882/leetcode_3882.csproj
```

The VS Code build task runs the Debug build, and its launch configuration starts the project under the CoreCLR debugger. No automated test command is configured because the repository has no test project.

## Coding Style

Follow `.editorconfig`: use four spaces in C# files, braces on separate lines, and sorted `System` usings. Use `PascalCase` for types and methods and `camelCase` for parameters and locals. Keep nullable annotations enabled and preserve the existing English and Traditional Chinese problem summaries when editing them. No separate formatter or linter is configured.

## Testing Guidelines

After implementing or changing a solution, build the project and check the problem's sample cases and relevant edge cases. If automated regression coverage is needed, add a dedicated test project rather than placing test-only behavior in the console entry point.

## Commits and Pull Requests

Recent commit subjects use short imperative verbs such as “Add,” “Implement,” and “Clarify”; follow that pattern (for example, `Implement LeetCode 3882 solution`). No PR template is present. Describe the problem and algorithm, include time and space complexity, and report the build and sample-case checks performed. Screenshots are unnecessary for this console-only project.
