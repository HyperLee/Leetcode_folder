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

公開 API 為 `public static bool MakeEqual(string[] words)`。此方法為純函式：不得修改輸入
或輸出主控台，只處理題目保證長度 1 至 100、每個字串長度 1 至 100 且只含小寫英文字母
的陣列。核心不變量是每種字元的總出現次數都必須可被 `words.Length` 整除；這也是可將
字元平均重新分配至所有字串的充要條件。以 Dictionary 統計字元，不要加入題目契約外的
無效輸入行為。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有八個確定性 acceptance cases。每案同時驗證回傳值
與輸入保存，並輸出 Case、Input、Expected、Actual、Input preserved 與 PASS/FAIL。全部
成功的結尾必須是 `Summary: 8/8 checks passed.`，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1897/`，不可加入測試專案、套件或題目契約外的無效輸入行為。
