# Repository Guidelines

## Project Structure & Module Organization

This repository is a small SDK-style .NET 10 console project for LeetCode 3903.

- `leetcode_3903/Program.cs` contains the problem summary, solution entry point, and local execution code.
- `leetcode_3903/leetcode_3903.csproj` is the project file and targets `net10.0` with nullable reference types enabled.
- `.vscode/launch.json` and `.vscode/tasks.json` provide the CoreCLR F5 profile and its pre-launch build task.
- `docs/readme-template.md` is a template for future project documentation.
- `bin/` and `obj/` are generated build folders and are ignored by Git. There are currently no dedicated test or asset directories.

## Build, Test, and Development Commands

Run these commands from the repository root:

```powershell
dotnet build .\leetcode_3903\leetcode_3903.csproj --nologo
dotnet run --project .\leetcode_3903\leetcode_3903.csproj --no-build
```

The first command compiles the project. The second runs the existing no-input console entry point after a successful build. In VS Code, use **Run leetcode_3903**; it invokes the matching build task automatically.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four spaces for C# indentation, no tabs, braces on their own lines, and a file-scoped namespace where appropriate. Use PascalCase for types and methods, and camelCase for locals and parameters. Keep nullable annotations valid and retain the existing XML problem documentation and public method signatures when changing the solution.

## Testing Guidelines

No test framework or test project is configured yet. For every code change, run the build and execute the console program, then inspect the output for the intended behavior. If automated tests are added, place them in a clearly named test project and name cases by behavior, for example `ReturnsSmallestStableIndex_WhenIndexIsValid`.

## Commit & Pull Request Guidelines

Recent history uses short, imperative subjects such as `Add ...` and scoped documentation subjects such as `README: ...`. Keep commits focused. Pull requests should explain the behavior or algorithm change, list the exact validation commands and results, link a related issue when one exists, and include a terminal transcript or screenshot only when it clarifies user-visible output.

## Security & Configuration Tips

Do not commit credentials, machine-specific settings, or generated `bin/` and `obj/` contents. Keep changes limited to the source, documentation, and configuration files relevant to the change.
