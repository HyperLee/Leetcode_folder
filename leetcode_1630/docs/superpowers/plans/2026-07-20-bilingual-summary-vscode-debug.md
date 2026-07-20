# LeetCode 1630 Bilingual Summary and VS Code Debug Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add the complete English and Traditional Chinese problem statement to `Main` documentation and provide a no-prompt VS Code F5 workflow using the integrated terminal.

**Architecture:** Keep runtime code unchanged and limit edits to entry-point XML documentation plus two workspace-level VS Code JSON configuration files. The launch configuration builds the nested .NET 10 project first, then starts its Debug DLL through `coreclr`.

**Tech Stack:** C# 14, .NET 10, VS Code C# debugger, JSON with comments

## Global Constraints

- Do not modify the algorithm or the `Console.WriteLine("Hello, World!");` runtime behavior.
- Use `integratedTerminal` and do not define any input variables, pickers, or required arguments.
- Target `leetcode_1630/leetcode_1630.csproj` and `leetcode_1630/bin/Debug/net10.0/leetcode_1630.dll` from the workspace root.
- Do not create a Git commit because the project is currently untracked inside a shared parent repository and the user did not request Git delivery.

---

### Task 1: Repair and Expand the Main XML Documentation

**Files:**
- Modify: `leetcode_1630/Program.cs`

**Interfaces:**
- Consumes: The English problem statement supplied by the user.
- Produces: One valid `<summary>` containing labeled English and Traditional Chinese sections and one meaningful `args` parameter description.

- [x] **Step 1: Replace the duplicated XML block**

Replace the malformed documentation preceding `Main` with one valid XML documentation block. Preserve the problem titles, source URLs, formal arithmetic condition, examples, query definition, and required Boolean result semantics in both languages. Use `<para>`, `<list>`, and `<c>` so the content remains valid XML documentation.

- [x] **Step 2: Confirm runtime code is unchanged**

Run: `git diff --no-index -- NUL leetcode_1630/Program.cs`

Expected: only the XML documentation above `Main` differs from the original file; `Console.WriteLine("Hello, World!");` remains unchanged.

### Task 2: Add the Direct VS Code Debug Configuration

**Files:**
- Create: `.vscode/tasks.json`
- Create: `.vscode/launch.json`

**Interfaces:**
- Consumes: Nested project path `leetcode_1630/leetcode_1630.csproj` and target framework `net10.0`.
- Produces: A default build task named `build leetcode_1630` and a launch configuration named `.NET 10: Debug leetcode_1630`.

- [x] **Step 1: Create the default build task**

Create a process task that calls `dotnet build ${workspaceFolder}/leetcode_1630/leetcode_1630.csproj`, uses the `$msCompile` problem matcher, and belongs to the default build group.

- [x] **Step 2: Create the coreclr launch configuration**

Create a `coreclr` launch entry whose `preLaunchTask` is `build leetcode_1630`, whose `program` is `${workspaceFolder}/leetcode_1630/bin/Debug/net10.0/leetcode_1630.dll`, whose `cwd` is `${workspaceFolder}/leetcode_1630`, whose `args` is empty, and whose `console` is `integratedTerminal`.

- [x] **Step 3: Parse both files as JSON with comments**

Run a PowerShell check that strips full-line `//` comments and pipes each file into `ConvertFrom-Json -ErrorAction Stop`.

Expected: both files parse successfully and the check prints both filenames.

### Task 3: Verify the Build and No-Input Run

**Files:**
- Verify: `leetcode_1630/Program.cs`
- Verify: `.vscode/tasks.json`
- Verify: `.vscode/launch.json`

**Interfaces:**
- Consumes: Outputs from Tasks 1 and 2.
- Produces: Fresh evidence that the project builds and runs without user input.

- [x] **Step 1: Build the nested project**

Run: `dotnet build leetcode_1630/leetcode_1630.csproj`

Expected: exit code 0 with 0 errors.

- [x] **Step 2: Run without arguments or standard input**

Run: `dotnet run --project leetcode_1630/leetcode_1630.csproj --no-build`

Expected: prints `Hello, World!` and exits with code 0 without prompting.

- [x] **Step 3: Review final workspace changes**

Run: `git status --short --untracked-files=all` and inspect the three requested deliverables directly.

Expected: requested changes are limited to `leetcode_1630/Program.cs`, `.vscode/tasks.json`, and `.vscode/launch.json`, in addition to the two approved workflow documents created during this session.
