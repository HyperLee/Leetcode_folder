# LeetCode 977 Summary and VS Code Debug Design

## Goal

Add the original English problem statement and a Traditional Chinese translation to the XML documentation for `Main`. Configure VS Code so pressing F5 builds and debugs the project directly without requesting a name or launch selection.

## Scope

- Update `leetcode_977/Program.cs` only within the existing `Main` XML `<summary>`.
- Add `.vscode/tasks.json` with one default build task.
- Add `.vscode/launch.json` with one fixed Classic C# (`coreclr`) launch profile.
- Do not change the algorithm or current runtime behavior.

## Documentation Content

Keep the existing problem number, title, and links. Add these statements to the `Main` summary:

- English: "Given an integer array nums sorted in non-decreasing order, return an array of the squares of each number sorted in non-decreasing order."
- Traditional Chinese: "給定一個以非遞減順序排列的整數陣列 `nums`，請回傳一個由每個數字的平方組成，並同樣以非遞減順序排列的陣列。"

## Debug Configuration

The workspace root is `leetcode_977`, while the .NET project is in the nested `leetcode_977` directory and targets `net10.0`.

- `tasks.json` runs `dotnet build ${workspaceFolder}/leetcode_977/leetcode_977.csproj` and registers it as the default build task.
- `launch.json` uses `coreclr`, invokes that build task through `preLaunchTask`, starts `${workspaceFolder}/leetcode_977/bin/Debug/net10.0/leetcode_977.dll`, and uses `${workspaceFolder}/leetcode_977` as its working directory.
- Neither file contains `${input:...}` variables. A single launch profile avoids a configuration picker during normal F5 use.

## Verification

1. Build `leetcode_977/leetcode_977.csproj` successfully.
2. Parse both JSON files to confirm valid syntax.
3. Confirm the `preLaunchTask` value exactly matches the build task label.
4. Confirm the configured DLL exists after the build.
5. Search `.vscode` and confirm there are no `${input:...}` variables.
