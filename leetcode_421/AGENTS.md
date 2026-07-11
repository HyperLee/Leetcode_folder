# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the pure
`FindMaximumXOR` solution, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_421/Program.cs`. The nested
`leetcode_421/leetcode_421.csproj` defines the executable. `.vscode/` contains
the direct build/debug configuration, while `docs/readme-template.md` is only a
template for initially creating a README.

## Build, Run, and Development Commands

Run commands from this repository folder with the explicit nested project path:

```bash
dotnet build leetcode_421/leetcode_421.csproj --nologo
dotnet run --no-build --project leetcode_421/leetcode_421.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_421`. Do not
use bare `dotnet build` or `dotnet test`: this folder has no root solution or
formal test project, so those commands do not validate this exercise.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary and the historical algorithm notes above `Main`.

Keep `public static int FindMaximumXOR(int[] nums)` pure: it returns the
maximum XOR without writing to the console or modifying `nums`. For every bit
from 30 through 0, test whether the greedy candidate with that bit set can be
formed by two existing shifted prefixes. The accepted candidate becomes the
next prefix of the answer; otherwise that bit is zero. Follow the LeetCode
valid-input contract instead of inventing invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. It
checks two official examples, minimum and duplicate inputs, bit boundaries, a
regression case, and a length-200000 spot check: 8 checks total. Each check
prints its input, expected/actual values, and PASS/FAIL. Require a clean build,
`Summary: 8/8 checks passed.`, and exit code 0. Do not claim test-framework
coverage because this repository has no separate test project.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From that root, review scoped
changes with `git diff --check -- leetcode_421` and `git status --short`, then
stage only `leetcode_421/`. Use `feat(leetcode-421): migrate project to .NET 10`.
Pull requests should describe the prefix-existence invariant, state the
`O(31n)` time and `O(n)` auxiliary-space complexity, and include the verified
8/8 harness result.
