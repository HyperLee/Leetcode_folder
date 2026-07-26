# Repository Guidelines

## Project Structure & Module Organization

The repository root contains workspace-wide configuration: `.editorconfig`, Git attributes/ignore rules, and VS Code launch and build tasks. The executable project is nested under `leetcode_2591/`; its `Program.cs` contains the LeetCode 2591 entry point and solution code, while `leetcode_2591.csproj` targets .NET 10. Keep problem-specific source in this project folder. Use `docs/readme-template.md` only as a structural reference when preparing a future `README.md`; documentation must match the implemented algorithm and actual console output. Generated `bin/` and `obj/` directories must remain untracked.

## Build, Test, and Development Commands

- `dotnet build .\leetcode_2591\leetcode_2591.csproj` — restore dependencies and compile the console application.
- `dotnet run --project .\leetcode_2591\leetcode_2591.csproj` — build and execute the current examples.
- `dotnet format .\leetcode_2591\leetcode_2591.csproj --verify-no-changes` — check formatting against `.editorconfig`.

From VS Code, press F5 with **Debug leetcode_2591**. The configured pre-launch task builds the same project and launches its `net10.0` DLL.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space indentation for C#, two spaces for project and JSON files, braces for control blocks, and file-scoped namespaces where practical. Use `PascalCase` for types and methods, `camelCase` for parameters and locals, `_camelCase` for private instance fields, and `s_camelCase` for private static fields. Nullable reference types and implicit usings are enabled. Add comments for algorithm intent, edge cases, and non-obvious choices rather than narrating each line.

## Testing Guidelines

No test framework is configured yet. Until one is added, place deterministic table-driven examples in `Main`, compare actual and expected values, and print clear PASS/FAIL results. Cover the official examples plus boundary cases such as insufficient money and distributions that would leave a child with exactly four dollars. Run both `dotnet build` and `dotnet run` before submitting changes.

## Commit & Pull Request Guidelines

Recent history uses concise subjects, including problem-number summaries and prefixes such as `chore:`. Prefer an imperative subject, for example `feat: implement greedy distribution` or `docs: explain edge cases`. Keep each commit focused. Pull requests should summarize the algorithm, explain complexity and edge-case handling, list verification commands and results, and link the relevant issue when available. Include screenshots only when console or documentation presentation materially changes.
