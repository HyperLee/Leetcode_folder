# Repository Guidelines

## Project Structure & Module Organization

This project directory contains one .NET console project. Keep solution code and the entry point in `leetcode_3622/Program.cs`; the SDK-style project file is `leetcode_3622/leetcode_3622.csproj` and targets `net10.0`. Use `docs/readme-template.md` only as the starting template for a future README. There is no separate test or asset directory at present; add project-specific files under `leetcode_3622/` unless a broader structure is justified.

## Build, Test, and Development Commands

Run these commands from `Leetcode_folder/leetcode_3622`:

```bash
dotnet restore leetcode_3622/leetcode_3622.csproj
dotnet build leetcode_3622/leetcode_3622.csproj --nologo
dotnet run --project leetcode_3622/leetcode_3622.csproj --no-build
```

Restore resolves dependencies, build compiles the project, and the final command runs the latest successful build. There is currently no test project, so `dotnet test` is not a meaningful check; use the executable's deterministic output as the acceptance check.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C# indentation, spaces rather than tabs, braces for control blocks, and no inserted final newline. Prefer explicit types over `var`. Use PascalCase for types, methods, and properties; camelCase for locals and parameters; and `_camelCase` for private fields. Keep namespaces and project names aligned with `leetcode_3622`.

## Testing Guidelines

When adding or changing solution logic, include representative cases in the console harness and label expected versus actual results so failures are obvious. Keep runs non-interactive and free of `Console.ReadKey()` so they work in CI or with redirected input. Before submitting, run restore, build, and run; report any known limitation or unverified edge case.

## Commit & Pull Request Guidelines

Recent parent-repository history includes Conventional Commit-style messages such as `feat(leetcode-3069): add solution and docs`, alongside older Chinese summaries. Prefer `type(scope): imperative summary`, for example `feat(leetcode-3622): implement solution`, and keep each commit focused. Pull requests should explain the algorithm or behavior change, link the relevant issue when one exists, and list the exact verification commands and results. Include screenshots only when a documentation or rendered-output change benefits from visual evidence.