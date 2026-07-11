# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the `ListNode` type, the
pure `AddTwoNumbers` solution, the bilingual XML problem summary, and the
deterministic acceptance harness in `leetcode_445/Program.cs`. The nested
`leetcode_445/leetcode_445.csproj` defines the executable. `.vscode/` contains
direct build/debug configuration, while `docs/readme-template.md` is only a
template for initial README creation.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_445/leetcode_445.csproj --nologo
dotnet run --no-build --project leetcode_445/leetcode_445.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_445`. Do not
use bare `dotnet build` or `dotnet test`: this folder has no root
solution/project and no formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary above `Main`.

Keep `public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)` pure: it
returns a new result list without writing to the console or mutating either
input. Push each input's digits onto a stack, then prepend each calculated
least-significant digit to the result so its most-significant digit stays at
the head. Follow the LeetCode non-empty-list contract instead of inventing
invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. It
checks three official examples, reversed length asymmetry, cascading carry, and
a 100-digit upper-bound spot check for six checks total. Each check prints
expected/actual values and confirms both inputs are unchanged. Require a clean
build, `Summary: 6/6 checks passed.`, and exit code 0. Do not claim
test-framework coverage because this repository has no separate test project.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From that root, review scoped
changes with `git diff --check -- leetcode_445` and `git status --short`, then
stage only `leetcode_445/`. Use a concise scoped commit subject such as
`feat(leetcode-445): migrate project to .NET 10`. Pull requests should describe
the stack/prepend invariant, state `O(m+n)` time and auxiliary space, and
include the verified 6/6 harness result.
