# Repository Guidelines

## Project Structure & Module Organization

本題目資料夾包含一個 .NET 10 主控台專案。`leetcode_1372/Program.cs` 保存
LeetCode 相容的 `TreeNode`、純粹的 `LongestZigZag` 解法、雙語 XML 題述與
deterministic acceptance harness。實際專案位於巢狀路徑
`leetcode_1372/leetcode_1372.csproj`；`.vscode/` 支援從本題目根目錄直接建置與
偵錯，`docs/readme-template.md` 只供首次建立 README 使用。

## Build, Run, and Development Commands

請從本題目資料夾（外層 `leetcode_1372/`）執行：

```bash
dotnet build leetcode_1372/leetcode_1372.csproj --nologo
dotnet run --no-build --project leetcode_1372/leetcode_1372.csproj
```

使用 `--no-build` 前必須先建置。VS Code 直接開啟本資料夾後使用
`Debug leetcode_1372`。不要使用裸的 `dotnet build` 或 `dotnet test`；本題沒有根
project/solution，也沒有正式測試專案。

## Coding Style & Solution Contract

遵循 `.editorconfig`：四空白縮排、file-scoped namespace、PascalCase 成員與
camelCase 區域變數。保留 `TreeNode` 的 `val`、`left`、`right` 公開欄位，以及
`public int LongestZigZag(TreeNode? root)` instance API。

解法不得輸出主控台、改寫輸入樹或保存跨呼叫狀態。迭代 DFS 的每個狀態必須保留
上一條邊方向；下一條邊交替時累加長度，同方向時從 1 重新起算。`Main` 是唯一輸出
驗收資訊的位置。

## Testing Guidelines

本題沒有額外測試專案；`Main` 的 acceptance harness 是驗證機制。九個確定性案例
必須顯示案例、Expected、Actual 與 PASS/FAIL，全部成功時最後一行必為
`Summary: 9/9 checks passed.`。任一失敗都必須設定 `Environment.ExitCode = 1`。

## Commits and Pull Requests

Git metadata 位於父層 repository root。從該根目錄檢查
`git diff --check -- leetcode_1372` 與 `git status --short`，只暫存
`leetcode_1372/`。提交訊息使用
`feat(leetcode-1372): migrate project to .NET 10`；PR 應說明方向狀態不變量、
`O(n)` 時間、`O(h)` 輔助空間與已驗證的 9/9 harness 結果。
