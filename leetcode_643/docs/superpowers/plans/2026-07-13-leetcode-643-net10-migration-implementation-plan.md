# leetcode_643 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 將 `leetcode_643` 升級為具備確定性驗收框架、繁體中文教學 README 與完整 GitHub 發佈流程的 .NET 10 專案。

**Architecture:** 保留 `FindMaxAverage(int[] nums, int k)` 作為唯一公開解法 API，並改用固定長度滑動視窗在不改動輸入的情況下維護最大區間總和。`Main` 只負責建立案例、渲染結果與設定 exit code；文件、VS Code 設定和 legacy 檔案清理都位於題目根目錄，確保可單獨開啟與執行。

**Tech Stack:** C#、.NET 10 SDK-style console application、VS Code CoreCLR、Git、GitHub CLI。

## Global Constraints

- 所有 tracked 變更只能位於 `leetcode_643/`。
- 專案檔必須是 SDK-style `net10.0`，且啟用 `ImplicitUsings` 與 nullable。
- 公開 API 必須保持 `public static double FindMaxAverage(int[] nums, int k)`；只處理 LeetCode 有效輸入，不新增 invalid-input 例外。
- `FindMaxAverage` 與所有 helper 不得輸出主控台；所有 `Console.WriteLine` 必須由 `Main` 執行。
- 僅能逐檔刪除 `leetcode_643.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`；不得使用批次刪除命令。
- README 必須是繁體中文教學文件，且完整 fresh-run transcript 是其中唯一的 `text` fence。
- 最終相對 `origin/main` 只能有一個 commit：`feat(leetcode-643): migrate project to .NET 10`。
- PR 使用 draft → Ready → squash merge，並只在 merge SHA 確認後勾選 Issue #2 的唯一 `leetcode_643` 行。

---

## File Structure

- Create: `leetcode_643/.editorconfig` — 題目根目錄的 C# 格式規則。
- Create: `leetcode_643/.gitattributes` — 文字檔正規化規則。
- Create: `leetcode_643/.gitignore` — .NET 建置輸出忽略規則。
- Create: `leetcode_643/.vscode/tasks.json` — 直接建置巢狀專案。
- Create: `leetcode_643/.vscode/launch.json` — 直接啟動 .NET 10 DLL。
- Create: `leetcode_643/AGENTS.md` — 題目專屬協作與驗證指南。
- Create: `leetcode_643/README.md` — 繁體中文教學與已驗證 transcript。
- Create: `leetcode_643/docs/readme-template.md` — 未來 README 起點。
- Create: `leetcode_643/docs/superpowers/specs/2026-07-13-leetcode-643-net10-migration-design.md` — 已核可設計紀錄。
- Create: `leetcode_643/docs/superpowers/plans/2026-07-13-leetcode-643-net10-migration-implementation-plan.md` — 本實作計畫。
- Modify: `leetcode_643/leetcode_643/leetcode_643.csproj` — SDK-style .NET 10 設定。
- Modify: `leetcode_643/leetcode_643/Program.cs` — 純滑動視窗 API 與 acceptance harness。
- Delete: `leetcode_643/leetcode_643.sln`。
- Delete: `leetcode_643/leetcode_643/App.config`。
- Delete: `leetcode_643/leetcode_643/Properties/AssemblyInfo.cs`。

### Task 1: Capture the Legacy Baseline and Create the .NET 10 Project Shell

**Files:**

- Modify: `leetcode_643/leetcode_643/leetcode_643.csproj`
- Create: `leetcode_643/.editorconfig`, `.gitattributes`, `.gitignore`, `.vscode/tasks.json`, `.vscode/launch.json`, `AGENTS.md` and `docs/readme-template.md`
- Delete: `leetcode_643/leetcode_643.sln`, `leetcode_643/leetcode_643/App.config` and `leetcode_643/leetcode_643/Properties/AssemblyInfo.cs`

**Interfaces:**

- Consumes: legacy nested project at `leetcode_643/leetcode_643/leetcode_643.csproj`.
- Produces: a buildable .NET 10 console project and direct-open workspace settings for later harness work.

- [ ] **Step 1: Record the legacy build evidence before changing project files.**

Run from the worktree root:

```bash
dotnet build leetcode_643/leetcode_643/leetcode_643.csproj --nologo
```

Expected: exit code `1` with `MSB3644` mentioning `.NETFramework,Version=v4.8`; save the working directory, command, exit code and key error text in the task log. Do not treat this platform limitation as TDD RED.

- [ ] **Step 2: Replace the legacy project file with this exact SDK-style project.**

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

- [ ] **Step 3: Copy stable common configuration and README template from the verified reference.**

Run exactly:

```bash
cp leetcode_590/.editorconfig leetcode_643/.editorconfig
cp leetcode_590/.gitattributes leetcode_643/.gitattributes
cp leetcode_590/.gitignore leetcode_643/.gitignore
cp leetcode_590/docs/readme-template.md leetcode_643/docs/readme-template.md
```

The source files already follow repository conventions and only copy into the in-scope target folder.

- [ ] **Step 4: Create direct-open VS Code configuration.**

Create `leetcode_643/.vscode/tasks.json` with:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_643",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/leetcode_643/leetcode_643.csproj"
      ],
      "group": {
        "kind": "build",
        "isDefault": true
      },
      "problemMatcher": "$msCompile"
    }
  ]
}
```

Create `leetcode_643/.vscode/launch.json` with:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Debug leetcode_643",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_643",
      "program": "${workspaceFolder}/leetcode_643/bin/Debug/net10.0/leetcode_643.dll",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_643",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
```

- [ ] **Step 5: Create the target-specific contributor guide.**

Write `leetcode_643/AGENTS.md` with this executable guidance:

````markdown
# leetcode_643 contributor guide

## Project layout

- The runnable project is `leetcode_643/leetcode_643/leetcode_643.csproj`.
- Run commands from this problem root or use explicit nested paths from the parent repository.
- No formal test project exists; `Program.Main` is the deterministic acceptance harness.

## Build and run

```bash
dotnet build leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643.csproj
```

## Code contract

- Keep `FindMaxAverage(int[] nums, int k)` public, pure and console-free.
- Use a fixed-length sliding window: add the entering value and subtract the leaving value.
- Do not add invalid-input behavior outside the LeetCode valid-input contract.
- Follow the root `.editorconfig`; nullable and implicit usings are enabled.
- `Main` owns all rendered acceptance output and returns a nonzero exit code when any case fails.

## Scope and Git

- Git metadata belongs to the parent `Leetcode_folder` repository.
- Limit commits and PR changes to `leetcode_643/`.
````

- [ ] **Step 6: Remove only the three confirmed legacy files.**

Run the three individual commands:

```bash
rm leetcode_643/leetcode_643.sln
rm leetcode_643/leetcode_643/App.config
rm leetcode_643/leetcode_643/Properties/AssemblyInfo.cs
```

- [ ] **Step 7: Validate the shell.**

```bash
jq empty leetcode_643/.vscode/launch.json leetcode_643/.vscode/tasks.json
test ! -e leetcode_643/leetcode_643.sln
test ! -e leetcode_643/leetcode_643/App.config
test ! -e leetcode_643/leetcode_643/Properties/AssemblyInfo.cs
```

Expected: all commands exit `0`.

### Task 2: Write the Deterministic Harness and Prove the Legacy Algorithm Fails

**Files:**

- Modify: `leetcode_643/leetcode_643/Program.cs`

**Interfaces:**

- Consumes: the .NET 10 project shell from Task 1.
- Produces: `public static double FindMaxAverage(int[] nums, int k)` plus a deterministic `k = 1` RED runner whose output is owned by `Main`.

- [ ] **Step 1: Replace `Program.cs` with this complete temporary RED runner.**

```csharp
using System.Globalization;

namespace leetcode_643;

internal static class Program
{
    private static void Main()
    {
        int[] nums = [-1, -2];
        const double expected = -1;
        double actual = FindMaxAverage(nums, 1);
        bool passed = Math.Abs(expected - actual) <= 0.000001;

        Console.WriteLine("Case: k = 1 regression");
        Console.WriteLine("Input: nums = [-1, -2], k = 1");
        Console.WriteLine($"Expected: {expected.ToString("0.######", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Actual: {actual.ToString("0.######", CultureInfo.InvariantCulture)}");
        Console.WriteLine(passed ? "PASS" : "FAIL");
        Console.WriteLine($"Summary: {(passed ? 1 : 0)}/1 checks passed.");
        Environment.ExitCode = passed ? 0 : 1;
    }

    /// <summary>
    /// 暫時保留舊版原地前綴和邏輯以證明 k 為 1 時的回歸錯誤；有效輸入會在方法內被改寫，回傳值可能遺漏第一個元素，因此僅用於 TDD RED。
    /// </summary>
    public static double FindMaxAverage(int[] nums, int k)
    {
        if (nums.Length == 1)
        {
            return nums[0];
        }

        int lastIndexOfFirstWindow = k - 1;
        double maxSum = double.MinValue;

        for (int index = 1; index < nums.Length; index++)
        {
            nums[index] += nums[index - 1];

            if (index == lastIndexOfFirstWindow)
            {
                maxSum = nums[index];
            }
            else if (index > lastIndexOfFirstWindow)
            {
                maxSum = Math.Max(maxSum, nums[index] - nums[index - k]);
            }
        }

        return maxSum / k;
    }
}
```

The runner's `k = 1` case is the real behavior regression that proves RED; Task 3 replaces this temporary runner with the final eight-case harness.

- [ ] **Step 2: Run the RED command.**

```bash
dotnet build leetcode_643/leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643/leetcode_643.csproj
```

Expected: build exits `0` with `0 Warning(s)` and `0 Error(s)`; run exits `1`, prints `FAIL` for `k = 1 regression`, and ends with `Summary: 0/1 checks passed.` Record the actual output as the valid RED evidence.

### Task 3: Implement the Sliding-Window GREEN Solution

**Files:**

- Modify: `leetcode_643/leetcode_643/Program.cs`

**Interfaces:**

- Consumes: the Task 2 harness and exact public method signature.
- Produces: a pure `FindMaxAverage` implementation with `O(n)` time, `O(1)` extra space and stable output formatting.

- [ ] **Step 1: Replace `Program.cs` with this final source.**

```csharp
using System.Globalization;

namespace leetcode_643;

internal static class Program
{
    /// <summary>
    /// 643. Maximum Average Subarray I；643. 子陣列最大平均數 I。
    /// English URL: https://leetcode.com/problems/maximum-average-subarray-i/
    /// 中文 URL: https://leetcode.cn/problems/maximum-average-subarray-i/
    /// Given an integer array and k, return the largest average among all contiguous subarrays of length k.
    /// 給定整數陣列與 k，回傳所有長度為 k 的連續子陣列中最大的平均值。
    /// </summary>
    private static void Main()
    {
        List<CaseResult> cases =
        [
            RunAverageCase("Official example", "nums = [1, 12, -5, -6, 50, 3], k = 4", 12.75, [1, 12, -5, -6, 50, 3], 4),
            RunAverageCase("Minimum valid input", "nums = [5], k = 1", 5, [5], 1),
            RunAverageCase("k = 1 regression", "nums = [-1, -2], k = 1", -1, [-1, -2], 1),
            RunAverageCase("All negative values", "nums = [-8, -6, -7], k = 2", -6.5, [-8, -6, -7], 2),
            RunAverageCase("k equals n", "nums = [7, -3, 10], k = 3", 14d / 3d, [7, -3, 10], 3),
            RunAverageCase("Last window wins", "nums = [0, 4, 0, 3, 2], k = 2", 2.5, [0, 4, 0, 3, 2], 2),
            RunUpperBoundCase(),
            RunInputUnchangedCase()
        ];

        foreach (CaseResult caseResult in cases)
        {
            Console.WriteLine($"Case: {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: {caseResult.Expected}");
            Console.WriteLine($"Actual: {caseResult.Actual}");
            Console.WriteLine(caseResult.Passed ? "PASS" : "FAIL");
        }

        int passedCount = cases.Count(caseResult => caseResult.Passed);
        Console.WriteLine($"Summary: {passedCount}/{cases.Count} checks passed.");
        Environment.ExitCode = passedCount == cases.Count ? 0 : 1;
    }

    /// <summary>
    /// 以固定長度滑動視窗維護每個候選區間的總和；nums 與 k 必須符合題目定義的有效輸入，回傳最大總和除以 k 的雙精度平均值，且不修改 nums 或輸出主控台訊息。
    /// </summary>
    public static double FindMaxAverage(int[] nums, int k)
    {
        int currentSum = 0;

        for (int index = 0; index < k; index++)
        {
            currentSum += nums[index];
        }

        int maxSum = currentSum;

        for (int right = k; right < nums.Length; right++)
        {
            // 固定視窗右移時只加入新右端並移除舊左端，總和始終對應長度 k 的連續區間。
            currentSum += nums[right] - nums[right - k];
            maxSum = Math.Max(maxSum, currentSum);
        }

        return (double)maxSum / k;
    }

    private static CaseResult RunAverageCase(string name, string input, double expected, int[] nums, int k)
    {
        double actual = FindMaxAverage(nums, k);
        return new CaseResult(name, input, FormatDouble(expected), FormatDouble(actual), AreClose(expected, actual));
    }

    private static CaseResult RunUpperBoundCase()
    {
        const int valueCount = 100000;
        const int value = 10000;
        int[] nums = Enumerable.Repeat(value, valueCount).ToArray();
        return RunAverageCase("Upper-bound spot check", "nums = [10000 x 100000], k = 100000", value, nums, valueCount);
    }

    private static CaseResult RunInputUnchangedCase()
    {
        int[] nums = [1, 12, -5, -6, 50, 3];
        int[] original = (int[])nums.Clone();
        double actual = FindMaxAverage(nums, 4);
        bool passed = AreClose(12.75, actual) && nums.SequenceEqual(original);
        string expected = "average = 12.75; nums = [1, 12, -5, -6, 50, 3]";
        string actualDescription = $"average = {FormatDouble(actual)}; nums = {FormatNumbers(nums)}";
        return new CaseResult("Input remains unchanged", "nums = [1, 12, -5, -6, 50, 3], k = 4", expected, actualDescription, passed);
    }

    private static bool AreClose(double expected, double actual)
    {
        return Math.Abs(expected - actual) <= 0.000001;
    }

    private static string FormatDouble(double value)
    {
        return value.ToString("0.######", CultureInfo.InvariantCulture);
    }

    private static string FormatNumbers(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    private sealed record CaseResult(string Name, string Input, string Expected, string Actual, bool Passed);
}
```

- [ ] **Step 2: Run the GREEN command.**

```bash
dotnet build leetcode_643/leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643/leetcode_643.csproj
```

Expected: build exits `0` with `0 Warning(s)` and `0 Error(s)`; run exits `0`; all eight cases print `PASS`; the final line is exactly `Summary: 8/8 checks passed.`

- [ ] **Step 3: Confirm the public API and console boundary.**

```bash
rg -n "public static double FindMaxAverage|Console\.WriteLine" leetcode_643/leetcode_643/Program.cs
```

Expected: one public API declaration, and all `Console.WriteLine` occurrences are inside `Main`.

### Task 4: Write the Teaching Documentation from the Verified Transcript

**Files:**

- Create: `leetcode_643/README.md`
- Modify: `leetcode_643/AGENTS.md` if the verified harness total changes.

**Interfaces:**

- Consumes: passing Task 3 program output.
- Produces: a Traditional Chinese README whose only `text` fence exactly equals the verified run output.

- [ ] **Step 1: Generate the fresh transcript before authoring README.**

```bash
dotnet run --no-build --project leetcode_643/leetcode_643/leetcode_643.csproj > /private/tmp/leetcode_643.actual.txt
```

Expected: `/private/tmp/leetcode_643.actual.txt` contains eight PASS sections and `Summary: 8/8 checks passed.`

- [ ] **Step 2: Create `README.md` with these exact sections and facts.**

Use this section order:

```markdown
# 643. Maximum Average Subarray I｜子陣列最大平均數 I

## 題目連結
## 題意與限制
## 解法：固定長度滑動視窗
## 核心不變量與容易出錯處
## 複雜度
## 逐步範例
## Acceptance harness
## 建置與執行
## 實際執行輸出
## 最終專案結構
```

State the English and Chinese official URLs, constraints `1 <= k <= n <= 100000`, value range `-10000..10000`, the first-window / add-entering-subtract-leaving invariant, `O(n)` time and `O(1)` extra space. The walkthrough must use `[1, 12, -5, -6, 50, 3]`, `k = 4` and show sums `2`, `51`, `42`, producing `12.75`; use `plaintext`, not `text`, for this walkthrough.

Document all eight harness cases by their exact names, inputs and expected results. State there is no formal test project, and include the two problem-root commands:

```bash
dotnet build leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643.csproj
```

Embed the exact contents of `/private/tmp/leetcode_643.actual.txt` inside the only `text` fence. The final tree must list the root config files, `.vscode`, `AGENTS.md`, `README.md`, `docs/readme-template.md`, the design and plan records, and nested `Program.cs` / `.csproj`.

- [ ] **Step 3: Verify README transcript and fence uniqueness.**

```bash
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_643/README.md > /private/tmp/leetcode_643.readme.txt
diff -u /private/tmp/leetcode_643.readme.txt /private/tmp/leetcode_643.actual.txt
test "$(rg -c '^```text$' leetcode_643/README.md)" = "1"
```

Expected: all three commands exit `0` with no diff output.

### Task 5: Complete Local Verification and Consolidate the Reviewed Migration Commit

**Files:**

- Review: all files under `leetcode_643/`

**Interfaces:**

- Consumes: fully passing program, exact README transcript and target-only diff.
- Produces: one locally verified migration commit, ready for whole-branch review before publication.

- [ ] **Step 1: Run the complete local gate.**

```bash
jq empty leetcode_643/.vscode/launch.json leetcode_643/.vscode/tasks.json
dotnet build leetcode_643/leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643/leetcode_643.csproj
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_643/README.md > /private/tmp/leetcode_643.readme.txt
diff -u /private/tmp/leetcode_643.readme.txt /private/tmp/leetcode_643.actual.txt
test "$(rg -c '^```text$' leetcode_643/README.md)" = "1"
test ! -e leetcode_643/leetcode_643.sln
test ! -e leetcode_643/leetcode_643/App.config
test ! -e leetcode_643/leetcode_643/Properties/AssemblyInfo.cs
git diff --check origin/main -- leetcode_643
test -z "$(git diff --name-only origin/main -- . ':!leetcode_643')"
```

Expected: every command exits `0`; the build has 0 warnings and 0 errors; the run ends `Summary: 8/8 checks passed.`

- [ ] **Step 2: Prepare the required independent read-only review handoff.**

Give the reviewer the full staged and untracked target scope, including:

```bash
git diff --binary origin/main -- leetcode_643
git diff --no-index -- /dev/null leetcode_643/README.md
git diff --no-index -- /dev/null leetcode_643/AGENTS.md
git diff --no-index -- /dev/null leetcode_643/docs/readme-template.md
```

The controller dispatches an independent read-only reviewer against the complete `origin/main..HEAD` diff. The reviewer must verify the algorithm, `k = 1` regression, input-purity invariant, XML summaries, console boundary, README transcript, VS Code paths, legacy deletion and changed-path scope. Fix every Critical or Important finding, then rerun this full local gate and review the repaired diff.

- [ ] **Step 3: Consolidate reviewed task commits into the required single migration commit.**

```bash
git reset --soft origin/main
git add leetcode_643
git diff --cached --check
git commit -m "feat(leetcode-643): migrate project to .NET 10"
git rev-list --count origin/main..HEAD
git diff --name-only origin/main
```

Expected: cached whitespace check exits `0`; commit count is exactly `1`; every changed path starts with `leetcode_643/`.

### Task 6: Publish the Reviewed Migration and Verify the Merge

**Files:**

- Publish: the reviewed `codex/leetcode-643-net10` branch, its draft PR and Issue #2 checkbox only.

**Interfaces:**

- Consumes: the one-commit branch that has passed the complete local gate and whole-branch read-only review.
- Produces: a squash-merged PR, exactly one checked `leetcode_643` Issue #2 line and a clean, verified `main`.

- [ ] **Step 4: Push and create a draft PR.**

Push `codex/leetcode-643-net10`, then create a draft PR titled `feat(leetcode-643): migrate project to .NET 10` with this exact body:

```markdown
## Summary
- Migrates `leetcode_643` from .NET Framework 4.8 to SDK-style .NET 10.
- Replaces the mutating prefix-sum implementation with a pure fixed-length sliding window.
- Adds an eight-case deterministic acceptance harness, VS Code direct-run support, Traditional Chinese README and contributor guide.

## Algorithm
- Invariant: each rightward move adds the entering value and subtracts the leaving value.
- Complexity: `O(n)` time and `O(1)` extra space.

## Verification
- `jq empty` for both VS Code JSON files.
- `dotnet build` with 0 warnings and 0 errors.
- `dotnet run --no-build` with `Summary: 8/8 checks passed.`
- README transcript diff, legacy absence and whitespace checks.
- Independent read-only review with no unresolved Critical or Important findings.

Refs #2
```

- [ ] **Step 5: Mark Ready and squash merge only the verified head.**

Read the PR JSON, confirm its head SHA equals the locally verified `HEAD`, its changed files are all under `leetcode_643/`, merge state is clean and checks are neither pending nor failing. Mark the draft Ready, then squash merge with the same verified head SHA. Record the GitHub returned merge SHA.

- [ ] **Step 6: Update only the matching Issue #2 checkbox after merge.**

Read the latest Issue #2 body and count this exact unchecked line:

```text
- [ ] `leetcode_643`
```

Continue only if the count is `1`. Replace only that line with the following line, update the issue body, then read it back:

```text
- [x] `leetcode_643`
```

Confirm the checked line exists once, unchecked form no longer exists, and the next ordered item `leetcode_645` remains unchecked.

- [ ] **Step 7: Verify merged main in a clean worktree.**

Run from the clean main checkout or detached verification worktree:

```bash
git pull --ff-only origin main
git rev-parse HEAD
git rev-parse origin/main
jq empty leetcode_643/.vscode/launch.json leetcode_643/.vscode/tasks.json
dotnet build leetcode_643/leetcode_643/leetcode_643.csproj --nologo
dotnet run --no-build --project leetcode_643/leetcode_643/leetcode_643.csproj
git diff --check HEAD^ HEAD -- leetcode_643
git status --short
```

Expected: both SHAs equal the recorded merge SHA, build is clean, run ends `Summary: 8/8 checks passed.`, whitespace check exits `0` and status is empty.
