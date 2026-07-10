# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the pure `FizzBuzz`
solution, bilingual problem XML summary, and deterministic acceptance harness in
`leetcode_412/Program.cs`. The nested `leetcode_412/leetcode_412.csproj` defines
the executable. `.vscode/` contains direct build/debug configuration, while
`docs/readme-template.md` is only a template for initial README creation.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_412/leetcode_412.csproj --nologo
dotnet run --no-build --project leetcode_412/leetcode_412.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_412`. Do not use
bare `dotnet build` or `dotnet test`: this folder has no root solution/project
and no formal test project, so those commands do not validate this exercise.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary above `Main`.

Keep `public static IList<string> FizzBuzz(int n)` pure: it returns the complete
ordered result without writing to the console. Check multiples of 15 before 3
or 5 so shared multiples become `FizzBuzz`; `Main` alone owns acceptance output.
Follow the LeetCode input contract instead of inventing invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. It
checks five full sequences plus six upper-bound facts for 11 checks total. Each
check prints expected/actual values and PASS/FAIL. Require a clean build,
`Summary: 11/11 checks passed.`, and exit code 0. Do not claim test-framework
coverage because this repository has no separate test project.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From that root, review scoped
changes with `git diff --check -- leetcode_412` and `git status --short`, then
stage only `leetcode_412/`. Use a concise scoped commit subject such as
`feat(leetcode-412): migrate project to .NET 10`. Pull requests should describe
the divisibility-order invariant, state the complexity, and include the verified
11/11 harness result.
