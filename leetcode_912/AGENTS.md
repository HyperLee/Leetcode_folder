# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep the in-place heap-sort
solution, bilingual problem XML summary, iterative `SiftDown` helper, and
deterministic acceptance harness in `leetcode_912/Program.cs`. The nested
`leetcode_912/leetcode_912.csproj` defines the executable. From this folder, run:

```bash
dotnet build leetcode_912/leetcode_912.csproj --nologo
dotnet run --no-build --project leetcode_912/leetcode_912.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_912` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no root
project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main` and the
Traditional Chinese XML summaries on `SortArray` and `SiftDown`.

Keep `public static int[] SortArray(int[] nums)` console-free. It must build an
in-place max heap from `nums.Length / 2 - 1`, repeatedly move the maximum to the
unsorted tail, and iteratively restore the heap. It returns the same array
reference, uses no built-in sorting, recursion, randomness, or third-party
package, and adds no behavior beyond LeetCode's valid-input contract. `Main`
alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It has eight cases and
nine checks; each prints its name, input, expected value, actual value, and
PASS/FAIL. Success ends with `Summary: 9/9 checks passed.` and exit code 0.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_912` and stage only `leetcode_912/`. Keep commits
and pull requests scoped to this folder.
