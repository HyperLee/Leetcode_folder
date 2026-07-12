# LeetCode 502 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `leetcode_502` to a deterministic .NET 10 IPO project and publish it through Issue #2.

**Architecture:** Sort projects by required capital. Scan the sorted array once, adding newly affordable profits to a maximum-priority queue; choose the best profit at most `k` times. `Main` is the sole console boundary and renders results returned by helpers.

**Tech Stack:** C# / .NET 10, `PriorityQueue<TElement, TPriority>`, VS Code CoreCLR configuration, GitHub CLI.

## Global Constraints

- Modify only `leetcode_502/`.
- Use SDK-style `net10.0` with implicit usings and nullable enabled.
- Keep `public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)` unchanged.
- Keep all console I/O in `Main`.
- End with one commit named `feat(leetcode-502): migrate project to .NET 10`.
- Do not create per-task commits; amend the existing design-record commit once after all reviews pass.
- Update Issue #2 only after GitHub confirms the squash merge.

---

### Task 1: Create the SDK project and behavior-specific RED

**Files:**
- Modify: `leetcode_502/leetcode_502/leetcode_502.csproj`
- Modify: `leetcode_502/leetcode_502/Program.cs`
- Delete: `leetcode_502/leetcode_502.sln`
- Delete: `leetcode_502/leetcode_502/App.config`
- Delete: `leetcode_502/leetcode_502/Properties/AssemblyInfo.cs`

**Interfaces:**
- Produces: a .NET 10 executable that fails only because the planned public API is missing.

- [ ] **Step 1: Replace the legacy project file.**

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

- [ ] **Step 2: Delete exactly the three confirmed legacy files.**

```text
leetcode_502/leetcode_502.sln
leetcode_502/leetcode_502/App.config
leetcode_502/leetcode_502/Properties/AssemblyInfo.cs
```

- [ ] **Step 3: Write this RED-only `Program.cs` before adding the API.**

```csharp
namespace leetcode_502;

internal static class Program
{
    /// <summary>
    /// 502. IPO／502. 首次公開募股。
    /// https://leetcode.com/problems/ipo/
    /// https://leetcode.cn/problems/ipo/
    /// Given up to k projects, choose feasible projects to maximize final capital.
    /// 給定最多 k 個專案，每次僅選目前可啟動的專案以最大化最終資金。
    /// </summary>
    private static void Main()
    {
        Console.WriteLine(FindMaximizedCapital(2, 0, new[] { 1, 2, 3 }, new[] { 0, 1, 1 }));
    }
}
```

- [ ] **Step 4: Verify RED.**

Run: `dotnet build leetcode_502/leetcode_502/leetcode_502.csproj --nologo`

Expected: exit code nonzero and `CS0103` for the intentionally missing `FindMaximizedCapital`.

### Task 2: Implement the priority-queue algorithm and 9-case harness

**Files:**
- Modify: `leetcode_502/leetcode_502/Program.cs`

**Interfaces:**
- Consumes: valid IPO parameters `k`, `w`, `profits`, and `capital`.
- Produces: the public API and `Summary: 9/9 checks passed.`.

- [ ] **Step 1: Add the public greedy API.**

```csharp
/// <summary>
/// 依目前資金挑選至多 k 個可啟動專案；最大堆確保每輪取得目前可得的最高利潤。
/// 有效輸入的 profits 與 capital 長度相同，回傳完成選擇後的最終資金。
/// </summary>
public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
{
    Project[] projects = new Project[profits.Length];
    for (int index = 0; index < projects.Length; index++)
    {
        projects[index] = new Project(capital[index], profits[index]);
    }

    Array.Sort(projects, static (left, right) => left.RequiredCapital.CompareTo(right.RequiredCapital));
    PriorityQueue<int, int> affordableProfits = new();
    int nextProject = 0;

    for (int selection = 0; selection < k; selection++)
    {
        // 只把目前資金可啟動的專案放進候選集合，指標永遠只往前移動。
        while (nextProject < projects.Length && projects[nextProject].RequiredCapital <= w)
        {
            int profit = projects[nextProject].Profit;
            affordableProfits.Enqueue(profit, -profit);
            nextProject++;
        }

        // 負 priority 讓堆頂永遠是可啟動專案中的最高利潤。
        if (!affordableProfits.TryDequeue(out int bestProfit, out _))
        {
            break;
        }

        w += bestProfit;
    }

    return w;
}
```

Add the nested type `private sealed record Project(int RequiredCapital, int Profit);`.

- [ ] **Step 2: Make `Main` render `CaseResult` values for exactly these cases.**

| Name | `k`, `w` | `profits` | `capital` | Expected |
| --- | --- | --- | --- | --- |
| Official example 1 | 2, 0 | [1,2,3] | [0,1,1] | 4 |
| Official example 2 | 3, 0 | [1,2,3] | [0,1,2] | 6 |
| Single affordable project | 1, 0 | [1] | [0] | 1 |
| No affordable project | 2, 1 | [2,3] | [2,3] | 1 |
| Choose highest affordable profit | 2, 0 | [1,7,3] | [0,0,1] | 10 |
| Unlock a later project | 3, 0 | [1,2,10] | [0,1,3] | 13 |
| More selections than projects | 5, 0 | [2,1] | [0,1] | 3 |
| Zero-profit projects | 3, 4 | [0,0] | [0,4] | 4 |
| Upper bound n=100000 | 100000, 0 | all 1 | all 0 | 100000 |

`Main` prints each case's name, input, expected, actual, and PASS/FAIL; it prints
`Summary: 9/9 checks passed.` at the end and sets `Environment.ExitCode = 1` if not all checks pass.

- [ ] **Step 3: Run GREEN.**

Run: `dotnet build leetcode_502/leetcode_502/leetcode_502.csproj --nologo`

Expected: exit 0 with 0 warnings and 0 errors.

Run: `dotnet run --no-build --project leetcode_502/leetcode_502/leetcode_502.csproj`

Expected: nine PASS cases and `Summary: 9/9 checks passed.` with exit 0.

### Task 3: Add fixed artifacts and documentation that match the run

**Files:**
- Create: `leetcode_502/.editorconfig`
- Create: `leetcode_502/.gitattributes`
- Create: `leetcode_502/.gitignore`
- Create: `leetcode_502/.vscode/tasks.json`
- Create: `leetcode_502/.vscode/launch.json`
- Create: `leetcode_502/AGENTS.md`
- Create: `leetcode_502/README.md`
- Create: `leetcode_502/docs/readme-template.md`

**Interfaces:**
- Produces: direct problem-root build/debug support and a README whose one `text` fence matches a fresh run.

- [ ] **Step 1: Recreate shared defaults from the verified `leetcode_448` reference with `apply_patch`.**

Read the four reference files, then create identical copies at `leetcode_502/.editorconfig`,
`leetcode_502/.gitattributes`, `leetcode_502/.gitignore`, and
`leetcode_502/docs/readme-template.md` using `apply_patch`; do not use shell copying to edit files.

- [ ] **Step 2: Create these direct-workspace VS Code files.**

`leetcode_502/.vscode/tasks.json`:

```json
{
  "version": "2.0.0",
  "tasks": [{
    "label": "build leetcode_502",
    "type": "process",
    "command": "dotnet",
    "args": ["build", "${workspaceFolder}/leetcode_502.csproj"],
    "group": {"kind": "build", "isDefault": true},
    "problemMatcher": "$msCompile"
  }]
}
```

`leetcode_502/.vscode/launch.json`:

```json
{
  "version": "0.2.0",
  "configurations": [{
    "name": "Debug leetcode_502",
    "type": "coreclr",
    "request": "launch",
    "preLaunchTask": "build leetcode_502",
    "program": "${workspaceFolder}/leetcode_502/bin/Debug/net10.0/leetcode_502.dll",
    "args": [],
    "cwd": "${workspaceFolder}",
    "console": "integratedTerminal",
    "stopAtEntry": false
  }]
}
```

- [ ] **Step 3: Write `AGENTS.md` and the Traditional Chinese README.**

The guide records nested build/run commands, no formal test project, the pure API, the scan/max-heap invariant, the 9/9 requirement, parent Git metadata, and the single-folder PR scope. The README contains bilingual title, links, statement and constraints, invariant and pitfalls, `O(n log n)` / `O(n)` complexity, a walkthrough, the nine-case table, actual build/run commands, final tree, and one complete `text` transcript fence.

- [ ] **Step 4: Capture and diff the README transcript.**

Run: `dotnet run --no-build --project leetcode_502/leetcode_502.csproj > /private/tmp/leetcode_502.actual.txt`

Run: `awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' README.md > /private/tmp/leetcode_502.readme.txt`

Run: `diff -u /private/tmp/leetcode_502.readme.txt /private/tmp/leetcode_502.actual.txt`

Run: `rg -c '^```text$' README.md`

Expected: no diff output and a fence count of `1`.

- [ ] **Step 5: Validate editor configuration and documentation commands.**

Run: `jq empty .vscode/tasks.json .vscode/launch.json`

Run: `dotnet build leetcode_502/leetcode_502.csproj --nologo`

Run: `dotnet run --no-build --project leetcode_502/leetcode_502.csproj`

Expected: valid JSON, a 0-warning / 0-error build, and the 9/9 summary.

### Task 4: Run the final local delivery gate and independent review

**Files:**
- Modify: all and only files in `leetcode_502/`.

**Interfaces:**
- Produces: fresh local evidence and an approved independent read-only review before any commit or GitHub mutation.

- [ ] **Step 1: Run the delivery gate.**

Run: `jq empty leetcode_502/.vscode/launch.json leetcode_502/.vscode/tasks.json`

Run: `dotnet build leetcode_502/leetcode_502/leetcode_502.csproj --nologo`

Run: `dotnet run --no-build --project leetcode_502/leetcode_502/leetcode_502.csproj`

Run: `git diff --check -- leetcode_502`

Expected: all exit 0 and the runner ends `Summary: 9/9 checks passed.`.

- [ ] **Step 2: Perform independent read-only review.**

Check public API/algorithm correctness, Main-only output, bilingual `Main` XML, Traditional Chinese major-function XML, harness boundaries, JSON paths, README transcript, legacy-file absence, scope, and dead-code absence. Repair every Critical or Important finding and repeat Step 1.

### Task 5: Commit, publish, merge, and complete Issue tracking

**Files:**
- Modify: all and only verified files in `leetcode_502/`.

**Interfaces:**
- Consumes: Task 4's clean local gate and approved independent review.
- Produces: one squash merge, one Issue #2 checkbox update, and fresh post-merge verification.

- [ ] **Step 1: Amend the design-record commit into the sole feature commit.**

Run: `git add leetcode_502`

Run: `git diff --cached --check`

Run: `git diff --cached --name-only`

Run: `git commit --amend -m "feat(leetcode-502): migrate project to .NET 10"`

Run: `git rev-list --count origin/main..HEAD`

Expected: only `leetcode_502/` paths, clean whitespace, and a commit count of `1`.

- [ ] **Step 2: Push, create Draft PR, mark Ready after checks, and squash merge with the verified head SHA.**

Run: `git push --set-upstream origin codex/leetcode-502-net10`

Run: `gh pr create --repo HyperLee/Leetcode_folder --base main --head codex/leetcode-502-net10 --draft --title "feat(leetcode-502): migrate project to .NET 10" --body "Refs #2"`

Expected: draft PR with one commit and only `leetcode_502/` paths.

- [ ] **Step 3: Update Issue #2 only after the merge response.**

Confirm `merged: true`, a merge SHA, and one exact `- [ ] \`leetcode_502\`` line. Change only that line to `- [x] \`leetcode_502\``, read Issue #2 back, and confirm `- [ ] \`leetcode_515\`` remains.

- [ ] **Step 4: Fast-forward `main` and rerun JSON, build, run, whitespace, and clean-status checks.**

Run: `git pull --ff-only origin main`

Run: `git status --short`

Run: `jq empty leetcode_502/.vscode/launch.json leetcode_502/.vscode/tasks.json`

Run: `dotnet build leetcode_502/leetcode_502/leetcode_502.csproj --nologo`

Run: `dotnet run --no-build --project leetcode_502/leetcode_502/leetcode_502.csproj`

Run: `git diff --check -- leetcode_502`

Expected: clean status, valid JSON, 0 warnings, 0 errors, and `Summary: 9/9 checks passed.`.
