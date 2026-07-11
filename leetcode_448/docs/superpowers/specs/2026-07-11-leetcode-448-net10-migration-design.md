# LeetCode 448 .NET 10 Migration Design

## Scope

Migrate only `leetcode_448/` from its legacy .NET Framework 4.8 project to a
SDK-style .NET 10 console application. The completed migration will include the
project file, runnable acceptance harness, Traditional Chinese teaching README,
editor and Git configuration, and a scoped collaboration guide. Delivery uses
one `codex/leetcode-448-net10` branch, one final feature commit, a draft pull
request, independent read-only review, squash merge, and then the single Issue
#2 checkbox update for `leetcode_448`.

## Selected Approach

Keep the existing pigeonhole / sign-marking algorithm and refactor it into the
LeetCode API `public static IList<int> FindDisappearedNumbers(int[] nums)`.
Each valid number `v` maps to index `v - 1`; negating that slot records that
`v` occurred. Remaining positive indexes map to missing values. This preserves
the intended O(n) time and O(1) auxiliary-space behavior (excluding the returned
list), while removing all console output from the solution method.

A `HashSet<int>` or Boolean-array rewrite would avoid input mutation but adds
O(n) auxiliary storage and is therefore not selected. A framework-only change
would leave the current side effects, no deterministic verification, and missing
delivery artifacts, so it is also rejected.

## Project and File Changes

- Replace `leetcode_448/leetcode_448/leetcode_448.csproj` with the .NET 10
  SDK-style executable contract, enabling implicit usings and nullable analysis.
- Remove these verified legacy files individually:
  `leetcode_448/leetcode_448.sln`,
  `leetcode_448/leetcode_448/App.config`, and
  `leetcode_448/leetcode_448/Properties/AssemblyInfo.cs`.
- Add `.editorconfig`, `.gitattributes`, `.gitignore`, `.vscode/tasks.json`,
  `.vscode/launch.json`, `AGENTS.md`, `README.md`, and
  `docs/readme-template.md` at the problem root. Shared configuration originates
  from the verified `leetcode_412` migration, with all paths and names changed
  to `leetcode_448`. The VS Code files treat this problem root as
  `${workspaceFolder}`, so they directly target the nested project and DLL
  without requiring the repository root to be opened.

## Program Design

`Main` will contain the required adjacent bilingual XML problem summary,
including both official links and concise English and Traditional Chinese
descriptions. The public solution method and its key helper(s) will have
Traditional Chinese XML documentation covering purpose, algorithm, valid input,
and return result. Inline comments will explain only the `v - 1` index mapping,
why `Math.Abs` is necessary after earlier sign changes, and why only positive
slots indicate missing numbers.

The acceptance harness owns all console output. It creates a fresh copy for each
solution invocation because sign marking deliberately mutates the supplied
array. Each check prints its case name, expected result, actual result, and
`PASS` or `FAIL`; a failed check sets `Environment.ExitCode` to 1. The harness
will check:

1. the official `[4,3,2,7,8,2,3,1]` example;
2. the minimum valid `[1]` input;
3. a single duplicated value `[1,1]`;
4. an array with no missing values;
5. missing first and trailing values;
6. missing only the final value;
7. multiple duplicate source values with multiple gaps;
8. reversed duplicate pairs to prove visit order does not matter;
9. the sign-marking invariant between the returned list and unmarked slots; and
10. a `n = 100000` spot check that avoids printing the full input or result.

## TDD and Verification Design

The legacy baseline has already been recorded: its explicit project build fails
with `MSB3644` because macOS has no .NET Framework 4.8 reference assemblies.
After the SDK migration, the acceptance harness will first call an intentionally
absent `FindDisappearedNumbers` method, producing the valid RED compiler error.
The smallest sign-marking implementation will then be added for GREEN, followed
by documentation and output refactoring. Build and harness are rerun after each
refactor.

Final evidence will include JSON parsing for both VS Code files; a warning-free
`dotnet build --nologo`; `dotnet run --no-build`; unique-`text`-fence README
validation; a byte-for-byte README transcript diff against a fresh run;
`git diff --check`; changed-path scope inspection; legacy-file absence checks;
and an independent read-only review. Because this problem has no formal test
project, the executable harness is its test mechanism.

## Documentation and Delivery

The Traditional Chinese README will explain the official contract, constraints,
sign-marking invariant, mutation trade-off, complexity, a worked example, every
acceptance case, real commands, final project structure, and exactly one full
fresh-run `text` transcript. After verification, the design-document commit will
be amended into the required single final commit:
`feat(leetcode-448): migrate project to .NET 10`.

The branch will be pushed as a draft PR referencing Issue #2. Once its verified
head SHA, scope, checks, and independent review are clean, it will be made ready
and squash merged using that expected head SHA. Only after GitHub confirms the
merge will the unique `leetcode_448` Issue #2 entry be changed from `[ ]` to
`[x]`, read back, and followed by fresh post-merge verification on `main`.
