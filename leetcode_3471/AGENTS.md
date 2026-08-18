# Repository Guidelines

## Project Structure & Module Organization

- `leetcode_3471/Program.cs` contains the console entry point and the problem solution code.
- `leetcode_3471/leetcode_3471.csproj` is the executable .NET 10 project with nullable reference types and implicit usings enabled.
- `docs/readme-template.md` is the template for project documentation. `.editorconfig` and `.gitignore` provide shared formatting and repository rules.
- There are currently no separate test or asset directories. Keep problem-specific changes inside `leetcode_3471/` unless shared documentation or configuration genuinely needs updating.

## Build, Test, and Development Commands

Run these commands from this directory:

```bash
dotnet restore leetcode_3471/leetcode_3471.csproj
dotnet build leetcode_3471/leetcode_3471.csproj --nologo
dotnet run --no-build --project leetcode_3471/leetcode_3471.csproj
dotnet format leetcode_3471/leetcode_3471.csproj --verify-no-changes --no-restore
```

The build restores dependencies and compiles the app; `run` executes the current sample harness; `format` checks style without rewriting files. The current baseline reports an existing `FINALNEWLINE` diagnostic in `Program.cs`; do not broaden a documentation-only change to rewrite unrelated source. There is no solution or test project at this level, so bare `dotnet test` fails with `MSB1003`. Treat build plus the executable sample run as the current validation gate.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces in C# files, file-scoped namespaces, braces, and explicit built-in types rather than `var`. Use PascalCase for namespaces, types, methods, and properties; use camelCase for parameters and local variables. Keep methods focused on one problem and avoid committing generated `bin/` or `obj/` output.

## Testing Guidelines

No automated test framework or coverage threshold is configured. When adding or changing an algorithm, prefer deterministic cases in `Program.cs` with clear expected-versus-actual output, and keep the console transcript stable so documentation can reproduce it. Run the build and `dotnet run --no-build` before submitting.

## Commit & Pull Request Guidelines

Recent history mixes scoped Conventional Commit-style messages such as `docs(leetcode-2029): add repository guidelines` and `fix(leetcode-2029): ...` with concise Chinese messages. Prefer a short imperative message with the `leetcode-3471` scope, for example `docs(leetcode-3471): add contributor guide`. Pull requests should explain the problem or algorithm change, list verification commands and results, link a related issue when applicable, and omit screenshots for console-only changes. Keep the diff limited to the requested project and documentation.
