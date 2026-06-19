# Repository Guidelines

## Project Structure & Module Organization

This folder contains the solution for LeetCode 209. The executable project is
in `leetcode_209/`: put the algorithm, its helper methods, and runnable sample
code in `leetcode_209/Program.cs`. Project settings, including the `.NET 10`
target, live in `leetcode_209/leetcode_209.csproj`. `docs/readme-template.md`
is a documentation template, not application code. Keep editor configuration
in `.editorconfig` and local launch/tasks settings in `.vscode/`.

## Build, Test, and Development Commands

- `dotnet restore leetcode_209/leetcode_209.csproj` restores project packages.
- `dotnet build leetcode_209/leetcode_209.csproj` compiles the console solution
  and applies configured analyzers.
- `dotnet run --project leetcode_209/leetcode_209.csproj` runs `Main`; use it
  to exercise the examples added for a solution.
- `git diff --check` catches whitespace errors before a commit.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces (four in C# and two in project/JSON files),
place opening braces on a new line, and prefer file-scoped namespaces. Use
PascalCase for types and methods (`MinSubArrayLen`), camelCase for locals and
parameters (`windowSum`), and explicit types rather than `var`. Keep the
problem statement XML comment intact; add focused XML documentation to public
or non-obvious helpers. Prefer clear, small solution methods over clever
one-liners.

## Testing Guidelines

There is currently no test project or testing framework. Add representative
cases to the runnable harness in `Program.cs`, including the canonical case,
an exact-match window, and a no-solution case. State expected results beside
each case, run the project, and ensure its output makes failures obvious. Add a
dedicated test project if a change needs regression coverage beyond the sample
harness.

## Commit & Pull Request Guidelines

Recent history uses short, focused descriptions, including Chinese messages,
problem-number summaries, and prefixes such as `feat:` and `chore:`. Use one
style consistently, for example `feat: add sliding-window solution for 209`.
Keep commits scoped to this problem and exclude generated `bin/`, `obj/`, and
IDE state. Pull requests should explain the approach and complexity, list the
commands run, link the relevant issue when one exists, and include sample
output when console behavior changes.
