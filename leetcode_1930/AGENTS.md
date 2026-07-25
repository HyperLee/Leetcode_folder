# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_1930/leetcode_1930.csproj`。從題目根目錄執行：

    dotnet build leetcode_1930/leetcode_1930.csproj --nologo
    dotnet run --no-build --project leetcode_1930/leetcode_1930.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1930`。此題沒有 solution、
根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary、公開解法的繁體
中文 XML summary，以及只解釋不變量或關鍵判斷原因的高訊號註解。

公開 API 為 `public static int CountPalindromicSubsequence(string s)` 與
`public static int CountPalindromicSubsequence2(string s)`。兩者皆為純函式：不得修改
輸入或輸出主控台，只處理題目保證長度 3 至 100000、且只含小寫英文字母的字串。核心
不變量是：固定首尾字元後，其第一次與最後一次出現位置形成最寬範圍；範圍內每種不同中心
字元恰好對應一個不同的長度三回文。第一個方法以 `HashSet<char>` 去重，第二個方法重用
固定 `bool[26]` 標記；不得重新加入 `Substring` 暫存字串或題目契約外的無效輸入行為。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，執行十個確定性案例。每案分別驗證兩個公開方法的結果，
共 20 個檢查。輸出必須穩定列出 Input、Expected、兩個 Actual 及 PASS/FAIL；全部成功的
結尾必須是 `Summary: 20/20 checks passed.`，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1930/`，不可加入測試專案、套件或題目契約外的無效輸入行為。
