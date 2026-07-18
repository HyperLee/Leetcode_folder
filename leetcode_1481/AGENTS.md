# Repository Guidelines

## Project Structure and Commands

此資料夾是題目根目錄；可執行的巢狀 .NET 10 專案為
`leetcode_1481/leetcode_1481.csproj`。從本題目根目錄執行：

```bash
dotnet build leetcode_1481/leetcode_1481.csproj --nologo
dotnet run --no-build --project leetcode_1481/leetcode_1481.csproj
```

使用 `--no-build` 前必須先建置。VS Code 請使用 `Debug leetcode_1481` 設定。此題沒有
solution、根專案或正式測試 project；`Main` 的 acceptance harness 是唯一可執行驗證方式。

## Coding Style and Solution Contract

遵守 `.editorconfig`：C# 使用四個空白縮排、控制流程保留大括號、公開成員採 PascalCase、
區域變數採 camelCase。保留 `Main` 的雙語 XML 題述與
`FindLeastNumOfUniqueInts` 的繁體中文 XML 摘要。

公開 API 必須維持 `public static int FindLeastNumOfUniqueInts(int[] arr, int k)`，且 API 本身
不得輸出。先統計頻率、再由低至高排序；只有完整移除一個頻率群組才可讓相異整數數量減一。
當下一個群組的頻率大於剩餘 `k` 時停止，保留的 `k` 可自任一仍存在群組移除，不會再降低
相異整數數量。不得排序或修改輸入 `arr`；所有主控台輸出只在 `Main`。

## Testing and Git Scope

Harness 有九個確定性案例，涵蓋官方案例、`k = 0`、全部移除、相同頻率、大數值與長度
100000 的邊界。每一案例都在呼叫 API 前複製完整陣列，只有答案正確且呼叫後輸入完整相等
才是 PASS。成功時精確結尾為 `Summary: 9/9 checks passed.`，exit code 為 0。

Git metadata 位於 parent repository；commit 與 PR 的變更必須只限 `leetcode_1481/`。
