# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET 10 console project for LeetCode 3568, with the workspace root one level above the executable project. The solution and fixed runner are in `leetcode_3568/Program.cs`; project metadata is in `leetcode_3568/leetcode_3568.csproj`. The root `README.md` documents the BFS, bitmask, and dominance-pruning approach, while `docs/readme-template.md` is a documentation reference. `.vscode/` contains the CoreCLR launch profile and build task. `bin/` and `obj/` are generated output and should remain ignored. There is currently no separate test project or asset directory.

## Build, Test, and Development Commands

Run these commands from the repository root:

- `dotnet build .\leetcode_3568\leetcode_3568.csproj --nologo` builds the project and reports compiler warnings or errors.
- `dotnet run --project .\leetcode_3568\leetcode_3568.csproj --no-build` runs the fixed three-case harness in `Main` and prints PASS/FAIL results.
- In VS Code, press F5 to use `.vscode/launch.json`; its pre-launch task builds before starting the CoreCLR program.

Build before using `--no-build`, especially after changing `Program.cs` or the project file.

## Coding Style & Naming Conventions

Follow the repository `.editorconfig`: use spaces, four-space indentation for C#, and two-space indentation for JSON and project files. Keep the existing block-scoped namespace, nullable, and implicit-using settings. Use PascalCase for public APIs such as `MinMoves`, camelCase for locals and parameters, and descriptive names for fixed test cases. Keep XML comments and README explanations consistent with the actual algorithm and output.

## Testing Guidelines

There is no xUnit, NUnit, or MSTest project. The executable’s `Main` method calls `RunTestCase` for three deterministic examples and serves as the current regression check. When changing the algorithm, update or add focused cases in that harness as needed, then confirm every case reports `PASS`; there is no configured coverage threshold.

## Commit & Pull Request Guidelines

Recent history favors short, imperative subjects such as `Add ...` and `Clarify ...`, with occasional concise Chinese subjects. Keep each commit focused. Pull requests should explain the algorithm or documentation change, include build/run commands and results, link an issue when applicable, and avoid committing `bin/` or `obj/` output.

## Security & Configuration Tips

Do not commit credentials or other secret material. For cleanup, use targeted named-file operations and avoid broad recursive deletion commands.
