# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The nested executable is
`leetcode_1493/leetcode_1493.csproj`. From this folder, run:

```bash
dotnet build leetcode_1493/leetcode_1493.csproj --nologo
dotnet run --no-build --project leetcode_1493/leetcode_1493.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1493` VS Code configuration.
There is no root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow, explicit
types, PascalCase public members, and camelCase locals. Preserve the bilingual XML
problem summary and Traditional Chinese `LongestSubarray` summary.

Keep `public static int LongestSubarray(int[] nums)` console-free and input-preserving.
Its sliding window may contain at most one zero, and `right - left` represents the
window length after deleting exactly one element. Do not add behavior outside
LeetCode's valid-input contract. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness has ten checks. Each case copies its input before the API call
and passes only when both the answer and complete input array match expectations.
Success ends with `Summary: 10/10 checks passed.` and exit code 0. Git metadata is at
the parent repository root; commits and pull requests must remain scoped to
`leetcode_1493/`.
