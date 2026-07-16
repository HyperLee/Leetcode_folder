# leetcode_1436 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate LeetCode 1436 to a documented, deterministic SDK-style .NET 10 console project and complete the single-commit PR, squash-merge, Issue #2, and post-merge delivery flow.

**Architecture:** Keep `DestCity(IList<IList<string>>)` as a console-free two-pass HashSet solution. `Main` owns an eight-case acceptance harness, while problem-root configuration and Traditional Chinese documentation describe and reproduce the verified executable behavior.

**Tech Stack:** C# on .NET 10, SDK-style MSBuild, VS Code JSON configuration, Git/GitHub CLI, Markdown documentation.

## Global Constraints

- All tracked changes stay under `leetcode_1436/`.
- Use `/private/tmp/codex-leetcode-1436-net10` on branch `codex/leetcode-1436-net10`, based on current `origin/main`.
- Treat `leetcode_1436/` as the VS Code workspace and documentation command root.
- Preserve the exact public API `public static string DestCity(IList<IList<string>> paths)`.
- Require every harness case to verify both the expected destination and deep structural equality of `paths` before and after the API call.
- Add no third-party dependency, formal test project, invalid-input exception, or console output outside `Main`.
- Subagent tasks may create temporary local commits for review. Before publication, consolidate all unpublished task commits into exactly one feature commit relative to `origin/main`; never force-push published history without explicit approval.
- Delete only the three exact legacy files; bulk deletion commands are forbidden.
- Only update Issue #2 after GitHub confirms the PR is merged.

---

## File Map

- `leetcode_1436/leetcode_1436/Program.cs`: bilingual problem contract, deterministic harness, and two-pass HashSet production API.
- `leetcode_1436/leetcode_1436/leetcode_1436.csproj`: SDK-style .NET 10 executable definition.
- `leetcode_1436/.vscode/tasks.json`: problem-root nested project build task.
- `leetcode_1436/.vscode/launch.json`: no-prompt .NET 10 debug launch.
- `leetcode_1436/.editorconfig`, `.gitattributes`, `.gitignore`: validated shared editor and Git defaults copied byte-for-byte from `leetcode_912`.
- `leetcode_1436/AGENTS.md`: problem-specific commands, API invariants, verification, and Git scope.
- `leetcode_1436/README.md`: Traditional Chinese tutorial and exact fresh-run transcript.
- `leetcode_1436/docs/readme-template.md`: validated shared README creation template copied byte-for-byte from `leetcode_912`.
- `leetcode_1436/docs/superpowers/specs/2026-07-17-leetcode-1436-net10-migration-design.md`: approved design.
- `leetcode_1436/docs/superpowers/plans/2026-07-17-leetcode-1436-net10-migration.md`: this execution plan.

---

### Task 1: Produce a behavior-specific RED on the migrated project shell

**Files:**
- Modify: `leetcode_1436/leetcode_1436/leetcode_1436.csproj`
- Modify: `leetcode_1436/leetcode_1436/Program.cs`
- Delete: `leetcode_1436/leetcode_1436.sln`
- Delete: `leetcode_1436/leetcode_1436/App.config`
- Delete: `leetcode_1436/leetcode_1436/Properties/AssemblyInfo.cs`

**Interfaces:**
- Consumes: `IList<IList<string>>` inputs that satisfy LeetCode 1436 constraints.
- Produces: an acceptance harness that calls the not-yet-defined `DestCity` API, so the compiler reports `CS0103` for the correct missing behavior.

- [ ] **Step 1: Replace the project file with the exact SDK-style contract**

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

- [ ] **Step 2: Replace `Program.cs` with the harness-only RED state**

```csharp
namespace leetcode_1436;

internal class Program
{
    /// <summary>
    /// 1436. Destination City
    /// 1436. 旅行終點站
    /// https://leetcode.com/problems/destination-city/
    /// https://leetcode.cn/problems/destination-city/
    /// Given directed travel paths that form one acyclic line, return the unique city with no outgoing path.
    /// 給定形成一條無環路線的有向旅行路徑，回傳唯一沒有外出路徑的終點城市。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        IList<IList<string>> maximumPaths = [];
        for (int index = 0; index < 100; index++)
        {
            maximumPaths.Add([CreateCityName(index), CreateCityName(index + 1)]);
        }

        TestCase[] testCases =
        [
            new("Official example 1", "[[London -> New York], [New York -> Lima], [Lima -> Sao Paulo]]", [["London", "New York"], ["New York", "Lima"], ["Lima", "Sao Paulo"]], "Sao Paulo"),
            new("Official example 2", "[[B -> C], [D -> B], [C -> A]]", [["B", "C"], ["D", "B"], ["C", "A"]], "A"),
            new("Official example 3 / minimum input", "[[A -> Z]]", [["A", "Z"]], "Z"),
            new("Shuffled path order", "[[Gamma -> Delta], [Alpha -> Beta], [Beta -> Gamma]]", [["Gamma", "Delta"], ["Alpha", "Beta"], ["Beta", "Gamma"]], "Delta"),
            new("Early destination candidate becomes a source", "[[A -> B], [C -> D], [B -> C]]", [["A", "B"], ["C", "D"], ["B", "C"]], "D"),
            new("City names containing spaces", "[[New York -> Rio City], [Rio City -> Cape Town]]", [["New York", "Rio City"], ["Rio City", "Cape Town"]], "Cape Town"),
            new("Case-sensitive city names", "[[A -> a]]", [["A", "a"]], "a"),
            new("Maximum 100-path chain", "[100-path chain CityA -> ... -> CityCW]", maximumPaths, "CityCW")
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            IList<IList<string>> originalPaths = ClonePaths(testCase.Paths);
            string actual = DestCity(testCase.Paths);
            bool isPassed = actual == testCase.Expected && PathsEqual(testCase.Paths, originalPaths);
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {testCase.Input}");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string CreateCityName(int index)
    {
        string suffix = string.Empty;
        do
        {
            suffix = (char)('A' + index % 26) + suffix;
            index = index / 26 - 1;
        }
        while (index >= 0);

        return $"City{suffix}";
    }

    private static IList<IList<string>> ClonePaths(IList<IList<string>> paths)
    {
        return paths.Select(path => (IList<string>)[.. path]).ToList();
    }

    private static bool PathsEqual(IList<IList<string>> left, IList<IList<string>> right)
    {
        return left.Count == right.Count
            && left.Zip(right).All(pair => pair.First.SequenceEqual(pair.Second));
    }

    private sealed record TestCase(
        string Name,
        string Input,
        IList<IList<string>> Paths,
        string Expected);
}
```

- [ ] **Step 3: Delete each exact legacy file with individual `apply_patch` delete operations**

```text
leetcode_1436/leetcode_1436.sln
leetcode_1436/leetcode_1436/App.config
leetcode_1436/leetcode_1436/Properties/AssemblyInfo.cs
```

- [ ] **Step 4: Run the migrated shell and verify the RED is behavior-specific**

Run:

```bash
dotnet build leetcode_1436/leetcode_1436/leetcode_1436.csproj --nologo
```

Expected: exit code 1, 0 warnings, and `CS0103` reporting that `DestCity` does not exist. Any project-format, syntax, nullable, or duplicate-attribute error must be fixed before continuing.

---

### Task 2: Implement the minimal two-pass HashSet solution and reach GREEN

**Files:**
- Modify: `leetcode_1436/leetcode_1436/Program.cs`

**Interfaces:**
- Consumes: `IList<IList<string>> paths`, with 1–100 two-city paths forming one loop-free line.
- Produces: `public static string DestCity(IList<IList<string>> paths)`, returning the unique city with no outgoing route.

- [ ] **Step 1: Add the production API immediately before `TestCase`**

```csharp
    /// <summary>
    /// 在有效路徑形成單一無環旅行線的前提下，先收集所有出發城市，再找出唯一未出現在
    /// 出發城市集合中的抵達城市，並將該城市作為旅行終點回傳。
    /// </summary>
    /// <param name="paths">每個元素皆為「出發城市、抵達城市」的有效二元素路徑。</param>
    /// <returns>唯一沒有任何外出路徑的終點城市。</returns>
    public static string DestCity(IList<IList<string>> paths)
    {
        HashSet<string> departureCities = [];
        foreach (IList<string> path in paths)
        {
            departureCities.Add(path[0]);
        }

        foreach (IList<string> path in paths)
        {
            // 終點是唯一不曾作為任何路徑起點的抵達城市。
            if (!departureCities.Contains(path[1]))
            {
                return path[1];
            }
        }

        return string.Empty;
    }
```

- [ ] **Step 2: Run GREEN build**

Run:

```bash
dotnet build leetcode_1436/leetcode_1436/leetcode_1436.csproj --nologo
```

Expected: exit code 0, `0 Warning(s)`, `0 Error(s)` (localized output may show `0 個警告`, `0 個錯誤`).

- [ ] **Step 3: Run the acceptance harness**

Run:

```bash
dotnet run --no-build --project leetcode_1436/leetcode_1436/leetcode_1436.csproj
```

Expected: eight `Result: PASS` lines, final `Summary: 8/8 checks passed.`, exit code 0. Each PASS requires both the expected destination and unchanged deep path structure.

- [ ] **Step 4: Mutation-test the no-input-mutation guard, then restore GREEN**

Temporarily add a production-side reversal of the mutable outer path list at the start of `DestCity`. This mutation preserves the correct destination for valid inputs but changes multi-edge input structure. Run the harness and require a nonzero exit, unchanged expected/actual destination values on affected cases, at least one `Result: FAIL`, and a non-green summary. Remove the temporary reversal with `apply_patch`, rebuild, and require `Summary: 8/8 checks passed.` with exit code 0. Never stage or commit the mutation.

---

### Task 3: Add problem-root tooling and exact documentation

**Files:**
- Create: `leetcode_1436/.editorconfig`
- Create: `leetcode_1436/.gitattributes`
- Create: `leetcode_1436/.gitignore`
- Create: `leetcode_1436/.vscode/tasks.json`
- Create: `leetcode_1436/.vscode/launch.json`
- Create: `leetcode_1436/AGENTS.md`
- Create: `leetcode_1436/README.md`
- Create: `leetcode_1436/docs/readme-template.md`

**Interfaces:**
- Consumes: the GREEN executable and its fresh console transcript.
- Produces: reproducible problem-root build/debug commands and documentation that exactly matches the implementation.

- [ ] **Step 1: Reuse the validated common configuration files exactly**

Run from repository root:

```bash
cp leetcode_912/.editorconfig leetcode_1436/.editorconfig
cp leetcode_912/.gitattributes leetcode_1436/.gitattributes
cp leetcode_912/.gitignore leetcode_1436/.gitignore
cp leetcode_912/docs/readme-template.md leetcode_1436/docs/readme-template.md
```

- [ ] **Step 2: Add exact VS Code configuration**

`leetcode_1436/.vscode/tasks.json`:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_1436",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/leetcode_1436/leetcode_1436.csproj"
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

`leetcode_1436/.vscode/launch.json`:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Debug leetcode_1436",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_1436",
      "program": "${workspaceFolder}/leetcode_1436/bin/Debug/net10.0/leetcode_1436.dll",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_1436",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
```

- [ ] **Step 3: Write `AGENTS.md` with the exact local contract**

Include these executable commands and facts verbatim:

````markdown
# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The nested executable is
`leetcode_1436/leetcode_1436.csproj`. From this folder, run:

```bash
dotnet build leetcode_1436/leetcode_1436.csproj --nologo
dotnet run --no-build --project leetcode_1436/leetcode_1436.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1436` VS Code configuration.
There is no root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow, explicit
types, PascalCase public members, and camelCase locals. Preserve the bilingual XML
problem summary and Traditional Chinese `DestCity` summary.

Keep `public static string DestCity(IList<IList<string>> paths)` console-free. It
must collect every departure city, then return the unique destination absent from
that set without modifying the outer path list or any nested city pair. Do not add
behavior outside LeetCode's valid-input contract. `Main` alone owns acceptance
output.

## Testing & Git Scope

The executable harness has eight checks. Each case deep-clones its input before the
API call and passes only when both the answer and the complete nested path structure
match expectations. Success ends with `Summary: 8/8 checks passed.` and exit code 0.
Git metadata is at the parent repository root; commits and pull requests must remain
scoped to `leetcode_1436/`.
````

- [ ] **Step 4: Write the Traditional Chinese README**

Use this complete content. The `text` fence is deliberately unique and must be replaced only if the fresh executable output differs:

~~~~markdown
# 1436. Destination City／旅行終點站

使用兩趟 HashSet 線性掃描，找出旅行路線中唯一沒有外出路徑的城市。

- [English problem](https://leetcode.com/problems/destination-city/)
- [中文題目](https://leetcode.cn/problems/destination-city/)

## 題目說明

每個 `paths[i] = [cityA, cityB]` 代表一條從 `cityA` 前往 `cityB` 的單向路徑。所有路徑形成一條沒有迴圈的旅行線，因此恰好有一座城市沒有任何外出路徑；回傳這座終點城市。

## 限制條件

- `1 <= paths.Length <= 100`
- `paths[i].Count == 2`
- `1 <= cityA.Length, cityB.Length <= 10`
- `cityA != cityB`
- 城市名稱只包含大小寫英文字母與空格。

## 解法與核心不變量

先將每條路徑的出發城市加入 `HashSet<string>`，再逐一檢查抵達城市。中途城市仍有下一段路徑，因此必定出現在出發城市集合；唯一不在集合中的抵達城市就是終點。

保留兩趟掃描可讓不變量直接對應程式碼，也不依賴 `paths` 的排列順序。公開方法只回傳答案，不修改輸入或輸出到主控台。Acceptance harness 會在呼叫前深層複製每個案例的路徑，並把答案正確與完整巢狀結構未變動共同列為該案例 PASS 的必要條件。

### 複雜度

- 時間複雜度：`O(n)`
- 結果空間：`O(1)`
- 輔助空間：`O(n)`，用於出發城市集合

## 逐步範例

官方範例一：

```plaintext
London -> New York -> Lima -> Sao Paulo
```

出發城市集合為 `{ London, New York, Lima }`。三個抵達城市中，只有 `Sao Paulo` 不在集合內，所以它是旅行終點。

## Acceptance harness

| 案例 | 驗證重點 | 預期結果 |
| --- | --- | --- |
| Official example 1 | 一般連續路線 | `Sao Paulo` |
| Official example 2 | 輸入順序不是旅行順序 | `A` |
| Official example 3 / minimum input | 單一路徑 | `Z` |
| Shuffled path order | 額外驗證順序獨立性 | `Delta` |
| Early destination candidate becomes a source | 防止過早回傳 | `D` |
| City names containing spaces | 合法空格字元 | `Cape Town` |
| Case-sensitive city names | 大小寫不同城市 | `a` |
| Maximum 100-path chain | 100 條合法路徑、101 個僅含英文字母的城市，以及輸入不變 | `CityCW` |

本專案沒有正式測試專案；`Main` 的確定性 acceptance harness 是可執行驗證機制。八個案例各自同時驗證回傳值與 `paths` 深層結構未被修改；任一案例失敗時，程式會將 exit code 設為 1。

## 建置與執行

從此題目根目錄 `leetcode_1436/` 執行：

```bash
dotnet build leetcode_1436/leetcode_1436.csproj --nologo
dotnet run --no-build --project leetcode_1436/leetcode_1436.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [[London -> New York], [New York -> Lima], [Lima -> Sao Paulo]]
Expected: Sao Paulo
Actual: Sao Paulo
Result: PASS

Case: Official example 2
Input: [[B -> C], [D -> B], [C -> A]]
Expected: A
Actual: A
Result: PASS

Case: Official example 3 / minimum input
Input: [[A -> Z]]
Expected: Z
Actual: Z
Result: PASS

Case: Shuffled path order
Input: [[Gamma -> Delta], [Alpha -> Beta], [Beta -> Gamma]]
Expected: Delta
Actual: Delta
Result: PASS

Case: Early destination candidate becomes a source
Input: [[A -> B], [C -> D], [B -> C]]
Expected: D
Actual: D
Result: PASS

Case: City names containing spaces
Input: [[New York -> Rio City], [Rio City -> Cape Town]]
Expected: Cape Town
Actual: Cape Town
Result: PASS

Case: Case-sensitive city names
Input: [[A -> a]]
Expected: a
Actual: a
Result: PASS

Case: Maximum 100-path chain
Input: [100-path chain CityA -> ... -> CityCW]
Expected: CityCW
Actual: CityCW
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1436/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       │   └── 2026-07-17-leetcode-1436-net10-migration.md
│       └── specs/
│           └── 2026-07-17-leetcode-1436-net10-migration-design.md
└── leetcode_1436/
    ├── Program.cs
    └── leetcode_1436.csproj
```
~~~~

- [ ] **Step 5: Run the local documentation and configuration gate**

Run:

```bash
jq empty leetcode_1436/.vscode/launch.json leetcode_1436/.vscode/tasks.json
dotnet build leetcode_1436/leetcode_1436/leetcode_1436.csproj --nologo
dotnet run --no-build --project leetcode_1436/leetcode_1436/leetcode_1436.csproj > /private/tmp/leetcode_1436.actual.txt
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_1436/README.md > /private/tmp/leetcode_1436.readme.txt
diff -u /private/tmp/leetcode_1436.readme.txt /private/tmp/leetcode_1436.actual.txt
test "$(rg -c '^```text$' leetcode_1436/README.md)" -eq 1
git diff --check -- leetcode_1436
```

Expected: every command exits 0; build has 0 warnings and 0 errors; transcript diff is empty.

---

### Task 4: Complete scope checks, amend the single commit, and review

**Files:**
- Review: every changed or deleted path under `leetcode_1436/`

**Interfaces:**
- Consumes: the complete local migration.
- Produces: one verified commit and an independent read-only review with no unresolved Critical/Important finding.

- [ ] **Step 1: Run exact scope and legacy-absence checks**

```bash
test -z "$(git status --short | awk '$2 !~ /^leetcode_1436\// { print }')"
test ! -e leetcode_1436/leetcode_1436.sln
test ! -e leetcode_1436/leetcode_1436/App.config
test ! -e leetcode_1436/leetcode_1436/Properties/AssemblyInfo.cs
rg -n '<Project Sdk="Microsoft.NET.Sdk">|<TargetFramework>net10.0</TargetFramework>|<ImplicitUsings>enable</ImplicitUsings>|<Nullable>enable</Nullable>' leetcode_1436/leetcode_1436/leetcode_1436.csproj
```

Expected: all tests exit 0 and the final search prints all four required project contract lines.

- [ ] **Step 2: Stage only the target and consolidate temporary commits into one unpublished feature commit**

```bash
git add -A leetcode_1436
git diff --cached --check
git diff --cached --name-status
BASE_SHA="$(git merge-base origin/main HEAD)"
git reset --soft "$BASE_SHA"
git commit -m "feat(leetcode-1436): migrate project to .NET 10"
git rev-list --count origin/main..HEAD
```

Expected: staged paths only under `leetcode_1436/`; all temporary local task commits are replaced by exactly one unpublished feature commit.

- [ ] **Step 3: Run the complete gate against the amended HEAD**

Run:

```bash
jq empty leetcode_1436/.vscode/launch.json leetcode_1436/.vscode/tasks.json
dotnet build leetcode_1436/leetcode_1436/leetcode_1436.csproj --nologo
dotnet run --no-build --project leetcode_1436/leetcode_1436/leetcode_1436.csproj > /private/tmp/leetcode_1436.actual.txt
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_1436/README.md > /private/tmp/leetcode_1436.readme.txt
diff -u /private/tmp/leetcode_1436.readme.txt /private/tmp/leetcode_1436.actual.txt
test "$(rg -c '^```text$' leetcode_1436/README.md)" -eq 1
git diff --check -- leetcode_1436
test -z "$(git status --short | awk '$2 !~ /^leetcode_1436\// { print }')"
test ! -e leetcode_1436/leetcode_1436.sln
test ! -e leetcode_1436/leetcode_1436/App.config
test ! -e leetcode_1436/leetcode_1436/Properties/AssemblyInfo.cs
test -z "$(git status --short)"
```

Expected: every command exits 0; build has 0 warnings and 0 errors; transcript diff is empty; the worktree is clean.

- [ ] **Step 4: Perform independent read-only review**

Review `origin/main...HEAD` for the exact spec contract, API correctness, test strength, XML summaries, high-signal comments, README transcript, VS Code paths, deleted legacy files, and changed-path scope. If any Critical or Important issue is found, fix it, repeat RED/GREEN as relevant, amend the same unpublished commit, rerun the full gate, and review again.

---

### Task 5: Publish, merge, update Issue #2, and verify merged main

**Files:**
- External state: branch `codex/leetcode-1436-net10`, GitHub PR, and exactly one Issue #2 checkbox.

**Interfaces:**
- Consumes: one reviewed, verified local commit.
- Produces: a squash-merged PR, checked `leetcode_1436` Issue entry, synced `main`, and fresh post-merge evidence.

- [ ] **Step 1: Push and open a draft PR**

Create `/private/tmp/leetcode_1436-pr-body.md` with this exact content after the full gate and review pass:

```markdown
## Summary

- migrate `leetcode_1436` from .NET Framework 4.8 to SDK-style .NET 10
- preserve the console-free two-pass HashSet solution and add an eight-case deterministic harness
- add problem-root VS Code configuration, Traditional Chinese documentation, and repository guidance

## Algorithm

The first pass records every departure city. The second pass returns the unique destination absent from that set. Time is `O(n)`, result space is `O(1)`, and auxiliary space is `O(n)`.

## Verification

- VS Code JSON parsed successfully
- build: 0 warnings, 0 errors
- acceptance harness: 8/8 checks passed
- README transcript: exact fresh-run match with one `text` fence
- whitespace, changed-path scope, and legacy-file absence checks passed
- independent read-only review: no unresolved Critical or Important findings

Refs #2
```

Then run:

```bash
git push -u origin codex/leetcode-1436-net10
gh pr create --repo HyperLee/Leetcode_folder --draft \
  --title "feat(leetcode-1436): migrate project to .NET 10" \
  --body-file /private/tmp/leetcode_1436-pr-body.md
```

- [ ] **Step 2: Verify PR identity and readiness gates**

```bash
gh pr view --repo HyperLee/Leetcode_folder --json number,url,isDraft,headRefOid,mergeable,mergeStateStatus,commits,files,statusCheckRollup
```

Expected: head SHA equals verified local `HEAD`, one commit, only `leetcode_1436/` files, mergeable clean state, and no failed/cancelled/pending check. Then mark Ready with `gh pr ready` and read the PR back.

- [ ] **Step 3: Squash merge using the verified head SHA**

```bash
VERIFIED_HEAD_SHA="$(git rev-parse HEAD)"
gh pr merge --repo HyperLee/Leetcode_folder --squash --match-head-commit "$VERIFIED_HEAD_SHA"
gh pr view --repo HyperLee/Leetcode_folder --json mergedAt,mergeCommit,url
gh pr view --repo HyperLee/Leetcode_folder --json mergeCommit --jq .mergeCommit.oid > /private/tmp/leetcode_1436-merge-sha.txt
test -s /private/tmp/leetcode_1436-merge-sha.txt
```

Do not continue unless `VERIFIED_HEAD_SHA` equals the PR head read immediately before merge, `mergedAt` plus `mergeCommit.oid` are present afterward, and `/private/tmp/leetcode_1436-merge-sha.txt` contains that exact GitHub merge SHA.

- [ ] **Step 4: Update exactly one Issue #2 line and read it back**

Run only after the merge readback succeeds:

```bash
MERGE_SHA="$(< /private/tmp/leetcode_1436-merge-sha.txt)"
gh issue view 2 --repo HyperLee/Leetcode_folder --json body --jq .body > /private/tmp/leetcode_1436-issue-before.md
test "$(rg -c '^- \[ \] `leetcode_1436`( |$)' /private/tmp/leetcode_1436-issue-before.md)" -eq 1
awk -v sha="$MERGE_SHA" '
  /^- \[ \] `leetcode_1436`( |$)/ {
    count++
    print "- [x] `leetcode_1436` | last commit `" sha "`"
    next
  }
  { print }
  END { if (count != 1) exit 1 }
' /private/tmp/leetcode_1436-issue-before.md > /private/tmp/leetcode_1436-issue-after.md
gh issue edit 2 --repo HyperLee/Leetcode_folder --body-file /private/tmp/leetcode_1436-issue-after.md
gh issue view 2 --repo HyperLee/Leetcode_folder --json body --jq .body > /private/tmp/leetcode_1436-issue-readback.md
test "$(rg -c '^- \[x\] `leetcode_1436`( |$)' /private/tmp/leetcode_1436-issue-readback.md)" -eq 1
test "$(rg -c '^- \[ \] `leetcode_1436`( |$)' /private/tmp/leetcode_1436-issue-readback.md)" -eq 0
awk 'seen && /^- \[ \]/{ print; exit } /`leetcode_1436`/{ print; seen=1 }' /private/tmp/leetcode_1436-issue-readback.md
```

Expected: the update and both target assertions exit 0; the final `awk` prints the checked `leetcode_1436` line and the next still-unchecked issue item. Abort before editing on zero or multiple target matches.

- [ ] **Step 5: Fast-forward local main and rerun post-merge verification**

From the primary checkout:

```bash
git switch main
git pull --ff-only origin main
MERGE_SHA="$(< /private/tmp/leetcode_1436-merge-sha.txt)"
test -n "$MERGE_SHA"
test "$(git rev-parse HEAD)" = "$MERGE_SHA"
test "$(git rev-parse origin/main)" = "$MERGE_SHA"
jq empty leetcode_1436/.vscode/launch.json leetcode_1436/.vscode/tasks.json
dotnet build leetcode_1436/leetcode_1436/leetcode_1436.csproj --nologo
dotnet run --no-build --project leetcode_1436/leetcode_1436/leetcode_1436.csproj
git diff --check -- leetcode_1436
git status --short
```

Expected: the persisted file is the GitHub merge readback source of truth; local `HEAD` and `origin/main` each equal that exact SHA. JSON/build/run/whitespace checks pass; output ends `Summary: 8/8 checks passed.`; primary checkout is clean.
