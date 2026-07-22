# Repository Guidelines

## 專案結構與命令

本資料夾包含一個巢狀 .NET 10 主控台專案：
`leetcode_1750/leetcode_1750.csproj`。從此題目根目錄執行：

    dotnet build leetcode_1750/leetcode_1750.csproj --nologo
    dotnet run --no-build --project leetcode_1750/leetcode_1750.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1750`；此題沒有
solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員
採 PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及
`MinimumLength` 的繁體中文 XML summary 與關鍵雙指標不變量註解。

唯一公開 API 是 `public static int MinimumLength(string s)`。此純函式不得修改輸入或
輸出到主控台，只處理題目保證由 `a`、`b`、`c` 組成且長度介於 1 到 100,000 的字串。
核心不變量是 `[left, right]` 永遠表示尚未刪除的區間；兩端相同時必須跳過兩側完整
的相同字元區段。

## 驗證與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有十個確定性 acceptance cases。每案都輸出
`Case`、`Input`、`Expected`、`Actual` 與 `Result: PASS|FAIL`；全部成功的結尾必須是
`Summary: 10/10 checks passed.`，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1750/`，且不可加入測試專案、套件、其他解法或題目契約外的無效輸入行為。
