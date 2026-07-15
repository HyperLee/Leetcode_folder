# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The bilingual problem XML
summary, pure recursive solution, and deterministic acceptance harness are in
`leetcode_779/Program.cs`; the executable project is
`leetcode_779/leetcode_779.csproj`. From this problem root, run:

```bash
dotnet build leetcode_779/leetcode_779.csproj --nologo
dotnet run --no-build --project leetcode_779/leetcode_779.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_779` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, formal test project, or third-party test framework.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the public API exactly as
`public static int KthGrammar(int n, int k)`.

The solution must remain pure and must not write to the console. For each row,
the first half equals the previous row and the second half is its bitwise
complement. Recursively map `k` to the previous row and XOR complemented-half
answers with `1`. Inputs follow the LeetCode contract; do not add unrelated
invalid-input behavior. `Main` alone owns console output.

## Testing & Git Scope

The executable acceptance harness is the verification mechanism. It covers ten
required checks, prints Expected, Actual, and PASS/FAIL for every check, and
must end with `Summary: 10/10 checks passed.` and exit code 0. A failure must set
`Environment.ExitCode = 1`.

Git metadata is at the parent repository root. From this problem root, review
with `git -C .. diff --check -- leetcode_779` and stage only this problem with
`git -C .. add -- leetcode_779/`. Keep commits and pull requests strictly scoped
to this problem folder.
