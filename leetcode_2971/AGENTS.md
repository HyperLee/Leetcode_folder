# Repository Guidelines

## Project Structure & Module Organization

This folder contains one .NET 10 console project. Keep the pure
`LargestPerimeter` solution, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_2971/Program.cs`. The nested
`leetcode_2971/leetcode_2971.csproj` defines the executable. `.vscode/` contains
direct build/debug configuration, while `docs/readme-template.md` is only a
template for initial README creation.

## Build, Run, and Development Commands

Run commands from this project folder with the explicit nested project path:

```bash
dotnet build leetcode_2971/leetcode_2971.csproj --nologo
dotnet run --no-build --project leetcode_2971/leetcode_2971.csproj
```

Build before using `--no-build`. In VS Code, use `Debug leetcode_2971`. Do not
use bare `dotnet build` or `dotnet test`: this folder has no root
solution/project and no formal test project, so those commands do not validate
this exercise.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces where appropriate,
PascalCase for public members, camelCase for locals/parameters, and
`s_camelCase` for private static fields. Preserve the bilingual XML problem
summary above `Main`.

Keep `public static long LargestPerimeter(int[] nums)` free of console output.
It sorts `nums` in place, accumulates a `long` prefix sum, and records a
candidate only when the prefix sum is strictly greater than twice its longest
edge. `Main` alone owns acceptance output. Follow the LeetCode valid-input
contract instead of inventing invalid-input behavior.

## Testing Guidelines

The executable acceptance harness is the current verification mechanism. It
checks three official examples, important inequality and prefix regressions,
64-bit arithmetic, and the maximum input length for nine checks total. Each
check prints its case, input, expected value, actual value, and PASS/FAIL.
Require a clean build, `Summary: 9/9 checks passed.`, and exit code 0. Do not
claim test-framework coverage because this repository has no separate test
project.

## Version Control and Pull Requests

Git metadata lives at the parent repository root, but the project migration
does not automate Git or GitHub operations. The user performs version control
and upload manually. Any eventual commit or pull request must contain only
`leetcode_2971/`, describe the sorted-prefix invariant and complexity, and
include the verified 9/9 harness result.
