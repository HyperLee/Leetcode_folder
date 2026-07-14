# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep the pure
`DailyTemperatures` solution, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_739/Program.cs`. The nested
`leetcode_739/leetcode_739.csproj` defines the executable. From this folder,
run:

```bash
dotnet build leetcode_739/leetcode_739.csproj --nologo
dotnet run --no-build --project leetcode_739/leetcode_739.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_739` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main`.

Keep `public static int[] DailyTemperatures(int[] temperatures)` pure: it
returns waiting days until the next strictly warmer temperature, does not write
to the console, and does not modify the input. The stack contains indices whose
next warmer day has not yet appeared. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print each case's
input, expected value, actual value, PASS/FAIL, and
`Summary: 10/10 checks passed.` on success with exit code 0. This project has
no separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_739` and stage only `leetcode_739/` if a future
delivery requests publishing. Keep commits and pull requests scoped to this
folder.
