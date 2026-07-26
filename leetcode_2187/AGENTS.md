# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2187/leetcode_2187.csproj`。從題目根目錄執行：

    dotnet build leetcode_2187/leetcode_2187.csproj --nologo
    dotnet run --no-build --project leetcode_2187/leetcode_2187.csproj

從 repository 根目錄則使用 `leetcode_2187/leetcode_2187/leetcode_2187.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_2187`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法與
可行性 helper 的繁體中文 XML summary；行內註解只說明二分搜尋不變量與早停防溢位原因。

公開 API 為 `MinimumTime(int[] time, int totalTrips)` 與
`MinimumTime2(int[] time, int totalTrips)`。兩者都是純函式：不得修改 `time`、輸出主控台，
或加入題目契約外的無效輸入行為。題目保證 `time.Length` 為 1 至 100,000、每個值為 1 至
10,000,000，且 `totalTrips` 為 1 至 10,000,000。

`MinimumTime` 使用 `left < right`，可行的中點保留在右界；`MinimumTime2` 使用
`left <= right` 與候選答案。兩者的邊界都是 1 與
`(long)time.Max() * totalTrips`，並共用會在達標時立即停止的可行性 helper，避免無用累加導致
`long` 溢位。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 9 個確定性 acceptance cases；每案以獨立副本分別驗證
兩個 API 的答案與輸入保存，共 36 個檢查。輸出使用穩定的 `PASS/FAIL` 行；全部成功時必須以
`Summary: 36/36 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2187/`，
不可加入測試專案、套件或題目契約外的行為。
