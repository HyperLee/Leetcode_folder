# Repository Guidelines

## Project Structure & Module Organization
This repository is a small .NET console workspace for LeetCode 198. The main code lives under `leetcode_198/`: `Program.cs` contains the entry point and solution methods, and `leetcode_198.csproj` targets `net10.0`. Root-level files such as `.editorconfig`, `.gitignore`, `.gitattributes`, and `.vscode/` define shared tooling behavior. `docs/readme-template.md` is a documentation helper. There is currently no separate test project or assets folder.

## Build, Test, and Development Commands
Use explicit project paths from the repository root:

- `dotnet build leetcode_198/leetcode_198.csproj` builds the app. This matches the default VS Code build task.
- `dotnet run --project leetcode_198/leetcode_198.csproj` runs `Main` for a quick smoke test.
- `dotnet test` at the repository root does not work today because no solution or test project exists there (`MSB1003`).

For local debugging, `.vscode/launch.json` launches `leetcode_198/bin/Debug/net10.0/leetcode_198.dll` after building.

## Coding Style & Naming Conventions
Follow `.editorconfig` exactly:

- Use 4 spaces in `*.cs`; use 2 spaces in JSON, XML, and project files.
- Keep file-scoped namespaces and nullable-aware code enabled.
- Prefer explicit types over `var` unless the type is obvious from the right-hand side.
- Use PascalCase for classes and methods, for example `Program`, `Rob`, and `Rob2`.

Keep solutions straightforward and repository-sized: for this repo, it is usually better to extend `Program.cs` than to introduce extra layers.

## Testing Guidelines
There is no automated test harness yet. For algorithm changes, verify behavior by updating `Main` with representative sample inputs and rerunning the project. If you introduce a real test project later, place it beside `leetcode_198/` and run it with an explicit path such as `dotnet test leetcode_198.Tests/leetcode_198.Tests.csproj`.

## Commit & Pull Request Guidelines
Recent history uses short, focused messages, sometimes in Chinese and sometimes with prefixes like `feat:`. Prefer concise, imperative commits such as `feat: add DP sample cases` or `docs: refine house robber notes`.

Pull requests should explain what changed, mention the affected problem number, and list the commands you ran locally. Include screenshots only when the change affects editor or tooling configuration.
