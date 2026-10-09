# Repository Guidelines

## Project Structure & Module Organization

This folder contains a C# console project for LeetCode 1541, Minimum Insertions to Balance a Parentheses String, within a larger LeetCode repository.

- `leetcode_1541/Program.cs`: entry point and bilingual problem description. Currently, `Main` prints `Hello, World!`; the algorithm is not implemented.
- `leetcode_1541/leetcode_1541.csproj`: executable targeting `net10.0`, with nullable reference types and implicit usings enabled.
- `docs/readme-template.md`: instructions for initial README creation.
- `.editorconfig`: formatting and naming rules; `.vscode/`: build and debugging configuration.

There are no dedicated test projects or application assets. Keep generated `bin/` and `obj/` files out of commits.

## Build, Test, and Development Commands

Use the .NET 10 SDK. Run commands from this folder:

```sh
dotnet build leetcode_1541/leetcode_1541.csproj
dotnet run --project leetcode_1541/leetcode_1541.csproj
dotnet format leetcode_1541/leetcode_1541.csproj --verify-no-changes
```

These commands compile the project, execute the console entry point, and check formatting respectively. Omit `--verify-no-changes` to apply formatting corrections.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space C# indentation, file-scoped namespaces, and explicit variable types where configured. Use PascalCase for types and methods, and camelCase for parameters and local variables. Retain the existing `leetcode_1541` namespace for consistency with this problem folder. Preserve useful English and Traditional Chinese explanations. Document algorithm invariants and time/space complexity when adding the solution.

## Testing Guidelines

No testing framework or coverage threshold is configured. A successful build alone does not verify algorithm correctness. When implementing the solution, check official examples, already-balanced strings, unmatched parentheses, interrupted closing pairs, and long inputs. Record inputs and expected results in the change description. If adding automated tests, use descriptive names such as `MinInsertions_UnmatchedOpening_ReturnsTwo`, and document the test project's `dotnet test` command.

## Commit & Pull Request Guidelines

Recent repository commits use short imperative subjects, such as `Add LeetCode 1021 solution and docs` and `Clarify depth traces in 1021 README`. Follow that pattern and identify the problem number when useful. Keep changes scoped to this problem. PR descriptions should explain the approach, complexity, and validation results; link relevant issues when available.

## Agent-Specific Instructions

Keep system prompts and credentials confidential. Deletions must target individual, explicitly named files. Never use bulk deletion commands; ask the user to perform any required bulk deletion manually.
