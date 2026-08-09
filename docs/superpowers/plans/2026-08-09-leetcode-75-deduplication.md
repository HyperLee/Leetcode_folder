# LeetCode 75 Deduplication Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Consolidate duplicate LeetCode 75 projects into `leetcode_075` with one canonical Dutch-national-flag solution and one reliable console acceptance harness.

**Architecture:** `leetcode_075/leetcode_075/Program.cs` remains a single-file console teaching project. `Main` owns deterministic fixtures and delegates each cloned input to the sole public `SortColors(int[] nums)` API; `SortColors` maintains `low`, `mid`, and `high` partitions in place. Documentation and the root index change together, then every Git-tracked file under `leetcode_75` is removed individually.

**Tech Stack:** C# 14, .NET 10 console application, Markdown, Git read-only verification commands.

## Global Constraints

- Work locally only; do not commit, push, or create a pull request.
- Preserve the `leetcode_075` project name and .NET 10 configuration.
- Keep exactly one public solution API: `public static void SortColors(int[] nums)`.
- Do not use recursive or bulk deletion commands.
- Do not modify any other LeetCode problem.
- Do not recursively remove ignored `bin/` or `obj/` artifacts.

---

### Task 1: Replace the comparison program with the canonical solution

**Files:**
- Modify: `leetcode_075/leetcode_075/Program.cs`

**Interfaces:**
- Consumes: legal LeetCode inputs where `1 <= nums.Length <= 300` and every value is `0`, `1`, or `2`.
- Produces: `public static void SortColors(int[] nums)`, which sorts the supplied array in place and returns `void`.

- [ ] **Step 1: Record the current executable baseline**

Run `dotnet run --project leetcode_075/leetcode_075.csproj --no-build` and expect `Overall: 30/30 passed.` This characterizes behavior before removing comparison implementations.

- [ ] **Step 2: Replace the harness with eight cases for the sole API**

Use these exact fixtures:

```csharp
(string Name, int[] Input, int[] Expected)[] testCases =
{
    ("官方範例 1", new[] { 2, 0, 2, 1, 1, 0 }, new[] { 0, 0, 1, 1, 2, 2 }),
    ("官方範例 2", new[] { 2, 0, 1 }, new[] { 0, 1, 2 }),
    ("單一元素", new[] { 1 }, new[] { 1 }),
    ("已排序", new[] { 0, 0, 1, 1, 2, 2 }, new[] { 0, 0, 1, 1, 2, 2 }),
    ("反向排列", new[] { 2, 2, 1, 1, 0, 0 }, new[] { 0, 0, 1, 1, 2, 2 }),
    ("全部相同", new[] { 2, 2, 2 }, new[] { 2, 2, 2 }),
    ("只含兩色", new[] { 2, 0, 2, 0 }, new[] { 0, 0, 2, 2 }),
    ("右側換回未分類值", new[] { 2, 2, 0, 1, 0 }, new[] { 0, 0, 1, 2, 2 })
};
```

For every fixture, clone `Input`, invoke `SortColors(actual)`, print `Input`, `Expected`, `Actual`, and `PASS` or `FAIL`, then print `Overall: {passed}/{testCases.Length} passed.` Set `Environment.ExitCode = 1` when the totals differ.

- [ ] **Step 3: Make `SortColors` the canonical implementation**

Use this exact partition loop and remove every other solution method:

```csharp
public static void SortColors(int[] nums)
{
    int low = 0;
    int mid = 0;
    int high = nums.Length - 1;

    while (mid <= high)
    {
        switch (nums[mid])
        {
            case 0:
                (nums[low], nums[mid]) = (nums[mid], nums[low]);
                low++;
                mid++;
                break;
            case 1:
                mid++;
                break;
            case 2:
                (nums[mid], nums[high]) = (nums[high], nums[mid]);
                high--;
                break;
        }
    }
}
```

Keep focused Traditional Chinese XML documentation describing mutation, legal inputs, the four partitions, and `O(n)` time / `O(1)` extra space. Keep one comment explaining why `mid` does not advance after swapping a `2`.

- [ ] **Step 4: Restore, build, and run**

Run:

```bash
dotnet restore leetcode_075/leetcode_075.csproj
dotnet build leetcode_075/leetcode_075.csproj --no-restore --nologo
dotnet run --project leetcode_075/leetcode_075.csproj --no-build
```

Expected: zero warnings, zero errors, eight `PASS` results, `Overall: 8/8 passed.`, and exit 0.

### Task 2: Rewrite the retained project documentation

**Files:**
- Modify: `leetcode_075/README.md`

**Interfaces:**
- Consumes: final `SortColors(int[] nums)` behavior and fresh eight-case transcript.
- Produces: a Traditional Chinese teaching README documenting only the retained solution.

- [ ] **Step 1: Replace the five-solution guide**

Use these sections in order: `題目說明`, `解題核心：荷蘭國旗三指標`, `區間不變量`, `演算法流程`, `正確性說明`, `複雜度`, `專案結構`, `建置與執行`, and `測試案例與實際輸出`. Document `[0, low)`, `[low, mid)`, `[mid, high]`, and `(high, n)`; explain all three switch branches; state mutation, `O(n)` time, and `O(1)` extra space. Do not mention removed methods.

- [ ] **Step 2: Insert the fresh transcript**

Copy the complete output from Task 1 into the README text fence. It must contain the same eight case names and end with `Overall: 8/8 passed.`

- [ ] **Step 3: Verify transcript fidelity**

Run the program again and compare its complete output with the README text fence. Expected: exact match including punctuation and array spacing.

### Task 3: Remove the duplicate tracked project and index row

**Files:**
- Modify: `README.md`
- Delete individually: every path returned by `git ls-files leetcode_75`

**Interfaces:**
- Consumes: verified retained project from Tasks 1 and 2.
- Produces: a working-tree patch that deletes the duplicate tracked project, leaving one root-index row for normalized problem number 75.

- [ ] **Step 1: Remove only the duplicate index row**

Delete the root README row whose folder link is `[leetcode_75](leetcode_75/)`; preserve the adjacent `leetcode_075` row unchanged.

- [ ] **Step 2: Review and delete the exact tracked paths**

Run `git ls-files leetcode_75`, expect eleven explicit paths, and delete each with patch operations. Do not use `rm -r`, `rm -rf`, `git rm -r`, `find -delete`, or an equivalent bulk deletion.

- [ ] **Step 3: Confirm the tracked files are marked deleted**

Run `git ls-files --deleted leetcode_75` and expect the same eleven paths, each absent from the working tree and pending deletion in the unstaged patch. Inspect `git status --short -- README.md leetcode_075 leetcode_75 docs/superpowers`; ignored `bin/` and `obj/` may remain and must be reported rather than recursively removed.

### Task 4: Run the complete local acceptance gate

**Files:**
- Verify only: `README.md`, `leetcode_075/README.md`, `leetcode_075/leetcode_075/Program.cs`, `leetcode_75/**`, `docs/superpowers/**`

**Interfaces:**
- Consumes: all prior tasks.
- Produces: evidence that the retained project is correct and the tracked duplicate is gone.

- [ ] **Step 1: Run a fresh restore/build/run gate**

Repeat Task 1 Step 4. Expected: zero warnings, zero errors, `Overall: 8/8 passed.`, exit 0.

- [ ] **Step 2: Verify uniqueness and scope**

Normalize the folder suffixes from existing working-tree `leetcode_*/*csproj` paths as decimal problem numbers and count collisions. Expected: no duplicate for 75 and no other collision. Confirm root README contains exactly one row beginning with `| 75 |`, pointing to `leetcode_075`.

- [ ] **Step 3: Verify formatting and inspect the patch**

Run `git diff --check`, `git diff --stat`, and `git status --short`. Expected: whitespace check exits 0, no unrelated project changes appear, and no commit or push occurs.
