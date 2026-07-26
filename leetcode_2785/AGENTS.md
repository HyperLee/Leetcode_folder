# Repository Guidelines

## Project Structure

This folder contains one .NET 10 console project. Keep the pure
`SortVowels(string s)` solution, its bilingual XML problem summary, and the
deterministic acceptance harness in `leetcode_2785/Program.cs`. The nested
`leetcode_2785/leetcode_2785.csproj` defines the executable. `.vscode/`
provides direct build/debug settings; `docs/readme-template.md` is only the
initial README template.

## Build, Run, and Development Commands

Run the explicit nested-project commands from this project folder:

```plaintext
dotnet build leetcode_2785/leetcode_2785.csproj --nologo
dotnet run --no-build --project leetcode_2785/leetcode_2785.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_2785`. Do not
use bare `dotnet build` or `dotnet test`: this folder has no root
solution/project and no formal test project.

## Coding Style and Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces, PascalCase for public
members, camelCase for locals/parameters, and `s_camelCase` for private static
fields. Preserve `public static string SortVowels(string s)` as a pure method:
it collects vowels, ASCII-sorts them, and restores them only to their original
vowel slots. `IsVowel` must use explicit `AEIOUaeiou` membership so culture
rules cannot change the result. `Main` alone owns console output.

## Verification

The executable acceptance harness is the verification mechanism. It runs five
small string cases and four large-input checks for nine checks total, including
the `tr-TR` regression. Require a clean build, `Summary: 9/9 checks passed.`,
and exit code 0. Do not claim test-framework coverage because this repository
has no separate test project.

## Commits and Pull Requests

Git metadata lives at the parent repository root. Review scoped changes with
`git diff --check -- leetcode_2785` and `git status --short`, then stage only
`leetcode_2785/`. Use concise scoped commit subjects such as
`feat(leetcode-2785): migrate project to .NET 10`.
