# Repository Guidelines

## Project Structure

This problem root contains one nested .NET 10 console project:
`leetcode_516/leetcode_516.csproj`. The solution API, interval-DP implementation,
and deterministic acceptance harness are in `leetcode_516/Program.cs`.
`docs/readme-template.md` is only a template for creating the README.

## Build, Run, and Testing

Run commands from this problem root:

```bash
dotnet build leetcode_516/leetcode_516.csproj --nologo
dotnet run --no-build --project leetcode_516/leetcode_516.csproj
```

There is no formal test project. The console acceptance harness is the verification
mechanism and must finish with `Summary: 10/10 checks passed.` and exit code 0.

## Coding Style and Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
file-scoped namespaces, explicit types, PascalCase public members, and camelCase
locals and parameters.

`public static int LongestPalindromeSubseq(string s)` uses two-dimensional interval
DP. `dp[i, j]` stores the longest palindromic subsequence length in `s[i..j]`.
The API is pure, writes nothing to the console, and follows the valid LeetCode
input contract without inventing invalid-input behavior. `Main` alone renders
acceptance output.

## Commits and Pull Requests

Git metadata is stored at the parent repository root:
`/Users/qiuzili/Leetcode/Leetcode_folder`.

Review scoped changes with `git diff --check -- leetcode_516` and stage only
`leetcode_516/`. Use the commit subject:
`feat(leetcode-516): migrate project to .NET 10`.
