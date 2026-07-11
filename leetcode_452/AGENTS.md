# Repository Guidelines

## Project Structure & Module Organization

This problem root contains documentation, VS Code settings, an Excel teaching
asset, and one nested .NET 10 console executable:
`leetcode_452/leetcode_452.csproj`. Keep the bilingual problem XML summary,
the public solution API, and the deterministic acceptance harness in
`leetcode_452/Program.cs`. The tracked `圖解座標.xls` file is a historical
teaching resource and is not a build input.

## Build, Run, and Testing

Run these commands from this problem root:

```bash
dotnet build leetcode_452/leetcode_452.csproj --nologo
dotnet run --no-build --project leetcode_452/leetcode_452.csproj
```

Build before using `--no-build`. There is no formal test project; the
executable acceptance harness must finish with `Summary: 8/8 checks passed.`
and exit code 0.

## Coding Style & Solution Contract

Follow `.editorconfig`: use four-space indentation, explicit local types,
PascalCase public members, and camelCase locals and parameters.
`public static int FindMinArrowShots(int[][] points)` is console-free, sorts
the valid LeetCode input in place, and returns the minimum arrow count. Clone
each harness input before calling it because sorting changes its order. An
interval beginning at the current arrow position is still burst by that arrow.

## Commits and Pull Requests

Git metadata lives at the parent repository root. From there, inspect scoped
changes with `git diff --check -- leetcode_452` and `git status --short`, then
stage only `leetcode_452/`. Keep each commit and pull request limited to this
problem folder; describe the right-endpoint greedy invariant and verified 8/8
harness result.
