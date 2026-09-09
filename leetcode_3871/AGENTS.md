# Repository Guidelines

## Project Structure & Module Organization

This checkout is a small SDK-style .NET console project for LeetCode 3871. The runnable project is nested under `leetcode_3871/`:

- `leetcode_3871/Program.cs` contains the entry point and problem documentation/solution code.
- `leetcode_3871/leetcode_3871.csproj` targets `net10.0`.
- `.vscode/launch.json` and `.vscode/tasks.json` provide the no-input CoreCLR F5 profile and its build task.
- `.editorconfig`, `.gitattributes`, and `.gitignore` define repository-wide conventions; `docs/readme-template.md` is the initial-README template.

Generated `bin/` and `obj/` folders belong under the nested project and are ignored. Keep future automated tests in a separate test project; none is present today.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore .\leetcode_3871\leetcode_3871.csproj
dotnet build .\leetcode_3871\leetcode_3871.csproj --nologo
dotnet run --project .\leetcode_3871\leetcode_3871.csproj
```

Restore dependencies, compile the `net10.0` project, and execute the console app respectively. The current scaffold prints `Hello, World!`. In VS Code, press F5 with `Run leetcode_3871`; it builds first and passes no command-line arguments.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four spaces, no tabs, braces on all control blocks, and no required final newline. Use file-scoped namespaces, PascalCase for types and methods, camelCase for locals and parameters, and `_camelCase` for private fields. Prefer explicit types over `var` where the configured rule applies. Keep LeetCode problem links and XML documentation accurate when changing `Program.cs`. No separate formatter or linter is configured; rely on the editor settings and compiler diagnostics.

## Testing Guidelines

There is no test framework or coverage threshold configured. Until a test project is added, use the build plus a no-input `dotnet run` as the smoke check. When implementing the solution, add deterministic boundary cases around comma thresholds and the inclusive `n` endpoint; name cases by scenario, such as `Includes1000`.

## Commit & Pull Request Guidelines

Recent commits use short imperative subjects, for example `Add ...` and `Unescape ...`. Keep commits focused and use the same style. Pull requests should explain the algorithm or configuration change, list the exact validation commands and results, link a related issue when available, and call out documentation-only changes. Screenshots are unnecessary for console-only changes unless they clarify debugger or output behavior.
