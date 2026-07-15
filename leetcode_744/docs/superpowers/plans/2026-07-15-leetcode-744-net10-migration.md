# leetcode_744 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `leetcode_744` to a documented, deterministic .NET 10 console project and publish it through a squash-merged pull request with Issue #2 updated only after merge.

**Architecture:** `Program.Main` is a deterministic acceptance harness and owns every console write. `NextGreatestLetter` is the sole public, pure lower-bound binary-search API: it finds the first letter strictly greater than `target`, using modulo only to model the required wraparound result.

**Tech Stack:** C# / .NET 10 SDK-style console project, VS Code CoreCLR launch configuration, GitHub CLI, Git worktree.

## Global Constraints

- All tracked changes must remain below `leetcode_744/`; no other LeetCode folder or repository-root file may change.
- The project file must be SDK-style with `<TargetFramework>net10.0</TargetFramework>`, `<ImplicitUsings>enable</ImplicitUsings>`, and `<Nullable>enable</Nullable>`.
- Remove `leetcode_744/leetcode_744.sln`, `leetcode_744/leetcode_744/App.config`, and `leetcode_744/leetcode_744/Properties/AssemblyInfo.cs` one file at a time; do not use a recursive or bulk deletion command.
- Preserve exactly `public static char NextGreatestLetter(char[] letters, char target)` and do not define behavior for invalid input outside the LeetCode contract.
- `NextGreatestLetter` must be pure: it cannot mutate `letters` or write to the console. `Main` owns all acceptance output and sets `Environment.ExitCode = 1` if any check fails.
- `Program.cs` must retain bilingual problem XML above `Main`; the public API needs a Traditional Chinese XML summary and only high-signal algorithm comments.
- README must be Traditional Chinese, contain exactly one `text` fence, and its complete transcript must byte-match a fresh `dotnet run --no-build` result.
- The branch must end with exactly one commit relative to `origin/main`, subject `feat(leetcode-744): migrate project to .NET 10`; amend the existing design commit instead of adding a second commit.
- Publish only after fresh JSON, build, run, transcript, whitespace, scope, legacy-absence, and independent read-only review evidence.

---

## File Structure

| Path | Responsibility |
| --- | --- |
| `leetcode_744/leetcode_744/leetcode_744.csproj` | Minimal .NET 10 executable project declaration. |
| `leetcode_744/leetcode_744/Program.cs` | Bilingual problem summary, pure binary-search API, and console-only acceptance harness. |
| `leetcode_744/.editorconfig`, `.gitattributes`, `.gitignore` | Verified shared formatting, text, and generated-file policy. |
| `leetcode_744/.vscode/tasks.json`, `.vscode/launch.json` | Direct build and CoreCLR debug commands for the nested project. |
| `leetcode_744/AGENTS.md`, `README.md`, `docs/readme-template.md` | Task-specific collaboration and Traditional Chinese teaching documentation. |
| `leetcode_744/docs/superpowers/specs/2026-07-15-leetcode-744-net10-migration-design.md` | Approved migration design. |
| `leetcode_744/docs/superpowers/plans/2026-07-15-leetcode-744-net10-migration.md` | This execution plan. |

## Task 1: Create the .NET 10 project shape and prove the acceptance RED

**Files:**
- Create: `leetcode_744/.editorconfig`, `leetcode_744/.gitattributes`, `leetcode_744/.gitignore`, `leetcode_744/.vscode/tasks.json`, `leetcode_744/.vscode/launch.json`
- Modify: `leetcode_744/leetcode_744/leetcode_744.csproj`, `leetcode_744/leetcode_744/Program.cs`
- Delete: `leetcode_744/leetcode_744.sln`, `leetcode_744/leetcode_744/App.config`, `leetcode_744/leetcode_744/Properties/AssemblyInfo.cs`

**Interfaces:**
- Consumes: the legacy signature `NextGreatestLetter(char[] letters, char target)` and the contract that `letters` is nonempty, sorted nondecreasing, lowercase characters.
- Produces: a .NET 10 host whose harness invokes the intentionally absent `NextGreatestLetter(char[] letters, char target)` to form an implementation-specific RED.

- [ ] **Step 1: Replace the legacy project declaration**

Replace `leetcode_744/leetcode_744/leetcode_744.csproj` with:

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

Delete these three files individually, then run:

```bash
test ! -e leetcode_744/leetcode_744.sln
test ! -e leetcode_744/leetcode_744/App.config
test ! -e leetcode_744/leetcode_744/Properties/AssemblyInfo.cs
```

Expected: every command exits `0`.

- [ ] **Step 3: Add verified shared configuration and direct VS Code wiring**

Run from the repository root:

```bash
cp leetcode_739/.editorconfig leetcode_744/.editorconfig
cp leetcode_739/.gitattributes leetcode_744/.gitattributes
cp leetcode_739/.gitignore leetcode_744/.gitignore
mkdir -p leetcode_744/.vscode
```

Create `leetcode_744/.vscode/tasks.json` with:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_744",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/leetcode_744/leetcode_744.csproj"
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

Create `leetcode_744/.vscode/launch.json` with:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Debug leetcode_744",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_744",
      "program": "${workspaceFolder}/leetcode_744/bin/Debug/net10.0/leetcode_744.dll",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_744",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
```

- [ ] **Step 4: Write the harness-only RED source**

Replace `leetcode_744/leetcode_744/Program.cs` with:

```csharp
namespace leetcode_744;

internal static class Program
{
    /// <summary>
    /// 744. Find Smallest Letter Greater Than Target
    /// https://leetcode.com/problems/find-smallest-letter-greater-than-target/
    /// 744. 尋找比目標字母大的最小字母
    /// https://leetcode.cn/problems/find-smallest-letter-greater-than-target/
    /// English: Given a sorted array of letters and a target letter, return the smallest letter that is strictly greater than target; if none exists, return the first letter.
    /// 中文：給定已排序的字母陣列與目標字母，回傳嚴格大於目標的最小字母；若不存在，則回傳第一個字母。
    /// </summary>
    /// <param name="args">命令列參數；驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, char[] Letters, char Target, char Expected)[] testCases =
        {
            ("Case 1: Official example (target = 'a')", new[] { 'c', 'f', 'j' }, 'a', 'c'),
            ("Case 2: Official example (target = 'c')", new[] { 'c', 'f', 'j' }, 'c', 'f'),
            ("Case 3: Official example (target = 'd')", new[] { 'c', 'f', 'j' }, 'd', 'f'),
            ("Case 4: Official example (target = 'g')", new[] { 'c', 'f', 'j' }, 'g', 'j'),
            ("Case 5: Official example (target = 'j')", new[] { 'c', 'f', 'j' }, 'j', 'c'),
            ("Case 6: Minimum valid length", new[] { 'a', 'z' }, 'a', 'z'),
            ("Case 7: Duplicate letters", new[] { 'a', 'a', 'b', 'c', 'c' }, 'a', 'b')
        };

        List<CaseResult> checks = new();

        foreach ((string caseName, char[] letters, char target, char expected) in testCases)
        {
            char actual = NextGreatestLetter(letters, target);
            checks.Add(new CaseResult(caseName, $"letters = {FormatLetters(letters)}, target = {FormatChar(target)}", "Next greatest letter", FormatChar(expected), FormatChar(actual), expected == actual));
        }

        char[] maximumLetters = Enumerable.Repeat('m', 10_000).ToArray();
        maximumLetters[^1] = 'z';
        char maximumGreaterActual = NextGreatestLetter(maximumLetters, 'm');
        char maximumWrapActual = NextGreatestLetter(maximumLetters, 'z');
        checks.Add(new CaseResult("Case 8: Maximum-length spot checks", "letters = 9,999 × 'm' followed by 'z'", "Target = 'm'", "'z'", FormatChar(maximumGreaterActual), maximumGreaterActual == 'z'));
        checks.Add(new CaseResult("Case 8: Maximum-length spot checks", "letters = 9,999 × 'm' followed by 'z'", "Target = 'z'", "'m'", FormatChar(maximumWrapActual), maximumWrapActual == 'm'));

        int passedCount = 0;
        Console.WriteLine("LeetCode 744 acceptance harness");

        foreach (CaseResult check in checks)
        {
            Console.WriteLine();
            Console.WriteLine(check.CaseName);
            Console.WriteLine($"Input: {check.Input}");
            Console.WriteLine($"{(check.Passed ? "PASS" : "FAIL")} | {check.CheckName} | Expected: {check.Expected} | Actual: {check.Actual}");

            if (check.Passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{checks.Count} checks passed.");

        if (passedCount != checks.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string FormatChar(char value)
    {
        return $"'{value}'";
    }

    private static string FormatLetters(IEnumerable<char> letters)
    {
        return $"[{string.Join(", ", letters)}]";
    }

    private readonly record struct CaseResult(string CaseName, string Input, string CheckName, string Expected, string Actual, bool Passed);
}
```

- [ ] **Step 5: Run and record the meaningful RED**

Run:

```bash
dotnet build leetcode_744/leetcode_744/leetcode_744.csproj --nologo
```

Expected: exit `1`, `0 warnings`, and `CS0103` errors naming missing `NextGreatestLetter`. This proves the planned behavior is absent; it must not fail on .NET Framework references, project syntax, or legacy assembly metadata.

## Task 2: Implement the pure lower-bound API and prove GREEN

**Files:**
- Modify: `leetcode_744/leetcode_744/Program.cs`
- Test: `leetcode_744/leetcode_744/Program.cs` acceptance harness

**Interfaces:**
- Consumes: calls to `NextGreatestLetter(char[] letters, char target)` from Task 1.
- Produces: `public static char NextGreatestLetter(char[] letters, char target)`, returning the first sorted character strictly greater than `target`, or `letters[0]` after wraparound.

- [ ] **Step 1: Add the minimal implementation and Traditional Chinese XML documentation**

Insert this method after `Main` and before `FormatChar` in `leetcode_744/leetcode_744/Program.cs`:

```csharp
    /// <summary>
    /// 以 lower-bound 二分搜尋找出排序字元陣列中第一個嚴格大於目標字元的位置。有效輸入必須符合題目定義的非空、非遞減小寫字元陣列；若搜尋結果超出尾端，依題意環繞並回傳第一個字元。方法不會修改輸入，也不會寫入主控台。
    /// </summary>
    /// <param name="letters">依非遞減順序排列的題目有效字元陣列。</param>
    /// <param name="target">要尋找其下一個嚴格較大字元的題目有效小寫字元。</param>
    /// <returns>第一個嚴格大於 <paramref name="target"/> 的字元；若不存在則為 <paramref name="letters"/> 的第一個字元。</returns>
    public static char NextGreatestLetter(char[] letters, char target)
    {
        int low = 0;
        int high = letters.Length;

        while (low < high)
        {
            int middle = low + ((high - low) / 2);

            // 區間 [low, high) 始終保留第一個可能嚴格大於 target 的位置。
            if (letters[middle] > target)
            {
                high = middle;
            }
            else
            {
                low = middle + 1;
            }
        }

        return letters[low % letters.Length];
    }
```

- [ ] **Step 2: Verify GREEN with the same build command**

Run:

```bash
dotnet build leetcode_744/leetcode_744/leetcode_744.csproj --nologo
```

Expected: exit `0`, `0 Warning(s)`, and `0 Error(s)`.

- [ ] **Step 3: Execute all deterministic checks**

Run:

```bash
dotnet run --no-build --project leetcode_744/leetcode_744/leetcode_744.csproj
```

Expected: exit `0`; every one of the nine checks prints `PASS`; final line is exactly `Summary: 9/9 checks passed.`.

- [ ] **Step 4: Prove the harness reports failures through its exit code**

Temporarily change only the maximum-case expected value from `'z'` to `'m'`, run `dotnet build leetcode_744/leetcode_744/leetcode_744.csproj --nologo`, then rerun the command and verify exit `1` plus a `FAIL` line. Restore expected `'z'`, build again, rerun the command, and verify `Summary: 9/9 checks passed.`. Do not alter `NextGreatestLetter` during this failure-path check.

## Task 3: Add collaborator documentation and a transcript-accurate README

**Files:**
- Create: `leetcode_744/AGENTS.md`
- Create: `leetcode_744/docs/readme-template.md`
- Create: `leetcode_744/README.md`

**Interfaces:**
- Consumes: the .NET 10 project path and fresh nine-check transcript from Task 2.
- Produces: direct build/debug instructions and a Traditional Chinese README that accurately documents `NextGreatestLetter(char[] letters, char target)`.

- [ ] **Step 1: Create the task-specific collaborator guide**

Create `leetcode_744/AGENTS.md` with the following content:

````markdown
# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. Keep the pure
`NextGreatestLetter` solution, bilingual problem XML summary, and deterministic
acceptance harness in `leetcode_744/Program.cs`. The nested
`leetcode_744/leetcode_744.csproj` defines the executable. From this folder,
run:

```bash
dotnet build leetcode_744/leetcode_744.csproj --nologo
dotnet run --no-build --project leetcode_744/leetcode_744.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_744` VS Code
configuration. Do not use bare `dotnet build` or `dotnet test`: there is no
root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, PascalCase public members, and camelCase local
variables. Preserve the bilingual XML problem summary above `Main`.

Keep `public static char NextGreatestLetter(char[] letters, char target)` pure:
it returns the first sorted letter strictly greater than `target`, wraps to the
first letter when required, does not write to the console, and does not modify
the input. The binary-search invariant is that `[low, high)` still contains the
first possible strictly greater letter. `Main` alone owns acceptance output.

## Testing & Git Scope

The executable harness is the verification mechanism. It must print each case's
input, expected value, actual value, PASS/FAIL, and
`Summary: 9/9 checks passed.` on success with exit code 0. This project has no
separate test framework.

Git metadata is at the parent repository root. Review only this exercise with
`git diff --check -- leetcode_744` and stage only `leetcode_744/` if a future
delivery requests publishing. Keep commits and pull requests scoped to this
folder.
````

- [ ] **Step 2: Add the verified README authoring template**

Run:

```bash
mkdir -p leetcode_744/docs
cp leetcode_739/docs/readme-template.md leetcode_744/docs/readme-template.md
```

- [ ] **Step 3: Capture the sole source of truth for the README transcript**

Run:

```bash
dotnet run --no-build --project leetcode_744/leetcode_744/leetcode_744.csproj > /private/tmp/leetcode_744.actual.txt
tail -n 1 /private/tmp/leetcode_744.actual.txt
```

Expected final line: `Summary: 9/9 checks passed.`

- [ ] **Step 4: Write `leetcode_744/README.md` with the following exact sections and facts**

Use Traditional Chinese and include, in this order:

1. Title: `# LeetCode 744：Find Smallest Letter Greater Than Target／尋找比目標字母大的最小字母`.
2. A .NET 10 overview that states `NextGreatestLetter` uses lower-bound binary search and `Main` owns acceptance output.
3. The exact official links `https://leetcode.com/problems/find-smallest-letter-greater-than-target/` and `https://leetcode.cn/problems/find-smallest-letter-greater-than-target/`.
4. A problem explanation: return the smallest sorted character strictly greater than `target`, or `letters[0]` when no greater character exists.
5. Constraints `2 <= letters.Length <= 10^4`, lowercase characters, and nondecreasing sorted input; do not claim invalid-input behavior.
6. A `[low, high)` invariant section explaining the `>` branch, the `<=` branch, and `low % letters.Length` wraparound.
7. A walk-through of `['c', 'f', 'j']` with target `'d'` that reaches `'f'`, plus an explanation that target `'j'` wraps to `'c'`.
8. Complexity: `O(log n)` time, `O(1)` result space, and `O(1)` auxiliary space.
9. An acceptance-case table with five official checks, one two-character minimum-length check, one repeated-character check, and two maximum-length checks; total `8` cases and `9` checks.
10. The verified commands, from the problem root:

```bash
dotnet build leetcode_744/leetcode_744.csproj --nologo
dotnet run --no-build --project leetcode_744/leetcode_744.csproj
```

11. One and only one fenced block opened by exactly ```text`. Its contents must be the exact bytes of `/private/tmp/leetcode_744.actual.txt`, copied without alteration, and no other `text` fence may exist.
12. A `plaintext` project tree containing `.editorconfig`, `.gitattributes`, `.gitignore`, `.vscode/`, `AGENTS.md`, `README.md`, `docs/readme-template.md`, both `docs/superpowers` records, and the nested `Program.cs` and `.csproj`.

- [ ] **Step 5: Verify transcript extraction and fence uniqueness**

Run:

```bash
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_744/README.md > /private/tmp/leetcode_744.readme.txt
diff -u /private/tmp/leetcode_744.readme.txt /private/tmp/leetcode_744.actual.txt
rg -c '^```text$' leetcode_744/README.md
```

Expected: `diff -u` exits `0` without output and the final command prints exactly `1`.

## Task 4: Perform release-gate verification, review, and complete publication

**Files:**
- Review: every changed path below `leetcode_744/`.
- Modify after merge only: the Issue #2 checkbox for `leetcode_744`.

**Interfaces:**
- Consumes: the green project, exact README transcript, and task-scoped change set from Tasks 1–3.
- Produces: one reviewed and verified commit, a merged squash PR, a read-back Issue #2 checkbox, and a clean, post-merge verified `main`.

- [ ] **Step 1: Run the complete local gate from the worktree root**

Run each command from `/private/tmp/codex-leetcode-744-net10`:

```bash
jq empty leetcode_744/.vscode/launch.json leetcode_744/.vscode/tasks.json
dotnet build leetcode_744/leetcode_744/leetcode_744.csproj --nologo
dotnet run --no-build --project leetcode_744/leetcode_744/leetcode_744.csproj
dotnet run --no-build --project leetcode_744/leetcode_744/leetcode_744.csproj > /private/tmp/leetcode_744.actual.txt
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_744/README.md > /private/tmp/leetcode_744.readme.txt
diff -u /private/tmp/leetcode_744.readme.txt /private/tmp/leetcode_744.actual.txt
rg -c '^```text$' leetcode_744/README.md
git diff --check -- leetcode_744
test ! -e leetcode_744/leetcode_744.sln
test ! -e leetcode_744/leetcode_744/App.config
test ! -e leetcode_744/leetcode_744/Properties/AssemblyInfo.cs
git status --short
```

Expected: JSON commands exit `0`; build has `0 Warning(s)` and `0 Error(s)`; run ends with `Summary: 9/9 checks passed.`; transcript diff has no output; the fence count is `1`; whitespace check exits `0`; every legacy absence command exits `0`; status lists only expected `leetcode_744/` files before staging.

- [ ] **Step 2: Conduct an independent read-only review**

Review the public API, lower-bound invariant, wraparound, all nine checks, XML summaries, README commands and transcript, VS Code nested paths, legacy absence, and change scope. Include untracked artifacts rather than relying only on `git diff`. Resolve every Critical or Important finding, then rerun Step 1 and this review.

- [ ] **Step 3: Amend all task-scoped artifacts into the single feature commit**

Run:

```bash
git add leetcode_744
git diff --cached --check
git diff --cached --name-only
git commit --amend -m "feat(leetcode-744): migrate project to .NET 10"
git rev-list --count origin/main..HEAD
```

Expected: every staged path begins `leetcode_744/`; staged whitespace check exits `0`; the commit count is exactly `1`.

- [ ] **Step 4: Push the branch and create the Draft PR**

Run:

```bash
git push -u origin codex/leetcode-744-net10
gh pr create --repo HyperLee/Leetcode_folder --base main --head codex/leetcode-744-net10 --draft --title "feat(leetcode-744): migrate project to .NET 10" --body "## Migration
- Converted leetcode_744 to an SDK-style .NET 10 console project.
- Replaced the empty entrypoint with a deterministic 9-check acceptance harness.
- Added Traditional Chinese documentation, VS Code setup, and a task-specific collaboration guide.

## Algorithm
- Public API: NextGreatestLetter(char[] letters, char target)
- Invariant: [low, high) contains the first possible strictly greater character.
- Complexity: O(log n) time and O(1) auxiliary space.

## Verification
- VS Code JSON parse check
- dotnet build with 0 warnings and 0 errors
- dotnet run with Summary: 9/9 checks passed.
- README transcript diff and unique text-fence check
- whitespace, scope, and legacy-file absence checks
- Independent read-only review: no unresolved Critical or Important findings

Refs #2"
```

Expected: push succeeds; returned PR URL is Draft and its body contains `Refs #2`, never `Closes #2`.

- [ ] **Step 5: Verify the PR, mark Ready, and squash merge the verified head**

Run:

```bash
git rev-parse HEAD
gh pr view --repo HyperLee/Leetcode_folder --json number,isDraft,headRefOid,mergeStateStatus,statusCheckRollup,files,commits
gh pr ready --repo HyperLee/Leetcode_folder
gh pr merge --repo HyperLee/Leetcode_folder --squash --match-head-commit VERIFIED_HEAD_SHA --delete-branch=false
```

Replace `VERIFIED_HEAD_SHA` with the exact output from `git rev-parse HEAD` only after verifying: the Draft PR head SHA matches it; every PR file is under `leetcode_744/`; exactly one commit exists; merge state is clean; and no check is failed, cancelled, or pending. Expected after merge: GitHub returns `merged: true` and the merge SHA.

- [ ] **Step 6: Update only the post-merge Issue #2 item and read it back**

Read Issue #2 again. Confirm the unchecked `leetcode_744` item appears exactly once. Replace only that line with the checked `leetcode_744` item using the current body, update the issue, and read it back. Confirm the checked line appears once, the unchecked form is absent, and `leetcode_746` remains unchecked.

- [ ] **Step 7: Fast-forward main and execute the post-merge gate**

From `/Users/qiuzili/Leetcode/Leetcode_folder`, run:

```bash
git switch main
git pull --ff-only origin main
git rev-parse HEAD
git rev-parse origin/main
jq empty leetcode_744/.vscode/launch.json leetcode_744/.vscode/tasks.json
dotnet build leetcode_744/leetcode_744/leetcode_744.csproj --nologo
dotnet run --no-build --project leetcode_744/leetcode_744/leetcode_744.csproj > /private/tmp/leetcode_744.postmerge.actual.txt
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_744/README.md > /private/tmp/leetcode_744.postmerge.readme.txt
diff -u /private/tmp/leetcode_744.postmerge.readme.txt /private/tmp/leetcode_744.postmerge.actual.txt
git diff --check HEAD^..HEAD -- leetcode_744
git status --short
```

Expected: local `HEAD` and `origin/main` equal the GitHub merge SHA; build is warning-free; run ends `Summary: 9/9 checks passed.`; transcript diff and whitespace check are clean; final status is empty.

## Plan Self-Review

- **Spec coverage:** Task 1 covers SDK conversion, fixed files, per-file legacy removal, VS Code setup, and a valid RED. Task 2 covers the pure API, lower-bound invariant, wraparound, GREEN evidence, and failure exit path. Task 3 covers AGENTS, README template, Traditional Chinese README, fixed commands, transcript, and fence verification. Task 4 covers full verification, independent review, one-commit amendment, Draft PR, merge checks, Issue update, and post-merge validation.
- **Placeholder scan:** No unspecified API, deferred implementation, or deferred validation is present. The transcript is generated from the harness rather than fabricated.
- **Type consistency:** Every harness call, guide, README rule, and PR body uses `NextGreatestLetter(char[] letters, char target)` returning `char`. The acceptance result is consistently `9` checks and `Summary: 9/9 checks passed.`.
