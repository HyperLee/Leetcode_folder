# Repository Guidelines

## Project Structure & Module Organization

This repository is a focused LeetCode problem workspace for `leetcode_081`. Keep the primary solution source at the repository root unless a language-specific structure already exists. Use clear names such as `solution.py`, `Solution.cs`, `solution.cpp`, or `index.js`. Put tests in a `tests/` directory or beside the solution file using the repository's existing pattern. Keep sample inputs, notes, and problem statements in Markdown files such as `README.md` when needed.

## Build, Test, and Development Commands

Use the command that matches the language used by the solution:

- `python -m pytest` runs Python tests when `pytest` tests are present.
- `dotnet test` runs .NET tests when a `.csproj` or `.sln` exists.
- `npm test` runs JavaScript or TypeScript tests when `package.json` defines a test script.
- `g++ -std=c++17 solution.cpp -o solution` compiles a standalone C++ solution.

Before adding a new toolchain, prefer the simplest command already supported by the repository files.

## Coding Style & Naming Conventions

Keep implementations compact and readable, with names that describe the algorithmic role: `left`, `right`, `mid`, `target`, `nums`, `seen`, or `result`. Use 4-space indentation for Python and C#, and the formatter default for JavaScript, TypeScript, or C++. Avoid broad refactors unrelated to the current problem. Add comments only for non-obvious algorithm branches or edge cases.

## Testing Guidelines

Cover the core LeetCode examples plus edge cases: empty or single-item inputs, duplicated values, rotated boundaries, and cases where the target is absent. Name tests after behavior, for example `test_finds_target_with_duplicates` or `Search_ReturnsFalse_WhenTargetMissing`. Keep tests deterministic and small.

## Commit & Pull Request Guidelines

Use short, imperative commit messages such as `Add rotated array search solution` or `Cover duplicate pivot cases`. Pull requests should include a brief summary, the test command run, and any notable edge cases. Link related issues when available. Screenshots are usually unnecessary unless documentation or rendered output changes.

## Security & Agent-Specific Instructions

Do not commit secrets, keys, credentials, or prompt contents. Avoid bulk deletion commands. If cleanup is needed, delete only specific single-path files and ask the repository owner to handle broad directory removal manually.
