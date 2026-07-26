# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the pure
`MaximumOddBinaryNumber` solution, bilingual problem XML summary, and
deterministic acceptance harness in `leetcode_2864/Program.cs`. The nested
`leetcode_2864/leetcode_2864.csproj` defines the executable. `.vscode/`
contains direct build/debug configuration, while `docs/readme-template.md` is
only a template for initial README creation.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_2864/leetcode_2864.csproj --nologo
dotnet run --no-build --project leetcode_2864/leetcode_2864.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_2864`. Do not
use bare `dotnet build` or `dotnet test`: this folder has no root
solution/project and no formal test project, so those commands do not validate
this exercise.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary above `Main`.

Keep `public static string MaximumOddBinaryNumber(string s)` pure. One `1`
must remain at the least-significant position so the result is odd; move every
other `1` ahead of all `0` bits to maximize the value. `Main` alone owns
acceptance output. Follow the LeetCode valid-input contract instead of
inventing invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. It
checks seven exact results and five upper-bound properties for 12 checks total.
Each check prints expected/actual values and PASS/FAIL. Require a clean build,
`Summary: 12/12 checks passed.`, and exit code 0. Do not claim test-framework
coverage because this repository has no separate test project.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From that root, review scoped
changes with `git diff --check -- leetcode_2864` and `git status --short`, then
stage only `leetcode_2864/`. Use the scoped commit subject
`feat(leetcode-2864): migrate project to .NET 10`. Pull requests should explain
the bit-placement invariant, state the complexity, include the verified 12/12
harness result, and reference Issue #2 without closing the entire issue.
