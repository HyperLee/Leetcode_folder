# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the pure
`MinOperations` solution, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_2870/Program.cs`. The nested
`leetcode_2870/leetcode_2870.csproj` defines the executable. `.vscode/`
contains direct build/debug configuration, while `docs/readme-template.md` is
only a template for initial README creation.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_2870/leetcode_2870.csproj --nologo
dotnet run --no-build --project leetcode_2870/leetcode_2870.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_2870`. Do not
use bare `dotnet build` or `dotnet test`: this folder has no root
solution/project and no formal test project, so those commands do not validate
this exercise.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary above `Main`.

Keep `public static int MinOperations(int[] nums)` pure: it returns the minimum
operation count without writing to the console or modifying the input. Count
each distinct value, reject any singleton frequency, and otherwise prefer
groups of three with one additional operation for a remainder. `Main` alone
owns acceptance output. Follow the LeetCode input contract instead of inventing
invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. It
checks the two official examples, frequency boundaries from two through seven,
mixed frequencies, an impossible singleton, the 100,000-element upper bound,
and input immutability for 12 checks total. Require a clean build,
`Summary: 12/12 checks passed.`, and exit code 0. Do not claim test-framework
coverage because this repository has no separate test project.

## Commits and Pull Requests

Version-control actions are handled manually outside this project migration.
When preparing a commit from the parent repository, include only
`leetcode_2870/` and use a concise subject such as
`feat(leetcode-2870): migrate project to .NET 10`. A pull request should explain
the frequency-grouping invariant, state the complexity, and include the
verified 12/12 harness result.
