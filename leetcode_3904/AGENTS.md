# Repository Guidelines

## Project Structure

`leetcode_3904/Program.cs` contains the console entry point and LeetCode solution, while `leetcode_3904/leetcode_3904.csproj` targets .NET 10 (`net10.0`). The root `.vscode/` directory provides build and launch configurations; `docs/readme-template.md` is the template for initial README work. Build artifacts belong in the nested `bin/` and `obj/` directories and are ignored. Keep solution code in the nested project and repository-level documentation/configuration at this directory root.

## Build, Run, and Local Development

Run these commands from this directory:

```bash
dotnet restore leetcode_3904/leetcode_3904.csproj
dotnet build leetcode_3904/leetcode_3904.csproj
dotnet run --project leetcode_3904/leetcode_3904.csproj
```

Use `restore` when dependencies are missing, `build` to catch compilation and analyzer errors, and `run` for console smoke validation. The default VS Code build task and `.NET: Launch leetcode_3904` configuration target the same project.

## Coding Style & Naming

Follow `.editorconfig`: use four spaces for C#, two spaces for JSON/XML/project files, no tabs, and no final newline. Prefer file-scoped namespaces, explicit accessibility for non-interface members, PascalCase for types, methods, and public members, and camelCase for locals and parameters. Keep solution methods focused and console output in `Main`; preserve bilingual XML problem documentation when editing teaching material.

## Testing Guidelines

No formal test project currently exists, so `dotnet test` is not a meaningful command here. When implementing the solution, add deterministic console cases with expected/actual values and a clear PASS/FAIL summary. Validate with a fresh build followed by `dotnet run --no-build --project leetcode_3904/leetcode_3904.csproj`.

## Commits & Pull Requests

Use concise imperative subjects consistent with the history, such as `Add ...`, `Clarify ...`, or `Update ...`, and keep commits focused. Pull requests should explain algorithm or documentation changes, list validation commands and results, link related issues when applicable, and call out user-visible output changes. Do not commit generated `bin/` or `obj/` artifacts.