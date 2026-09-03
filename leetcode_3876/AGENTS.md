# Repository Guidelines

## Project Structure & Module Organization

This repository is a small nested .NET 10 console project. The root contains repository support files (`.editorconfig`, `.gitattributes`, `.gitignore`, `.vscode/`) and `docs/readme-template.md`. The implementation is in `leetcode_3876/Program.cs`; its project file is `leetcode_3876/leetcode_3876.csproj`. There is currently no separate test or asset directory. Keep algorithm code, runnable examples, and problem-specific comments together in the inner project, and keep generated `bin/` and `obj/` output untracked.

## Build, Test, and Development Commands

Run from the repository root:

```powershell
dotnet build .\leetcode_3876\leetcode_3876.csproj --nologo
dotnet run --project .\leetcode_3876\leetcode_3876.csproj
```

The first command restores as needed and compiles the project; the second runs the console entry point. In VS Code, use `Run leetcode_3876` (F5); it invokes `build leetcode_3876` and launches the generated `net10.0` DLL with no arguments.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use spaces, four-space indentation for C#, and file-scoped namespaces. Use PascalCase for types and public methods (`UniformArray`), camelCase for locals and parameters, and clear names for algorithm state. Nullable reference types and implicit usings are enabled. No separate formatter or linter is configured; keep changes compiler-clean and consistent with surrounding code.

## Testing Guidelines

No test framework, test project, or coverage threshold is currently configured. For algorithm changes, exercise representative inputs through the runnable entry point and verify expected results. If tests are added later, name them after the behavior under test (for example, `UniformArray_ReturnsTrueForAllEven`) and run `dotnet test <test-project>`. Include edge cases such as single-element, all-even, all-odd, and mixed-parity inputs.

## Commit & Pull Request Guidelines

Recent commits use short imperative summaries, such as `Add ...`, `Improve ...`, and `README: add ...`. Follow that style and keep each commit focused. Pull requests should explain the algorithm or documentation change, link any issue, list validation commands and results, and call out behavior or file-structure impact. Include screenshots only when they add value beyond text output. Do not rewrite unrelated working-tree changes.

## Security & Configuration Tips

Do not commit secrets, credentials, or generated output. Keep repository configuration changes minimal and explain why they are needed.
