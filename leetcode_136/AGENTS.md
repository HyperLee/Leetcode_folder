# AGENTS.md

## Safety Rules

- Do not bulk-delete files or directories.
- Do not use these commands:
  - `del /s`
  - `rd /s`
  - `rmdir /s`
  - `Remove-Item -Recurse`
  - `rm -rf`
- If a file must be deleted, delete only one explicit file path at a time.
- If bulk deletion seems necessary, stop and ask the user to handle it manually.

## Project Notes

- This is a C# console solution for `leetcode_136`.
- Main source file: `leetcode_136/Program.cs`.
- Project file: `leetcode_136/leetcode_136.csproj`.
- Solution file: `leetcode_136.sln`.
- Target framework: `net10.0`.
- Nullable reference types and implicit usings are enabled.

## Common Commands

- Build: `dotnet build leetcode_136.sln`
- Run: `dotnet run --project leetcode_136/leetcode_136.csproj`

## Editing Guidance

- Prefer small, direct changes in `leetcode_136/Program.cs` unless the task clearly requires more structure.
- Do not edit generated files under `leetcode_136/obj/`.
- There is no test project currently; if adding tests, keep them focused on the LeetCode solution behavior.
