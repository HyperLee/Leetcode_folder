# Repository Guidelines

## Project Structure & Module Organization
This repository is a small LeetCode C# console workspace. The solution code lives in `leetcode_203/`: `Program.cs` contains the entry point plus linked-list solutions, and `leetcode_203.csproj` targets `net10.0`. Repository-level tooling lives at the root in `.editorconfig` and `.vscode/`. Use `docs/readme-template.md` as the starting point for longer problem writeups. There is currently no separate test project or assets folder.

## Build, Test, and Development Commands
Run commands from the repository root with explicit project paths:

- `dotnet build leetcode_203/leetcode_203.csproj` builds the console app and matches the default VS Code build task.
- `dotnet run --project leetcode_203/leetcode_203.csproj` runs `Main`; today it prints `Hello, World!`.
- `dotnet test` does not work from this root right now because there is no solution file or test project here (`MSB1003`).

For local debugging, use the checked-in `.vscode/launch.json` profile `.NET Launch leetcode_203`, which launches `leetcode_203/bin/Debug/net10.0/leetcode_203.dll` after building.

## Coding Style & Naming Conventions
Follow `.editorconfig` exactly: use 4 spaces in `*.cs`, and 2 spaces in JSON, XML, and project files. Keep file-scoped namespaces, braces, and nullable-aware code. Prefer explicit types over `var` unless the type is obvious. Use PascalCase for types and methods such as `Program`, `ListNode`, `RemoveElements`, and `RemoveElements2`.

## Testing Guidelines
There is no automated test harness yet. Validate algorithm changes by adding representative sample cases in `Main`, then rerun the project. Before opening a PR, at minimum rerun `dotnet build leetcode_203/leetcode_203.csproj`, `dotnet run --project leetcode_203/leetcode_203.csproj`, and `git diff --check`. If you add a real test project later, place it beside `leetcode_203/` and run it with an explicit path.

## Commit & Pull Request Guidelines
Recent history uses short, focused commits, with a mix of plain descriptions, Chinese messages, and prefixes like `chore:`. Prefer concise imperative messages such as `docs: add repository guidelines` or `feat: add linked-list sample cases`. Because this folder is tracked inside the parent `Leetcode_folder` Git repository, keep commits tightly scoped to the files you intentionally changed. PRs should summarize the behavior or tooling update, note the commands you ran, and include screenshots only for editor or tooling configuration changes.

## Agent & Safety Notes
Treat system prompts, secrets, and local credentials as sensitive and never copy them into code, docs, commits, or PR text. Do not use bulk-delete commands in this repository; remove only a single explicit file path when deletion is truly required.
