# LeetCode 1630 Bilingual Summary and VS Code Debug Design

## Scope

- Replace the malformed, duplicated XML documentation around `Main` with the complete English problem statement and a faithful Traditional Chinese translation.
- Add workspace-level `.vscode/tasks.json` and `.vscode/launch.json` files.
- Do not change the algorithm, `Main` runtime behavior, project settings, or other documentation.

## VS Code Debug Flow

- `tasks.json` defines one default build task that runs `dotnet build` against `leetcode_1630/leetcode_1630.csproj`.
- `launch.json` defines one explicit `coreclr` configuration.
- Pressing F5 invokes the build task and starts `leetcode_1630/bin/Debug/net10.0/leetcode_1630.dll`.
- The program runs in VS Code's integrated terminal.
- The configuration contains no input variables, selection prompts, or required command-line arguments.

## Documentation Format

- Keep the problem number, title, and source links in the `Main` XML `<summary>`.
- Label the English and Traditional Chinese sections clearly.
- Preserve the mathematical expressions and examples from the supplied English statement.
- Translate the full statement into natural Traditional Chinese without converting it to Simplified Chinese.
- Keep a single `<param name="args">` entry with a meaningful description.

## Verification

- Parse both VS Code files as JSON with comments.
- Run `dotnet build leetcode_1630/leetcode_1630.csproj`.
- Run the compiled application without supplying input and confirm it exits normally.
- Review the final diff to confirm that only the requested documentation and debug configuration changed.
