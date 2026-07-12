# LeetCode 501 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (- [ ]) syntax for tracking.

**Goal:** Convert leetcode_501 into a .NET 10 console project with a deterministic mode-finding harness, then publish its single migration commit.

**Architecture:** Main renders all verification results. FindMode performs iterative in-order traversal, so each equal-value run is contiguous and can update a local mode tracker in O(1) per visited node.

**Tech Stack:** C# 14, .NET 10, SDK-style MSBuild, VS Code coreclr, GitHub CLI.

## Global Constraints

- All tracked changes stay under leetcode_501/.
- Use net10.0 with ImplicitUsings and Nullable enabled; add no packages.
- Remove only the old solution, App.config, and AssemblyInfo.cs files.
- Keep public static int[] FindMode(TreeNode? root) console-free and use no static algorithm state.
- Main is the only console-output owner.
- The first meaningful red is a CS0103 missing FindMode failure.
- README is Traditional Chinese with exactly one text transcript fence.
- Do not commit per task; design, plan, implementation, and docs go in the single final commit: feat(leetcode-501): migrate project to .NET 10.

---

## File Structure

| Path | Responsibility |
| --- | --- |
| leetcode_501/leetcode_501/leetcode_501.csproj | SDK-style .NET 10 executable contract. |
| leetcode_501/leetcode_501/Program.cs | Public API and deterministic acceptance harness. |
| leetcode_501/.editorconfig, .gitattributes, .gitignore | Reused verified common settings. |
| leetcode_501/.vscode/tasks.json, launch.json | Direct nested-project build and launch. |
| leetcode_501/AGENTS.md, README.md, docs/readme-template.md | Collaboration guide and teaching documentation. |
| leetcode_501/docs/superpowers/specs/2026-07-12-leetcode-501-net10-migration-design.md | Approved design record. |
| leetcode_501/docs/superpowers/plans/2026-07-12-leetcode-501-net10-migration.md | This implementation plan. |

### Task 1: Create SDK Project and Behavioural RED

**Files:**

- Modify: leetcode_501/leetcode_501/leetcode_501.csproj
- Modify: leetcode_501/leetcode_501/Program.cs
- Delete: leetcode_501/leetcode_501.sln
- Delete: leetcode_501/leetcode_501/App.config
- Delete: leetcode_501/leetcode_501/Properties/AssemblyInfo.cs

**Interfaces:**

- Consumes: TreeNode? input.
- Produces: a RED build where FindMode(TreeNode? root) is intentionally absent.

- [ ] **Step 1: Replace the project file.**

~~~xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
~~~

- [ ] **Step 2: Delete exactly the three legacy files.**

~~~bash
rm leetcode_501/leetcode_501.sln
rm leetcode_501/leetcode_501/App.config
rm leetcode_501/leetcode_501/Properties/AssemblyInfo.cs
~~~

- [ ] **Step 3: Write this minimal harness before production behaviour exists.**

~~~csharp
namespace leetcode_501;

internal static class Program
{
    /// <summary>
    /// 501. Find Mode in Binary Search Tree
    /// 501. 二元搜尋樹中的眾數
    /// https://leetcode.com/problems/find-mode-in-binary-search-tree/
    /// https://leetcode.cn/problems/find-mode-in-binary-search-tree/
    /// Given a binary search tree that can contain duplicates, return every value with the greatest frequency.
    /// 給定可含重複值的二元搜尋樹，回傳所有出現次數最高的數值。
    /// </summary>
    private static void Main()
    {
        TreeNode root = new(1, null, new TreeNode(2, new TreeNode(2)));
        int[] actual = FindMode(root);

        Console.WriteLine(actual.SequenceEqual([2]) ? "PASS" : "FAIL");
        Environment.ExitCode = actual.SequenceEqual([2]) ? 0 : 1;
    }

    public sealed class TreeNode
    {
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }

        public int val;
        public TreeNode? left;
        public TreeNode? right;
    }
}
~~~

- [ ] **Step 4: Verify the actual RED.**

Run:

~~~bash
dotnet build leetcode_501/leetcode_501/leetcode_501.csproj --nologo
~~~

Expected: a nonzero exit with CS0103 that FindMode does not exist. Reject MSB3644, malformed-project, and fixture-error reds.

### Task 2: Add Iterative In-Order Solution and Full Harness

**Files:**

- Modify: leetcode_501/leetcode_501/Program.cs

**Interfaces:**

- Consumes: TreeNode? root satisfying the LeetCode BST ordering contract.
- Produces: public static int[] FindMode(TreeNode? root) with all modes returned in stable in-order order.

- [ ] **Step 1: Add the production API and its state-update helper.**

Insert these methods after TreeNode. They are console-free and all algorithm state belongs to the invocation.

~~~csharp
    /// <summary>
    /// 以迭代中序走訪處理可重複值的二元搜尋樹；有效輸入必須符合左子樹值不大於節點、右子樹值不小於節點的題目契約，回傳所有最高出現次數的值。
    /// </summary>
    public static int[] FindMode(TreeNode? root)
    {
        List<int> modes = [];
        Stack<TreeNode> pending = new();
        TreeNode? current = root;
        int? previousValue = null;
        int currentFrequency = 0;
        int maxFrequency = 0;

        while (current is not null || pending.Count > 0)
        {
            while (current is not null)
            {
                pending.Push(current);
                current = current.left;
            }

            current = pending.Pop();

            // BST 的中序結果非遞減，因此相同值必定形成一段連續區間。
            RecordValue(current.val, ref previousValue, ref currentFrequency, ref maxFrequency, modes);
            current = current.right;
        }

        return modes.ToArray();
    }

    /// <summary>
    /// 依目前中序值更新連續次數與眾數清單；呼叫端必須按非遞減順序提供值，方法會在新最高頻率時替換結果、在平手時保留所有眾數。
    /// </summary>
    private static void RecordValue(int value, ref int? previousValue, ref int currentFrequency, ref int maxFrequency, List<int> modes)
    {
        if (previousValue == value)
        {
            currentFrequency++;
        }
        else
        {
            previousValue = value;
            currentFrequency = 1;
        }

        // 新最高頻率才淘汰舊結果；平手值也必須保留。
        if (currentFrequency > maxFrequency)
        {
            maxFrequency = currentFrequency;
            modes.Clear();
            modes.Add(value);
        }
        else if (currentFrequency == maxFrequency)
        {
            modes.Add(value);
        }
    }
~~~

- [ ] **Step 2: Expand Main into the eight-case acceptance runner.**

Create a CaseResult record containing Name, Input, Expected, Actual, and Passed. Build each case through a pure RunCase helper that returns CaseResult. Main must print, in this order for every case, Case name, Input, Expected, Actual, and PASS or FAIL; it must end with exactly Summary: 8/8 checks passed. on a green run and use Environment.ExitCode = 1 otherwise.

Use these exact cases:

| Case | Input | Expected |
| --- | --- | --- |
| Official example 1 | [1,null,2,2] | [2] |
| Official example 2 / minimal tree | [0] | [0] |
| Distinct constraint bounds | [-100000,0,100000] | [-100000,0,100000] |
| Tied modes | [2,1,2,1] | [1,2] |
| Right subtree supplies mode | [2,1,2] | [2] |
| Repeated negative value | [-1,-1,0] | [-1] |
| Repeated invocations | first [1,1,2], then [2,1,2] | [1], then [2] |
| Maximum-height spot check | 10000-node right chain of 7 | [7] |

BuildTree accepts a nullable level-order list and BuildRightChain(value, count) constructs the final case. Both only create input data. Use SequenceEqual for expected/actual comparison and a Format method returning a bracketed comma-separated list.

- [ ] **Step 3: Verify GREEN.**

~~~bash
dotnet build leetcode_501/leetcode_501/leetcode_501.csproj --nologo
dotnet run --no-build --project leetcode_501/leetcode_501/leetcode_501.csproj
~~~

Expected: build exit 0 with 0 warnings and 0 errors; all eight cases PASS; exit 0.

### Task 3: Add Tooling and Documentation

**Files:**

- Create: leetcode_501/.editorconfig
- Create: leetcode_501/.gitattributes
- Create: leetcode_501/.gitignore
- Create: leetcode_501/.vscode/tasks.json
- Create: leetcode_501/.vscode/launch.json
- Create: leetcode_501/AGENTS.md
- Create: leetcode_501/docs/readme-template.md
- Create: leetcode_501/README.md

**Interfaces:**

- Consumes: verified .NET 10 build artefact and fresh harness transcript.
- Produces: direct-open problem workspace and code-accurate Traditional Chinese documentation.

- [ ] **Step 1: Reuse the common settings.**

Copy the exact .editorconfig, .gitattributes, .gitignore, and docs/readme-template.md content from leetcode_452. Confirm that its ignore rules retain both .vscode JSON files.

- [ ] **Step 2: Create VS Code configurations.**

tasks.json:

~~~json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_501",
      "type": "process",
      "command": "dotnet",
      "args": ["build", "${workspaceFolder}/leetcode_501/leetcode_501.csproj"],
      "group": {"kind": "build", "isDefault": true},
      "problemMatcher": "$msCompile"
    }
  ]
}
~~~

launch.json:

~~~json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Debug leetcode_501",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_501",
      "program": "${workspaceFolder}/leetcode_501/bin/Debug/net10.0/leetcode_501.dll",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_501",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
~~~

These paths assume VS Code opens the leetcode_501 problem root as its workspace.

- [ ] **Step 3: Write AGENTS.md and README.md using evidence, not invented text.**

AGENTS.md must state nested paths, problem-root build/run commands, no formal test project, required 8/8 result, four-space explicit C# style, console-free FindMode API, contiguous-run invariant, parent Git metadata, and target-only staging.

README.md must be Traditional Chinese with bilingual title and official links, bilingual problem statement, constraints, invariant, iterative-stack trade-off, O(n)/O(h), [1,null,2,2] walkthrough, eight-case table, verified commands, project tree, and exactly one text fence populated by this fresh output:

~~~bash
dotnet run --no-build --project leetcode_501/leetcode_501/leetcode_501.csproj > /private/tmp/leetcode_501.actual.txt
~~~

### Task 4: Verify, Review, and Publish

**Files:**

- Verify and publish: leetcode_501/ only

**Interfaces:**

- Consumes: complete code, config, documentation, and fresh execution transcript.
- Produces: reviewed one-commit PR, squash merge, Issue #2 update, and post-merge evidence.

- [ ] **Step 1: Run the complete local gate.**

~~~bash
jq empty leetcode_501/.vscode/launch.json leetcode_501/.vscode/tasks.json
dotnet build leetcode_501/leetcode_501/leetcode_501.csproj --nologo
dotnet run --no-build --project leetcode_501/leetcode_501/leetcode_501.csproj
dotnet run --no-build --project leetcode_501/leetcode_501/leetcode_501.csproj > /private/tmp/leetcode_501.actual.txt
awk '/^\x60\x60\x60text$/{copy=1;next} copy && /^\x60\x60\x60$/{exit} copy' leetcode_501/README.md > /private/tmp/leetcode_501.readme.txt
diff -u /private/tmp/leetcode_501.readme.txt /private/tmp/leetcode_501.actual.txt
test "$(rg -c '^\x60\x60\x60text$' leetcode_501/README.md)" = "1"
test ! -e leetcode_501/leetcode_501.sln
test ! -e leetcode_501/leetcode_501/App.config
test ! -e leetcode_501/leetcode_501/Properties/AssemblyInfo.cs
git diff --check -- leetcode_501
~~~

Expected: all commands exit 0, run summary is 8/8, and diff emits no output.

- [ ] **Step 2: Complete independent read-only review.**

Review git diff -- leetcode_501 without changing files. Check API, iterative invariant, acceptance coverage, Main-only output, Chinese XML documentation, README transcript, VS Code paths, legacy absence, and scope. Fix every Critical or Important result, then repeat the full gate and review.

- [ ] **Step 3: Create the only commit, then publish and merge.**

Stage only leetcode_501, run staged whitespace and name-only checks, commit exactly feat(leetcode-501): migrate project to .NET 10, and confirm one commit relative to origin/main. Push codex/leetcode-501-net10, create a draft PR whose body includes migration details, invariant, O(n)/O(h), verification, review, and Refs #2. Before ready, confirm head SHA, one commit, target-only files, clean merge state, and no failed/cancelled/pending checks. Squash merge with expected head SHA.

- [ ] **Step 4: Update Issue #2 only after merged true and reverify main.**

Read Issue #2, change only the unique unchecked leetcode_501 entry, read it back to confirm leetcode_501 checked and leetcode_502 remains unchecked. Fast-forward clean main to the merge SHA and rerun JSON, build, run, whitespace, and clean-status checks.
