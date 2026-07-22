# Repository Guidelines

## 專案結構與命令

本資料夾包含一個巢狀 .NET 10 主控台專案：
`leetcode_1759/leetcode_1759.csproj`。從此題目根目錄執行：

    dotnet build leetcode_1759/leetcode_1759.csproj --nologo
    dotnet run --no-build --project leetcode_1759/leetcode_1759.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1759`；此題沒有
solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員
採 PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及
`CountHomogenous` 的繁體中文 XML summary 與關鍵段落結算不變量註解。

唯一公開 API 是 `public static int CountHomogenous(string s)`。此純函式不得修改輸入或
輸出到主控台，只處理題目保證由小寫英文字母組成且長度介於 1 到 100,000 的字串。
核心不變量是每個已結束、長度為 `L` 的連續同字元區段恰好貢獻
`L * (L + 1) / 2` 個同質子字串；最終答案對 `1_000_000_007` 取模。

## 驗證與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有九個確定性 acceptance cases。每案都輸出
`Case`、`Input`、`Expected`、`Actual` 與 `Result: PASS|FAIL`；全部成功的結尾必須是
`Summary: 9/9 checks passed.`，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1759/`，且不可加入測試專案、套件、第二解法或題目契約外的無效輸入行為。
