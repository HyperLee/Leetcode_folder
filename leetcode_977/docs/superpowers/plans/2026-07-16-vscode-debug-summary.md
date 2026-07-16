# LeetCode 977 Summary and VS Code Debug Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add the LeetCode 977 problem statement to `Main` in English and Traditional Chinese, and provide a direct F5 VS Code debugging workflow with no input prompts.

**Architecture:** Preserve the existing nested .NET console project and add documentation only to its entry point. Place one fixed `coreclr` launch profile and one matching default `dotnet build` task in the workspace-level `.vscode` directory.

**Tech Stack:** C# 14, .NET 10, VS Code C# debugger, JSON with comments (`launch.json` and `tasks.json`)

## Global Constraints

- The workspace root is `leetcode_977`; the project path is `leetcode_977/leetcode_977.csproj`.
- The target framework and debug output folder remain `net10.0`.
- Do not change the algorithm or current runtime behavior.
- Do not use `${input:...}` variables or add multiple launch profiles.

---

### Task 1: Add the bilingual `Main` summary

**Files:**
- Modify: `leetcode_977/Program.cs:5-12`

**Interfaces:**
- Consumes: The existing `Program.Main(string[] args)` entry point and its XML `<summary>`.
- Produces: English and Traditional Chinese problem statements visible in source documentation; no runtime interface changes.

- [ ] **Step 1: Capture the current documentation check**

Run:

```powershell
rg -n "Given an integer array|給定一個以非遞減順序" leetcode_977/Program.cs
```

Expected: no matching bilingual problem statements before the edit.

- [ ] **Step 2: Add the exact bilingual statements**

Replace the blank line inside the existing `<summary>` with:

```csharp
    ///
    /// English:
    /// Given an integer array nums sorted in non-decreasing order, return an array of the squares
    /// of each number sorted in non-decreasing order.
    ///
    /// 繁體中文：
    /// 給定一個以非遞減順序排列的整數陣列 nums，請回傳一個由每個數字的平方組成，
    /// 並同樣以非遞減順序排列的陣列。
```

Keep the existing problem titles and URLs unchanged.

- [ ] **Step 3: Verify both statements and unchanged runtime code**

Run:

```powershell
rg -n "English:|Given an integer array|繁體中文|給定一個以非遞減順序|Hello, World!" leetcode_977/Program.cs
```

Expected: all five patterns match; `Console.WriteLine("Hello, World!");` remains present.

- [ ] **Step 4: Commit the documentation change**

```powershell
git add leetcode_977/Program.cs
git commit -m "docs: add leetcode 977 bilingual summary"
```

### Task 2: Add direct F5 debugging configuration

**Files:**
- Create: `.vscode/tasks.json`
- Create: `.vscode/launch.json`

**Interfaces:**
- Consumes: `leetcode_977/leetcode_977.csproj`, its `net10.0` target, and the DLL emitted by `dotnet build`.
- Produces: Task label `build leetcode_977` consumed by launch profile `Debug leetcode_977` through `preLaunchTask`.

- [ ] **Step 1: Confirm the expected configuration is absent**

Run:

```powershell
Test-Path .vscode/launch.json
Test-Path .vscode/tasks.json
```

Expected: both commands return `False` before creation.

- [ ] **Step 2: Create the default build task**

Create `.vscode/tasks.json` with:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_977",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/leetcode_977/leetcode_977.csproj"
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

- [ ] **Step 3: Create the fixed launch profile**

Create `.vscode/launch.json` with:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Debug leetcode_977",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_977",
      "program": "${workspaceFolder}/leetcode_977/bin/Debug/net10.0/leetcode_977.dll",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_977",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
```

- [ ] **Step 4: Validate JSON and cross-file configuration**

Run:

```powershell
Get-Content -Raw .vscode/tasks.json | ConvertFrom-Json | Out-Null
Get-Content -Raw .vscode/launch.json | ConvertFrom-Json | Out-Null
rg -n "build leetcode_977|Debug leetcode_977|coreclr|net10\.0" .vscode
```

Expected: both JSON parses succeed; the build label appears in both files, and the launch profile uses `coreclr` and `net10.0`.

- [ ] **Step 5: Build and confirm the configured executable exists**

Run:

```powershell
dotnet build leetcode_977/leetcode_977.csproj
Test-Path leetcode_977/bin/Debug/net10.0/leetcode_977.dll
```

Expected: build succeeds with 0 errors and `Test-Path` returns `True`.

- [ ] **Step 6: Confirm no interactive input variable exists**

Run:

```powershell
rg -n --fixed-strings '${input:' .vscode
```

Expected: no matches.

- [ ] **Step 7: Commit the VS Code configuration**

```powershell
git add .vscode/launch.json .vscode/tasks.json
git commit -m "chore: add leetcode 977 debug configuration"
```

### Task 3: Final scoped verification

**Files:**
- Verify: `leetcode_977/Program.cs`
- Verify: `.vscode/launch.json`
- Verify: `.vscode/tasks.json`

**Interfaces:**
- Consumes: Outputs from Tasks 1 and 2.
- Produces: Evidence that the requested documentation and direct-debug workflow are complete.

- [ ] **Step 1: Run the final checks**

Run:

```powershell
dotnet build leetcode_977/leetcode_977.csproj
git diff --check
git status --short --untracked-files=all
```

Expected: build succeeds with 0 errors, the diff check reports no whitespace errors, and only pre-existing untracked project files remain outside the committed scope.
