# Repository Guidelines

## Project Structure & Module Organization

This repository is a nested LeetCode .NET console project. The runnable source lives in `leetcode_260/Program.cs`, and the project file is `leetcode_260/leetcode_260.csproj` targeting `net10.0` with nullable reference types and implicit usings enabled. Documentation scaffolding is in `docs/readme-template.md`. VS Code launch and build tasks are under `.vscode/`; use the existing `Debug leetcode_260` configuration when debugging locally.

## Build, Test, and Development Commands

Run commands from `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_260`:

```bash
dotnet build leetcode_260/leetcode_260.csproj --nologo
dotnet run --project leetcode_260/leetcode_260.csproj --no-build
dotnet test --nologo
```

The build command compiles the console app. The run command executes the current sample harness after a successful build. There is no test project yet; root-level `dotnet test --nologo` currently fails with `MSB1003` because this folder has no solution or root project file. Until tests are added, treat build plus representative `Main` output as the smoke check.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, 4-space indentation for C#, file-scoped namespaces, braces on new lines, and explicit built-in types instead of `var`. Keep types, methods, properties, and namespaces in `PascalCase`; interfaces should use `I` + `PascalCase`. Preserve the existing problem-description XML summary in `Program.cs` when it represents the original LeetCode prompt, and place implementation comments near the code they explain.

## Testing Guidelines

Add focused sample cases to `Main` for this single-project setup. If a formal test suite is introduced, create a separate test project and document the exact project-path command instead of relying on root discovery.

## Commit & Pull Request Guidelines

This folder is inside the parent git repository at `/Users/qiuzili/Leetcode/Leetcode_folder`. Recent history mixes concise Chinese subjects, such as `新增專案`, with scoped Conventional Commit messages, such as `feat(leetcode-228): add summary ranges samples and README`. Prefer a short, imperative subject with the problem scope, for example `feat(leetcode-260): add single number iii demo`. PRs should describe the algorithm/documentation change, list verified commands, and mention any missing test coverage.

## Agent-Specific Safety

Do not commit secrets, local prompts, keys, or machine-specific credentials. Avoid bulk deletion commands; if cleanup is needed, delete only one explicit file path at a time.
