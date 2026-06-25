# Repository Guidelines

## Project Structure & Module Organization
This repository keeps the runnable .NET console project under `leetcode_225/`. The main implementation entry point is `leetcode_225/Program.cs`, and the project file is `leetcode_225/leetcode_225.csproj`. Shared repository rules live in the root `.editorconfig`. Use `docs/readme-template.md` only when creating an initial `README.md`; there is no separate assets or test directory at the moment.

## Build, Test, and Development Commands
Run commands from the repository root with an explicit project path because the Git root is one level above the `.csproj`.

- `dotnet build leetcode_225/leetcode_225.csproj --nologo` builds the console project.
- `dotnet run --project leetcode_225/leetcode_225.csproj --no-build` runs the current sample program.
- `dotnet test` fails here with `MSB1003` because the repo root does not contain a solution or project file.
- `dotnet test leetcode_225/leetcode_225.csproj --nologo` is only a smoke check today; there is no dedicated test project yet.

## Coding Style & Naming Conventions
Follow the root `.editorconfig`. Use 4 spaces in C# files and 2 spaces in `.csproj` and JSON files. Keep file-scoped namespaces, explicit types instead of `var` when the type is not built-in, and braces on all control blocks. Use PascalCase for types and methods, camelCase for parameters and locals. Preserve the problem statement XML block in `Program.cs` when updating the solution; add short implementation comments around it instead of rewriting the prompt text.

## Testing Guidelines
This repository currently relies on build and runtime verification rather than a separate unit-test project. Before opening a PR, run `dotnet build leetcode_225/leetcode_225.csproj --nologo` and `dotnet run --project leetcode_225/leetcode_225.csproj --no-build`, then confirm the console output matches the current sample scenario.

## Commit & Pull Request Guidelines
Recent history mixes concise Traditional Chinese subjects such as `新增專案` with conventional-style messages such as `feat(leetcode-222): ...`. Keep commit messages short, imperative, and scoped to one change. PRs should describe the algorithm or documentation update, list the verification commands you ran, and include console output when behavior changes. Screenshots are usually unnecessary for this console-only project.
