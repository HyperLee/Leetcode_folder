# LeetCode 448 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (- [ ]) syntax for tracking.

**Goal:** Convert leetcode_448 to a runnable, documented .NET 10 project and deliver it through the repository single-commit workflow.

**Architecture:** Program owns deterministic acceptance output. FindDisappearedNumbers uses in-place sign marking and writes nothing to the console.

**Tech Stack:** C#, .NET 10, dotnet CLI, GitHub CLI, VS Code CoreCLR.

## Global Constraints

- Change only leetcode_448/.
- Work in /private/tmp/codex-leetcode-448-net10 on codex/leetcode-448-net10.
- Keep public static IList<int> FindDisappearedNumbers(int[] nums).
- Target net10.0, implicit usings enabled, and nullable enabled.
- Keep one final commit relative to origin/main by amending the existing design commit.
- Do not deliver remotely before fresh verification and an independent review.

---

### Task 1: Convert configuration and establish RED

**Files:**
- Modify: leetcode_448/leetcode_448/leetcode_448.csproj
- Modify: leetcode_448/leetcode_448/Program.cs
- Create: leetcode_448/.editorconfig
- Create: leetcode_448/.gitattributes
- Create: leetcode_448/.gitignore
- Create: leetcode_448/.vscode/tasks.json
- Create: leetcode_448/.vscode/launch.json
- Delete: leetcode_448/leetcode_448.sln
- Delete: leetcode_448/leetcode_448/App.config
- Delete: leetcode_448/leetcode_448/Properties/AssemblyInfo.cs

**Interfaces:**
- Consumes: valid LeetCode 448 integer arrays.
- Produces: a compile-time RED call to FindDisappearedNumbers.

- [ ] **Step 1: Replace the project file.**

~~~
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
~~~

- [ ] **Step 2: Copy shared configuration and create VS Code task/launch files.**

~~~
cp leetcode_412/.editorconfig leetcode_448/.editorconfig
cp leetcode_412/.gitattributes leetcode_448/.gitattributes
cp leetcode_412/.gitignore leetcode_448/.gitignore
~~~

The task label is build leetcode_448. Its args are build and ${workspaceFolder}/leetcode_448.csproj. The launch configuration is Debug leetcode_448 and starts ${workspaceFolder}/leetcode_448/bin/Debug/net10.0/leetcode_448.dll with that task as preLaunchTask. Its cwd is ${workspaceFolder}. These settings intentionally support opening the problem root leetcode_448/ directly in VS Code.

- [ ] **Step 3: Delete the legacy files individually.**

~~~
rm leetcode_448/leetcode_448.sln
rm leetcode_448/leetcode_448/App.config
rm leetcode_448/leetcode_448/Properties/AssemblyInfo.cs
~~~

Do not recursively delete Properties.

- [ ] **Step 4: Add a minimal Main that calls the intentionally absent public API.**

~~~csharp
namespace leetcode_448;

internal static class Program
{
    private static void Main()
    {
        IList<int> actual = FindDisappearedNumbers([4, 3, 2, 7, 8, 2, 3, 1]);
        Console.WriteLine(string.Join(", ", actual));
    }
}
~~~

- [ ] **Step 5: Run the real RED build.**

~~~
dotnet build leetcode_448/leetcode_448/leetcode_448.csproj --nologo
~~~

Expected: exit code 1 and a compiler error naming only the missing FindDisappearedNumbers behavior, not a configuration error.
### Task 2: Implement sign marking and make the acceptance harness GREEN

**Files:**
- Modify: leetcode_448/leetcode_448/Program.cs

**Interfaces:**
- Consumes: int[] nums where n equals length, values are in 1..n, and values occur once or twice.
- Produces: ascending missing values in IList<int>. The method mutates nums but has no console side effect.

- [ ] **Step 1: Add the public solution method.**

~~~csharp
    /// <summary>
    /// 以原地負號標記找出未出現的數字。有效輸入必須長度為 n、元素皆在 [1, n]，
    /// 且每個元素出現一次或兩次；回傳依數字遞增排列的所有缺失值。
    /// </summary>
    public static IList<int> FindDisappearedNumbers(int[] nums)
    {
        List<int> missingNumbers = [];

        for (int index = 0; index < nums.Length; index++)
        {
            int markedIndex = Math.Abs(nums[index]) - 1;
            // 先前的標記可能已改變符號，必須取絕對值才能回到原始值對應的索引。
            if (nums[markedIndex] > 0)
            {
                nums[markedIndex] = -nums[markedIndex];
            }
        }

        for (int index = 0; index < nums.Length; index++)
        {
            // 未被標記的正數位置代表 index + 1 從未在輸入中出現。
            if (nums[index] > 0)
            {
                missingNumbers.Add(index + 1);
            }
        }

        return missingNumbers;
    }
~~~

- [ ] **Step 2: Replace the one-call RED Main with a clone-per-case harness.**

~~~csharp
        RunCase("Official example", [4, 3, 2, 7, 8, 2, 3, 1], [5, 6], ref passedChecks, ref totalChecks);
        RunCase("Minimum valid input", [1], [], ref passedChecks, ref totalChecks);
        RunCase("One duplicated value", [1, 1], [2], ref passedChecks, ref totalChecks);
        RunCase("Complete coverage", [1, 2, 3, 4, 5], [], ref passedChecks, ref totalChecks);
        RunCase("Missing first and trailing values", [2, 2, 3, 3, 4, 4], [1, 5, 6], ref passedChecks, ref totalChecks);
        RunCase("Missing final value", [1, 1, 2, 3], [4], ref passedChecks, ref totalChecks);
        RunCase("Multiple gaps", [2, 1, 2, 1], [3, 4], ref passedChecks, ref totalChecks);
        RunCase("Reversed duplicate pairs", [4, 4, 3, 3, 2, 2, 1, 1], [5, 6, 7, 8], ref passedChecks, ref totalChecks);
        RunCase("Upper-bound spot check", CreateUpperBoundCase(100000), [100000], ref passedChecks, ref totalChecks);
        RunInvariantCheck(ref passedChecks, ref totalChecks);
~~~

Use these helpers so every result is deterministic and prints Input, Expected, Actual, and Result.

~~~csharp
    private static void RunCase(string name, int[] source, int[] expected, ref int passedChecks, ref int totalChecks)
    {
        int[] input = (int[])source.Clone();
        IList<int> actual = FindDisappearedNumbers(input);
        bool passed = actual.SequenceEqual(expected);
        PrintCheck(name, source, expected, actual, passed);
        totalChecks++;
        if (passed)
        {
            passedChecks++;
        }
    }

    private static int[] CreateUpperBoundCase(int length)
    {
        int[] nums = Enumerable.Range(1, length).ToArray();
        nums[length - 1] = length - 1;
        return nums;
    }

    private static void PrintCheck(string name, int[] source, int[] expected, IList<int> actual, bool passed)
    {
        Console.WriteLine($"Case: {name}");
        Console.WriteLine($"Input: {FormatNumbers(source)}");
        Console.WriteLine($"Expected: {FormatNumbers(expected)}");
        Console.WriteLine($"Actual: {FormatNumbers(actual)}");
        Console.WriteLine(passed ? "Result: PASS" : "Result: FAIL");
        Console.WriteLine();
    }

    private static string FormatNumbers(IEnumerable<int> numbers)
    {
        return $"[{string.Join(", ", numbers)}]";
    }
~~~

- [ ] **Step 3: Add the sign-marking invariant check.**

~~~csharp
    /// <summary>
    /// 驗證負號標記的不變量：位置 index 保持正數當且僅當數字 index + 1 位於缺失結果；
    /// 回傳該不變量是否成立。
    /// </summary>
    private static bool MissingValuesMatchUnmarkedSlots(int[] markedNums, IList<int> missingValues)
    {
        for (int index = 0; index < markedNums.Length; index++)
        {
            bool isMissing = missingValues.Contains(index + 1);
            if ((markedNums[index] > 0) != isMissing)
            {
                return false;
            }
        }

        return true;
    }
~~~

RunInvariantCheck must clone [4, 3, 2, 7, 8, 2, 3, 1], require result [5, 6], require this helper to return true, and count one check.

- [ ] **Step 4: Build and run GREEN.**

~~~
dotnet build leetcode_448/leetcode_448/leetcode_448.csproj --nologo
dotnet run --no-build --project leetcode_448/leetcode_448/leetcode_448.csproj
~~~

Expected: 0 warnings, 0 errors, ten PASS lines, and the exact final line Summary: 10/10 checks passed.
### Task 3: Write the problem documentation from fresh evidence

**Files:**
- Create: leetcode_448/AGENTS.md
- Create: leetcode_448/README.md
- Create: leetcode_448/docs/readme-template.md

**Interfaces:**
- Consumes: the verified ten-check output from Task 2.
- Produces: a Traditional Chinese README with one full-run text transcript.

- [ ] **Step 1: Copy the current README template.**

~~~
cp leetcode_412/docs/readme-template.md leetcode_448/docs/readme-template.md
~~~

- [ ] **Step 2: Write AGENTS.md with the concrete project contract.**

State that the executable is leetcode_448/leetcode_448/leetcode_448.csproj; commands run from the problem root are dotnet build leetcode_448/leetcode_448.csproj --nologo and dotnet run --no-build --project leetcode_448/leetcode_448.csproj; there is no formal test project; style is four spaces, explicit local types, PascalCase public members, and camelCase locals; the public API has no console I/O; sign marking uses v - 1 and input must be cloned by the harness; success is Summary: 10/10 checks passed.; Git metadata is at the parent root; and commits/PR changes remain under leetcode_448/.

- [ ] **Step 3: Write README.md in Traditional Chinese with actual implementation facts.**

Use sections in this order: title, 題目連結, 題意與限制, 核心不變量, 解法：原地負號標記, 複雜度, 逐步範例, Acceptance Harness, 建置與執行, 實際執行輸出, 專案結構. Include English and Chinese official URLs; n equals array length; 1 <= n <= 100000; each value lies in 1..n; each value occurs once or twice; the O(n) / O(1) excluding-result complexity; the mutation trade-off; the official example walk-through; and every Task 2 case name. The walkthrough must use a plaintext code fence. The full execution transcript is the only text code fence.

- [ ] **Step 4: Generate, insert, and compare the literal transcript.**

~~~
dotnet run --no-build --project leetcode_448/leetcode_448/leetcode_448.csproj > /private/tmp/leetcode_448.actual.txt
~~~

Insert every line of that file in the one README text fence. Then execute the transcript extraction and diff commands from Section 11.2 of LEETCODE_NET10_MIGRATION_SPEC.md and the unique text-fence count check. Expected: the diff is empty and the count is 1.

### Task 4: Verify, review, and deliver the single-commit migration

**Files:**
- Review: every changed leetcode_448 path

**Interfaces:**
- Consumes: valid JSON, a warning-free build, a 10/10 harness, and a matching README transcript.
- Produces: one reviewed feature commit, a clean squash merge, and a single Issue #2 checkbox update.

- [ ] **Step 1: Run the complete local gate.**

~~~
jq empty leetcode_448/.vscode/launch.json leetcode_448/.vscode/tasks.json
dotnet build leetcode_448/leetcode_448/leetcode_448.csproj --nologo
dotnet run --no-build --project leetcode_448/leetcode_448/leetcode_448.csproj
test ! -e leetcode_448/leetcode_448.sln
test ! -e leetcode_448/leetcode_448/App.config
test ! -e leetcode_448/leetcode_448/Properties/AssemblyInfo.cs
git diff --check -- leetcode_448
~~~

Expected: every command exits 0, build has 0 warnings and 0 errors, and run ends with Summary: 10/10 checks passed.

- [ ] **Step 2: Amend only scoped files into the required feature commit.**

~~~
git add leetcode_448
git diff --cached --check
git diff --cached --name-only
git commit --amend -m "feat(leetcode-448): migrate project to .NET 10"
git rev-list --count origin/main..HEAD
~~~

Expected: every staged path begins with leetcode_448/ and the commit count prints 1.

- [ ] **Step 3: Perform independent read-only review before remote delivery.**

Inspect git diff origin/main...HEAD -- leetcode_448. Check the public API, cloned input, sign invariant, bilingual Main XML, Traditional Chinese function XML, sparse rationale comments, all README requirements, unique transcript, VS Code paths, legacy absence, scope, and no Critical or Important finding. For any fix, repeat Steps 1 through 3 and amend the same commit.

- [ ] **Step 4: Push a draft PR and verify its exact head.**

~~~
git push -u origin codex/leetcode-448-net10
gh pr create --repo HyperLee/Leetcode_folder --base main --head codex/leetcode-448-net10 --draft --title "feat(leetcode-448): migrate project to .NET 10" --body "Converted leetcode_448 to SDK-style .NET 10. Preserved sign marking: value v negates index v - 1. Verified deterministic 10/10 harness. Time O(n); auxiliary space O(1) excluding output. Refs #2"
pr_number=$(gh pr view --repo HyperLee/Leetcode_folder --head codex/leetcode-448-net10 --json number --jq .number)
gh pr view "$pr_number" --repo HyperLee/Leetcode_folder --json headRefOid,mergeable,mergeStateStatus,statusCheckRollup,commits,files
~~~

Require local verified headRefOid, MERGEABLE, CLEAN, one commit, only leetcode_448 paths, and no failed, cancelled, or pending checks.

- [ ] **Step 5: Ready and squash merge the verified head, then update Issue #2 only after merged confirmation.**

~~~
gh pr ready "$pr_number" --repo HyperLee/Leetcode_folder
gh pr merge "$pr_number" --repo HyperLee/Leetcode_folder --squash --match-head-commit "$verified_head_sha"
~~~

After GitHub reports merged true and its merge SHA, read Issue #2; replace exactly the unchecked leetcode_448 row with the checked row; update and read it back; confirm leetcode_452 remains unchecked. Fast-forward main to that merge SHA and rerun the JSON, build, run, README transcript, whitespace, and clean-status checks.
