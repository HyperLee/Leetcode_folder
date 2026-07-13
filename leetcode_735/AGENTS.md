# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep the pure
`AsteroidCollision` solution, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_735/Program.cs`. The nested
`leetcode_735/leetcode_735.csproj` defines the executable. From this folder,
run:

```bash
dotnet build leetcode_735/leetcode_735.csproj --nologo
dotnet run --no-build --project leetcode_735/leetcode_735.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_735` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main`.

Keep `public static int[] AsteroidCollision(int[] asteroids)` pure: it returns
the surviving asteroids in original left-to-right order without writing to the
console or changing the input. A collision is possible only when a stored
positive asteroid meets a new negative asteroid. Continue comparing while the
incoming asteroid remains alive; equal sizes destroy both. `Main` alone owns
acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print each case's
input, expected value, actual value, PASS/FAIL, and
`Summary: 11/11 checks passed.` on success with exit code 0. This project has
no separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_735` and stage only `leetcode_735/` if a future
delivery requests publishing. Keep commits and pull requests scoped to this
folder.
