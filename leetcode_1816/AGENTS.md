# Repository Guidelines

## 專案結構與命令

本資料夾包含一個巢狀 .NET 10 主控台專案：
`leetcode_1816/leetcode_1816.csproj`。從此題目根目錄執行：

    dotnet build leetcode_1816/leetcode_1816.csproj --nologo
    dotnet run --no-build --project leetcode_1816/leetcode_1816.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1816`；此題沒有
solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員
採 PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及
`TruncateSentence` 的繁體中文 XML summary。

唯一題目公開 API 是
`public static string TruncateSentence(string s, int k)`。解法不輸出且不修改輸入；
只有 `Main` 可做 console I/O。對題目保證有效的句子計數空格，遇到第 k 個空格時回傳
不含該空格的前綴；若句子恰有 k 個單字則回傳原字串。不得加入題目契約外的輸入例外、
完整單字陣列或第二種同質解法。

## 驗證與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有八個確定性 acceptance cases。每案都輸出
`Case`、`Input`、`Expected`、`Actual` 與 `Result: PASS|FAIL`；全部成功的結尾必須是
`Summary: 8/8 checks passed.`，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1816/`，且不可加入測試專案、第三方套件或其他題目的變更。
