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
中文 XML summary；行內註解只說明頻率字典的鍵值對齊與狀態轉換不變量。

公開 API 為 `CountWords(string[] words1, string[] words2)` 與
`CountWords2(string[] words1, string[] words2)`。兩者都是純函式：不得修改兩個輸入陣列、
輸出主控台，或加入題目契約外的無效輸入行為。兩個陣列長度皆介於 1 至 1000，每個元素長度
介於 1 至 30 且只含小寫英文字母。

`CountWords` 分別統計兩邊頻率；只有同一個字串在兩個字典中的次數都恰好為 1 才能計入答案。
`CountWords2` 只為第一個陣列建立單一 `Dictionary<string, WordState>`；第二個陣列只能把候選
狀態轉為兩邊各一次或第二邊重複，未出現在第一邊的字串不得加入字典。最終只計算
`SeenOnceInBoth`。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 9 個確定性 acceptance cases；每案以獨立副本分別驗證
兩個 API 的答案與兩個輸入陣列保存，共 36 個檢查。輸出使用穩定的 `PASS/FAIL` 行；全部成功
時必須以 `Summary: 36/36 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2085/`，
不可加入測試專案、套件或題目契約外的行為。
