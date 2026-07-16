# Repository Guidelines

## Project Structure

- `leetcode_974/Program.cs` contains the console entry point and the LeetCode 974 solution/demo.
- `leetcode_974/leetcode_974.csproj` defines the executable .NET 10 project.
- `.vscode/launch.json` and `.vscode/tasks.json` provide the integrated build and F5 debug workflow.
- `docs/readme-template.md` is the shared template for creating a project README.
- `bin/` and `obj/` are generated build outputs; do not edit or commit them.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet build .\leetcode_974\leetcode_974.csproj
dotnet run --project .\leetcode_974\leetcode_974.csproj
```

`dotnet build` verifies compilation and analyzer output. `dotnet run` builds and executes the current console demo. In VS Code, press F5 to run the `Launch leetcode_974` configuration; it invokes the `build leetcode_974` pre-launch task.

## Coding Style & Naming

Follow the repository `.editorconfig`: use four spaces for C# indentation, file-scoped namespaces, braces for control blocks, and a final newline when practical. Use PascalCase for types and methods, camelCase for parameters and locals, `_camelCase` for private fields, and `s_camelCase` for private static fields. Keep algorithm demonstrations readable, preserve the existing English/Traditional Chinese problem documentation, and avoid adding dependencies unless necessary.

## Testing Guidelines

There is currently no test project or test framework. For solution changes, at minimum run `dotnet build` and `dotnet run`, and exercise representative inputs through the console demo. If automated tests are added later, place them in a clearly named test project and run them with `dotnet test`.

## Commit & Pull Request Guidelines

Use short, imperative commit subjects. Existing history mixes concise Chinese summaries with Conventional Commit-style messages; follow the same scope, for example: `feat(leetcode-974): add prefix remainder demo`. Pull requests should explain the algorithm or behavior changed, list verification commands and results, and include sample output or screenshots when console-visible behavior changes.
