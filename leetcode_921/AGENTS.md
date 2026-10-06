# Repository Guidelines

## Project Structure & Module Organization

This repository is a single .NET 10 console project for LeetCode 921, “Minimum Add to Make Parentheses Valid.” The main paths are:

- `leetcode_921/leetcode_921.csproj` — executable project definition and target framework.
- `leetcode_921/Program.cs` — the greedy and stack solutions plus the deterministic console runner.
- `README.md` — Traditional-Chinese problem statement, derivations, traces, and complexity notes.
- `docs/readme-template.md` — reusable documentation template.
- `.vscode/tasks.json` and `.vscode/launch.json` — build and CoreCLR launch configuration.

`bin/` and `obj/` are generated outputs; do not commit them.

## Build, Test, and Development Commands

Run these commands from the repository root:

- `dotnet restore leetcode_921/leetcode_921.csproj` — restore the project’s SDK dependencies.
- `dotnet build leetcode_921/leetcode_921.csproj --configuration Debug` — compile the project.
- `dotnet run --project leetcode_921/leetcode_921.csproj` — execute the fixed examples.

The runner reads no standard input. It prints each expected/actual result and returns a nonzero exit code if any check fails. There is no separate test project or coverage threshold; use the runner together with a successful build as the current validation path.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four-space indentation for C#, braces on their own lines, and no tabs. Use file-scoped namespaces, PascalCase for types and public methods, and camelCase for locals and parameters. Prefer explicit types in new C# code, keep public methods documented with XML comments, and preserve the concise algorithm-focused style of `Program.cs`. Avoid unrelated reformatting.

## Testing Guidelines

Keep deterministic cases in `Program.Main` representative of empty, balanced, unmatched, and mixed parentheses. When changing an algorithm, add expected-value cases, run the command above, and confirm both implementations report all checks as `PASS` with exit code 0.

## Commit & Pull Request Guidelines

Recent commits use short imperative subjects such as `Add...`, `Clarify...`, and `Improve...`, without a trailing period. Keep commits focused. Pull requests should explain the algorithm or documentation change, list validation commands and relevant output, link an issue when one exists, and call out any user-visible README changes. Screenshots are usually unnecessary for this console project.

## Security & Configuration Tips

Do not add secrets, machine-specific paths, or unnecessary runtime dependencies. Before committing, review the diff and run `git diff --check`.
