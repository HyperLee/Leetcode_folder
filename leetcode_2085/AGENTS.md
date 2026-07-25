# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2085/leetcode_2085.csproj`。從題目根目錄執行：

    dotnet build leetcode_2085/leetcode_2085.csproj --nologo
    dotnet run --no-build --project leetcode_2085/leetcode_2085.csproj

從 repository 根目錄則使用 `leetcode_2085/leetcode_2085/leetcode_2085.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_2085`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法的繁體
中文 XML summary；行內註解只說明頻率字典的鍵值對齊不變量。

公開 API 為 `public static int CountWords(string[] words1, string[] words2)`。它是純函式：不得修改
兩個輸入陣列、輸出主控台，或加入題目契約外的無效輸入行為。兩個陣列長度皆介於 1 至 1000，
每個元素長度介於 1 至 30 且只含小寫英文字母。必須分別統計兩邊頻率，只有同一個字串在兩個
字典中的次數都恰好為 1 才能計入答案。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 9 個確定性 acceptance cases；每案驗證答案與兩個輸入
陣列的保存，共 18 個檢查。輸出使用穩定的 `PASS/FAIL` 行；全部成功時必須以
`Summary: 18/18 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2085/`，
不可加入測試專案、套件或題目契約外的行為。
