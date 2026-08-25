# Repository Guidelines

## Project Structure

This repository is a single .NET 10 console project for LeetCode 3718. `leetcode_3718/Program.cs` contains the three solution methods and fixed-case runner; `leetcode_3718/leetcode_3718.csproj` defines the executable. Root `README.md` documents the problem, algorithms, complexity, and output. `docs/readme-template.md` is a documentation reference, and `.vscode/` holds build/launch settings. There is no separate test project or asset directory.

## Build, Run, and Development

Requires the .NET 10 SDK.

```powershell
dotnet build .\leetcode_3718\leetcode_3718.csproj
dotnet run --project .\leetcode_3718\leetcode_3718.csproj
```

`dotnet build` compiles the project; `dotnet run` executes the no-input runner across three cases and three methods. A healthy run ends with `總結：9/9 通過，0 個失敗。` VS Code F5 uses `.vscode/launch.json` and the `build leetcode_3718` pre-launch task. Use `--no-build` only after building successfully.

## Testing Guidelines

The runner in `Program.Main` is the smoke test; no xUnit, NUnit, MSTest, or coverage gate is configured. Add representative and boundary inputs to `testCases` with explicit expected values. Preserve cloned inputs so methods remain isolated. Run both commands above before submitting, and require every method to report `PASS`.

## Coding Style and Naming

Follow `.editorconfig`: four spaces, no tabs, file-scoped namespaces, standard braces, and explicit types (`var` is discouraged). Use PascalCase for types and methods (`MissingMultiple2`) and camelCase for locals and parameters (`expected`). Keep XML documentation and the English/Traditional Chinese problem links current with behavior changes. No separate formatter or linter is configured.

## Commits and Pull Requests

Recent history uses short imperative subjects such as `Add ...`, `Clarify ...`, and `調整註解`. Keep commits focused on one logical change. Pull requests should summarize the algorithm or documentation change, note complexity when code changes, identify affected paths, link the LeetCode problem when relevant, and include the exact build/run result. For console changes, include a short representative output summary.
