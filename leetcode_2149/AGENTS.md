# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2149/leetcode_2149.csproj`。從題目根目錄執行：

    dotnet build leetcode_2149/leetcode_2149.csproj --nologo
    dotnet run --no-build --project leetcode_2149/leetcode_2149.csproj

從 repository 根目錄則使用 `leetcode_2149/leetcode_2149/leetcode_2149.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_2149`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法的繁體
中文 XML summary；行內註解只說明正負索引與組內相對順序不變量。

公開 API 為 `RearrangeArray(int[] nums)` 與 `RearrangeArray2(int[] nums)`。兩者接收長度 2 至
200,000 的偶數長度陣列，元素絕對值介於 1 至 100,000，且正數與負數數量相等。兩個方法都必須
回傳以正數開頭、正負號交錯且維持同號元素原相對順序的新陣列；不得修改輸入、輸出主控台或
加入題目契約外的無效輸入行為。

`RearrangeArray` 直接將正數寫入偶數索引、負數寫入奇數索引。`RearrangeArray2` 先依原順序
分別收集正負數，再逐對交錯寫入結果。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 7 個 acceptance cases；每案以獨立副本分別驗證兩個 API
的精確結果與輸入保存，共 28 個檢查。輸出使用穩定的 `PASS/FAIL` 行；全部成功時必須以
`Summary: 28/28 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2149/`，
不可加入測試專案、套件或題目契約外的行為。
