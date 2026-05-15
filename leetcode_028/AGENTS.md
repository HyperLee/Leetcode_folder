# Repository Guidelines

## Project Structure & Module Organization
`leetcode_028/Program.cs` is the current entry point and holds the solution code for LeetCode 28. `leetcode_028/leetcode_028.csproj` defines the .NET console project and target framework. Use `docs/` for supporting documentation and planning artifacts; `docs/readme-template.md` is the existing documentation reference. Treat `leetcode_028/bin/` and `leetcode_028/obj/` as generated output and do not commit changes from those directories.

## Build, Test, and Development Commands
Run `dotnet build leetcode_028/leetcode_028.csproj` to compile the project and catch errors before committing. Run `dotnet run --project leetcode_028/leetcode_028.csproj` to execute the current console entry point locally. There is no committed test project yet, so `dotnet test` is not part of the current workflow.

## Coding Style & Naming Conventions
Follow `.editorconfig`: use 4 spaces in C# files and 2 spaces in project or JSON files. Prefer explicit types over `var`, keep braces on control blocks, and use file-scoped namespaces when adding new source files. Name types and methods in `PascalCase`, parameters and locals in `camelCase`, and private fields in `_camelCase`. Preserve the existing XML documentation style in `Program.cs`, including concise problem summaries and links when relevant.

## Testing Guidelines
Because this repository does not yet include automated tests, validate changes with `dotnet build` and `dotnet run` before opening a pull request. If you add tests, place them in a sibling project such as `leetcode_028.Tests/` and use descriptive method names like `StrStr_WhenNeedleMissing_ReturnsMinusOne`.

## Commit & Pull Request Guidelines
Recent commits favor short, imperative subjects, and maintenance changes sometimes use conventional prefixes such as `chore:`. Prefer messages like `docs: add contributor guide` or `fix: replace placeholder main output`. Pull requests should explain the problem being solved, list the commands you ran to verify the change, and include sample console output when runtime behavior changes.

## Repository Hygiene
Do not commit secrets, local IDE noise beyond the tracked workspace settings, or generated build output. If cleanup requires bulk deletion, stop and let the repository owner perform it manually.
