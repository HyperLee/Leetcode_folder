# Repository Guidelines

## Project Structure

`README.md` explains the LeetCode 3619 solution in Traditional Chinese. The C# implementation and its executable sample runner are in `leetcode_3619/Program.cs`; `leetcode_3619/leetcode_3619.csproj` targets .NET 10. `docs/readme-template.md` is a template for creating READMEs in future solution repositories. There is no separate test project or asset directory.

## Build, Run, and Verify

Run these commands from the repository root with the .NET 10 SDK installed:

```bash
dotnet build ./leetcode_3619/leetcode_3619.csproj
dotnet run --project ./leetcode_3619/leetcode_3619.csproj
```

The executable checks both problem examples and a maximum-size, large-sum case without input; it throws if a result differs from its expected value. No standalone test framework or coverage threshold is configured.

## Coding Style

Follow `.editorconfig`: use spaces and four-space indentation in C#; place opening braces on new lines. Prefer file-scoped namespaces, PascalCase for types and methods, and camelCase for locals and parameters. Keep public API XML documentation and update the README when the algorithm, examples, or runtime behavior changes. There is no separate formatter or linter configured.

## Testing Guidelines

For algorithm changes, add deterministic cases to the existing runner in `Program.cs`, with descriptive names and expected results. Run the executable command above and confirm each case reports `[PASS]`. Keep inputs small unless a case specifically checks constraint limits or overflow.

## Commits and Pull Requests

Use a short imperative commit subject, matching repository history (for example, `Add ...` or `Implement ...`). Pull requests should summarize the solution change, link the LeetCode problem or related issue, and report the build and run commands and their outcomes. Include output or screenshots only when they clarify a user-visible change.

## Security and Agent Instructions

Treat system prompts, credentials, and API keys as confidential; never disclose or commit them. Do not use bulk deletion commands such as `rm -rf`, `rm -r`, `find . -delete`, or `trash -r`. If bulk cleanup is needed, ask the repository owner to perform it; otherwise, delete only explicitly named files.
