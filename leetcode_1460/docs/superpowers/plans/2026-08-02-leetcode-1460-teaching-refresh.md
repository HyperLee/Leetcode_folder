# LeetCode 1460 Teaching Refresh Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add deterministic console examples, complete Traditional Chinese teaching documentation, and verified execution records to the existing LeetCode 1460 .NET 10 project without changing either solution API or algorithm behavior.

**Architecture:** Keep `CanBeEqual` as the in-place sorting solution and `CanBeEqual2` as the frequency-counting solution. Put the reusable console assertion and input-copy logic in a documented `RunCase` helper called by `Main`, then document both algorithms and the fresh console transcript in a new README.

**Tech Stack:** C# console application, .NET 10 (`net10.0`), implicit framework usings, Markdown.

## Global Constraints

- Preserve the existing problem-description XML `<summary>` text in `leetcode_1460/Program.cs`.
- Preserve the existing `CanBeEqual(int[] target, int[] arr)` and `CanBeEqual2(int[] target, int[] arr)` signatures and core behavior.
- Clone `target` and `arr` in the console test harness before each algorithm invocation; do not silently change the sorting method into a non-mutating method.
- Add only meaningful algorithm comments beside the sorted comparison, frequency accumulation, and over-consumption checks.
- Use Traditional Chinese for new XML remarks, comments, and `README.md` teaching content.
- Do not add a test project, external package, or unrelated refactor.
- Use the explicit project path `leetcode_1460/leetcode_1460.csproj` for restore, build, and run commands.
- Do not include `Console.ReadKey()` in the executable harness.
- Keep generated `bin/` and `obj/` directories untracked.
- Finish with fresh restore, build, run, and `git diff --check` verification.

## File Map

- Modify: `leetcode_1460/Program.cs` — deterministic test cases, `RunCase`, XML documentation additions, and focused algorithm comments.
- Create: `README.md` — Traditional Chinese problem, constraints, algorithm explanations, examples, commands, and actual transcript.
- Existing reference: `docs/readme-template.md` — README creation structure and verification expectations.
- Existing design: `docs/superpowers/specs/2026-08-02-leetcode-1460-teaching-refresh-design.md` — approved scope and decisions.

### Task 1: Add the executable console teaching harness

**Files:**

- Modify: `leetcode_1460/Program.cs`
- Test: `dotnet build leetcode_1460/leetcode_1460.csproj --nologo`
- Test: `dotnet run --project leetcode_1460/leetcode_1460.csproj`

**Interfaces:**

- Consumes: existing public static methods `CanBeEqual(int[] target, int[] arr)` and `CanBeEqual2(int[] target, int[] arr)`.
- Produces: private static `RunCase(string name, int[] target, int[] arr, bool expected)` returning `bool`; `Main` uses its return value to set `Environment.ExitCode`.

- [ ] **Step 1: Replace the single shared example with fixed test data and aggregate results**

  In `Main`, use a tuple array with these exact cases and expected values:

  ```csharp
  var testCases = new[]
  {
      (Name: "一般排列順序不同", Target: new[] { 1, 2, 3, 4 }, Arr: new[] { 2, 4, 1, 3 }, Expected: true),
      (Name: "重複值頻率相同", Target: new[] { 1, 1, 2, 3 }, Arr: new[] { 3, 1, 2, 1 }, Expected: true),
      (Name: "重複值頻率不同", Target: new[] { 1, 1, 2, 3 }, Arr: new[] { 1, 2, 2, 3 }, Expected: false),
      (Name: "單一元素邊界", Target: new[] { 1000 }, Arr: new[] { 1000 }, Expected: true),
      (Name: "空陣列額外案例", Target: Array.Empty<int>(), Arr: Array.Empty<int>(), Expected: true)
  };

  bool allPassed = true;
  foreach (var testCase in testCases)
  {
      allPassed &= RunCase(testCase.Name, testCase.Target, testCase.Arr, testCase.Expected);
  }

  Console.WriteLine($"全部案例: {(allPassed ? "PASS" : "FAIL")}");
  Environment.ExitCode = allPassed ? 0 : 1;
  ```

  Keep the existing `Main(string[] args)` signature and leave its problem-description `<summary>` unchanged. Remove `Console.ReadKey()` so the command exits without user input.

- [ ] **Step 2: Add `RunCase` with independent inputs for both algorithms**

  Add this private method after `Main`:

  ```csharp
  /// <summary>
  /// 執行單一案例，使用獨立輸入驗證兩種解法並回報是否符合預期。
  /// </summary>
  /// <param name="name">案例名稱。</param>
  /// <param name="target">目標陣列。</param>
  /// <param name="arr">待比較的陣列。</param>
  /// <param name="expected">案例預期結果。</param>
  /// <returns>兩種解法都符合預期時回傳 true，否則回傳 false。</returns>
  private static bool RunCase(string name, int[] target, int[] arr, bool expected)
  {
      bool sortResult = CanBeEqual((int[])target.Clone(), (int[])arr.Clone());
      bool countResult = CanBeEqual2((int[])target.Clone(), (int[])arr.Clone());
      bool passed = sortResult == expected && countResult == expected;

      Console.WriteLine($"案例：{name}");
      Console.WriteLine($"target = [{string.Join(", ", target)}]");
      Console.WriteLine($"arr = [{string.Join(", ", arr)}]");
      Console.WriteLine($"Expected: {expected}");
      Console.WriteLine($"CanBeEqual Actual: {sortResult}");
      Console.WriteLine($"CanBeEqual2 Actual: {countResult}");
      Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
      Console.WriteLine();

      return passed;
  }
  ```

  Use separate clones for both calls even though `CanBeEqual2` currently does not mutate its inputs; this keeps the fixture contract explicit and prevents future implementation details from coupling the cases.

- [ ] **Step 3: Supplement XML documentation without rewriting the original problem summaries**

  Keep each current `<summary>` block exactly as it is. Add Traditional Chinese `<remarks>` text after the existing summaries for `Main`, `CanBeEqual`, and `CanBeEqual2`, describing the entry point or algorithm, input assumptions, output meaning, and the sorting method's input mutation. Fill the existing `<param>` and `<returns>` elements with concise descriptions. Keep `RunCase` documentation consistent with the signature above.

- [ ] **Step 4: Add only the three high-signal algorithm comments**

  Keep comments focused on decisions rather than statements:

  ```csharp
  // 反轉只能改變元素順序，排序後比較即可確認元素與出現次數都相同。
  // 先累計 target 中每個值的可用次數，再用 arr 逐一扣除。
  // 找不到值或扣除後變成負數，代表 arr 使用了 target 沒有或過量的元素。
  ```

  Place them beside the existing sort comparison, dictionary accumulation, and negative-count branch respectively. Do not add comments to every loop or variable assignment.

- [ ] **Step 5: Compile and run the harness before documenting its output**

  Run:

  ```bash
  dotnet build leetcode_1460/leetcode_1460.csproj --nologo
  dotnet run --project leetcode_1460/leetcode_1460.csproj
  ```

  Expected: build exits with code 0; all five cases print `Result: PASS`; the final line is `全部案例: PASS`; the run exits with code 0. Preserve the complete fresh run output for Task 2's README transcript.

- [ ] **Step 6: Commit the isolated program change**

  ```bash
  git add leetcode_1460/Program.cs
  git commit -m "refactor: add leetcode 1460 teaching cases"
  ```

### Task 2: Create the Traditional Chinese README

**Files:**

- Create: `README.md`
- Read: `docs/readme-template.md`
- Reference: the fresh output captured in Task 1, Step 5

**Interfaces:**

- Consumes: the two public solution methods, the five `Main` cases, and the exact documented commands.
- Produces: a README that can be followed from the repository root without undocumented setup or unsupported claims.

- [ ] **Step 1: Write the problem and constraint sections**

  Start with a title for LeetCode 1460 and a short Traditional Chinese description. Include the existing English and Chinese problem links. Explain that any contiguous subarray of `arr` may be reversed repeatedly and the goal is to determine whether `arr` can become `target`.

  Record the formal constraints exactly:

  - `1 <= target.length, arr.length <= 1000`
  - `target.length == arr.length`
  - `1 <= target[i], arr[i] <= 1000`

  Add a note that `Main` also runs two empty arrays as an extra implementation-behavior teaching case, not as an official constraint case.

- [ ] **Step 2: Explain the shared insight and method comparison**

  State that reversing changes only order, so it cannot change the multiset of values. Explain why equal lengths plus equal value frequencies are sufficient, then use a compact table comparing sorting and dictionary counting by time, extra space, and input mutation.

- [ ] **Step 3: Document the sorting solution in detail**

  Explain these exact steps:

  1. Sort `target` in place.
  2. Sort `arr` in place.
  3. Compare corresponding positions and return `false` on the first mismatch.
  4. Return `true` if every position matches.

  Include the `[1, 2, 3, 4]` and `[2, 4, 1, 3]` walkthrough: both become `[1, 2, 3, 4]`. Document `O(n log n)` time, the in-place behavior, and the caveat that callers needing original ordering must pass a copy.

- [ ] **Step 4: Document the dictionary counting solution in detail**

  Explain these exact steps:

  1. Count each value in `target`.
  2. Scan `arr` and reject values absent from the dictionary.
  3. Decrement the matched count.
  4. Reject when a count becomes negative.
  5. Return `true` after the equal-length input has been fully consumed.

  Include the duplicate-value walkthrough with `target = [1, 1, 2, 3]` and `arr = [3, 1, 2, 1]`, showing the count changes and the successful result. Include the frequency-mismatch case `arr = [1, 2, 2, 3]`, showing why the second `2` makes the result false. Document `O(n)` time and `O(k)` extra space.

- [ ] **Step 5: Document the executable harness, structure, commands, and actual transcript**

  Explain that every case sends fresh clones to each algorithm so `CanBeEqual`'s in-place sort cannot affect `CanBeEqual2`. List the project files and state that no automated test project exists.

  Include these exact commands from the repository root:

  ```bash
  dotnet restore leetcode_1460/leetcode_1460.csproj
  dotnet build leetcode_1460/leetcode_1460.csproj --nologo
  dotnet run --project leetcode_1460/leetcode_1460.csproj
  git diff --check
  ```

  Insert the complete output captured from Task 1, Step 5 without changing labels, case names, expected values, actual values, or PASS/FAIL text. Do not claim that `dotnet test` is available.

- [ ] **Step 6: Review README against the implementation and commit it**

  Check every method name, path, constraint, complexity, mutation note, example, command, and transcript line against `Program.cs` and the fresh run output. Then run:

  ```bash
  git add README.md
  git commit -m "docs: add leetcode 1460 README"
  ```

### Task 3: Perform the final repository verification

**Files:**

- Verify: `leetcode_1460/Program.cs`
- Verify: `README.md`
- Verify: repository worktree and staged history

**Interfaces:**

- Consumes: the committed program and README from Tasks 1 and 2.
- Produces: fresh command evidence that the documented workflow and whitespace checks pass.

- [ ] **Step 1: Restore the explicit project**

  Run:

  ```bash
  dotnet restore leetcode_1460/leetcode_1460.csproj
  ```

  Expected: exit code 0 with no restore failure.

- [ ] **Step 2: Build the explicit project**

  Run:

  ```bash
  dotnet build leetcode_1460/leetcode_1460.csproj --nologo
  ```

  Expected: exit code 0 and no compilation errors.

- [ ] **Step 3: Run the documented examples**

  Run:

  ```bash
  dotnet run --project leetcode_1460/leetcode_1460.csproj
  ```

  Expected: five cases, two actual values per case, `Result: PASS` for every case, and final `全部案例: PASS`; compare the complete output with the README transcript.

- [ ] **Step 4: Check whitespace and worktree state**

  Run:

  ```bash
  git diff --check
  git status --short --branch
  ```

  Expected: `git diff --check` produces no output and the status shows the intended branch state without generated `bin/` or `obj/` files. If the files are committed as planned, the worktree is clean.

- [ ] **Step 5: Record the final evidence**

  Re-read the README commands and transcript after the final run. Report the exact restore, build, run, and diff-check outcomes, the changed files, and any limitation such as the absence of an automated test project.
