# leetcode_645 contributor guide

## Project layout

- The runnable project is `leetcode_645/leetcode_645/leetcode_645.csproj`.
- Run commands from this problem root or use explicit nested paths from the parent repository.
- No formal test project exists; `Program.Main` is the deterministic acceptance harness.

## Build and run

```bash
dotnet build leetcode_645/leetcode_645.csproj --nologo
dotnet run --no-build --project leetcode_645/leetcode_645.csproj
```

## Code contract

- Keep `FindErrorNums(int[] nums)` public, pure, and console-free.
- Valid LeetCode input contains values in `1..n`; use a length `n + 1` count array and interpret counts `2` and `0` while scanning `1..n` as the duplicate and missing values.
- Return `[duplicate, missing]` without modifying `nums` or adding invalid-input behavior outside the LeetCode contract.
- Follow the root `.editorconfig`; nullable and implicit usings are enabled.
- `Main` owns all rendered acceptance output and returns a nonzero exit code when any of the eight checks fails.

## Scope and Git

- Git metadata belongs to the parent `Leetcode_folder` repository.
- Limit commits and PR changes to `leetcode_645/`.
