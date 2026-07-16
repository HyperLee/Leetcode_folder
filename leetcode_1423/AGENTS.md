# Repository Guidelines

## Project Structure & Module Organization

本題目資料夾包含一個 .NET 10 主控台專案。`leetcode_1423/Program.cs` 保存純粹的
`MaxScore` 解法、雙語 XML 題述與 deterministic acceptance harness。實際專案位於
巢狀路徑 `leetcode_1423/leetcode_1423.csproj`；`.vscode/` 支援從本題目根目錄直接
建置與偵錯，`docs/readme-template.md` 只供首次建立 README 使用。

## Build, Run, and Development Commands

請從本題目資料夾（外層 `leetcode_1423/`）執行：

```bash
dotnet build leetcode_1423/leetcode_1423.csproj --nologo
dotnet run --no-build --project leetcode_1423/leetcode_1423.csproj
```

使用 `--no-build` 前必須先建置。VS Code 直接開啟本資料夾後使用
`Debug leetcode_1423`。不要使用裸的 `dotnet build` 或 `dotnet test`；本題沒有根
project/solution，也沒有正式測試專案。

## Coding Style & Solution Contract

遵循 `.editorconfig`：四空白縮排、file-scoped namespace、PascalCase 成員與
camelCase 區域變數。保留公開 API
`public static int MaxScore(int[] cardPoints, int k)`。

解法不得輸出主控台或修改輸入。長度 `n-k` 的滑動視窗代表未取走的連續中段；找出
最小中段總和後，以全部點數減去它即為最大得分。`Main` 是唯一輸出驗收資訊的位置。

## Testing Guidelines

本題沒有額外測試專案；`Main` 的 acceptance harness 是驗證機制。八個確定性案例
必須顯示案例、Input、Expected、Actual 與 PASS/FAIL，全部成功時最後一行必為
`Summary: 8/8 checks passed.`。任一失敗都必須設定 `Environment.ExitCode = 1`。

## Commits and Pull Requests

Git metadata 位於父層 repository root。從該根目錄檢查
`git diff --check -- leetcode_1423` 與 `git status --short`，只暫存
`leetcode_1423/`。提交訊息使用
`feat(leetcode-1423): migrate project to .NET 10`；PR 應說明補集滑動視窗不變量、
`O(n)` 時間、`O(1)` 輔助空間與已驗證的 8/8 harness 結果。
