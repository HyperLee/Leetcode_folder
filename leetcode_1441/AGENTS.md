# Repository Guidelines

## Project Structure & Module Organization

- `leetcode_1441/Program.cs` contains the console entry point and the three `BuildArray` solution implementations for LeetCode 1441.
- `leetcode_1441/leetcode_1441.csproj` is the SDK-style .NET 10 console project with nullable reference types and implicit usings enabled.
- `.vscode/launch.json` and `.vscode/tasks.json` provide the direct F5 debug profile and its pre-launch build task.
- `docs/readme-template.md` is a prompt/template for creating a project README. There is currently no separate test project or asset directory.

## Build, Test, and Development Commands

Run these commands from the repository root:

```powershell
dotnet build .\leetcode_1441\leetcode_1441.csproj
dotnet run --project .\leetcode_1441\leetcode_1441.csproj
```

`dotnet build` verifies compilation; `dotnet run` executes `Main`. The current `Main` prints a simple greeting, while the solution methods are intended to be exercised through a local sample harness or debugger. In VS Code, press F5 to use the checked-in `coreclr` launch configuration.

## Coding Style & Naming Conventions

Use four spaces for C# indentation, braces on the declaration line, and the existing nullable/implicit-using project settings. Use PascalCase for types and methods (`BuildArray2`) and camelCase for parameters and local variables (`target`, `prev`). Keep XML documentation accurate; preserve the existing English and Traditional Chinese problem explanations when editing them. No formatter or linter is configured, so run `git diff --check` before submitting changes.

## Testing Guidelines

There is no automated test framework or coverage threshold in this repository. For algorithm changes, manually verify representative cases such as `[1,3]` with `n = 3`, gaps before a target value, and a target that ends before `n` by running a temporary/local harness or debugger. Record the build and execution commands used in the pull request.

## Commit & Pull Request Guidelines

Use concise Conventional Commit-style subjects consistent with repository history, for example `feat(leetcode-1441): add sample runner`, `fix(leetcode-1441): correct stack simulation`, or `docs: clarify BuildArray2`. Pull requests should explain the behavior changed, identify the relevant files, include validation commands and results, and link an issue when applicable. Screenshots are unnecessary unless the change adds a user-facing visual.

## Security & Repository Hygiene

Do not commit secrets, local machine settings, or generated `bin/` and `obj/` output. Keep documentation and VS Code paths synchronized with the actual nested project layout.
