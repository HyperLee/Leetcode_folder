# Repository Guidelines

## 專案結構與命令

本資料夾包含一個巢狀 .NET 10 主控台專案：
`leetcode_1818/leetcode_1818.csproj`。從此題目根目錄執行：

    dotnet build leetcode_1818/leetcode_1818.csproj --nologo
    dotnet run --no-build --project leetcode_1818/leetcode_1818.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1818`；此題沒有
solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員
採 PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及兩個
公開演算法方法的繁體中文 XML summary。

公開 API 僅有 `public static int MinAbsoluteSumDiff(int[] nums1, int[] nums2)` 與
`public static int BinarySearch(int[] rec, int target)`。解法不輸出且不修改輸入；只有
`Main` 可做 console I/O。複製並排序 `nums1`，對每個 `nums2[i]` 用 lower bound 找到
第一個不小於目標值的元素，並比較該後繼與前驅可省下的最大差值；不得加入題目契約外的
輸入例外、套件、測試專案或第二種解法。

## 驗證與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有九個確定性案例、每案兩項檢查。每項都輸出
`Case`、`Input`、`Expected`、`Actual` 與 `Result: PASS|FAIL`；全部成功的結尾必須是
`Summary: 18/18 checks passed.`，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1818/`，且不可加入測試專案、第三方套件或其他題目的變更。
