# leetcode_589 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use `superpowers:executing-plans` or `superpowers:subagent-driven-development` to implement this plan task-by-task.

**Goal:** 將 `leetcode_589` 遷移至 .NET 10，通過 deterministic harness 與文件驗證後完成 GitHub 發布。

**Architecture:** 使用 SDK-style `net10.0` console project；`Preorder` 保持純遞迴 DFS，`Main` 集中輸出 8 個 acceptance checks。

**Tech Stack:** .NET 10、C# nullable、VS Code、GitHub CLI。

## Global Constraints

- 只修改 `leetcode_589/`。
- 保留 `public static IList<int> Preorder(Node? root)`。
- 不新增正式測試專案或第三方套件。
- README 完整輸出只使用一個 `text` fence。
- commit 使用 `feat(leetcode-589): migrate project to .NET 10`。
- Issue #2 僅更新 `leetcode_589` 一行。

## Tasks

### Task 1: Isolate and capture baseline

- 使用 `codex/leetcode-589-net10` 與 `/private/tmp/codex-leetcode-589-net10`。
- 記錄舊專案 `MSB3644` baseline。
- 確認 worktree 初始狀態乾淨且只追蹤 `origin/main`。

### Task 2: Migrate project and implementation

- 將 `.csproj` 改為 SDK-style `net10.0`。
- 移除 `.sln`、`App.config`、`AssemblyInfo.cs`。
- 以 TDD 先驗證 leaf node 失敗，再初始化 `children` 並完成 `Preorder`。
- 建立官方範例、邊界、順序、重複呼叫、1000 層與 invariant checks。

### Task 3: Add docs and run gates

- 新增共用設定、VS Code 設定、AGENTS、README 與本設計/計畫文件。
- 執行 `jq empty`、`dotnet build`、`dotnet run --no-build`、README transcript
  diff、唯一 fence、legacy file 與 `git diff --check` 驗證。
- 執行獨立唯讀 review，修正 Critical/Important 或規格不一致問題。

### Task 4: Publish and close Issue #2

- 建立單一 scoped commit 並 push feature branch。
- 建立 draft PR，review 與 checks 通過後轉 Ready。
- 使用 expected head SHA squash merge。
- merge 成功後精準勾選 Issue #2 的 `leetcode_589`，讀回確認 `leetcode_590` 未勾選。
- fast-forward `main` 並執行合併後完整驗證。
