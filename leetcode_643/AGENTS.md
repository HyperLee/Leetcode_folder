# leetcode_643 contributor guide

## Project layout

- The runnable project is `leetcode_643/leetcode_643/leetcode_643.csproj`.
- Run commands from this problem root or use explicit nested paths from the parent repository.
- No formal test project exists; `Program.Main` is the deterministic acceptance harness.

## Build and run

```bash
dotnet build leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643.csproj
```

## Code contract

- Keep `FindMaxAverage(int[] nums, int k)` public, pure and console-free.
- Use a fixed-length sliding window: add the entering value and subtract the leaving value.
- Do not add invalid-input behavior outside the LeetCode valid-input contract.
- Follow the root `.editorconfig`; nullable and implicit usings are enabled.
- `Main` owns all rendered acceptance output and returns a nonzero exit code when any case fails.

## Scope and Git

- Git metadata belongs to the parent `Leetcode_folder` repository.
- Limit commits and PR changes to `leetcode_643/`.
