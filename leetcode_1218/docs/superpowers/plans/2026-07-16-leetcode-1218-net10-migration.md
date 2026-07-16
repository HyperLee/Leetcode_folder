# leetcode_1218 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `leetcode_1218` to a documented, deterministic .NET 10 console project with two genuinely distinct linear-time dynamic-programming solutions, then publish it through a squash-merged pull request and update Issue #2 only after merge.

**Architecture:** `Program.Main` owns a deterministic 16-check console acceptance harness. `LongestSubsequence` stores the best subsequence length per ending value in a dictionary, while `LongestSubsequence2` stores the same state in a fixed 20,001-entry array using the problem's bounded value range; both APIs stay pure and preserve input order.

**Tech Stack:** C# / .NET 10 SDK-style console project, VS Code CoreCLR launch configuration, GitHub CLI, Git worktree.

## Global Constraints

- All tracked changes must remain below `leetcode_1218/`; no other LeetCode folder or repository-root file may change.
- The project file must be SDK-style with `<TargetFramework>net10.0</TargetFramework>`, `<ImplicitUsings>enable</ImplicitUsings>`, and `<Nullable>enable</Nullable>`.
- Remove `leetcode_1218/leetcode_1218.sln`, `leetcode_1218/leetcode_1218/App.config`, and `leetcode_1218/leetcode_1218/Properties/AssemblyInfo.cs` one file at a time; never use a recursive or bulk deletion command.
- Preserve exactly `public static int LongestSubsequence(int[] arr, int difference)` and `public static int LongestSubsequence2(int[] arr, int difference)`; do not define behavior for invalid input outside the LeetCode contract.
- Both solution APIs must be pure: they cannot mutate `arr` or write to the console. `Main` owns all acceptance output and sets `Environment.ExitCode = 1` if any check fails.
- `Program.cs` must retain bilingual problem XML above `Main`; both public APIs need Traditional Chinese XML summaries and only high-signal algorithm comments.
- README must be Traditional Chinese, contain exactly one `text` fence, and its complete transcript must byte-match a fresh `dotnet run --no-build` result.
- The branch must end with exactly one commit relative to `origin/main`, subject `feat(leetcode-1218): migrate project to .NET 10`; amend the existing design/plan commit instead of adding commits.
- Publish only after fresh JSON, build, run, failure-path, transcript, whitespace, scope, legacy-absence, and independent read-only review evidence.

---

## File Structure

| Path | Responsibility |
| --- | --- |
| `leetcode_1218/leetcode_1218/leetcode_1218.csproj` | Minimal .NET 10 executable project declaration. |
| `leetcode_1218/leetcode_1218/Program.cs` | Bilingual problem summary, two pure DP APIs, and console-only acceptance harness. |
| `leetcode_1218/.editorconfig`, `.gitattributes`, `.gitignore` | Verified shared formatting, text, and generated-file policy. |
| `leetcode_1218/.vscode/tasks.json`, `.vscode/launch.json` | Direct build and CoreCLR debug commands for the nested project. |
| `leetcode_1218/AGENTS.md`, `README.md`, `docs/readme-template.md` | Task-specific collaboration and Traditional Chinese teaching documentation. |
| `leetcode_1218/docs/superpowers/specs/2026-07-16-leetcode-1218-net10-migration-design.md` | Approved migration design. |
| `leetcode_1218/docs/superpowers/plans/2026-07-16-leetcode-1218-net10-migration.md` | This execution plan. |

## Task 1: Create the .NET 10 project shape and prove the acceptance RED

**Files:**
- Create: `leetcode_1218/.editorconfig`, `leetcode_1218/.gitattributes`, `leetcode_1218/.gitignore`, `leetcode_1218/.vscode/tasks.json`, `leetcode_1218/.vscode/launch.json`
- Modify: `leetcode_1218/leetcode_1218/leetcode_1218.csproj`, `leetcode_1218/leetcode_1218/Program.cs`
- Delete: `leetcode_1218/leetcode_1218.sln`, `leetcode_1218/leetcode_1218/App.config`, `leetcode_1218/leetcode_1218/Properties/AssemblyInfo.cs`

**Interfaces:**
- Consumes: the legacy signatures `LongestSubsequence(int[] arr, int difference)` and `LongestSubsequence2(int[] arr, int difference)`, plus the LeetCode valid-input constraints `1 <= arr.Length <= 100000` and `-10000 <= arr[i], difference <= 10000`.
- Produces: a .NET 10 host whose harness invokes the intentionally absent public APIs to form an implementation-specific RED.

- [ ] **Step 1: Replace the legacy project declaration**

Replace `leetcode_1218/leetcode_1218/leetcode_1218.csproj` with:

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

- [ ] **Step 2: Remove each confirmed legacy artifact by exact path**

Delete only these three files with exact single-path patches:

```text
leetcode_1218/leetcode_1218.sln
leetcode_1218/leetcode_1218/App.config
leetcode_1218/leetcode_1218/Properties/AssemblyInfo.cs
```

Then run:

```bash
test ! -e leetcode_1218/leetcode_1218.sln
test ! -e leetcode_1218/leetcode_1218/App.config
test ! -e leetcode_1218/leetcode_1218/Properties/AssemblyInfo.cs
```

Expected: every command exits `0`.

- [ ] **Step 3: Add verified shared configuration and direct VS Code wiring**

Copy the verified common files from the latest migrated neighbor and create the VS Code directory:

```bash
cp leetcode_1207/.editorconfig leetcode_1218/.editorconfig
cp leetcode_1207/.gitattributes leetcode_1218/.gitattributes
cp leetcode_1207/.gitignore leetcode_1218/.gitignore
mkdir -p leetcode_1218/.vscode
```

Create `leetcode_1218/.vscode/tasks.json` with:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_1218",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/leetcode_1218/leetcode_1218.csproj"
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

Create `leetcode_1218/.vscode/launch.json` with:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Debug leetcode_1218",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_1218",
      "program": "${workspaceFolder}/leetcode_1218/bin/Debug/net10.0/leetcode_1218.dll",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_1218",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
```

- [ ] **Step 4: Write the harness-only RED source**

Replace `leetcode_1218/leetcode_1218/Program.cs` with the following source. Do not add either solution method yet.

```csharp
namespace leetcode_1218;

internal static class Program
{
    /// <summary>
    /// 1218. Longest Arithmetic Subsequence of Given Difference
    /// https://leetcode.com/problems/longest-arithmetic-subsequence-of-given-difference/
    /// 1218. 最長定差子序列
    /// https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/
    /// English: Given an integer array and a fixed difference, return the length of the longest subsequence whose adjacent elements differ by exactly that value; deleting elements must not change the order of the remaining elements.
    /// 中文：給定整數陣列與固定公差，回傳相鄰元素差恰為該公差的最長子序列長度；可以刪除元素，但不可改變其餘元素的順序。
    /// </summary>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int[] Values, int Difference, int Expected)[] testCases =
        {
            ("Case 1: Official increasing example", new[] { 1, 2, 3, 4 }, 1, 4),
            ("Case 2: Official no-chain example", new[] { 1, 3, 5, 7 }, 1, 1),
            ("Case 3: Official negative-difference example", new[] { 1, 5, 7, 8, 5, 3, 4, 2, 1 }, -2, 4),
            ("Case 4: Minimum valid input", new[] { 5 }, 7, 1),
            ("Case 5: Zero difference with duplicates", new[] { 7, 7, 7, 7 }, 0, 4),
            ("Case 6: Subsequence order regression", new[] { 4, 1, 2, 3 }, 1, 3),
            ("Case 7: Value-range boundaries", new[] { -10_000, 0, 10_000 }, 10_000, 3)
        };

        List<CaseResult> checks = new();

        foreach ((string caseName, int[] values, int difference, int expected) in testCases)
        {
            string input = $"arr = {FormatArray(values)}, difference = {difference}";
            int dictionaryActual = LongestSubsequence(values, difference);
            int arrayActual = LongestSubsequence2(values, difference);
            checks.Add(new CaseResult(caseName, input, "Dictionary DP", expected, dictionaryActual, expected == dictionaryActual));
            checks.Add(new CaseResult(caseName, input, "Value-range array DP", expected, arrayActual, expected == arrayActual));
        }

        int[] maximumInput = Enumerable.Repeat(7, 100_000).ToArray();
        const int maximumExpected = 100_000;
        int maximumDictionaryActual = LongestSubsequence(maximumInput, 0);
        int maximumArrayActual = LongestSubsequence2(maximumInput, 0);
        const string maximumInputSummary = "arr = 100,000 × 7, difference = 0";
        checks.Add(new CaseResult("Case 8: Maximum-length spot check", maximumInputSummary, "Dictionary DP", maximumExpected, maximumDictionaryActual, maximumExpected == maximumDictionaryActual));
        checks.Add(new CaseResult("Case 8: Maximum-length spot check", maximumInputSummary, "Value-range array DP", maximumExpected, maximumArrayActual, maximumExpected == maximumArrayActual));

        int passedCount = 0;
        Console.WriteLine("LeetCode 1218 acceptance harness");

        foreach (IGrouping<string, CaseResult> caseGroup in checks.GroupBy(check => check.CaseName))
        {
            CaseResult firstCheck = caseGroup.First();
            Console.WriteLine();
            Console.WriteLine(firstCheck.CaseName);
            Console.WriteLine($"Input: {firstCheck.Input}");

            foreach (CaseResult check in caseGroup)
            {
                Console.WriteLine($"{(check.Passed ? "PASS" : "FAIL")} | {check.CheckName} | Expected: {check.Expected} | Actual: {check.Actual}");

                if (check.Passed)
                {
                    passedCount++;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checks.Count} checks passed.");

        if (passedCount != checks.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string FormatArray(IEnumerable<int> values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    private readonly record struct CaseResult(string CaseName, string Input, string CheckName, int Expected, int Actual, bool Passed);
}
```

- [ ] **Step 5: Run and record the meaningful RED**

Run from the repository root:

```bash
dotnet build leetcode_1218/leetcode_1218/leetcode_1218.csproj --nologo
```

Expected: exit `1`, `0 warnings`, and `CS0103` errors naming both missing methods. The RED must not be caused by .NET Framework references, project syntax, test typos, or legacy assembly metadata.

## Task 2: Implement both pure DP APIs and prove GREEN

**Files:**
- Modify: `leetcode_1218/leetcode_1218/Program.cs`
- Test: `leetcode_1218/leetcode_1218/Program.cs` acceptance harness

**Interfaces:**
- Consumes: harness calls to the two public signatures from Task 1.
- Produces: `LongestSubsequence` using dictionary state and `LongestSubsequence2` using fixed value-range array state; both return the longest valid subsequence length without side effects.

- [ ] **Step 1: Add the minimal dictionary implementation and documentation**

Insert after `Main` and before `FormatArray`:

```csharp
    /// <summary>
    /// 使用雜湊動態規劃計算指定公差的最長子序列。對每個目前值保存以該值結尾的最佳長度，並由「目前值減去公差」的既有狀態延伸。有效輸入須符合題目定義的非空整數陣列與公差；方法不修改輸入，也不寫入主控台。
    /// </summary>
    /// <param name="arr">題目定義的非空整數陣列，元素順序即子序列可使用的順序。</param>
    /// <param name="difference">相鄰子序列元素必須符合的固定差值。</param>
    /// <returns>相鄰差皆為 <paramref name="difference"/> 的最長子序列長度。</returns>
    public static int LongestSubsequence(int[] arr, int difference)
    {
        Dictionary<int, int> maxLengthByValue = new();
        int longest = 0;

        foreach (int value in arr)
        {
            int previousLength = maxLengthByValue.GetValueOrDefault(value - difference);
            int currentLength = previousLength + 1;

            // 依掃描順序延伸前驅狀態，確保只形成合法子序列而不是重排後的數列。
            maxLengthByValue[value] = currentLength;
            longest = Math.Max(longest, currentLength);
        }

        return longest;
    }
```

- [ ] **Step 2: Add the fixed-range implementation and documentation**

Insert immediately after `LongestSubsequence`:

```csharp
    /// <summary>
    /// 利用題目固定值域，以陣列動態規劃計算指定公差的最長子序列。陣列索引代表結尾值，前驅值超出合法值域時視為沒有可延伸狀態。有效輸入須符合題目定義；方法不修改輸入，也不寫入主控台。
    /// </summary>
    /// <param name="arr">元素介於 -10,000 到 10,000 的題目有效非空陣列。</param>
    /// <param name="difference">介於 -10,000 到 10,000 的固定差值。</param>
    /// <returns>相鄰差皆為 <paramref name="difference"/> 的最長子序列長度。</returns>
    public static int LongestSubsequence2(int[] arr, int difference)
    {
        const int minimumValue = -10_000;
        const int maximumValue = 10_000;
        const int offset = 10_000;
        int[] maxLengthByValue = new int[maximumValue - minimumValue + 1];
        int longest = 0;

        foreach (int value in arr)
        {
            int previousValue = value - difference;
            int previousLength = previousValue >= minimumValue && previousValue <= maximumValue
                ? maxLengthByValue[previousValue + offset]
                : 0;
            int currentLength = previousLength + 1;

            // 題目值域經 offset 後對應至 0..20,000，避免雜湊並維持固定空間。
            maxLengthByValue[value + offset] = currentLength;
            longest = Math.Max(longest, currentLength);
        }

        return longest;
    }
```

- [ ] **Step 3: Verify GREEN with the same build command**

Run:

```bash
dotnet build leetcode_1218/leetcode_1218/leetcode_1218.csproj --nologo
```

Expected: exit `0`, `0 Warning(s)`, and `0 Error(s)`.

- [ ] **Step 4: Execute all deterministic checks**

Run:

```bash
dotnet run --no-build --project leetcode_1218/leetcode_1218/leetcode_1218.csproj
```

Expected: exit `0`; every dictionary and array check prints `PASS`; final line is exactly `Summary: 16/16 checks passed.`.

- [ ] **Step 5: Prove the harness reports failures through its exit code**

Temporarily change only Case 7 expected value from `3` to `4`, rebuild, and rerun the harness. Verify exit `1`, two `FAIL` lines for Case 7, and `Summary: 14/16 checks passed.`. Restore expected `3`, rebuild, rerun, and verify exit `0` plus `Summary: 16/16 checks passed.`. Do not change either production method during this failure-path check, and do not retain the temporary expectation in Git.

- [ ] **Step 6: Refactor only while green**

Review names, XML summaries, comments, and line wrapping without changing the two public signatures, state formulas, output format, or case set. Rerun the Step 3 build and Step 4 harness after any refactor; expected results remain 0 warnings, 0 errors, and `16/16`.

## Task 3: Add collaborator documentation and transcript-accurate teaching material

**Files:**
- Create: `leetcode_1218/AGENTS.md`
- Create: `leetcode_1218/docs/readme-template.md`
- Create: `leetcode_1218/README.md`

**Interfaces:**
- Consumes: the .NET 10 project path, both public DP methods, and the fresh 16-check transcript from Task 2.
- Produces: direct build/debug instructions and a Traditional Chinese README that accurately distinguishes dictionary and value-range array DP.

- [ ] **Step 1: Create the task-specific collaborator guide**

Create `leetcode_1218/AGENTS.md` with:

````markdown
# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep both pure dynamic
programming solutions, the bilingual problem XML summary, and the deterministic
acceptance harness in `leetcode_1218/Program.cs`. The nested
`leetcode_1218/leetcode_1218.csproj` defines the executable. From this folder,
run:

```bash
dotnet build leetcode_1218/leetcode_1218.csproj --nologo
dotnet run --no-build --project leetcode_1218/leetcode_1218.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1218` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main`.

Keep `LongestSubsequence(int[] arr, int difference)` and
`LongestSubsequence2(int[] arr, int difference)` pure: neither method writes to
the console or modifies `arr`. Both process elements in input order and maintain
the invariant that the state for value `x` is the best valid subsequence length
ending at `x`. The first API uses dictionary state; the second uses the problem's
bounded `[-10000, 10000]` value range. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print every
case's input, each method's expected and actual value, PASS/FAIL, and
`Summary: 16/16 checks passed.` on success with exit code 0. This project has no
separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_1218` and stage only `leetcode_1218/` during
publishing. Keep commits and pull requests scoped to this folder.
````

- [ ] **Step 2: Add the verified README authoring template**

Run:

```bash
mkdir -p leetcode_1218/docs
cp leetcode_1207/docs/readme-template.md leetcode_1218/docs/readme-template.md
```

- [ ] **Step 3: Capture the sole source of truth for the README transcript**

Run:

```bash
dotnet run --no-build --project leetcode_1218/leetcode_1218/leetcode_1218.csproj > /private/tmp/leetcode_1218.actual.txt
tail -n 1 /private/tmp/leetcode_1218.actual.txt
```

Expected final line: `Summary: 16/16 checks passed.`

- [ ] **Step 4: Write `leetcode_1218/README.md` with these exact sections and facts**

Use Traditional Chinese and include, in this order:

1. Title `# LeetCode 1218：Longest Arithmetic Subsequence of Given Difference／最長定差子序列`.
2. A .NET 10 overview stating that `Main` owns all console output and that both public methods process values in input order without mutating the array.
3. Exact official links `https://leetcode.com/problems/longest-arithmetic-subsequence-of-given-difference/` and `https://leetcode.cn/problems/longest-arithmetic-subsequence-of-given-difference/`.
4. The problem definition and exact constraints `1 <= arr.Length <= 10^5` and `-10^4 <= arr[i], difference <= 10^4`; do not claim invalid-input behavior.
5. A shared DP invariant section: state `dp[x]` is the best scanned-prefix subsequence ending in value `x`, and the transition is `dp[x] = dp[x - difference] + 1`.
6. A dictionary solution section for `LongestSubsequence`, including why scanning order preserves subsequence semantics.
7. A bounded-array solution section for `LongestSubsequence2`, including the `10,000` offset, the `20,001` slots, and why an out-of-range predecessor contributes zero.
8. A tradeoff table: both methods use `O(n)` time and `O(1)` result space; dictionary auxiliary space is `O(u)`, while array auxiliary space is fixed `O(20,001)`.
9. A step-by-step walkthrough of the official negative-difference input showing the chain `[7, 5, 3, 1]` reaches length `4` while retaining original order.
10. An acceptance table for all 8 cases and 16 checks, including minimum input, zero difference, ordering regression, both value boundaries, and the 100,000-element spot check.
11. Verified commands from the problem root:

```bash
dotnet build leetcode_1218/leetcode_1218.csproj --nologo
dotnet run --no-build --project leetcode_1218/leetcode_1218.csproj
```

12. One and only one fenced block opened by exactly ```text`. Copy the exact bytes of `/private/tmp/leetcode_1218.actual.txt` into it without editing; no other `text` fence may exist.
13. A `plaintext` project tree containing shared dotfiles, `.vscode/`, `AGENTS.md`, `README.md`, `docs/readme-template.md`, both `docs/superpowers` records, and the nested `Program.cs` and `.csproj`.

- [ ] **Step 5: Verify transcript extraction and fence uniqueness**

Run:

```bash
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_1218/README.md > /private/tmp/leetcode_1218.readme.txt
diff -u /private/tmp/leetcode_1218.readme.txt /private/tmp/leetcode_1218.actual.txt
rg -c '^```text$' leetcode_1218/README.md
```

Expected: `diff -u` exits `0` without output and the final command prints exactly `1`.

## Task 4: Perform the local release gate and independent read-only review

**Files:**
- Review: every changed or deleted path below `leetcode_1218/`.
- Modify: only files below `leetcode_1218/` when correcting review findings.

**Interfaces:**
- Consumes: green code, exact documentation, and target-scoped changes from Tasks 1–3.
- Produces: one clean reviewed change set ready to amend into the existing single commit.

- [ ] **Step 1: Run the complete local verification bundle from the repository root**

```bash
jq empty leetcode_1218/.vscode/launch.json leetcode_1218/.vscode/tasks.json
dotnet build leetcode_1218/leetcode_1218/leetcode_1218.csproj --nologo
dotnet run --no-build --project leetcode_1218/leetcode_1218/leetcode_1218.csproj
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_1218/README.md > /private/tmp/leetcode_1218.readme.txt
diff -u /private/tmp/leetcode_1218.readme.txt /private/tmp/leetcode_1218.actual.txt
test "$(rg -c '^```text$' leetcode_1218/README.md)" -eq 1
git diff --check -- leetcode_1218
test ! -e leetcode_1218/leetcode_1218.sln
test ! -e leetcode_1218/leetcode_1218/App.config
test ! -e leetcode_1218/leetcode_1218/Properties/AssemblyInfo.cs
```

Expected: all commands exit `0`; build reports 0 warnings and 0 errors; run ends `Summary: 16/16 checks passed.`; transcript diff is empty.

- [ ] **Step 2: Verify scope and project contracts explicitly**

```bash
git status --short
git diff --name-only origin/main...HEAD
git status --short | awk '{print $2}' | rg -v '^leetcode_1218/'
rg -n '<TargetFramework>net10.0</TargetFramework>|<ImplicitUsings>enable</ImplicitUsings>|<Nullable>enable</Nullable>' leetcode_1218/leetcode_1218/leetcode_1218.csproj
rg -n 'public static int LongestSubsequence\(int\[\] arr, int difference\)|public static int LongestSubsequence2\(int\[\] arr, int difference\)' leetcode_1218/leetcode_1218/Program.cs
```

Expected: every working-tree path is below `leetcode_1218/`; the out-of-scope filter prints nothing; the project search returns all three properties; the API search returns exactly two signatures.

- [ ] **Step 3: Perform an independent read-only code review**

Review the full `origin/main`-relative patch, including untracked files, against `LEETCODE_NET10_MIGRATION_SPEC.md` and the approved design. The reviewer must check algorithm correctness, both methods' purity, value-range boundaries, the 100,000-element case, bilingual XML, README fidelity, VS Code paths, legacy absence, and scope. The reviewer may report findings but must not edit, stage, commit, push, open a PR, or update Issue #2.

Expected: no unresolved Critical or Important findings. Fix any reportable issue in the main execution context, rerun Steps 1–2, and repeat read-only review.

- [ ] **Step 4: Amend all migration files into the existing single commit**

Stage only `leetcode_1218/`, inspect the cached scope, then amend:

```bash
git add leetcode_1218
git diff --cached --check
git diff --cached --name-only
git commit --amend -m "feat(leetcode-1218): migrate project to .NET 10"
git rev-list --count origin/main..HEAD
git diff --name-only origin/main...HEAD
```

Expected: cached and committed paths are all below `leetcode_1218/`; commit count prints `1`.

- [ ] **Step 5: Re-run the complete gate at the exact committed HEAD**

Repeat Task 4 Step 1, then run:

```bash
git status --short
git log -1 --format='%H%n%s'
```

Expected: clean status, one recorded head SHA, subject `feat(leetcode-1218): migrate project to .NET 10`, and the same green verification evidence.

## Task 5: Publish, merge, update Issue #2, and verify post-merge

**Files:**
- Read-only before merge: committed `leetcode_1218/` change set and GitHub PR metadata.
- External update after merge only: the unique Issue #2 checkbox line for `leetcode_1218`.

**Interfaces:**
- Consumes: the exact reviewed commit SHA and clean local gate from Task 4.
- Produces: pushed branch, Draft then Ready PR, squash merge, precise Issue #2 readback, synchronized `main`, and green post-merge evidence.

- [ ] **Step 1: Push the verified branch and create a Draft PR**

```bash
git push -u origin codex/leetcode-1218-net10
gh pr create --repo HyperLee/Leetcode_folder --draft --base main --head codex/leetcode-1218-net10 --title "feat(leetcode-1218): migrate project to .NET 10" --body-file /private/tmp/leetcode_1218-pr-body.md
```

Before the second command, create `/private/tmp/leetcode_1218-pr-body.md` with this content after confirming every stated result against the committed run:

```markdown
## Summary

- migrate the legacy .NET Framework 4.8 project to SDK-style .NET 10
- retain two pure `O(n)` dynamic-programming solutions with distinct dictionary and bounded-array storage tradeoffs
- add a deterministic 16-check harness, problem-root VS Code configuration, contributor guidance, and transcript-verified Traditional Chinese documentation

## Algorithm

- `LongestSubsequence` stores the best scanned-prefix length ending at each value in a dictionary and extends the state for `value - difference`; auxiliary space is `O(u)` for `u` distinct values
- `LongestSubsequence2` stores the same state in 20,001 fixed slots for values from -10,000 through 10,000; auxiliary space is fixed `O(20,001)`
- both methods run in `O(n)` time, use `O(1)` result space, preserve input order, leave the input unchanged, and keep console output in `Main`

## Verification

- meaningful RED: .NET 10 build failed with `CS0103` for both intentionally absent public APIs
- GREEN: build passed with 0 warnings and 0 errors
- acceptance harness passed with `Summary: 16/16 checks passed.` and exit code 0
- forced failure-path check produced two FAIL results, `Summary: 14/16 checks passed.`, and exit code 1 before restoration
- VS Code JSON, README transcript equality, single `text` fence, whitespace, changed-path scope, and legacy-file absence checks passed
- independent read-only review found no unresolved Critical or Important findings

Refs #2
```

Never write `Closes #2`.

- [ ] **Step 2: Validate the public PR before marking it ready**

Read back PR head SHA, base, changed files, commit count, draft state, merge state, and checks. Confirm the PR head equals the locally verified SHA, exactly one commit exists, all changed paths are below `leetcode_1218/`, merge state is clean, and no check is failed, cancelled, or pending.

Expected: all release gates remain true. If `origin/main` advanced, rebase before publication, rerun Task 4, preserve the single-commit rule, and revalidate the PR; do not force-push published history without explicit user authorization.

- [ ] **Step 3: Mark Ready and squash merge with the expected head SHA**

```bash
PR_NUMBER="$(gh pr view --repo HyperLee/Leetcode_folder --json number --jq .number)"
VERIFIED_HEAD_SHA="$(git rev-parse HEAD)"
test "$(gh pr view "$PR_NUMBER" --repo HyperLee/Leetcode_folder --json headRefOid --jq .headRefOid)" = "$VERIFIED_HEAD_SHA"
gh pr ready "$PR_NUMBER" --repo HyperLee/Leetcode_folder
gh pr merge "$PR_NUMBER" --repo HyperLee/Leetcode_folder --squash --delete-branch --match-head-commit "$VERIFIED_HEAD_SHA"
```

Expected: GitHub reports `merged: true` and a merge SHA. Do not update Issue #2 without that confirmation.

- [ ] **Step 4: Update exactly one Issue #2 checkbox and read it back**

Read the latest Issue #2 body, verify the exact unchecked prefix `- [ ] \`leetcode_1218\`` appears once, replace only that occurrence with `- [x] \`leetcode_1218\``, and update the body. Read Issue #2 again and verify:

```text
- [x] `leetcode_1218`
- [ ] `leetcode_1232`
```

Expected: the checked target exists once, the unchecked target is absent, and `leetcode_1232` remains unchecked. Do not alter any other issue content, labels, or state.

- [ ] **Step 5: Synchronize local main and run post-merge verification**

From the clean primary checkout:

```bash
git switch main
git pull --ff-only origin main
git rev-parse HEAD
git rev-parse origin/main
```

Expected: both SHAs equal GitHub's merge SHA. Repeat Task 4 Step 1 against merged `main`, then verify `git status --short` is empty. If the primary checkout acquires unrelated user changes, use a clean detached verification worktree instead of modifying or cleaning that checkout.

- [ ] **Step 6: Preserve evidence and stop at the requested boundary**

Report the final branch commit, PR URL/number, merge SHA, Issue #2 readback, build result, `Summary: 16/16 checks passed.`, transcript diff, review status, and post-merge clean status. Do not begin `leetcode_1232` or any later problem.
