# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The bilingual problem XML
summary, pure solution, and deterministic acceptance harness are in
`leetcode_767/Program.cs`; the executable project is
`leetcode_767/leetcode_767.csproj`. From this problem root, run:

```bash
dotnet build leetcode_767/leetcode_767.csproj --nologo
dotnet run --no-build --project leetcode_767/leetcode_767.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_767` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, formal test project, or third-party test framework.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the public API exactly as
`public static string ReorganizeString(string s)`.

The solution must remain pure and must not write to the console. It counts the
26 lowercase letters. A result exists exactly when the maximum frequency is at
most `(s.Length + 1) / 2`; place that highest-frequency letter in even indices,
then fill remaining even indices before wrapping to odd indices. `Main` alone
owns console output.

## Testing & Git Scope

The executable acceptance harness is the verification mechanism. It covers the
nine required cases, prints Expected, Actual, and PASS/FAIL for every check, and
must end with `Summary: 27/27 checks passed.` and exit code 0. A failure must set
`Environment.ExitCode = 1`.

Git metadata is at the parent repository root. Review with
`git diff --check -- leetcode_767`, stage only `leetcode_767/`, and keep commits
and pull requests strictly scoped to this problem folder.
