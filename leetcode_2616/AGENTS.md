# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET 10 console solution for LeetCode 2616.

- `leetcode_2616/Program.cs` contains the entry point and all solution methods.
- `leetcode_2616/leetcode_2616.csproj` defines the executable project.
- `.vscode/` provides the default build task and CoreCLR launch profile.
- `docs/readme-template.md` is the source template for a future project README.
- `bin/` and `obj/` are generated artifacts and must not be committed.

Keep algorithm code in the nested project. Put repository-wide documentation and editor configuration at the outer root.

## Build, Run, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore leetcode_2616/leetcode_2616.csproj
dotnet build leetcode_2616/leetcode_2616.csproj
dotnet run --project leetcode_2616/leetcode_2616.csproj
dotnet format leetcode_2616/leetcode_2616.csproj --verify-no-changes
```

`restore` resolves dependencies, `build` compiles the project, `run` executes `Main`, and `format --verify-no-changes` checks `.editorconfig` compliance. In VS Code, `Ctrl+Shift+B` builds and the `Debug leetcode_2616` profile runs the compiled DLL.

## Coding Style & Naming Conventions

Follow the root `.editorconfig`: use four-space indentation for C#, braces on new lines, spaces around binary operators, file-scoped namespaces, and no final newline. Use `PascalCase` for classes and methods, `camelCase` for parameters and locals, and descriptive solution suffixes such as `MinimizeMax2`. Preserve useful bilingual problem and algorithm XML comments. Add comments for non-obvious reasoning, not routine statements.

## Testing Guidelines

There is currently no automated test project. Add deterministic cases to `Main` and print expected versus actual results with a clear PASS/FAIL summary. Cover official examples plus edge cases such as `p == 0`, duplicate values, and minimum-length inputs. Because the solution methods sort `nums` in place, clone each input before invoking multiple implementations. Always run both `dotnet build` and `dotnet run` before submitting.

## Commit & Pull Request Guidelines

Recent feature commits use concise scoped subjects such as `feat(leetcode-2418): migrate project to .NET 10`. Follow that pattern where practical: `feat(leetcode-2616): add runnable test cases` or `docs(leetcode-2616): add solution notes`. Keep each commit focused. Pull requests should summarize the approach, list verification commands and results, link the relevant issue when available, and include console output when behavior changes. Screenshots are only needed for visual documentation changes.
