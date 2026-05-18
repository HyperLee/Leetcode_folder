# Repository Guidelines

## Project Structure & Module Organization
The repository is centered on a single .NET console project in `leetcode_050/`. `Program.cs` contains both the sample harness in `Main()` and the `Solution` implementations for LeetCode 50. `leetcode_050.csproj` targets `net10.0`. Root-level files such as `.editorconfig`, `.gitattributes`, and `.gitignore` define shared repository behavior. `docs/readme-template.md` is a documentation scaffold; keep reusable docs in `docs/` and problem code inside the project folder.

## Build, Test, and Development Commands
Use the project file directly when running local commands:

- `dotnet restore leetcode_050/leetcode_050.csproj` restores dependencies.
- `dotnet build leetcode_050/leetcode_050.csproj -c Release` compiles the console app.
- `dotnet run --project leetcode_050/leetcode_050.csproj` runs the built-in sample cases and prints `PASS` or `FAIL`.
- `dotnet clean leetcode_050/leetcode_050.csproj` clears build outputs if `bin/` or `obj/` become stale.

## Coding Style & Naming Conventions
Follow `.editorconfig` as the source of truth. Use 4 spaces in C# files and 2 spaces in project or JSON files. Prefer file-scoped namespaces, explicit types over `var` when the type is not obvious, and braces on new lines. Use PascalCase for classes and methods (`Solution`, `MyPow`) and camelCase for locals and parameters (`factor`, `exponent`). Keep comments short and useful; bilingual problem notes are fine when they clarify algorithm intent.

## Testing Guidelines
There is no separate test project yet. For now, verify behavior by adding deterministic cases to `Main()` and checking that `dotnet run` prints `PASS` for standard, negative-exponent, and boundary inputs such as `int.MinValue`. If you add automated tests later, place them in a sibling `*.Tests` project and name tests by scenario, for example `MyPow_ReturnsReciprocal_ForNegativeExponent`.

## Commit & Pull Request Guidelines
Recent commits use short, action-first subjects, mostly in Traditional Chinese, with occasional scoped prefixes such as `docs:`. Keep one concern per commit, start with a verb, and add scope when useful, for example `docs: 補充 MyPow 說明`. Pull requests should summarize the problem, the chosen approach, and the commands you ran to verify the change.

## Safety & Repository Hygiene
Do not commit secrets or machine-specific credentials. Keep deletions scoped to a single file or an explicit path; avoid bulk delete commands. Preserve shared editor settings unless the change benefits the whole repository.
