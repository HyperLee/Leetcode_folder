# Repository Guidelines
## Project Structure and Commands

此資料夾是題目根目錄；可執行的巢狀 .NET 10 專案是
`leetcode_1743/leetcode_1743.csproj`。從本資料夾執行：

```bash
dotnet build leetcode_1743/leetcode_1743.csproj --nologo
dotnet run --no-build --project leetcode_1743/leetcode_1743.csproj
```

使用 `--no-build` 前必須先建置。VS Code 請使用 `Debug leetcode_1743` 設定。此題沒有
solution、根專案或正式測試專案；`Main` 的 acceptance harness 是可執行驗證方式。

## Coding Style and Solution Contract

遵守 `.editorconfig`：C# 使用四個空白縮排、控制流程保留大括號、方法採 PascalCase、區域
變數採 camelCase。保留 `Main` 的雙語 XML 題述，以及 `RestoreArray`、核心 helper 的
繁體中文 XML 摘要與少量高訊號註解。

公開 API 必須維持 `public int[] RestoreArray(int[][] adjacentPairs)`，且不得輸出或修改
輸入。解法以雙向鄰接表表示所有 pair，從度數為 1 的端點開始，後續排除上一個節點後前進；
整體反向的還原結果同樣合法。不要新增題目未要求的 invalid-input 行為；所有主控台輸出只在
`Main`。

## Testing and Git Scope

Harness 有八個確定性案例，驗證所有相鄰關係、元素集合、輸入未修改、100000 個元素的上限
spot check，以及錯誤實作回傳空陣列時仍能完整報告失敗；成功時必須精確結尾為
`Summary: 8/8 checks passed.`，exit code 為 0。Git
metadata 位於 parent repository；commit 與 PR 的變更必須只限 `leetcode_1743/`。
