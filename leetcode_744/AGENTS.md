# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep the pure
`NextGreatestLetter` solution, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_744/Program.cs`. The nested
`leetcode_744/leetcode_744.csproj` defines the executable. From this folder,
run:

```bash
dotnet build leetcode_744/leetcode_744.csproj --nologo
dotnet run --no-build --project leetcode_744/leetcode_744.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_744` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main`.

Keep `public static char NextGreatestLetter(char[] letters, char target)` pure:
it returns the first sorted letter strictly greater than `target`, wraps to the
first letter when required, does not write to the console, and does not modify
the input. The binary-search invariant is that `[low, high)` still contains the
first possible strictly greater letter. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print each case's
input, expected value, actual value, PASS/FAIL, and
`Summary: 9/9 checks passed.` on success with exit code 0. This project has no
separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_744` and stage only `leetcode_744/` if a future
delivery requests publishing. Keep commits and pull requests scoped to this
folder.
