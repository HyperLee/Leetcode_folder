# Repository Guidelines

## Project Structure & Module Organization

The executable project lives in `leetcode_1630/`. `Program.cs` contains the console entry point and the LeetCode 1630 solution, while `leetcode_1630.csproj` targets .NET 10 with nullable reference types and implicit usings enabled. Workspace-level configuration is kept in `.editorconfig`, `.vscode/`, `.gitignore`, and `.gitattributes`. Use `docs/` for supporting notes and documentation; `docs/readme-template.md` is the reusable README template. Generated `bin/` and `obj/` directories must remain untracked.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore .\leetcode_1630\leetcode_1630.csproj
dotnet build .\leetcode_1630\leetcode_1630.csproj
dotnet run --project .\leetcode_1630\leetcode_1630.csproj
dotnet format .\leetcode_1630\leetcode_1630.csproj --verify-no-changes
```

`restore` resolves dependencies, `build` compiles the project, and `run` executes the sample harness in `Main`. The format check verifies `.editorconfig` compliance without rewriting files. In VS Code, press F5 to use the `.NET 10: Debug leetcode_1630` profile; it builds first and launches without command-line arguments.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space indentation for C#, file-scoped namespaces, braces, and explicit types instead of `var`. Use PascalCase for classes and methods, camelCase for parameters and local variables, and descriptive solution names such as `CheckArithmeticSubarrays`. Keep algorithm explanations close to the relevant method, and preserve bilingual problem documentation when editing the entry point.

## Testing Guidelines

There is no automated test project or coverage requirement. Validate changes by adding deterministic cases to `Main`, comparing actual and expected results, and ensuring failures are visible in console output. Run `dotnet build` followed by `dotnet run`; include normal, boundary, and duplicate-value cases when algorithm behavior changes.

## Commit & Pull Request Guidelines

Recent history uses short imperative subjects, sometimes with Conventional Commit prefixes such as `chore:`. Prefer focused messages such as `docs: add contributor guide` or `fix: handle duplicate arithmetic values`. Pull requests should summarize the algorithm or documentation change, list build/run evidence, and link the relevant issue when available. Include screenshots only for changes to rendered documentation or debugging behavior.
