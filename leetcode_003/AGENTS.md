# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 10 console solution for LeetCode 3. The root `leetcode_003.sln` references `leetcode_003/leetcode_003.csproj`. Algorithm implementations, XML documentation, and the sample console entry point are all in `leetcode_003/Program.cs`. VS Code build and debug configuration lives in `.vscode/`; `.editorconfig` defines formatting and naming rules. `docs/readme-template.md` is guidance for creating a future README, not runtime content. Generated `bin/` and `obj/` directories must remain untracked. There is currently no test project or assets directory.

## Build, Test, and Development Commands

Run commands from this repository root:

- `dotnet restore leetcode_003.sln` restores project dependencies.
- `dotnet build leetcode_003.sln --nologo` compiles the solution for `net10.0`.
- `dotnet run --project leetcode_003/leetcode_003.csproj` runs both sample implementations.
- `dotnet test leetcode_003.sln --nologo` is a smoke check only; no automated tests are discovered.
- In VS Code, use the default build task or the `Run leetcode_003` launch configuration.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space indentation for C#, and two spaces for JSON and project XML. Put braces on new lines, prefer explicit types over `var`, and keep `using` directives outside namespaces with `System` imports first. Use PascalCase for types and methods, camelCase for locals and parameters, `_camelCase` for private instance fields, and `s_camelCase` for private static fields. Keep explanatory comments and XML documentation consistent with the existing Traditional Chinese teaching style.

## Testing Guidelines

No xUnit, NUnit, or MSTest project exists. For algorithm changes, add representative cases to `Main`, exercise both `LengthOfLongestSubstring` methods, and cover empty, single-character, repeated-character, and mixed-input cases. Verify expected console output, then run the build and smoke-check commands above. If adding a test project, name tests by behavior, for example `LengthOfLongestSubstring_RepeatedCharacters_ReturnsOne`.

## Commit & Pull Request Guidelines

Recent history mixes concise Chinese or English subjects with prefixes such as `chore:`. Prefer a short imperative subject focused on one change, for example `feat: add Unicode test cases`. Pull requests should explain the algorithm or documentation change, list verification commands and results, and link the relevant issue. Include console output when behavior changes; screenshots are unnecessary for this console-only project.
