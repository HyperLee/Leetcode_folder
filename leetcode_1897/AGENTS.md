# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_1897/leetcode_1897.csproj`。從題目根目錄執行：

    dotnet build leetcode_1897/leetcode_1897.csproj --nologo
    dotnet run --no-build --project leetcode_1897/leetcode_1897.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1897`。此題沒有 solution、
根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法
的繁體中文 XML summary 與高訊號不變量註解。

公開 API 為 `public static bool MakeEqual(string[] words)` 與
`public static bool MakeEqual2(string[] words)`。兩者皆為純函式：不得修改輸入或輸出
主控台，只處理題目保證長度 1 至 100、每個字串長度 1 至 100 且只含小寫英文字母的陣列。
核心不變量是每種字元的總出現次數都必須可被 `words.Length` 整除；這也是可將字元平均
重新分配至所有字串的充要條件。MakeEqual 以 Dictionary 統計字元，較一般化且教學友善，
但雜湊常數較高；MakeEqual2 使用 `character - 'a'` 對應的固定 `int[26]`，僅限題目的
小寫字母契約並提供固定 `O(1)` 輔助空間。兩個方法均不得加入題目契約外的無效輸入行為。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有八個確定性 acceptance cases。每案必須以獨立的輸入
陣列副本分別呼叫 MakeEqual 與 MakeEqual2，且各方法都驗證回傳值與輸入保存，共 32 個檢查。
輸出必須以穩定的 `PASS/FAIL MakeEqual...` 與 `PASS/FAIL MakeEqual2...` 行識別方法與
檢查項目。全部成功的結尾必須是 `Summary: 32/32 checks passed.`，任一失敗時 exit code
必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1897/`，不可加入測試專案、套件或題目契約外的無效輸入行為。
