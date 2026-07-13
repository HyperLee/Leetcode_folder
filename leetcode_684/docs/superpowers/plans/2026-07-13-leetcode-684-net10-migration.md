# leetcode_684 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Deliver LeetCode 684 as a documented, deterministic, SDK-style .NET 10 console project and publish it through a squash-merged pull request.

**Architecture:** Keep `FindRedundantConnection(int[][])` pure. A union-find implementation with path compression and union by size returns the first edge whose endpoints already share a representative. `Main` is the deterministic acceptance harness and the sole owner of console output.

**Tech Stack:** C# 14 on .NET 10, SDK-style MSBuild, VS Code CoreCLR launch settings, GitHub CLI.

## Global Constraints

- All tracked changes remain under `/private/tmp/codex-leetcode-684-net10/leetcode_684/`.
- Target exactly `net10.0`, enable `ImplicitUsings` and nullable reference types, and add no package references.
- Preserve `public static int[] FindRedundantConnection(int[][] edges)` and add no invalid-input exceptions outside the LeetCode contract.
- Only `Main` writes to the console; every production helper remains pure.
- The final branch has exactly one Conventional Commit relative to `origin/main`; amend the existing design-document commit before publication.

## File Map

| File | Responsibility |
| --- | --- |
| `leetcode_684/leetcode_684/leetcode_684.csproj` | SDK-style .NET 10 executable project. |
| `leetcode_684/leetcode_684/Program.cs` | Public API, union-find algorithm, XML docs, and acceptance harness. |
| `leetcode_684/.vscode/*.json` | Direct nested-project build and debugger configuration. |
| `leetcode_684/README.md` | Traditional Chinese teaching document and fresh transcript. |
| `leetcode_684/AGENTS.md` | Problem-specific build, API, and Git guidance. |

## Task 1: Migrate the project and establish the TDD RED

**Files:**
- Modify: `/private/tmp/codex-leetcode-684-net10/leetcode_684/leetcode_684/leetcode_684.csproj`
- Modify: `/private/tmp/codex-leetcode-684-net10/leetcode_684/leetcode_684/Program.cs`
- Delete: `/private/tmp/codex-leetcode-684-net10/leetcode_684/leetcode_684.sln`
- Delete: `/private/tmp/codex-leetcode-684-net10/leetcode_684/leetcode_684/App.config`
- Delete: `/private/tmp/codex-leetcode-684-net10/leetcode_684/leetcode_684/Properties/AssemblyInfo.cs`
- Test: the `Main` acceptance harness in `Program.cs`

**Interfaces:**
- Consumes: valid LeetCode input where `edges.Length == n`, labels are in `1..n`, and exactly one edge completes a cycle.
- Produces: a compile-time call to the intentionally absent `FindRedundantConnection(int[][] edges)` API.

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

- [ ] **Step 2: Delete only the three confirmed legacy files.**

Run each single-path command:

~~~bash
rm leetcode_684/leetcode_684.sln
rm leetcode_684/leetcode_684/App.config
rm leetcode_684/leetcode_684/Properties/AssemblyInfo.cs
~~~

- [ ] **Step 3: Add the acceptance cases and call the missing API.**

Replace `Program.cs` with this harness. It deliberately calls the missing API in `RunCase`.

~~~csharp
using System;
using System.Linq;

namespace leetcode_684;

internal static class Program
{
    /// <summary>
    /// 684. Redundant Connection
    /// 684. 冗餘連線
    /// https://leetcode.com/problems/redundant-connection/
    /// https://leetcode.cn/problems/redundant-connection/
    /// A tree with one extra undirected edge is given; return the final input edge that creates a cycle.
    /// 給定一棵多了一條無向邊的樹；回傳輸入順序中最後造成環的邊。
    /// </summary>
    private static void Main()
    {
        RedundantConnectionCase[] cases =
        [
            new("Official example 1", "[[1,2], [1,3], [2,3]]", [[1, 2], [1, 3], [2, 3]], [2, 3]),
            new("Official example 2", "[[1,2], [2,3], [3,4], [1,4], [1,5]]", [[1, 2], [2, 3], [3, 4], [1, 4], [1, 5]], [1, 4]),
            new("Minimum reordered cycle", "[[3,1], [1,2], [2,3]]", [[3, 1], [1, 2], [2, 3]], [2, 3]),
            new("Long tree prefix", "[[1,2], [2,3], [3,4], [4,5], [2,5]]", [[1, 2], [2, 3], [3, 4], [4, 5], [2, 5]], [2, 5]),
            new("Preserve answer direction", "[[2,1], [3,2], [4,3], [4,1]]", [[2, 1], [3, 2], [4, 3], [4, 1]], [4, 1]),
            new("Branches meet in a cycle", "[[1,2], [1,3], [2,4], [3,5], [4,5]]", [[1, 2], [1, 3], [2, 4], [3, 5], [4, 5]], [4, 5]),
            new("Forest becomes one cycle", "[[1,2], [3,4], [2,3], [1,4]]", [[1, 2], [3, 4], [2, 3], [1, 4]], [1, 4]),
            new("100000-node spot check", "chain 1..100000 plus [1,100000]", CreateLargeChainWithCycle(100000), [1, 100000])
        ];

        CaseResult[] results = cases.Select(RunCase).ToArray();
        foreach (CaseResult result in results)
        {
            Console.WriteLine($"Case: {result.Name}");
            Console.WriteLine($"Input: {result.InputDescription}");
            Console.WriteLine($"Expected: {FormatEdge(result.Expected)}");
            Console.WriteLine($"Actual: {FormatEdge(result.Actual)}");
            Console.WriteLine($"Result: {(result.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = results.Count(result => result.Passed);
        Console.WriteLine($"Summary: {passedCount}/{results.Length} checks passed.");
        if (passedCount != results.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(RedundantConnectionCase testCase)
    {
        int[] actual = FindRedundantConnection(testCase.Edges);
        bool passed = testCase.Expected.SequenceEqual(actual);
        return new CaseResult(testCase.Name, testCase.InputDescription, testCase.Expected, actual, passed);
    }

    private static int[][] CreateLargeChainWithCycle(int nodeCount)
    {
        int[][] edges = new int[nodeCount][];
        for (int node = 1; node < nodeCount; node++)
        {
            edges[node - 1] = [node, node + 1];
        }

        edges[^1] = [1, nodeCount];
        return edges;
    }

    private static string FormatEdge(int[] edge) => $"[{string.Join(',', edge)}]";

    private sealed record RedundantConnectionCase(string Name, string InputDescription, int[][] Edges, int[] Expected);

    private sealed record CaseResult(string Name, string InputDescription, int[] Expected, int[] Actual, bool Passed);
}
~~~

- [ ] **Step 4: Verify the RED.**

~~~bash
dotnet build leetcode_684/leetcode_684/leetcode_684.csproj --nologo
~~~

Expected: non-zero exit code and `CS0103` naming `FindRedundantConnection`, rather than an old-project configuration error.

## Task 2: Implement union-find and prove GREEN

**Files:**
- Modify: `/private/tmp/codex-leetcode-684-net10/leetcode_684/leetcode_684/Program.cs`
- Test: the eight-case acceptance harness in `Program.cs`

**Interfaces:**
- Consumes: the eight valid input arrays from Task 1.
- Produces: `public static int[] FindRedundantConnection(int[][] edges)`, `Find(int[] parent, int node)`, and `Union(int[] parent, int[] componentSize, int firstNode, int secondNode)`.

- [ ] **Step 1: Add the smallest union-find implementation that satisfies the harness.**

~~~csharp
public static int[] FindRedundantConnection(int[][] edges)
{
    int[] parent = new int[edges.Length + 1];
    int[] componentSize = new int[edges.Length + 1];
    for (int node = 1; node <= edges.Length; node++)
    {
        parent[node] = node;
        componentSize[node] = 1;
    }

    foreach (int[] edge in edges)
    {
        if (!Union(parent, componentSize, edge[0], edge[1]))
        {
            return edge;
        }
    }

    return Array.Empty<int>();
}

private static int Find(int[] parent, int node)
{
    if (parent[node] != node)
    {
        parent[node] = Find(parent, parent[node]);
    }

    return parent[node];
}

private static bool Union(int[] parent, int[] componentSize, int firstNode, int secondNode)
{
    int firstRoot = Find(parent, firstNode);
    int secondRoot = Find(parent, secondNode);
    if (firstRoot == secondRoot)
    {
        return false;
    }

    if (componentSize[firstRoot] < componentSize[secondRoot])
    {
        (firstRoot, secondRoot) = (secondRoot, firstRoot);
    }

    parent[secondRoot] = firstRoot;
    componentSize[firstRoot] += componentSize[secondRoot];
    return true;
}
~~~

Add Traditional Chinese XML summaries to all three methods. The public method summary must describe valid inputs and the return edge; `Find` must describe path compression; `Union` must describe its boolean result and union-by-size invariant. Keep only comments that explain the representative and size-update reasoning.

- [ ] **Step 2: Build the GREEN implementation.**

~~~bash
dotnet build leetcode_684/leetcode_684/leetcode_684.csproj --nologo
~~~

Expected: exit code 0 with zero warnings and zero errors.

- [ ] **Step 3: Run all behavior checks.**

~~~bash
dotnet run --no-build --project leetcode_684/leetcode_684/leetcode_684.csproj
~~~

Expected: eight `PASS` results and the exact final line `Summary: 8/8 checks passed.`. The 100000-node case prints a compact input label, not every edge.

## Task 3: Add fixed artifacts and teaching documentation

**Files:**
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/.editorconfig`
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/.gitattributes`
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/.gitignore`
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/.vscode/tasks.json`
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/.vscode/launch.json`
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/AGENTS.md`
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/docs/readme-template.md`
- Create: `/private/tmp/codex-leetcode-684-net10/leetcode_684/README.md`

**Interfaces:**
- Consumes: the green project and its fresh harness output.
- Produces: problem-root tooling and docs whose paths, API name, complexity, case count, and transcript match the implementation exactly.

- [ ] **Step 1: Reproduce the shared files with `apply_patch`.**

Copy the exact contents of the current merged references at `leetcode_662/.editorconfig`, `leetcode_662/.gitattributes`, `leetcode_662/.gitignore`, and `leetcode_662/docs/readme-template.md` into the matching `leetcode_684` paths. Keep their generic shared rules unchanged.

- [ ] **Step 2: Add the direct project-root VS Code configuration.**

`leetcode_684/.vscode/tasks.json`:

~~~json
{"version":"2.0.0","tasks":[{"label":"build leetcode_684","type":"process","command":"dotnet","args":["build","${workspaceFolder}/leetcode_684/leetcode_684.csproj"],"group":{"kind":"build","isDefault":true},"problemMatcher":"$msCompile"}]}
~~~

`leetcode_684/.vscode/launch.json`:

~~~json
{"version":"0.2.0","configurations":[{"name":"Debug leetcode_684","type":"coreclr","request":"launch","preLaunchTask":"build leetcode_684","program":"${workspaceFolder}/leetcode_684/bin/Debug/net10.0/leetcode_684.dll","args":[],"cwd":"${workspaceFolder}/leetcode_684","console":"integratedTerminal","stopAtEntry":false}]}
~~~

- [ ] **Step 3: Write `AGENTS.md` in Traditional Chinese.**

Describe the nested project path, commands run from the problem root, no formal test project, four-space style, pure public API, path-compression/union-by-size invariant, the eight-check success line, parent Git metadata, and target-only staging.

- [ ] **Step 4: Write `README.md` in Traditional Chinese.**

Use the sections `題目說明`, `限制條件`, `核心不變量`, `演算法設計`, `複雜度`, `逐步範例`, `Acceptance harness`, `建置與執行`, `實際輸出`, and `專案結構`. Include both official links; explain preserved edge direction and input purity; state `O(n alpha(n))` time and `O(n)` auxiliary space; table all eight exact cases; and state no formal test project exists. Any walk-through fence uses `plaintext`.

Run this command before adding `實際輸出`:

~~~bash
dotnet run --no-build --project leetcode_684/leetcode_684/leetcode_684.csproj > /private/tmp/leetcode_684.actual.txt
~~~

Copy the complete output from that file verbatim into the README's one and only `text` fence.

## Task 4: Verify, review, and create the one feature commit

**Files:**
- Verify: every modified path under `/private/tmp/codex-leetcode-684-net10/leetcode_684/`
- Modify only when review finds a requirement mismatch: the responsible target file

**Interfaces:**
- Consumes: the full migration and its fresh transcript.
- Produces: one locally verified commit relative to `origin/main`.

- [ ] **Step 1: Run the full verification bundle from the worktree.**

~~~bash
jq empty leetcode_684/.vscode/launch.json leetcode_684/.vscode/tasks.json
dotnet build leetcode_684/leetcode_684/leetcode_684.csproj --nologo
dotnet run --no-build --project leetcode_684/leetcode_684/leetcode_684.csproj
dotnet run --no-build --project leetcode_684/leetcode_684/leetcode_684.csproj > /private/tmp/leetcode_684.actual.txt
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_684/README.md > /private/tmp/leetcode_684.readme.txt
diff -u /private/tmp/leetcode_684.readme.txt /private/tmp/leetcode_684.actual.txt
test "$(rg -c '^```text$' leetcode_684/README.md)" -eq 1
git diff --check -- leetcode_684
test ! -e leetcode_684/leetcode_684.sln
test ! -e leetcode_684/leetcode_684/App.config
test ! -e leetcode_684/leetcode_684/Properties/AssemblyInfo.cs
~~~

Expected: every command exits 0; the README diff is empty; the run ends `Summary: 8/8 checks passed.`.

- [ ] **Step 2: Perform an independent read-only review of all changed and new target files.**

Check public API compatibility and purity; all eight expected edges; Main-only console I/O; bilingual `Main` XML; Chinese XML docs for all three algorithm methods; meaningful invariant comments; README/code/command/transcript agreement; VS Code nested paths; legacy absence; target-only scope; and dead-code or hidden-exception absence. Repair every Critical or Important finding, then repeat Step 1.

- [ ] **Step 3: Amend the design commit into the required single feature commit.**

~~~bash
git add leetcode_684
git diff --cached --check
git commit --amend -m "feat(leetcode-684): migrate project to .NET 10"
git rev-list --count origin/main..HEAD
~~~

Expected: the last command prints `1`.

## Task 5: Publish and close the delivery loop

**Files:**
- Modify remotely after merge only: the one Issue #2 line for `leetcode_684`.

**Interfaces:**
- Consumes: a reviewed, locally verified, one-commit branch.
- Produces: a squash merge, one checked Issue #2 checkbox, and verified post-merge main.

- [ ] **Step 1: Push the branch and create a draft PR.**

~~~bash
git push -u origin codex/leetcode-684-net10
~~~

Create a draft PR titled `feat(leetcode-684): migrate project to .NET 10`. Its body includes the .NET 10 migration, union-find invariant, `O(n alpha(n))` time, `O(n)` auxiliary space, all local verification results, independent review outcome, and `Refs #2`.

- [ ] **Step 2: Read the PR and ensure its head equals the locally verified SHA.**

Confirm one commit, target-only changed files, clean mergeability, and no pending, cancelled, or failed checks. If origin/main advanced, rebase only after recording the state, then rerun Task 4.

- [ ] **Step 3: Mark ready and squash merge with the expected head SHA.**

Read GitHub's merge response and record the merge SHA before touching Issue #2.

- [ ] **Step 4: Update Issue #2 only after merge.**

Read the latest issue body, confirm `- [ ] \`leetcode_684\` | last commit \`2023-07-02\`` appears exactly once, change only that marker to `[x]`, read the issue back, and confirm `leetcode_705` remains unchecked.

- [ ] **Step 5: Verify merged main.**

Fast-forward a clean main checkout or clean detached worktree to the recorded merge SHA, repeat Task 4 Step 1, and require empty `git status --short` before reporting release.
