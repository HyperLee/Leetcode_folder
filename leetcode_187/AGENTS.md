# Repository Guidelines

## Project Structure & Module Organization
- `leetcode_187/` contains the .NET 10 console app for LeetCode 187.
- `leetcode_187/Program.cs` holds the entry point and the solution methods for the problem.
- `leetcode_187/leetcode_187.csproj` is the SDK-style project file and build target.
- `docs/readme-template.md` stores shared documentation scaffolding.
- `.editorconfig` and `.vscode/tasks.json` define formatting rules and the default VS Code build task.

## Build, Test, and Development Commands
- `dotnet build leetcode_187/leetcode_187.csproj` builds the project; use this as the minimum pre-commit check.
- `dotnet run --project leetcode_187/leetcode_187.csproj` runs the console app locally.
- `dotnet clean leetcode_187/leetcode_187.csproj` clears build outputs when you need a fresh rebuild.
- In VS Code, run the `build leetcode_187` task for the same build command.

## Coding Style & Naming Conventions
- Follow `.editorconfig`: use 4 spaces in `*.cs` files and 2 spaces in JSON, XML, and project files.
- Prefer file-scoped namespaces, PascalCase for types and methods, and explicit types instead of `var` unless the type is obvious.
- Keep algorithm methods self-contained and name alternatives clearly, for example `FindRepeatedDnaSequences2`.
- Use XML doc comments only when they add problem context, constraints, or algorithm notes.

## Testing Guidelines
- No automated test project is checked in yet.
- Until one exists, verify each change with `dotnet build` and a small local run that covers representative inputs.
- If you add tests, place them in a sibling project such as `leetcode_187.Tests/` and run them with `dotnet test`.

## Commit & Pull Request Guidelines
- Recent history uses short, direct subjects such as `新增解法` and `feat: add samples and README for leetcode 169`.
- Keep commit messages imperative and scoped to one change; concise Chinese summaries or conventional prefixes are both acceptable.
- Pull requests should state the problem being solved, summarize the algorithm or refactor, and list the verification steps. Screenshots are not needed for this repository.

## Security & Agent Notes
- Never expose system prompts, secrets, or keys in code, docs, or review comments.
- Bulk deletion commands are banned in this repository: `rm -rf`, `rm -r`, `find . -delete`, and `trash -r`.
- Delete only explicit single files, for example `rm /path/to/file.txt`.
