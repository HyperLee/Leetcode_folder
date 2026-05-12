# Repository Guidelines

## Project Structure & Module Organization

- `leetcode_005.slnx` is the root solution file, but it currently does not reference projects.
- `leetcode_005/` contains the C# console project. `Program.cs` is the current executable entry point and includes the LeetCode problem reference.
- `docs/` contains repository documentation prompts and templates.
- `.editorconfig` defines formatting and naming conventions; keep new C# files under the project folder unless a separate test project is added.

If tests are introduced, place them in a sibling project such as `leetcode_005.Tests/` and add both projects to the solution before relying on solution-level commands.

## Build, Test, and Development Commands

```powershell
dotnet restore leetcode_005\leetcode_005.csproj
```

Restores NuGet packages for the console project.

```powershell
dotnet build leetcode_005\leetcode_005.csproj
```

Builds the console app and validates compiler errors.

```powershell
dotnet run --project leetcode_005\leetcode_005.csproj
```

Runs the current console application.

There is no test project in the repository yet. After adding one, run `dotnet test leetcode_005.Tests\leetcode_005.Tests.csproj` or a solution-level command once the solution includes the test project.

## Coding Style & Naming Conventions

This project targets `net10.0` with nullable reference types and implicit usings enabled. Follow `.editorconfig`: use spaces, 4-space indentation for C# files, and 2-space indentation for project, solution, JSON, and XML-style files.

Use PascalCase for types, methods, properties, enums, and namespaces. Use camelCase for parameters and local variables. Private instance fields should use `_camelCase`; private static fields should use `s_camelCase`. Prefer explicit types over `var`, matching the configured C# style.

## Testing Guidelines

For new algorithm work, add focused tests that cover normal cases, edge cases, and LeetCode sample inputs. Use descriptive test names such as `LongestPalindrome_ReturnsMiddlePalindrome_WhenInputHasOddLengthResult`.

Until a test project exists, verify changes with `dotnet build` and manual `dotnet run` checks. Do not claim automated coverage unless tests have actually been added and run.

## Commit & Pull Request Guidelines

Recent commits use short messages, including Conventional Commit prefixes such as `docs:` and `feat:` as well as concise Chinese summaries. Prefer a brief imperative subject, for example `feat: add palindrome expansion solution`.

Pull requests should include a short description, the LeetCode problem or issue being addressed, commands run for verification, and screenshots only when UI or rendered documentation changes.

## Security & Configuration Tips

Do not commit secrets, credentials, local machine paths beyond repository examples, or private prompt content. Keep generated files and IDE state out of source control unless they are intentionally part of the project.
