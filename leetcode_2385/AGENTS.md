# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：`leetcode_2385/leetcode_2385.csproj`。從題目根目錄執行：

    dotnet build leetcode_2385/leetcode_2385.csproj --nologo
    dotnet run --no-build --project leetcode_2385/leetcode_2385.csproj

從 repository 根目錄則使用 `leetcode_2385/leetcode_2385/leetcode_2385.csproj`。先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_2385`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、file-scoped namespace，以及公開成員採 PascalCase、區域變數採 camelCase。`TreeNode.val`、`TreeNode.left` 與 `TreeNode.right` 是 LeetCode 指定欄位，保留原始命名。`Main` 保留雙語題目 XML summary；公開 API 與核心 helper 使用繁體中文 XML summary，行內註解只說明 BFS 層級不變量。

公開 API 為 `public static int AmountOfTime(TreeNode root, int start)`。輸入為題目保證非空、值唯一且 `start` 存在的二元樹；方法必須純粹，不得輸出主控台、修改樹或保留跨呼叫狀態。先建立 parent map，再從 start 逐層 BFS；感染 queue 與 visited 皆以 start 初始化，分鐘從 `-1` 開始且每處理完一層才加一。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 9 個確定性 acceptance cases，包括跨根路徑、重複呼叫、拓撲不變與 100,000 節點斜樹。全部成功時輸出必須以 `Summary: 9/9 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2385/`；不可加入 solution、測試專案、套件、額外公開 API 或題目契約外的行為。
