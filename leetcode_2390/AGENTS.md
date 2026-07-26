# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：`leetcode_2390/leetcode_2390.csproj`。從題目根目錄執行：

    dotnet build leetcode_2390/leetcode_2390.csproj --nologo
    dotnet run --no-build --project leetcode_2390/leetcode_2390.csproj

從 repository 根目錄則使用 `leetcode_2390/leetcode_2390/leetcode_2390.csproj`。先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_2390`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、file-scoped namespace，以及公開成員採 PascalCase、區域變數採 camelCase。`Main` 保留雙語題目 XML summary；三個公開 API 使用繁體中文 XML summary，行內註解只說明 Stack 列舉方向的不變量。

公開 API 為 `RemoveStars(string s)`、`RemoveStars2(string s)` 與 `RemoveStars3(string s)`，分別使用 `List<char>`、`StringBuilder` 與 `Stack<char>`。輸入依題目保證只含小寫英文字母與星號，且每個星號左側都有可刪除字元。三個方法必須保持純粹，不得輸出主控台、修改輸入、保留跨呼叫狀態或新增題目未要求的例外。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 8 組確定性案例並逐一驗證 3 個解法，共 24 項檢查；包含兩個官方範例、最小輸入、無星號、交錯與連續刪除，以及 100,000 字元上限案例。全部成功時輸出必須以 `Summary: 24/24 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2390/`；不可加入 solution、測試專案、套件、額外公開 API 或題目契約外的行為。
