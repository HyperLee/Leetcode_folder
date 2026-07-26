# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：`leetcode_2395/leetcode_2395.csproj`。從題目根目錄執行：

    dotnet build leetcode_2395/leetcode_2395.csproj --nologo
    dotnet run --no-build --project leetcode_2395/leetcode_2395.csproj

從 repository 根目錄則使用 `leetcode_2395/leetcode_2395/leetcode_2395.csproj`。先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_2395`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、file-scoped namespace，以及公開成員採 PascalCase、區域變數採 camelCase。`Main` 保留雙語題目 XML summary；兩個公開 API 使用繁體中文 XML summary，行內註解只說明雜湊集合與索引範圍的不變量。

公開 API 為 `FindSubarrays(int[] nums)` 與 `FindSubarrays2(int[] nums)`，分別使用 HashSet 線性掃描與雙層迴圈暴力比較。輸入依題目保證長度 2 至 1000，元素介於 -10^9 至 10^9。兩個方法必須保持純粹，不得輸出主控台、修改輸入、保留跨呼叫狀態或新增題目未要求的例外。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 7 組確定性案例並逐一驗證 2 個解法的結果與輸入未修改，共 28 項檢查；包含三個官方範例、最小長度、非相鄰重複和、數值上下限，以及長度 1000 的最壞 false 案例。全部成功時輸出必須以 `Summary: 28/28 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2395/`；不可加入 solution、測試專案、套件、額外公開 API 或題目契約外的行為。
