# LeetCode 148 Debug Config Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add the LeetCode 148 problem statement to `Program.cs` and make the outer workspace directly runnable in VS Code without prompting for a project name.

**Architecture:** Keep the current outer-folder workspace and explicitly point VS Code build and launch settings at the inner `leetcode_148` C# project. Update the existing XML summary in `Program.cs` instead of introducing new documentation files or runtime code.

**Tech Stack:** C#, .NET 10 console app, VS Code `coreclr` debugger, VS Code tasks

---

### Task 1: Update the `Program.cs` XML summary

**Files:**
- Modify: `leetcode_148/Program.cs`

- [ ] **Step 1: Replace the empty summary body with the bilingual problem statement**

```csharp
    /// <summary>
    /// 148. Sort List
    /// https://leetcode.com/problems/sort-list/description/
    /// Given the head of a linked list, return the list after sorting it in ascending order.
    ///
    /// 148. 排序鏈結串列
    /// https://leetcode.cn/problems/sort-list/description/
    /// 給定一個 linked list 的 head，請將該 linked list 依照遞增順序排序後回傳。
    /// </summary>
```

- [ ] **Step 2: Verify the file still compiles syntactically**

Run: `dotnet build .\leetcode_148\leetcode_148.csproj`
Expected: `Build succeeded.`

### Task 2: Add the root-level VS Code build task

**Files:**
- Create: `.vscode/tasks.json`

- [ ] **Step 1: Create the fixed build task**

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_148",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/leetcode_148/leetcode_148.csproj"
      ],
      "problemMatcher": "$msCompile",
      "group": {
        "kind": "build",
        "isDefault": true
      }
    }
  ]
}
```

- [ ] **Step 2: Verify the task target path matches the real project**

Check that `${workspaceFolder}/leetcode_148/leetcode_148.csproj` resolves to `C:\GitHubFolder\Leetcode_folder\leetcode_148\leetcode_148\leetcode_148.csproj`.
Expected: the file exists.

### Task 3: Add the root-level VS Code launch config and verify output

**Files:**
- Create: `.vscode/launch.json`

- [ ] **Step 1: Create the fixed launch configuration**

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Launch leetcode_148",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_148",
      "program": "${workspaceFolder}/leetcode_148/bin/Debug/net10.0/leetcode_148.exe",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_148",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
```

- [ ] **Step 2: Build the project and verify the configured executable path exists**

Run: `dotnet build .\leetcode_148\leetcode_148.csproj`
Expected: `Build succeeded.`

Run: `Test-Path .\leetcode_148\bin\Debug\net10.0\leetcode_148.exe`
Expected: `True`

- [ ] **Step 3: Sanity-check the launch file paths**

Check that:
- `program` points to `${workspaceFolder}/leetcode_148/bin/Debug/net10.0/leetcode_148.exe`
- `cwd` points to `${workspaceFolder}/leetcode_148`
- `preLaunchTask` matches `build leetcode_148`

Expected: all three values match the build task and actual folder structure.
