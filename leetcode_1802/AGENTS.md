# Repository Guidelines

## 專案結構與命令

本資料夾包含一個巢狀 .NET 10 主控台專案：
`leetcode_1802/leetcode_1802.csproj`。從此題目根目錄執行：

    dotnet build leetcode_1802/leetcode_1802.csproj --nologo
    dotnet run --no-build --project leetcode_1802/leetcode_1802.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1802`；此題沒有
solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員
採 PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及
`MaxValue`、`IsFeasible` 與 `CalculateSideSum` 的繁體中文 XML summary。

唯一題目公開 API 是
`public static int MaxValue(int n, int index, int maxSum)`。解法與 helper 不輸出；
只有 `Main` 可做 console I/O。對候選峰值計算左右兩側逐步下降且最低為 1 的最小總和，
可行候選提高二分下界，不可行候選降低上界。總和與乘法必須使用 `long`，不得建立長度
可達 `10^9` 的實際陣列，也不得加入題目契約外的輸入例外。

## 驗證與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有十個確定性 acceptance cases。每案都輸出
`Case`、`Input`、`Expected`、`Actual` 與 `Result: PASS|FAIL`；全部成功的結尾必須是
`Summary: 10/10 checks passed.`，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1802/`，且不可加入測試專案、第三方套件、實際巨大陣列或其他替代解法。
