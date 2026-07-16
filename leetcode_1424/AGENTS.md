# Repository Guidelines

## Project Structure & Module Organization

本題目資料夾包含一個 .NET 10 主控台專案。`leetcode_1424/Program.cs` 保存純粹的
`FindDiagonalOrder` 解法、雙語 XML 題述與 deterministic acceptance harness。實際
專案位於巢狀路徑 `leetcode_1424/leetcode_1424.csproj`；`.vscode/` 支援從本題目
根目錄直接建置與偵錯，`docs/readme-template.md` 只供首次建立 README 使用。

## Build, Run, and Development Commands

請從本題目資料夾（外層 `leetcode_1424/`）執行：

```bash
dotnet build leetcode_1424/leetcode_1424.csproj --nologo
dotnet run --no-build --project leetcode_1424/leetcode_1424.csproj
```

使用 `--no-build` 前必須先建置。VS Code 直接開啟本資料夾後使用
`Debug leetcode_1424`。不要使用裸的 `dotnet build` 或 `dotnet test`；本題沒有根
project/solution，也沒有正式測試專案。

## Coding Style & Solution Contract

遵循 `.editorconfig`：四空白縮排、file-scoped namespace、PascalCase 成員與
camelCase 區域變數。保留公開 API
`public static int[] FindDiagonalOrder(IList<IList<int>> nums)`。

解法不得輸出主控台或修改輸入。元素屬於索引 `row + column` 的對角線 bucket；反向
走訪列並在列內由左向右走訪，讓每個 bucket 自然保持由下往上的順序。`Main` 是唯一
輸出驗收資訊的位置。

## Testing Guidelines

本題沒有額外測試專案；`Main` 的 acceptance harness 是驗證機制。七個確定性案例
必須顯示案例、Input、Expected、Actual 與 PASS/FAIL，全部成功時最後一行必為
`Summary: 7/7 checks passed.`。任一失敗都必須設定 `Environment.ExitCode = 1`。

## Commits and Pull Requests

Git metadata 位於父層 repository root。從該根目錄檢查
`git diff --check -- leetcode_1424` 與 `git status --short`，只暫存
`leetcode_1424/`。提交訊息使用
`feat(leetcode-1424): migrate project to .NET 10`；PR 應說明對角線 bucket 不變量、
`O(N)` 時間、`O(N + R + C)` 輔助空間與已驗證的 7/7 harness 結果。
