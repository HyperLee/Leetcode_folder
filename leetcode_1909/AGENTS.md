# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_1909/leetcode_1909.csproj`。從題目根目錄執行：

    dotnet build leetcode_1909/leetcode_1909.csproj --nologo
    dotnet run --no-build --project leetcode_1909/leetcode_1909.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1909`。此題沒有 solution、
根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法
與核心 helper 的繁體中文 XML summary；行內註解只說明刪除候選與掃描不變量。

公開 API 為 `public static bool CanBeIncreasing(int[] nums)` 與
`public static bool CanBeIncreasingBruteForce(int[] nums)`。兩者皆為純函式：不得修改輸入或
輸出主控台，也不得加入題目契約外的無效輸入行為。輸入長度介於 2 至 1000，元素值介於
1 至 1000。`CanBeIncreasing` 以單次掃描處理第一個非嚴格遞增位置，分別判斷刪除前項或
目前項能否接回兩側；第二個違規或兩種刪除都不可行時回傳 false。brute-force 版本逐一略過
每個索引，保留原解法的教學比較。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 11 個確定性 acceptance cases。每案必須以獨立輸入
副本分別呼叫兩個解法，且各方法都驗證回傳值與輸入保存，共 44 個檢查。輸出使用穩定的
`PASS/FAIL CanBeIncreasing...` 與 `PASS/FAIL CanBeIncreasingBruteForce...` 行；全部成功
時以 `Summary: 44/44 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1909/`，不可加入測試專案、套件或題目契約外的行為。
