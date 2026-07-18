# LeetCode 1481 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 將 `leetcode_1481` 翻新為具純函式解法、確定性 acceptance harness、繁體中文教學文件與完整發布證據的 SDK-style .NET 10 專案。

**Architecture:** 保留既有的頻率貪心思路：公開 API 統計各整數頻率、升冪排序後優先完整移除最低頻率群組。`Main` 是唯一 console I/O 邊界，以九個案例同時驗證答案與輸入不變性；題目根目錄承載 VS Code、README、AGENTS 與共用設定。

**Tech Stack:** C# 14、.NET 10 SDK、SDK-style `.csproj`、VS Code CoreCLR 設定、Git／GitHub CLI。

## Global Constraints

- 所有 tracked changes 必須限制在 `leetcode_1481/`。
- 公開 API 固定為 `public static int FindLeastNumOfUniqueInts(int[] arr, int k)`。
- 專案必須使用 `net10.0`，並啟用 `ImplicitUsings` 與 `Nullable`。
- 不新增第三方套件或正式測試專案；`Main` acceptance harness 是可執行驗證來源。
- 公開 API 不輸出、不修改輸入，且不新增題目限制外輸入的例外行為。
- 所有 console I/O 僅能位於 `Main`。
- README 必須使用繁體中文，fresh run 是唯一 `text` fence，且 transcript 必須精確一致。
- 舊 `.sln`、`App.config`、`Properties/AssemblyInfo.cs` 必須逐檔移除。
- 相對 `origin/main` 必須維持單一 commit：`feat(leetcode-1481): migrate project to .NET 10`。
- 只有 PR 確認合併後，才能更新 Issue #2 的 `leetcode_1481` 核取方塊。

---

## File Map

- `leetcode_1481/leetcode_1481/Program.cs`：公開解法 API、九案例 acceptance harness、雙語題述 XML。
- `leetcode_1481/leetcode_1481/leetcode_1481.csproj`：SDK-style .NET 10 executable project。
- `leetcode_1481/.vscode/tasks.json`：從題目根目錄建置 nested project。
- `leetcode_1481/.vscode/launch.json`：啟動 `net10.0` DLL。
- `leetcode_1481/.editorconfig`、`.gitattributes`、`.gitignore`：沿用最近已驗證題目的共用規則。
- `leetcode_1481/AGENTS.md`：題目專屬協作、API、驗證與 Git scope 契約。
- `leetcode_1481/README.md`：繁體中文教學文件與 fresh run 證據。
- `leetcode_1481/docs/readme-template.md`：最近已驗證的 README 起始範本。
- `leetcode_1481/docs/superpowers/specs/2026-07-18-leetcode-1481-net10-migration-design.md`：已核准設計。
- `leetcode_1481/docs/superpowers/plans/2026-07-18-leetcode-1481-net10-migration.md`：本計畫。
- 刪除 `leetcode_1481/leetcode_1481.sln`、`leetcode_1481/leetcode_1481/App.config`、`leetcode_1481/leetcode_1481/Properties/AssemblyInfo.cs`。

---

### Task 1: 建立 SDK 專案與可觀察的 TDD RED

**Files:**
- Modify: `leetcode_1481/leetcode_1481/leetcode_1481.csproj`
- Modify: `leetcode_1481/leetcode_1481/Program.cs`
- Delete: `leetcode_1481/leetcode_1481.sln`
- Delete: `leetcode_1481/leetcode_1481/App.config`
- Delete: `leetcode_1481/leetcode_1481/Properties/AssemblyInfo.cs`

**Interfaces:**
- Consumes: LeetCode 有效輸入 `int[] arr` 與 `0 <= k <= arr.Length`。
- Produces: 一個會呼叫尚未存在之 `FindLeastNumOfUniqueInts(int[], int)` 的九案例 harness，作為真實 compiler RED。

- [ ] **Step 1: 將 `.csproj` 改為 SDK-style .NET 10**

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

- [ ] **Step 2: 建立會呼叫缺少 API 的完整 harness**

`Program.cs` 先只保留 `Main` 與 `TestCase`，其中上限案例使用：

```csharp
int[] maximumLengthNumbers =
[
    .. Enumerable.Repeat(1, 50_000),
    .. Enumerable.Repeat(2, 25_000),
    .. Enumerable.Range(3, 25_000)
];

TestCase[] testCases =
[
    new("Official example 1", "arr = [5, 5, 4], k = 1", [5, 5, 4], 1, 1),
    new("Official example 2", "arr = [4, 3, 1, 1, 3, 3, 2], k = 3", [4, 3, 1, 1, 3, 3, 2], 3, 2),
    new("Minimum input / remove nothing", "arr = [1], k = 0", [1], 0, 1),
    new("Remove every element", "arr = [1], k = 1", [1], 1, 0),
    new("Zero removals", "arr = [1, 2, 2, 3, 3, 3], k = 0", [1, 2, 2, 3, 3, 3], 0, 3),
    new("Cannot remove the next frequency group", "arr = [1, 1, 2, 2, 2, 3, 3, 3], k = 1", [1, 1, 2, 2, 2, 3, 3, 3], 1, 3),
    new("Equal frequencies", "arr = [1, 1, 2, 2, 3, 3, 4], k = 4", [1, 1, 2, 2, 3, 3, 4], 4, 2),
    new("Large values", "arr = [1000000000, 1000000000, 999999999, 123456789], k = 2", [1_000_000_000, 1_000_000_000, 999_999_999, 123_456_789], 2, 1),
    new("Maximum length", "arr = [length 100000; 1 x 50000, 2 x 25000, 3..25002], k = 25000", maximumLengthNumbers, 25_000, 2)
];
```

每次呼叫前以 `int[] original = [.. testCase.Numbers];` 複製輸入，然後執行：

```csharp
int actual = FindLeastNumOfUniqueInts(testCase.Numbers, testCase.K);
bool isPassed = actual == testCase.Expected && testCase.Numbers.SequenceEqual(original);
```

輸出固定為 `Case`、`Input`、`Expected`、`Actual`、`Result` 與空白行；結尾輸出
`Summary: {passed}/{testCases.Length} checks passed.`，未全數通過時設定
`Environment.ExitCode = 1`。記錄型別固定為：

```csharp
private sealed record TestCase(string Name, string Input, int[] Numbers, int K, int Expected);
```

- [ ] **Step 3: 逐檔刪除 legacy artifacts**

使用 `apply_patch` 分別刪除已確認的三個檔案，不執行任何遞迴或批次刪除：

```text
leetcode_1481/leetcode_1481.sln
leetcode_1481/leetcode_1481/App.config
leetcode_1481/leetcode_1481/Properties/AssemblyInfo.cs
```

- [ ] **Step 4: 執行 build 並確認有效 RED**

Run:

```bash
dotnet build leetcode_1481/leetcode_1481/leetcode_1481.csproj --nologo
```

Expected: exit code 非 0，0 warnings，錯誤為 `CS0103`，指出目前內容中不存在
`FindLeastNumOfUniqueInts`；不得接受專案設定、拼字或 legacy assembly 所造成的錯誤。

---

### Task 2: 加入最小正確實作並取得 GREEN

**Files:**
- Modify: `leetcode_1481/leetcode_1481/Program.cs`

**Interfaces:**
- Consumes: `int[] arr`，長度 `1..100000`；元素 `1..1000000000`；`k` 為 `0..arr.Length`。
- Produces: `public static int FindLeastNumOfUniqueInts(int[] arr, int k)`，回傳恰好移除 `k` 個元素後可達到的最少相異整數數量。

- [ ] **Step 1: 在 `Program` 加入公開 API**

```csharp
/// <summary>
/// 在 arr 與 k 符合題目限制的有效輸入下，統計各整數頻率並由低至高完整移除頻率群組，
/// 以最小化恰好移除 k 個元素後的相異整數數量；回傳仍存在的相異整數數量，且不修改 arr。
/// </summary>
/// <param name="arr">長度介於 1 至 100000，元素介於 1 至 1000000000 的有效整數陣列。</param>
/// <param name="k">要移除的元素數量，介於 0 至 arr.Length。</param>
/// <returns>恰好移除 k 個元素後可得到的最少相異整數數量。</returns>
public static int FindLeastNumOfUniqueInts(int[] arr, int k)
{
    Dictionary<int, int> frequencyByNumber = [];
    foreach (int number in arr)
    {
        frequencyByNumber[number] = frequencyByNumber.GetValueOrDefault(number) + 1;
    }

    List<int> sortedFrequencies = [.. frequencyByNumber.Values];
    sortedFrequencies.Sort();

    int remainingUniqueCount = sortedFrequencies.Count;
    foreach (int frequency in sortedFrequencies)
    {
        // 只有完整刪除一個最低頻率群組，才會讓相異整數數量減一。
        if (frequency > k)
        {
            break;
        }

        k -= frequency;
        remainingUniqueCount--;
    }

    return remainingUniqueCount;
}
```

- [ ] **Step 2: 執行 build 取得 GREEN**

Run:

```bash
dotnet build leetcode_1481/leetcode_1481/leetcode_1481.csproj --nologo
```

Expected: exit code 0，`0 Warning(s)`、`0 Error(s)`。

- [ ] **Step 3: 執行九案例 harness**

Run:

```bash
dotnet run --no-build --project leetcode_1481/leetcode_1481/leetcode_1481.csproj
```

Expected: 九個 `Result: PASS`，最後一行精確為 `Summary: 9/9 checks passed.`，exit code 0。

---

### Task 3: 完成題目根目錄設定與教學文件

**Files:**
- Create: `leetcode_1481/.editorconfig`
- Create: `leetcode_1481/.gitattributes`
- Create: `leetcode_1481/.gitignore`
- Create: `leetcode_1481/.vscode/tasks.json`
- Create: `leetcode_1481/.vscode/launch.json`
- Create: `leetcode_1481/AGENTS.md`
- Create: `leetcode_1481/README.md`
- Create: `leetcode_1481/docs/readme-template.md`
- Modify: `leetcode_1481/leetcode_1481/Program.cs`

**Interfaces:**
- Consumes: Task 2 已通過的 API、九案例與 fresh run。
- Produces: problem-root VS Code workspace、繁中協作契約與可精確重播的 README。

- [ ] **Step 1: 加入共用設定與 README 範本**

以 `leetcode_1464` 已合併版本作為逐檔來源，完整複製以下四個檔案，不改內容：

```text
leetcode_1464/.editorconfig -> leetcode_1481/.editorconfig
leetcode_1464/.gitattributes -> leetcode_1481/.gitattributes
leetcode_1464/.gitignore -> leetcode_1481/.gitignore
leetcode_1464/docs/readme-template.md -> leetcode_1481/docs/readme-template.md
```

- [ ] **Step 2: 加入精確的 VS Code 設定**

`tasks.json` 的 task label 為 `build leetcode_1481`，args 固定為
`build` 與 `${workspaceFolder}/leetcode_1481/leetcode_1481.csproj`。

`launch.json` 使用 `coreclr`、`preLaunchTask: build leetcode_1481`，program 固定為
`${workspaceFolder}/leetcode_1481/bin/Debug/net10.0/leetcode_1481.dll`，cwd 固定為
`${workspaceFolder}/leetcode_1481`，console 為 `integratedTerminal`。

- [ ] **Step 3: 補全 `Main` 雙語 XML 題述**

XML summary 必須包含：

```text
1481. Least Number of Unique Integers after K Removals
1481. 移除 K 個元素後相異整數的最少數目
https://leetcode.com/problems/least-number-of-unique-integers-after-k-removals/
https://leetcode.cn/problems/least-number-of-unique-integers-after-k-removals/
Given an integer array arr and an integer k, return the least number of unique integers
after removing exactly k elements.
給定整數陣列 arr 與整數 k，恰好移除 k 個元素後，回傳可能留下的最少相異整數數量。
```

`args` 文件固定為「主控台啟動參數；本驗證器不使用。」；不得改變 harness 輸出。

- [ ] **Step 4: 撰寫題目專屬 `AGENTS.md`**

內容必須明確記錄 nested project 路徑、從題目根目錄執行的 build/run 命令、沒有正式測試
project、公開 API、頻率排序不變量、輸入不變性、九案例成功摘要、parent Git metadata 與
`leetcode_1481/` scope。

- [ ] **Step 5: 由 fresh run 撰寫 `README.md`**

README 依序包含：雙語標題與官方連結、題意、三項官方限制、核心不變量與舊實作陷阱、採用
解法、`O(n + u log u)` 時間與 `O(u)` 輔助空間、`[4,3,1,1,3,3,2], k=3` 逐步走查、九案例表、
repository-root 與 problem-root 命令、fresh run 完整輸出、最終結構。

完整執行輸出必須取自：

```bash
dotnet run --no-build --project leetcode_1481/leetcode_1481/leetcode_1481.csproj \
  > /private/tmp/leetcode_1481.actual.txt
```

只有該完整輸出使用 `text` fence；演算法走查使用 `plaintext` fence。

- [ ] **Step 6: 驗證 JSON、build、run 與 transcript**

Run:

```bash
jq empty leetcode_1481/.vscode/launch.json leetcode_1481/.vscode/tasks.json
dotnet build leetcode_1481/leetcode_1481/leetcode_1481.csproj --nologo
dotnet run --no-build --project leetcode_1481/leetcode_1481/leetcode_1481.csproj \
  > /private/tmp/leetcode_1481.actual.txt
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' \
  leetcode_1481/README.md > /private/tmp/leetcode_1481.readme.txt
diff -u /private/tmp/leetcode_1481.readme.txt /private/tmp/leetcode_1481.actual.txt
test "$(rg -c '^```text$' leetcode_1481/README.md)" -eq 1
```

Expected: 全部 exit code 0；build 0 warnings／0 errors；transcript diff 無輸出。

---

### Task 4: 完整本機 gate、唯讀 review 與單一 commit

**Files:**
- Review: all changed files under `leetcode_1481/`
- Amend: existing `feat(leetcode-1481): migrate project to .NET 10` commit

**Interfaces:**
- Consumes: Tasks 1–3 的完整 migration diff。
- Produces: 一個相對 `origin/main` 的已驗證 commit，無未解決 Critical／Important review finding。

- [ ] **Step 1: 執行完整 gate**

```bash
jq empty leetcode_1481/.vscode/launch.json leetcode_1481/.vscode/tasks.json
dotnet build leetcode_1481/leetcode_1481/leetcode_1481.csproj --nologo
dotnet run --no-build --project leetcode_1481/leetcode_1481/leetcode_1481.csproj
git diff --check -- leetcode_1481
test ! -e leetcode_1481/leetcode_1481.sln
test ! -e leetcode_1481/leetcode_1481/App.config
test ! -e leetcode_1481/leetcode_1481/Properties/AssemblyInfo.cs
test -z "$(git status --short | awk '$2 !~ /^leetcode_1481\// { print }')"
```

Expected: 全部 exit code 0，九案例 PASS，summary `9/9`，scope 外沒有變更。

- [ ] **Step 2: 進行獨立唯讀 review**

Reviewer 只讀 `origin/main...HEAD` 加上尚未提交檔案，逐項檢查公開 API、貪心正確性、題目限制、
輸入不變性、console boundary、XML、README、VS Code 路徑、legacy absence、scope 與九案例鑑別力。
Reviewer 不得修改、commit、push、建立 PR 或更新 Issue。Critical／Important finding 必須先修正，
然後重跑 Task 3 Step 6 與 Task 4 Step 1，再重新 review。

- [ ] **Step 3: 將所有本題變更 amend 至既有單一 commit**

```bash
git add leetcode_1481
git diff --cached --check
git commit --amend --no-edit
test "$(git rev-list --count origin/main..HEAD)" -eq 1
test -z "$(git status --short)"
```

Expected: 全部 exit code 0；相對 `origin/main` 只有一個 commit，subject 為
`feat(leetcode-1481): migrate project to .NET 10`，工作區乾淨。

- [ ] **Step 4: 對最終 commit 重跑完整 gate**

重新執行 Task 3 Step 6 與 Task 4 Step 1，並記錄最終 head SHA。只有此 SHA 可進入 PR。

---

### Task 5: Draft PR、Ready、Squash Merge、Issue #2 與合併後驗證

**Files:**
- No repository file changes after the verified commit.
- External state: GitHub branch、PR、Issue #2。

**Interfaces:**
- Consumes: Task 4 的 verified head SHA。
- Produces: merged PR、Issue #2 中唯一已勾選的 `leetcode_1481`、同步且重新驗證的 `main`。

- [ ] **Step 1: 推送已驗證分支並建立 Draft PR**

```bash
git push -u origin codex/leetcode-1481-net10
gh pr create --draft \
  --repo HyperLee/Leetcode_folder \
  --base main \
  --head codex/leetcode-1481-net10 \
  --title "feat(leetcode-1481): migrate project to .NET 10" \
  --body-file /private/tmp/leetcode_1481-pr-body.md
```

PR body 必須包含 migration 內容、頻率貪心不變量、`O(n + u log u)`／`O(u)`、實際 gate、
review 結果與 `Refs #2`；不得包含 `Closes #2`。

- [ ] **Step 2: 驗證 PR 狀態後轉 Ready**

使用 `gh pr view`／`gh pr checks` 確認 PR head 等於 verified SHA、changed files 全在
`leetcode_1481/`、commit count 為 1、merge state clean，且沒有 failed、cancelled 或 pending
checks。全部成立後執行 `gh pr ready`。

- [ ] **Step 3: 以 expected head SHA squash merge**

在 merge 前重新讀取 PR head；若與 verified SHA 不同立即停止。相同時執行 squash merge，並由
GitHub 回傳 `merged: true` 與 merge SHA。

- [ ] **Step 4: 只更新 Issue #2 的目標行並讀回**

重新讀取最新 Issue body，確認完整未勾選行 `- [ ] \`leetcode_1481\`` 恰好一次，只將該行改為
`- [x] \`leetcode_1481\``。更新後再次讀取並確認：已勾選行唯一存在、未勾選版本消失、下一題
`leetcode_1493` 仍為 `[ ]`。

- [ ] **Step 5: 同步 `main` 並執行合併後 gate**

```bash
git -C /Users/qiuzili/Leetcode/Leetcode_folder pull --ff-only origin main
git -C /Users/qiuzili/Leetcode/Leetcode_folder rev-parse HEAD
git -C /Users/qiuzili/Leetcode/Leetcode_folder rev-parse origin/main
```

兩個 SHA 都必須等於 GitHub merge SHA。從同步後 `main` 重跑 Task 3 Step 6 與 Task 4 Step 1，
最後確認 `git status --short` 為空；至此才可宣告完整發布完成。
