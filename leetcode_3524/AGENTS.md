# Repository Guidelines

## Project Structure

This checkout contains one SDK-style .NET 10 console project. Keep the algorithm and `Main` entry point in `leetcode_3524/Program.cs`; keep framework and build metadata in `leetcode_3524/leetcode_3524.csproj`. Root `.vscode/launch.json` and `.vscode/tasks.json` provide the configured build/debug path. `docs/readme-template.md` is the template for a future README. There is currently no separate test or asset directory. `bin/` and `obj/` under the project are generated and ignored.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore .\leetcode_3524\leetcode_3524.csproj
dotnet build .\leetcode_3524\leetcode_3524.csproj --nologo --no-restore
dotnet run --project .\leetcode_3524\leetcode_3524.csproj --no-build --no-restore
```

Restore resolves SDK inputs, build compiles the project, and run performs the console smoke check. In VS Code, press F5 or use `Run leetcode_3524`; the launch configuration invokes the matching pre-launch build task. No test project or coverage threshold is configured yet.

## Coding Style & Naming Conventions

Follow the root `.editorconfig`: use four spaces in C#, spaces rather than tabs, file-scoped namespaces, braces for control flow, and explicit local types by default. Use PascalCase for types, methods, and properties; camelCase for locals and parameters; `I...` for interfaces; and `_camelCase` for private instance fields. Keep nullable reference types and implicit usings enabled, and keep the bilingual XML problem statement and links accurate when changing the solution.

## Testing Guidelines

Until a test project is added, verify solution changes with a successful build and deterministic console run. Avoid interactive input so checks remain reproducible. If focused tests are introduced, keep them separate from the solution project and report the exact test command and result in the pull request.

## Commit & Pull Request Guidelines

Recent history uses short, imperative subjects such as `Add ...`, `Document ...`, and `Initialize ...`. Keep each commit focused on one logical change. Pull requests should explain the problem or algorithm, list affected paths, link an issue when applicable, and include build/run or test commands with their results. Add screenshots only when a documentation or IDE change has meaningful visual output.

## Security & Configuration

Do not commit credentials, secrets, machine-specific settings, or generated `bin/` and `obj/` output. Keep solution-only changes within this nested project unless a broader repository update is intentional.
