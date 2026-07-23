# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_1822/leetcode_1822.csproj`。從題目根目錄執行：

    dotnet build leetcode_1822/leetcode_1822.csproj --nologo
    dotnet run --no-build --project leetcode_1822/leetcode_1822.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1822`。此題沒有 solution、
根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及兩個公開
方法的繁體中文 XML summary 與關鍵不變量註解。

公開 API 為 `public static int ArraySign(int[] nums)` 與
`public static int ArraySign2(int[] nums)`。兩者皆為純函式：不得修改輸入、計算完整乘積或
輸出主控台，只處理題目保證長度 1 至 1000、元素 -100 至 100 的陣列。`ArraySign` 的核心
不變量是非零乘積的符號只由負數個數奇偶決定；`ArraySign2` 則在每個負數出現時翻轉符號。
遇到零時，兩者均立即回傳 0。

## 驗證與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有八個確定性 acceptance cases。每案對兩個 API 各以
獨立輸入副本進行結果與輸入保存檢查，共 32 項；每項輸出 `PASS|FAIL`、check name、
`Expected` 與 `Actual`。全部成功的結尾必須是 `Summary: 32/32 checks passed.`，失敗時
exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1822/`，不可加入測試專案、套件或題目契約外的無效輸入行為。
