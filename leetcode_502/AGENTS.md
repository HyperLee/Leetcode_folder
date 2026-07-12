# Repository Guidelines

## Project Structure & Module Organization

This problem root contains documentation, direct VS Code settings, and one nested .NET
10 console executable: `leetcode_502/leetcode_502.csproj`. Keep the bilingual
problem XML summary, the public solution API, and the deterministic acceptance
harness in `leetcode_502/Program.cs`. The `docs/readme-template.md` file is only
the starting template for an initial README.

## Build, Run, and Testing

Run these commands from this problem root:

```bash
dotnet build leetcode_502/leetcode_502.csproj --nologo
dotnet run --no-build --project leetcode_502/leetcode_502.csproj
```

Build before using `--no-build`. There is no formal test project; the executable
acceptance harness is the verification mechanism. It must finish with
`Summary: 9/9 checks passed.` and exit code 0.

## Coding Style & Solution Contract

Follow `.editorconfig`: use four-space indentation, explicit local types,
PascalCase for public members, and camelCase for locals and parameters.
`public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)`
is console-free: it returns final capital and leaves all output to `Main`.

Sort projects by required capital. The scan pointer advances only once; before
each selection it moves every project with `RequiredCapital <= w` into the
candidate heap. `.NET` `PriorityQueue` removes the smallest priority, so enqueue
each profit with `-profit` to make its highest profit the next dequeue. If the
heap is empty, no remaining project is affordable and the loop must stop.

## Commits and Pull Requests

Git metadata is at the parent repository root. From there, inspect scoped changes
with `git diff --check -- leetcode_502` and `git status --short`, then stage only
`leetcode_502/`. Keep every commit and pull request limited to `leetcode_502/`;
describe the capital scan/max-heap invariant and the verified 9/9 harness result.
