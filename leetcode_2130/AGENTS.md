# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2130/leetcode_2130.csproj`。從題目根目錄執行：

    dotnet build leetcode_2130/leetcode_2130.csproj --nologo
    dotnet run --no-build --project leetcode_2130/leetcode_2130.csproj

從 repository 根目錄則使用 `leetcode_2130/leetcode_2130/leetcode_2130.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_2130`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。`ListNode.val` 與 `ListNode.next` 為 LeetCode 題目指定欄位，
保留原始命名。`Main` 保留雙語題目 XML summary；主要演算法函式使用繁體中文 XML summary，
行內註解只說明孿生配對、反轉順序與輸入還原不變量。

公開 API 為 `PairSum(ListNode head)` 與 `PairSum2(ListNode head)`。兩者接收節點數 2 至 100,000
的偶數長度有效串列，節點值介於 1 至 100,000；不得加入題目契約外的無效輸入行為或主控台輸出。

`PairSum` 以 Stack 保存節點值，串列必須保持不變。`PairSum2` 以快慢指標找到後半、反轉後半並
同步計算孿生和，回傳前必須再次反轉，使原節點參考、值、順序與所有 `next` 連結完全還原。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 8 個 acceptance cases。每案分別驗證兩個公開 API 的結果與
完整輸入拓撲，共 32 個檢查。輸出使用穩定的 `PASS/FAIL` 行；全部成功時必須以
`Summary: 32/32 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2130/`，
不可加入測試專案、套件或題目契約外的行為。
