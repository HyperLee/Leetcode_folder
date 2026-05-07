# Repository Guidelines

## Project Structure & Module Organization
This repository contains a single C# console solution. The root `leetcode_3660.sln` references the application project in `leetcode_3660/`.

- `leetcode_3660/Program.cs` is the current entry point.
- `leetcode_3660/leetcode_3660.csproj` targets `net10.0` with nullable reference types and implicit usings enabled.
- `.editorconfig` defines formatting and naming rules.
- `.vscode/tasks.json` and `.vscode/launch.json` provide build, run, and debug shortcuts.

There is no dedicated test project in the current tree. If tests are added, prefer a sibling project such as `leetcode_3660.Tests/` and include it in `leetcode_3660.sln`.

## Build, Test, and Development Commands
Run commands from the repository root unless noted otherwise.

- `dotnet restore leetcode_3660.sln` restores NuGet dependencies.
- `dotnet build leetcode_3660.sln -c Debug` builds the solution.
- `dotnet run --project leetcode_3660/leetcode_3660.csproj -c Debug` runs the console app.
- `dotnet format leetcode_3660.sln` applies `.editorconfig` formatting when the SDK command is available.
- `dotnet test leetcode_3660.sln` runs tests after a test project has been added to the solution.

## Coding Style & Naming Conventions
Follow `.editorconfig`. Use 4-space indentation for C# and place opening braces on new lines. Prefer explicit types over `var` unless project conventions change. Keep nullable annotations correct; do not silence nullable warnings without a specific reason.

Use PascalCase for namespaces, classes, methods, properties, enums, and public fields. Use camelCase for locals and parameters. Use `_camelCase` for private instance fields and `s_camelCase` for private static fields.

## Testing Guidelines
No coverage threshold is currently configured. For new algorithm work, add focused unit tests covering normal cases, edge cases, and malformed or minimal input. Name test methods descriptively, for example `RotateTheBox_MovesStonesAroundObstacles`.

Keep test data small and readable. If a future framework is introduced, document it here and keep tests runnable through `dotnet test leetcode_3660.sln`.

## Commit & Pull Request Guidelines
Recent history uses short Conventional Commit-style prefixes such as `feat:` and `fix:`, with concise Chinese descriptions. Continue that style, for example `feat: 新增邊界案例測試` or `fix: 修正空輸入處理`.

Pull requests should describe the behavior change, list verification commands run, and link any related issue. Include console output or screenshots only when they clarify changed behavior. Avoid committing generated `bin/` or `obj/` outputs.
