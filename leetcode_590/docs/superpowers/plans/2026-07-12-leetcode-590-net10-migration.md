# leetcode_590 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use `superpowers:executing-plans` to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 將 `leetcode_590` 遷移為符合規格的 SDK-style .NET 10 專案，以 8/8 deterministic acceptance checks 驗證 N 元樹後序遍歷，完成 review、PR、squash merge 與 Issue #2 更新。

**Architecture:** 使用題目根目錄 `leetcode_590/` 放置 README、AGENTS、`.vscode` 與文件；巢狀 `leetcode_590/leetcode_590.csproj` 是唯一 executable project。`Postorder(Node? root)` 保持純遞迴 DFS，`Main` 建立案例並集中輸出。

**Tech Stack:** .NET 10 SDK、C# nullable reference types、SDK-style console project、VS Code、GitHub CLI、Git worktree。

## Global Constraints

- 每次只處理一個 `leetcode_xxx` 題目資料夾；本計畫只允許變更 `leetcode_590/`。
- 新專案必須使用 SDK-style `net10.0`，並啟用 `ImplicitUsings` 與 `Nullable`。
- 不建立正式測試專案、共用 solution、第三方套件或題目外重構。
- `public static IList<int> Postorder(Node? root)` 必須保持題目契約；解法與 helper 不得直接輸出 console。
- `Main` 必須集中輸出每個案例的 Input、Expected、Actual、PASS/FAIL，結尾精確輸出 `Summary: 8/8 checks passed.`。
- README 完整執行輸出只能使用一個 `text` fence；其他文字示範使用 `plaintext` fence。
- legacy `.sln`、`App.config`、`Properties/AssemblyInfo.cs` 必須逐一移除。
- commit subject 必須為 `feat(leetcode-590): migrate project to .NET 10`，且相對 `origin/main` 只保留一個功能 commit。
- Issue #2 只有 PR squash merge 成功後才能精準更新 `leetcode_590` 一行。

---

### Task 1: 建立隔離基線並確認題目契約

**Files:**
- Read: `/Users/qiuzili/Leetcode/Leetcode_folder/LEETCODE_NET10_MIGRATION_SPEC.md`
- Read: `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_590/leetcode_590/Program.cs`
- Read: `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_590/leetcode_590/leetcode_590.csproj`
- Read: `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_590/leetcode_590.sln`
- Read: `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_590/leetcode_590/App.config`
- Read: `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_590/leetcode_590/Properties/AssemblyInfo.cs`
- Create: `/private/tmp/codex-leetcode-590-net10/` worktree on `codex/leetcode-590-net10`

**Interfaces:**
- Consumes: clean `origin/main` at base SHA `bf6523b0` and the official N-ary Tree Postorder Traversal contract.
- Produces: isolated branch, recorded legacy baseline, and a target scope limited to `leetcode_590/`.

- [x] **Step 1: Verify the main checkout and remote base**

  Run from `/Users/qiuzili/Leetcode/Leetcode_folder`:

  ```bash
  git status --short --branch
  git rev-parse origin/main
  git pull --ff-only origin main
  ```

  Expected: clean `main`, pull reports already up to date, and the worktree branch is created from `origin/main`.

- [x] **Step 2: Capture the legacy baseline**

  ```bash
  dotnet build leetcode_590/leetcode_590/leetcode_590.csproj --nologo
  ```

  Expected on macOS: exit code 1 with `MSB3644` for `.NETFramework,Version=v4.8`; record it as legacy evidence, not as the TDD RED.

- [x] **Step 3: Confirm Issue #2 scope**

  ```bash
  gh issue view 2 --repo HyperLee/Leetcode_folder --json number,state,body,url --jq .body > /private/tmp/issue-2-body.txt
  rg -n -F -- '- [ ] `leetcode_590`' /private/tmp/issue-2-body.txt
  ```

  Expected: exactly one unchecked `leetcode_590` entry, with `leetcode_589` checked and `leetcode_643` still unchecked.

---

### Task 2: 遷移 SDK project 並建立有效 RED

**Files:**
- Modify: `leetcode_590/leetcode_590/leetcode_590.csproj`
- Modify: `leetcode_590/leetcode_590/Program.cs`
- Delete: `leetcode_590/leetcode_590.sln`
- Delete: `leetcode_590/leetcode_590/App.config`
- Delete: `leetcode_590/leetcode_590/Properties/AssemblyInfo.cs`

**Interfaces:**
- Consumes: legacy project and the `Node` public shape.
- Produces: SDK-style `net10.0` project and a minimal acceptance check that fails because `Postorder` is intentionally absent.

- [x] **Step 1: Replace the old project file**

  Replace `leetcode_590/leetcode_590/leetcode_590.csproj` with:

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

- [x] **Step 2: Remove only the three confirmed legacy files**

  ```bash
  rm "leetcode_590/leetcode_590.sln"
  rm "leetcode_590/leetcode_590/App.config"
  rm "leetcode_590/leetcode_590/Properties/AssemblyInfo.cs"
  ```

- [x] **Step 3: Write the minimal RED harness before production behavior**

  Temporarily replace `Program.cs` with:

  ```csharp
  namespace leetcode_590;

  internal static class Program
  {
      public sealed class Node
      {
          public Node(int value = 0, IList<Node>? childNodes = null)
          {
              val = value;
              children = childNodes ?? [];
          }

          public int val;
          public IList<Node> children;
      }

      private static void Main()
      {
          IList<int> actual = Postorder(new Node(42));
          Console.WriteLine(actual.SequenceEqual([42]) ? "PASS" : "FAIL");
      }
  }
  ```

- [x] **Step 4: Run the RED command and record the implementation-specific failure**

  ```bash
  dotnet build leetcode_590/leetcode_590/leetcode_590.csproj --nologo
  ```

  Expected: exit code 1 with `CS0103` stating that `Postorder` does not exist in the current context. This is the valid TDD RED; it must not be confused with the earlier `MSB3644` baseline.

---

### Task 3: 完成純遞迴解法與 8 組 acceptance harness

**Files:**
- Modify: `leetcode_590/leetcode_590/Program.cs`

**Interfaces:**
- Consumes: SDK-style project and the RED harness from Task 2.
- Produces: `public static IList<int> Postorder(Node? root)`, nullable-safe `Node`, deterministic `Main`, and data-only helpers.

- [x] **Step 1: Replace the RED harness with the complete implementation**

  Use this complete `Program.cs`:

  ```csharp
  namespace leetcode_590;

  internal static class Program
  {
      /// <summary>
      /// 590. N-ary Tree Postorder Traversal；590. N 元樹的後序遍歷。
      /// English URL: https://leetcode.com/problems/n-ary-tree-postorder-traversal/
      /// 中文 URL: https://leetcode.cn/problems/n-ary-tree-postorder-traversal/
      /// Given the root of an n-ary tree, return the postorder traversal of its nodes' values.
      /// 給定 N 元樹的根節點，回傳其節點值的後序走訪結果。
      /// </summary>
      /// <remarks>
      /// 後序遍歷會先依 children 原始順序完成所有子樹，再將目前根節點放入結果。
      /// 本題是 N-way tree，不是只有左右子樹的 binary tree；可參考 leetcode_145 的二元樹版本。
      ///
      /// 590. N-ary Tree Postorder Traversal
      /// https://leetcode.com/problems/n-ary-tree-postorder-traversal/
      /// 590. N 叉树的后序遍历
      /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/description/?envType=daily-question&amp;envId=Invalid%20Date
      ///
      /// 二元樹 binary tree 的後序順序是左子節點、右子節點、根節點；N-way tree 則是依序完成 children，再加入根節點。
      /// 類似題目為 leetcode_145 Binary Tree Postorder Traversal，但本題不是二元樹。
      /// </remarks>
      private static void Main()
      {
          List<CaseResult> cases =
          [
              RunSequenceCase(
                  "Official example 1",
                  "[1,null,3,2,4,null,5,6]",
                  [5, 6, 3, 2, 4, 1],
                  () => Postorder(BuildOfficialExample1())),
              RunSequenceCase(
                  "Official example 2",
                  "[1,null,2,3,4,5,null,null,6,7,null,8,null,9,10,null,null,11,null,12,null,13,null,null,14]",
                  [2, 6, 14, 11, 7, 3, 12, 8, 4, 13, 9, 10, 5, 1],
                  () => Postorder(BuildOfficialExample2())),
              RunSequenceCase(
                  "Minimum: null root",
                  "root = null",
                  [],
                  () => Postorder(null)),
              RunSequenceCase(
                  "Minimum: single leaf",
                  "root = [42]",
                  [42],
                  () => Postorder(new Node(42))),
              RunSequenceCase(
                  "Children keep input order",
                  "root 10 -> children 30, 20, 40; 30 -> child 5",
                  [5, 30, 20, 40, 10],
                  () => Postorder(new Node(10, [new Node(30, [new Node(5)]), new Node(20), new Node(40)]))),
              RunRepeatedInvocationCase(),
              RunDeepChainCase(),
              RunInvariantCase()
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
      /// 表示 N 元樹節點；有效題目輸入會提供 children 集合，空建構子與值建構子則建立空的 children，避免 leaf 節點需要額外 null 判斷。
      /// </summary>
      public sealed class Node
      {
          public Node() : this(0, [])
          {
          }

          public Node(int value) : this(value, [])
          {
          }

          public Node(int value, IList<Node> childNodes)
          {
              val = value;
              children = childNodes;
          }

          public int val;
          public IList<Node> children;
      }

      /// <summary>
      /// 以遞迴 DFS 依 children 原始順序完成所有子樹後再加入根值；有效輸入是符合題目定義的 N 元樹，回傳完整的後序節點值序列，空樹回傳空集合。
      /// </summary>
      /// <remarks>
      /// 官方解法參考：
      /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/1327327/n-cha-shu-de-hou-xu-bian-li-by-leetcode-txesi/
      /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/1459136/by-stormsunshine-tnzr/
      /// 本實作採用遞迴解法。
      /// </remarks>
      public static IList<int> Postorder(Node? root)
      {
          List<int> values = [];
          PostorderVisit(root, values);
          return values;
      }

      /// <summary>
      /// 將目前節點的所有子樹依原始順序追加到結果後，再追加目前節點；node 可以是 null，result 必須是呼叫端提供的可寫入集合，方法不產生主控台輸出。
      /// </summary>
      /// <remarks>
      /// 舊版筆記保留 N-way tree 與 binary tree 的差異：本題不是左右子樹，而是走訪 children 集合；每個孩子完成後才加入根節點。
      /// https://leetcode.cn/problems/n-ary-tree-postorder-traversal/solutions/2645191/jian-dan-dfspythonjavacgojs-by-endlessch-ytdk/
      /// </remarks>
      private static void PostorderVisit(Node? node, IList<int> result)
      {
          if (node is null)
          {
              return;
          }

          // 後序不變量是所有 children 都完成後，根值才可以加入結果。
          foreach (Node child in node.children)
          {
              PostorderVisit(child, result);
          }

          result.Add(node.val);
      }

      private static CaseResult RunSequenceCase(
          string name,
          string input,
          IReadOnlyList<int> expected,
          Func<IList<int>> execute)
      {
          try
          {
              IList<int> actual = execute();
              bool passed = expected.SequenceEqual(actual);
              return new CaseResult(name, input, FormatValues(expected), FormatValues(actual), passed);
          }
          catch (Exception exception)
          {
              return new CaseResult(name, input, FormatValues(expected), exception.GetType().Name, false);
          }
      }

      private static CaseResult RunRepeatedInvocationCase()
      {
          const string name = "Repeated invocations";
          const string input = "first root [1,2], then root [9,8]";
          const string expected = "[2, 1], then [8, 9]";

          try
          {
              IList<int> first = Postorder(new Node(1, [new Node(2)]));
              IList<int> second = Postorder(new Node(9, [new Node(8)]));
              string actual = $"{FormatValues(first)}, then {FormatValues(second)}";
              bool passed = first.SequenceEqual([2, 1]) && second.SequenceEqual([8, 9]);
              return new CaseResult(name, input, expected, actual, passed);
          }
          catch (Exception exception)
          {
              return new CaseResult(name, input, expected, exception.GetType().Name, false);
          }
      }

      private static CaseResult RunDeepChainCase()
      {
          const int nodeCount = 1000;
          const string name = "Height upper-bound spot check";
          const string input = "1000-node chain with values 0..999";
          const string expected = "count=1000, first=999, last=0";

          try
          {
              IList<int> actual = Postorder(BuildChain(nodeCount));
              bool passed = actual.Count == nodeCount
                  && actual[0] == nodeCount - 1
                  && actual[actual.Count - 1] == 0
                  && actual.SequenceEqual(Enumerable.Range(0, nodeCount).Reverse());
              string actualSummary = actual.Count == 0
                  ? "count=0"
                  : $"count={actual.Count}, first={actual[0]}, last={actual[actual.Count - 1]}";
              return new CaseResult(name, input, expected, actualSummary, passed);
          }
          catch (Exception exception)
          {
              return new CaseResult(name, input, expected, exception.GetType().Name, false);
          }
      }

      private static CaseResult RunInvariantCase()
      {
          const string name = "Traversal invariants";
          const string input = "root 1 -> children 2 -> 4 and 3 -> 5";
          const string expected = "count=5, root-last=1, sequence=[4, 2, 5, 3, 1]";
          Node root = new(1, [new Node(2, [new Node(4)]), new Node(3, [new Node(5)])]);

          try
          {
              IList<int> actual = Postorder(root);
              int expectedNodeCount = CountNodes(root);
              bool passed = actual.Count == expectedNodeCount
                  && actual.Count > 0
                  && actual[actual.Count - 1] == root.val
                  && actual.SequenceEqual([4, 2, 5, 3, 1]);
              string actualSummary = actual.Count == 0
                  ? "count=0"
                  : $"count={actual.Count}, root-last={actual[actual.Count - 1]}, sequence={FormatValues(actual)}";
              return new CaseResult(name, input, expected, actualSummary, passed);
          }
          catch (Exception exception)
          {
              return new CaseResult(name, input, expected, exception.GetType().Name, false);
          }
      }

      private static Node BuildOfficialExample1()
      {
          return new Node(1, [new Node(3, [new Node(5), new Node(6)]), new Node(2), new Node(4)]);
      }

      private static Node BuildOfficialExample2()
      {
          Node node14 = new(14);
          Node node11 = new(11, [node14]);
          Node node7 = new(7, [node11]);
          Node node3 = new(3, [new Node(6), node7]);
          Node node12 = new(12);
          Node node8 = new(8, [node12]);
          Node node4 = new(4, [node8]);
          Node node13 = new(13);
          Node node9 = new(9, [node13]);
          Node node5 = new(5, [node9, new Node(10)]);

          return new Node(1, [new Node(2), node3, node4, node5]);
      }

      private static Node BuildChain(int count)
      {
          Node root = new(0);
          Node current = root;

          for (int value = 1; value < count; value++)
          {
              Node child = new(value);
              current.children.Add(child);
              current = child;
          }

          return root;
      }

      private static int CountNodes(Node? node)
      {
          if (node is null)
          {
              return 0;
          }

          return 1 + node.children.Sum(CountNodes);
      }

      private static string FormatValues(IEnumerable<int> values)
      {
          return $"[{string.Join(", ", values)}]";
      }

      private sealed record CaseResult(
          string Name,
          string Input,
          string Expected,
          string Actual,
          bool Passed);
  }
  ```

- [x] **Step 2: Run the GREEN build and harness**

  ```bash
  dotnet build leetcode_590/leetcode_590/leetcode_590.csproj --nologo
  dotnet run --no-build --project leetcode_590/leetcode_590/leetcode_590.csproj
  ```

  Expected: build exit code 0 with 0 warnings and 0 errors; run exit code 0 with 8 PASS lines for the cases in the code and `Summary: 8/8 checks passed.`.

- [x] **Step 3: Confirm failure propagation**

  Temporarily change the expected value in the single-leaf case from `[42]` to `[41]`, run the same `dotnet run --no-build` command, and verify a `FAIL` plus exit code 1. Restore `[42]`, rerun, and keep the green output as the source for README.

---

### Task 4: 加入固定產物、README 與 VS Code 設定

**Files:**
- Create: `leetcode_590/.editorconfig`
- Create: `leetcode_590/.gitattributes`
- Create: `leetcode_590/.gitignore`
- Create: `leetcode_590/.vscode/launch.json`
- Create: `leetcode_590/.vscode/tasks.json`
- Create: `leetcode_590/AGENTS.md`
- Create: `leetcode_590/README.md`
- Create: `leetcode_590/docs/readme-template.md`

**Interfaces:**
- Consumes: final `Program.cs` API, exact 8-case output, and shared configuration pattern from `leetcode_589`.
- Produces: directly runnable problem-folder workspace and documentation whose transcript matches a fresh run byte-for-byte.

- [x] **Step 1: Reuse the validated common configuration**

  Copy the already validated shared files from `leetcode_589` into the target folder, then verify their paths:

  ```bash
  cp leetcode_589/.editorconfig leetcode_590/.editorconfig
  cp leetcode_589/.gitattributes leetcode_590/.gitattributes
  cp leetcode_589/.gitignore leetcode_590/.gitignore
  mkdir -p leetcode_590/.vscode leetcode_590/docs
  cp leetcode_589/docs/readme-template.md leetcode_590/docs/readme-template.md
  ```

- [x] **Step 2: Create exact VS Code task and launch configuration**

  `leetcode_590/.vscode/tasks.json` must contain one default task whose label is `build leetcode_590` and whose project argument is `${workspaceFolder}/leetcode_590/leetcode_590.csproj`.

  `leetcode_590/.vscode/launch.json` must contain one `coreclr` configuration with `preLaunchTask` `build leetcode_590`, program `${workspaceFolder}/leetcode_590/bin/Debug/net10.0/leetcode_590.dll`, cwd `${workspaceFolder}/leetcode_590`, `console` `integratedTerminal`, and `stopAtEntry` false.

- [x] **Step 3: Write `AGENTS.md` with executable problem-specific guidance**

  Document the nested project path, problem-folder build/run commands, absence of a formal test project, four-space C# style, pure `Postorder` contract, postorder invariant, 8/8 acceptance condition, parent-repository Git metadata, and scoped commit/PR rule.

- [x] **Step 4: Write the Traditional Chinese README**

  Include the bilingual title `590. N-ary Tree Postorder Traversal / N 元樹的後序遍歷`, both official links, English/Chinese problem description, constraints `[0, 10^4]` and height `<= 1000`, recursive DFS invariant, complexity `O(n)` time / `O(n)` result space / `O(h)` auxiliary stack space, a plaintext walkthrough for example 1, an 8-row acceptance table, exact problem-folder commands, the complete fresh output in exactly one `text` fence, and the final nested project tree.

  Generate the transcript from the verified run instead of typing it manually:

  ```bash
  dotnet run --no-build --project leetcode_590/leetcode_590/leetcode_590.csproj > /private/tmp/leetcode_590.actual.txt
  ```

  The README `text` fence must be the only fence extracted by:

  ```bash
  awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_590/README.md > /private/tmp/leetcode_590.readme.txt
  ```

---

### Task 5: 完整本地驗證、唯讀 review 與單一 commit

**Files:**
- Review: all files under `leetcode_590/`
- Modify: only files under `leetcode_590/` if review finds a defect

**Interfaces:**
- Consumes: completed migration artifacts and fresh run output.
- Produces: review-clean branch, one scoped commit, and a verified head SHA suitable for draft PR.

- [x] **Step 1: Run the complete verification bundle**

  ```bash
  jq empty leetcode_590/.vscode/launch.json leetcode_590/.vscode/tasks.json
  dotnet build leetcode_590/leetcode_590/leetcode_590.csproj --nologo
  dotnet run --no-build --project leetcode_590/leetcode_590/leetcode_590.csproj
  dotnet run --no-build --project leetcode_590/leetcode_590/leetcode_590.csproj > /private/tmp/leetcode_590.actual.txt
  awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_590/README.md > /private/tmp/leetcode_590.readme.txt
  diff -u /private/tmp/leetcode_590.readme.txt /private/tmp/leetcode_590.actual.txt
  test "$(rg -c '^```text$' leetcode_590/README.md)" -eq 1
  test ! -e leetcode_590/leetcode_590.sln
  test ! -e leetcode_590/leetcode_590/App.config
  test ! -e leetcode_590/leetcode_590/Properties/AssemblyInfo.cs
  git diff --check -- leetcode_590
  git status --short
  ```

  Expected: all commands exit 0, build has 0 warnings/0 errors, run has 8/8 PASS, transcript diff is empty, exactly one `text` fence exists, legacy files are absent, and all status paths are under `leetcode_590/`.

- [x] **Step 2: Perform an independent read-only review**

  Inspect the complete diff including untracked files. Check public API, null handling, child ordering, 1000-node boundary, helper XML summaries, key algorithm comment, Main-only console output, README transcript, VS Code paths, legacy absence, and scope. Do not modify, commit, push, create PR, or update Issue during the review.

- [x] **Step 3: Repair review findings and rerun the complete bundle**

  For every Critical, Important, or spec-inconsistent finding, edit the target files, rerun the full bundle, and repeat the read-only review until no actionable findings remain.

- [ ] **Step 4: Create the single scoped commit**

  ```bash
  git add leetcode_590
  git diff --cached --check
  git diff --cached --name-only
  git commit -m "feat(leetcode-590): migrate project to .NET 10"
  git rev-list --count origin/main..HEAD
  ```

  Expected: staged paths are exclusively under `leetcode_590/`, cached whitespace check is clean, and commit count is exactly `1`.

---

### Task 6: Draft PR、merge、Issue #2 與合併後驗證

**Files:**
- Modify remotely: GitHub PR for `codex/leetcode-590-net10`
- Modify remotely after merge: Issue #2 only at the unique `leetcode_590` checkbox line
- Modify locally after merge: parent `main` ref only through fast-forward

**Interfaces:**
- Consumes: verified single-commit head SHA and review result from Task 5.
- Produces: merged squash commit on `main`, checked Issue #2 entry, and clean post-merge verification.

- [ ] **Step 1: Push and create the draft PR**

  ```bash
  git push -u origin codex/leetcode-590-net10
  gh pr create --repo HyperLee/Leetcode_folder --base main --head codex/leetcode-590-net10 --draft --title "feat(leetcode-590): migrate project to .NET 10" --body-file /private/tmp/leetcode_590-pr.md
  ```

  The PR body must list the migration, recursive postorder invariant, complexities, exact JSON/build/run/transcript/scope checks, read-only review result, and `Refs #2` (never `Closes #2`).

- [ ] **Step 2: Verify PR head and checks before Ready**

  ```bash
  PR_NUMBER="$(gh pr view codex/leetcode-590-net10 --repo HyperLee/Leetcode_folder --json number --jq .number)"
  gh pr view "$PR_NUMBER" --repo HyperLee/Leetcode_folder --json number,headRefName,headRefOid,isDraft,mergeStateStatus,statusCheckRollup,files,commits
  ```

  Confirm head SHA equals the locally verified commit, changed files are scoped, commit count is one, merge state is clean, and no checks are failed or pending. Then mark Ready:

  ```bash
  gh pr ready "$PR_NUMBER" --repo HyperLee/Leetcode_folder
  ```

- [ ] **Step 3: Squash merge with expected head SHA**

  ```bash
  VERIFIED_HEAD_SHA="$(git rev-parse HEAD)"
  gh pr merge "$PR_NUMBER" --repo HyperLee/Leetcode_folder --squash --delete-branch=false --subject "feat(leetcode-590): migrate project to .NET 10" --body "Migrate leetcode_590 to SDK-style .NET 10 with deterministic acceptance checks. Refs #2" --match-head-commit "$VERIFIED_HEAD_SHA"
  ```

  Continue only when GitHub reports `merged: true` and returns a merge SHA.

- [ ] **Step 4: Update Issue #2 exactly once after merge**

  ```bash
  gh issue view 2 --repo HyperLee/Leetcode_folder --json body --jq .body > /private/tmp/issue-2-before.txt
  rg -n -F -- '- [ ] `leetcode_590`' /private/tmp/issue-2-before.txt
  ```

  Confirm exactly one unchecked target line, replace only that line with `- [x] `leetcode_590`` using a targeted patch, update the issue body, then read it back. Verify `leetcode_590` is checked and `leetcode_643` remains unchecked.

- [ ] **Step 5: Fast-forward local main and verify the merged result**

  ```bash
  git switch main
  git pull --ff-only origin main
  git rev-parse HEAD origin/main
  jq empty leetcode_590/.vscode/launch.json leetcode_590/.vscode/tasks.json
  dotnet build leetcode_590/leetcode_590/leetcode_590.csproj --nologo
  dotnet run --no-build --project leetcode_590/leetcode_590/leetcode_590.csproj
  git diff --check -- leetcode_590
  git status --short
  ```

  Expected: local `HEAD` equals `origin/main` and the returned merge SHA, build/run/JSON/whitespace checks pass, and `git status --short` is empty. Preserve the feature worktree until this post-merge verification is complete.

## Plan Self-Review

- Spec sections 5-8: Task 1 records issue/base/baseline; Task 2 migrates the project and removes all three legacy files; Task 4 adds fixed artifacts and problem-root VS Code paths.
- Spec sections 9-11: Task 3 preserves the API, documents all major non-Main functions, centralizes console output, covers official/minimum/boundary/invariant cases, and Task 4 derives the README transcript from a fresh run.
- Spec sections 12-13: Task 5 runs all gates and performs an independent read-only review, including untracked files.
- Spec sections 14-16: Task 5 enforces one scoped commit; Task 6 handles draft/Ready PR, expected-head squash merge, Issue update after merge, and post-merge verification.
- Spec sections 17-18: every stop condition has an explicit verification or pause point, and no bulk deletion command is used.
- Placeholder scan: no incomplete placeholder, angle-bracket token, or unspecified test command is used; shell variables are populated from live Git and GitHub readbacks immediately before use.
- Type consistency: all harness cases call `Postorder(Node? root)` and all builders return `Node`; output count is fixed at eight cases.
