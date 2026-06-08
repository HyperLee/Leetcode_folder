# LeetCode 148 Debug Config Design

## Goal

Make the outer workspace `C:\GitHubFolder\Leetcode_folder\leetcode_148` directly runnable in VS Code without prompting for a project name, while also adding the LeetCode problem statement to `Program.cs` in both original English and Traditional Chinese.

## Current Context

- The VS Code workspace root is the outer folder.
- The actual C# project lives at `leetcode_148/leetcode_148.csproj`.
- There is no existing `.vscode/launch.json`.
- There is no existing `.vscode/tasks.json`.
- `Program.cs` already has a placeholder XML summary above `Main`.

## Design

### 1. `Program.cs` summary update

Replace the empty area inside the existing XML summary with:

- The original English problem statement:
  `Given the head of a linked list, return the list after sorting it in ascending order.`
- A Traditional Chinese translation:
  `給定一個 linked list 的 head，請將該 linked list 依照遞增順序排序後回傳。`

This keeps the problem statement close to the entry point and matches the project's existing documentation style.

### 2. Root-level VS Code build task

Add `.vscode/tasks.json` at the outer workspace root with one explicit build task:

- Task label: `build leetcode_148`
- Command: `dotnet build`
- Target project: `${workspaceFolder}/leetcode_148/leetcode_148.csproj`
- Problem matcher: `$msCompile`

This avoids any project picker or interactive prompt because the task always points to the same `.csproj`.

### 3. Root-level VS Code launch configuration

Add `.vscode/launch.json` at the outer workspace root with one explicit `.NET` launch config:

- Name: `.NET Launch leetcode_148`
- Type: `coreclr`
- Request: `launch`
- `preLaunchTask`: `build leetcode_148`
- Program path: `${workspaceFolder}/leetcode_148/bin/Debug/net10.0/leetcode_148.exe`
- CWD: `${workspaceFolder}/leetcode_148`
- `console`: `integratedTerminal`
- `stopAtEntry`: `false`

This makes `F5` run the same project every time, using the outer folder as the VS Code workspace but the inner folder as the actual build/runtime target.

## Error Handling

- If the user changes the target framework later, the `program` path in `launch.json` must be updated to match the new output folder.
- If the project file is renamed, both `tasks.json` and `launch.json` need the new path.

## Verification

After implementation:

1. Run `dotnet build .\leetcode_148\leetcode_148.csproj`
2. Confirm `.vscode/tasks.json` and `.vscode/launch.json` resolve the intended project path
3. Confirm the generated executable exists at `leetcode_148/bin/Debug/net10.0/leetcode_148.exe`
