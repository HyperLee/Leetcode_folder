# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：`leetcode_2243/leetcode_2243.csproj`。從題目根目錄執行：

    dotnet build leetcode_2243/leetcode_2243.csproj --nologo
    dotnet run --no-build --project leetcode_2243/leetcode_2243.csproj

從 repository 根目錄則使用 `leetcode_2243/leetcode_2243/leetcode_2243.csproj`。先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_2243`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採 PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法的繁體中文 XML summary；行內註解只說明分組邊界等高訊號不變量。

保留唯一公開 API：`public static string DigitSum(string s, int k)`。它是純函式：不得輸出主控台、修改外部狀態，或加入題目契約外的無效輸入行為。題目保證 `s.Length` 為 1 至 100、`k` 為 2 至 100，且 `s` 只含數字。

演算法必須迭代地將每輪字串分成由左至右、不重疊且完全覆蓋原字串的至多 `k` 字元群組；尾端短群組也必須求和。每輪將群組和依序附加成下一輪字串，只有在長度 `<= k` 時停止。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 8 個確定性 acceptance cases；每案驗證 `DigitSum` 的預期回傳值。全部成功時輸出必須以 `Summary: 8/8 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2243/`；不可加入 solution、測試專案、套件、額外公開 API 或題目契約外的行為。
