# leetcode_872 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 872「Leaf-Similar Trees／葉子相似的樹」從舊式 .NET Framework 4.8 主控台專案遷移成可在 .NET 10 建置、執行、驗證與完整發布的單題專案。所有 tracked changes 僅限 `leetcode_872/`。

## 專案與 API

- 使用 SDK-style `net10.0` console project，啟用 implicit usings 與 nullable。
- 保留 `public static bool LeafSimilar(TreeNode root1, TreeNode root2)`。
- `TreeNode.left` 與 `TreeNode.right` 使用 nullable reference。
- 私有 `CollectLeaves(TreeNode node, IList<int> leaves)` 以左子樹、右子樹順序收集葉值。
- `LeafSimilar` 比較兩份葉序列，不修改輸入、不輸出，也不新增題目契約以外的 invalid-input 行為。

## Acceptance Harness

`Main` 是唯一 console I/O 邊界，固定執行九項檢查：兩個官方範例、單節點相同與不同、不同樹形但同葉序列、葉集合相同但順序不同、重複葉值、0/200 值域邊界，以及 200 節點深鏈。每項輸出 Input、Expected、Actual 與 PASS/FAIL；失敗設定 `Environment.ExitCode = 1`，全綠結尾為 `Summary: 9/9 checks passed.`。

## 文件、驗證與發布

題目根目錄補齊共用設定、VS Code、`AGENTS.md`、繁中 `README.md` 與 README 範本；逐檔移除舊 `.sln`、`App.config`、`Properties/AssemblyInfo.cs`。先以缺少 `LeafSimilar` 的 `CS0103` 證明 RED，再完成 GREEN、failure-path 檢查、README transcript 精確比對與獨立唯讀審查。最終維持單一 commit，經 Draft PR、Ready 與 expected-head squash merge 後才更新 Issue #2。

## 自審

- API、九項檢查、複雜度與發布順序均已鎖定。
- 無 placeholder 或未決需求。
- 不修改其他題目或 repository 根目錄的 tracked files。
