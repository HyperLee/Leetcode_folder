# Repository Guidelines

## Project Structure and Commands

此資料夾是一個題目根目錄；可執行的巢狀 .NET 10 專案是
`leetcode_1464/leetcode_1464.csproj`。從本資料夾執行：

```bash
dotnet build leetcode_1464/leetcode_1464.csproj --nologo
dotnet run --no-build --project leetcode_1464/leetcode_1464.csproj
```

使用 `--no-build` 前必須先建置。VS Code 請使用 `Debug leetcode_1464` 設定。此題沒有
solution、根專案或正式測試專案；`Main` 的 acceptance harness 是可執行驗證方式。

## Coding Style and Solution Contract

遵守 `.editorconfig`：C# 使用四個空白縮排、控制流程保留大括號、公開成員採 PascalCase、
區域變數採 camelCase。保留 `Main` 的雙語 XML 題述與 `MaxProduct` 的繁體中文 XML 摘要。

公開 API 必須維持 `public static int MaxProduct(int[] nums)` 且不輸出。它以單趟掃描維護
最大值與次大值，回傳 `(largest - 1) * (secondLargest - 1)`，不排序也不修改 `nums`。
輸入位於 LeetCode 有效範圍內，不新增額外的 invalid-input 行為；所有主控台輸出只在 `Main`。

## Testing and Git Scope

Harness 有八個確定性案例。每個案例在呼叫 API 前複製完整陣列，只有答案正確且呼叫後陣列
完整相等時才是 PASS。成功時必須精確結尾為 `Summary: 8/8 checks passed.`，exit code 為 0。
Git metadata 位於 parent repository；commit 與 PR 的變更必須只限 `leetcode_1464/`。
