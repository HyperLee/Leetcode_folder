# Repository Guidelines

## Project Structure & Module Organization
The repository root contains editor helpers in `.vscode/` and the actual console app in `leetcode_287/`. Keep solution code in `leetcode_287/Program.cs`, which is the single entry point for LeetCode 287 demos and helper methods. Project settings live in `leetcode_287/leetcode_287.csproj`. Treat `leetcode_287/bin/` and `leetcode_287/obj/` as generated output only.

## Build, Test, and Development Commands
Use commands from the repo root unless you intentionally `cd` into `leetcode_287/`.

- `dotnet build .\leetcode_287\leetcode_287.csproj` builds the .NET 10 console app.
- `dotnet run --project .\leetcode_287\leetcode_287.csproj` runs the local sample harness.
- `F5` in VS Code launches `.NET Debug leetcode_287`, which uses the root task `build leetcode_287`.

When you update examples, keep any documented command output aligned with the real `dotnet run` output.

## Coding Style & Naming Conventions
Use 4-space indentation and standard C# brace style. Prefer PascalCase for methods and helper names such as `FindDuplicate` or `FindDuplicateWithFloydCycle`. Keep methods small and deterministic. `Main` should stay readable and focus on fixed sample inputs plus clear console output. ASCII is preferred, but bilingual problem notes are acceptable when they improve clarity.

## Testing Guidelines
There is no separate test project yet, so verification is runner-based. Add or update deterministic sample cases in `Main`, print labeled results, then verify with:

- `dotnet build .\leetcode_287\leetcode_287.csproj`
- `dotnet run --project .\leetcode_287\leetcode_287.csproj`

If you later add a dedicated test project, keep its name aligned with the app folder and document the new command here.

## Commit & Pull Request Guidelines
Recent history uses short, task-focused subjects in both Chinese and English, with occasional Conventional Commits such as `feat(leetcode-274): add runnable h-index demos and docs`. Prefer `type(scope): summary`, for example `docs(leetcode-287): add contributor guide`. PRs should briefly state what changed, why it changed, and which commands you ran to verify it.
