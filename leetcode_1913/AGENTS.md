# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_1913/leetcode_1913.csproj`。從題目根目錄執行：

    dotnet build leetcode_1913/leetcode_1913.csproj --nologo
    dotnet run --no-build --project leetcode_1913/leetcode_1913.csproj

從 repository 根目錄則使用 `leetcode_1913/leetcode_1913/leetcode_1913.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_1913`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法的繁體
中文 XML summary；行內註解只說明 extrema tracker 的更新不變量。

公開 API 為 `public static int MaxProductDifference(int[] nums)`。它是純函式：不得修改輸入、輸出
主控台，或加入題目契約外的無效輸入行為。輸入長度介於 4 至 10000，元素值介於 1 至 10000。
單次掃描時維護兩個最大值與兩個最小值；更新最大值或最小值都必須先把舊第一名移到第二名，
再覆寫第一名，才能正確處理重複極值。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 7 個確定性 acceptance cases；每案驗證答案與輸入保存，
共 14 個檢查。輸出使用穩定的 `PASS/FAIL MaxProductDifference...` 行；全部成功時必須以
`Summary: 14/14 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_1913/`，
不可加入測試專案、套件或題目契約外的行為。
