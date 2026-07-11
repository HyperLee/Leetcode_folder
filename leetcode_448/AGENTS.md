# Repository Guidelines

## Project Structure & Module Organization

This problem root contains documentation, VS Code settings, and one nested .NET
10 console executable: `leetcode_448/leetcode_448.csproj`. Keep the bilingual
problem XML summary, the public solution API, and the deterministic acceptance
harness in `leetcode_448/Program.cs`. The `docs/readme-template.md` file is only
the starting template for an initial README.

## Build, Run, and Testing

Run these commands from this problem root:

```bash
dotnet build leetcode_448/leetcode_448.csproj --nologo
dotnet run --no-build --project leetcode_448/leetcode_448.csproj
```

Build before using `--no-build`. There is no formal test project; the executable
acceptance harness is the verification mechanism. It must finish with
`Summary: 10/10 checks passed.` and exit code 0.

## Coding Style & Solution Contract

Follow `.editorconfig`: use four-space indentation, explicit local types,
PascalCase for public members, and camelCase for locals and parameters.
`public static IList<int> FindDisappearedNumbers(int[] nums)` is console-free:
it returns the missing values and leaves all output to `Main`.

The algorithm marks the slot for a value `v` at index `v - 1` by making that
slot negative. Use `Math.Abs` before deriving the index so a previous mark does
not change the mapped value. Because marking mutates the input array, every
harness case must clone its source array before calling the public API.

## Commits and Pull Requests

Git metadata is at the parent repository root. From there, inspect scoped
changes with `git diff --check -- leetcode_448` and `git status --short`, then
stage only `leetcode_448/`. Keep each commit and pull request limited to
`leetcode_448/`; describe the `v - 1` marking invariant and the verified 10/10
harness result.
