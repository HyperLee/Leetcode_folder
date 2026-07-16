# LeetCode 1160 bilingual summary and VS Code debug design

## Scope

- Add the supplied English problem statement to the XML `<summary>` on `Main`.
- Add an accurate Traditional Chinese translation immediately after the English statement.
- Preserve the existing problem titles and links.
- Add one fixed VS Code debug profile and one matching build task.
- Do not change the solution logic or runtime output.

## Design

`leetcode_1160/Program.cs` remains the only source file involved. Its `Main` XML documentation will contain the original English statement and a Traditional Chinese translation that explicitly preserves the per-word character usage rule.

`.vscode/launch.json` will define one `coreclr` launch profile. Pressing F5 will first invoke the fixed build task, then launch `${workspaceFolder}/leetcode_1160/bin/Debug/net10.0/leetcode_1160.dll` with the project directory as the working directory. It will contain no `${input:...}` variables or project pickers.

`.vscode/tasks.json` will define one default process task that runs `dotnet build` against `${workspaceFolder}/leetcode_1160/leetcode_1160.csproj` and uses the `$msCompile` problem matcher.

## Verification

- Parse both JSON files to confirm valid syntax.
- Search the VS Code configuration for interactive `${input:...}` variables.
- Run `dotnet build leetcode_1160/leetcode_1160.csproj` and require zero build errors.
- Read back the edited summary and configuration files.
