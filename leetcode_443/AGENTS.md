# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the in-place
`Compress` solution, bilingual problem XML summary, and deterministic acceptance
harness in `leetcode_443/Program.cs`. The nested
`leetcode_443/leetcode_443.csproj` defines the executable. `.vscode/` contains
direct build/debug configuration, while `docs/readme-template.md` is only a
template for initial README creation.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_443/leetcode_443.csproj --nologo
dotnet run --no-build --project leetcode_443/leetcode_443.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_443`. Do not use
bare `dotnet build` or `dotnet test`: this folder has no root solution/project
and no formal test project, so those commands do not validate this exercise.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary above `Main`.

Keep `public static int Compress(char[] chars)` free of console I/O and make it
write only the compressed prefix of the supplied buffer. `read` must consume one
full equal-character run, while `write` always marks the next prefix position;
write a count only for runs longer than one, then reverse the initially
backwards decimal digits. Follow the LeetCode input contract instead of
inventing invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. It
checks six cases: repeated runs, a single character, mixed run lengths, a
two-digit letter count, a digit-character run, and a 2000-character run. Each
case prints input, expected/actual prefix lengths, and PASS/FAIL. Require a
clean build, `Summary: 6/6 checks passed.`, and exit code 0. Do not claim
test-framework coverage because this repository has no separate test project.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From that root, review scoped
changes with `git diff --check -- leetcode_443` and `git status --short`, then
stage only `leetcode_443/`. Keep this migration scoped to its folder and use a
concise subject such as `feat(leetcode-443): migrate project to .NET 10`; do not
push, create a pull request, or merge unless explicitly requested.
