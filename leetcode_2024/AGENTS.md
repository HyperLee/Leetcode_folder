# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2024/leetcode_2024.csproj`。從題目根目錄執行：

    dotnet build leetcode_2024/leetcode_2024.csproj --nologo
    dotnet run --no-build --project leetcode_2024/leetcode_2024.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_2024`。此題沒有 solution、
根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary、主要函式的繁體
中文 XML summary，以及只解釋不變量或關鍵判斷原因的高訊號註解。

公開 API 為 `public static int MaxConsecutiveAnswers(string answerKey, int k)` 與
`public static int MaxConsecutiveChar(string answerKey, int k, char ch)`。兩者皆為純函式，
只處理題目保證長度介於 1 至 50,000、由 `T` 與 `F` 組成，且 `1 <= k <= n` 的有效輸入；
不得輸出主控台或加入題目契約外的例外。helper 的 `ch` 表示視窗內允許被替換的字元，
合法視窗內最多只能包含 `k` 個 `ch`。時間複雜度為 O(n)，輔助空間為 O(1)。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，執行九個確定性案例。每案驗證整體答案，以及直接以
`T`、`F` 呼叫 helper 的結果，共 27 個檢查。輸出必須穩定列出 Input、Expected、Actual
及 PASS/FAIL；全部成功的結尾必須是 `Summary: 27/27 checks passed.`，任一失敗時 exit
code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_2024/`，不可加入測試專案、套件、替代演算法或題目契約外的無效輸入行為。
