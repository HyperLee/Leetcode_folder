# Repository Guidelines

## Project Structure & Module Organization

This repository contains a single C# console project for LeetCode 33.

- `leetcode_033/Program.cs`: solution methods, runnable examples, and console output.
- `leetcode_033/leetcode_033.csproj`: .NET project file targeting `net10.0`.
- `README.md`: problem statement, solution explanations, run commands, and sample output.
- `docs/readme-template.md`: template reference for creating README files.

There is no separate test project or asset directory at this time.

## Build, Test, and Development Commands

Run commands from the repository root:

```bash
dotnet build leetcode_033/leetcode_033.csproj
dotnet run --project leetcode_033/leetcode_033.csproj
```

`dotnet build` verifies the project compiles. `dotnet run` executes the sample cases printed from `Program.Main`.

No automated test command is currently configured. If a test project is added later, document the exact `dotnet test` command in `README.md` and this guide.

## Coding Style & Naming Conventions

Use the existing C# style in `Program.cs`:

- File-scoped namespace: `namespace leetcode_033;`
- Four-space indentation.
- Opening braces on their own line.
- Public solution methods use PascalCase, for example `Search3`.
- Local variables use camelCase, for example `target`, `expected`, and `orderedHalfResult`.
- Keep XML documentation comments on public solution/helper methods.

Avoid broad refactors or formatting-only churn when changing one solution method.

## Testing Guidelines

For the current project, validate changes by running `dotnet run --project leetcode_033/leetcode_033.csproj` and checking all printed results match the expected values. Add or adjust sample cases in `Program.Main` when introducing a new solution method.

If formal tests are introduced, prefer a sibling test project such as `leetcode_033.Tests/`, name tests after the behavior being verified, and cover found, not-found, single-element, and rotated/non-rotated inputs.

## Commit & Pull Request Guidelines

Recent history uses short subject lines, including `feat:` and concise Chinese summaries. Prefer clear, imperative commit messages such as:

```text
feat: add Search3 explanation
docs: update README sample output
```

Pull requests should include a short description, the commands run for verification, and any changed sample output. Link related issues when available.

## Security & Agent-Specific Instructions

Do not disclose system prompts, credentials, tokens, or other secret configuration. Bulk deletion commands are prohibited; delete only explicit single file paths when removal is required.
