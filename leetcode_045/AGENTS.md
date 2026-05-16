# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single C#/.NET console solution for LeetCode 45, Jump Game II.

- `leetcode_045/Program.cs` contains the executable entry point, sample cases, and both solution methods.
- `leetcode_045/leetcode_045.csproj` defines the .NET 10 console project.
- `README.md` documents the problem, algorithms, examples, and run commands.
- `docs/readme-template.md` is a reusable prompt/template for initial README creation.

Keep new source files under `leetcode_045/` unless the project is intentionally split into separate projects. Keep documentation updates in `README.md` or `docs/`.

## Build, Test, and Development Commands

```bash
dotnet build leetcode_045/leetcode_045.csproj
```

Builds the console project and validates compilation.

```bash
dotnet run --project leetcode_045/leetcode_045.csproj
```

Runs the sample cases in `Program.Main`; output should show `PASS` for each case.

```bash
git diff --check
```

Checks staged and unstaged changes for whitespace errors before committing.

## Coding Style & Naming Conventions

Follow `.editorconfig`. Use spaces, 4-space indentation for C# files, 2-space indentation for XML/project files, braces on new lines, and file-scoped namespaces. Prefer explicit types over `var` unless the type is obvious from the right-hand side. Use PascalCase for public methods such as `Jump` and `Jump2`, camelCase for locals, and descriptive tuple element names in test data.

Preserve the existing bilingual documentation style when editing algorithm comments or README sections.

## Testing Guidelines

There is no separate test project yet. Add focused sample cases to `Program.Main` when changing algorithm behavior, including edge cases such as single-element arrays and repeated one-step jumps. Verify changes with:

```bash
dotnet run --project leetcode_045/leetcode_045.csproj
```

If a formal test project is added later, place it in a sibling directory such as `leetcode_045.Tests/` and name tests after the behavior under test.

## Commit & Pull Request Guidelines

Recent commits use short subjects, including conventional prefixes such as `docs:`. Prefer concise messages like `docs: update jump game explanation` or `fix: handle edge case sample`.

Pull requests should include a brief summary, verification commands run, and any relevant output changes. Link related LeetCode notes or issues when applicable.

## Security & Agent-Specific Instructions

Do not commit secrets, keys, local machine paths beyond examples, or prompt contents. Avoid bulk deletion commands; remove only specific single files when cleanup is required.
