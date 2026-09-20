# Repository Guidelines

## Project Structure

This repository contains one .NET 10 console project. Keep the entry point and solution code in `leetcode_3498/Program.cs` and project metadata in `leetcode_3498/leetcode_3498.csproj`. The root `.vscode/launch.json` and `.vscode/tasks.json` provide the direct build/debug setup. `docs/readme-template.md` is the documentation template. Build output is generated under `leetcode_3498/bin/` and `leetcode_3498/obj/`; both are ignored. There is currently no separate test project or checked-in asset directory.

## Build, Test, and Development Commands

Run these commands from the repository root:

```bash
dotnet restore leetcode_3498/leetcode_3498.csproj
dotnet build leetcode_3498/leetcode_3498.csproj --nologo --no-restore
dotnet run --project leetcode_3498/leetcode_3498.csproj --no-build --no-restore --nologo
dotnet format leetcode_3498/leetcode_3498.csproj --verify-no-changes --no-restore
```

Restore downloads/resolves dependencies, build compiles the project, run performs the current console smoke check, and format verifies `.editorconfig` compliance. In VS Code, use the supplied `Debug leetcode_3498` configuration; it invokes the default `build leetcode_3498` task first.

## Coding Style & Naming Conventions

Use four-space indentation and spaces, file-scoped namespaces, braces for control flow, and explicit types where practical. Follow the root `.editorconfig`, including its C# spacing and modifier rules. Use PascalCase for types, methods, and properties; camelCase for locals and parameters; prefix interfaces with `I`. Keep XML documentation and problem links accurate when changing the solution.

## Testing Guidelines

No unit-test framework or coverage threshold is configured. For the current scaffold, a successful `dotnet run` is the smoke test. When adding algorithm behavior, include deterministic input/output checks or a focused test project, avoid requiring interactive input, and report the commands and results in the pull request.

## Commit & Pull Request Guidelines

Use short, focused subjects consistent with history, such as `Add ...`, `Document ...`, `Initialize ...`, or an equally clear Chinese subject. Pull requests should explain the purpose and affected paths, link an issue when applicable, and list verification commands with their results. Include screenshots only when a VS Code or other user-visible configuration change needs visual evidence.

## Security & Configuration

Never commit credentials or local configuration; `.env` is ignored for this purpose. Keep generated files, personal IDE state, and build artifacts out of commits, and limit changes to this nested project unless the task explicitly requires broader repository updates.