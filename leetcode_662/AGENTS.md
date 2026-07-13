# Repository Guidelines

## Project Structure & Module Organization

本題目資料夾包含一個 .NET 10 主控台專案。保留 `leetcode_662/Program.cs`
中的 LeetCode 相容節點模型、純粹的 `WidthOfBinaryTree` 解法、雙語 XML 題述與
deterministic acceptance harness。實際可執行專案位於巢狀路徑
`leetcode_662/leetcode_662.csproj`；`.vscode/` 讓本題目根目錄可直接建置與偵錯，
`docs/readme-template.md` 僅供首次建立 README 使用。

## Build, Run, and Development Commands

請從本題目資料夾（`leetcode_662/`）執行：

```bash
dotnet build leetcode_662/leetcode_662.csproj --nologo
dotnet run --no-build --project leetcode_662/leetcode_662.csproj
```

使用 `--no-build` 前必須先建置。VS Code 開啟本資料夾後使用
`Debug leetcode_662`。不要使用裸的 `dotnet build` 或 `dotnet test`；此資料夾沒有
根 project/solution，也沒有正式測試專案。

## Coding Style & Solution Contract

遵循 `.editorconfig`：四空白縮排、file-scoped namespace、PascalCase 成員與
camelCase 區域變數。保留 `TreeNode` 的 `val`、`left`、`right` 公開欄位，以及
`public int WidthOfBinaryTree(TreeNode? root)` instance API。

解法必須保持 console-free、不可改寫輸入樹、不可保存跨呼叫狀態。它以逐層 BFS
配合完整二元樹位置計算寬度；同一層必須先減去最左位置再產生子節點位置，才能同時
保留 null 缺口與避免深樹索引膨脹。`Main` 是唯一輸出驗收資訊的位置。

## Testing Guidelines

沒有額外正式測試專案；`Main` 的 acceptance harness 是驗證機制。八個確定性案例
必須顯示輸入標籤、Expected、Actual 與 PASS/FAIL，全部成功時最後一行必為
`Summary: 8/8 checks passed.`。任一失敗都必須設定 `Environment.ExitCode = 1`。

## Commits and Pull Requests

Git metadata 位於父層 repository root。從該根目錄檢查
`git diff --check -- leetcode_662` 和 `git status --short`，只暫存
`leetcode_662/`。提交訊息使用
`feat(leetcode-662): migrate project to .NET 10`；PR 應說明 per-level
normalization invariant、`O(n)` 時間／空間與已驗證的 8/8 harness 結果。
