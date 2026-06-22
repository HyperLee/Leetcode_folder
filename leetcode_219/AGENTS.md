# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET 10 console application for LeetCode 219, **Contains Duplicate II**. Keep implementation and executable examples together in `leetcode_219/Program.cs`; it contains the two solution methods and the `Main`-driven sample harness. `leetcode_219/leetcode_219.csproj` defines the executable project. Maintain the teaching-oriented explanation and verified console output in `README.md`; use `docs/readme-template.md` when reshaping that documentation.

The existing XML documentation at the top of `Program.cs` is the problem statement. Preserve it when changing implementation code, and add method-specific explanation around it instead of rewriting the original requirements.

## Build, Run, and Verification

Run commands from the repository root:

```bash
dotnet build leetcode_219/leetcode_219.csproj --nologo
dotnet run --project leetcode_219/leetcode_219.csproj
git diff --check
```

The build command catches compiler and nullable-analysis errors. The run command executes seven fixed cases against both implementations and prints fourteen `PASS`/`FAIL` checks. `git diff --check` catches whitespace errors before review. There is no solution or test project yet, so root-level `dotnet test` is not a valid verification command and will report `MSB1003`.

## Coding Style & Naming

Follow `.editorconfig`: use four spaces in C#, braces on new lines, and explicit types rather than `var`. Use PascalCase for classes and methods (`ContainsNearbyDuplicate2`), camelCase for parameters and locals (`passedChecks`), and `_camelCase` for private instance fields when fields are necessary. Nullable reference types and implicit usings are enabled; retain nullability-correct signatures. Keep each algorithm's invariant visible through focused XML comments or short inline comments—avoid narrating every line.

## Testing Changes

Add or adjust deterministic cases in `RunSamples` for behavioral changes. Cover the expected result with both algorithms, including boundaries such as `k = 0`, equality at distance `k`, and distance `k + 1`. After changing sample output, update the matching output block and explanation in `README.md` from a fresh `dotnet run` result.

## Commits & Pull Requests

Recent history mixes concise Chinese subjects with Conventional Commit-style messages. Prefer an imperative, scoped subject such as `feat(leetcode-219): add boundary case` or a concise equivalent. Keep commits focused. A pull request should summarize the algorithm or documentation change, list the commands run, and state any sample cases added; console-only changes do not need screenshots.
