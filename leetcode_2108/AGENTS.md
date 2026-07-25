# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2108/leetcode_2108.csproj`。從題目根目錄執行：

    dotnet build leetcode_2108/leetcode_2108.csproj --nologo
    dotnet run --no-build --project leetcode_2108/leetcode_2108.csproj

從 repository 根目錄則使用 `leetcode_2108/leetcode_2108/leetcode_2108.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_2108`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及兩個公開 API 的
繁體中文 XML summary；行內註解只說明第一個匹配與雙指標提前失敗的不變量。

公開 API 為 `FirstPalindrome(string[] words)` 與 `IsPalindrome(string word)`。兩者都是純函式：
不得修改輸入、輸出主控台，或加入題目契約外的無效輸入行為。陣列長度介於 1 至 100，每個元素
長度介於 1 至 100，且只含小寫英文字母。

`FirstPalindrome` 必須依輸入順序回傳第一個通過 `IsPalindrome` 的字串，找不到時回傳空字串。
`IsPalindrome` 以左右指標比較對稱字元，任何一組不相同便立即回傳 false；指標相遇或交錯時
代表所有對稱位置都相同。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 8 個 `FirstPalindrome` acceptance cases；每案驗證結果與
輸入保存，共 16 個檢查，另以 6 個案例直接驗證 `IsPalindrome`，總計 22 個檢查。輸出使用穩定的
`PASS/FAIL` 行；全部成功時必須以 `Summary: 22/22 checks passed.` 結尾，任一失敗時 exit code
必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2108/`，
不可加入測試專案、套件或題目契約外的行為。
