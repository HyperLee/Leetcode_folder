# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：`leetcode_2265/leetcode_2265.csproj`。從題目根目錄執行：

    dotnet build leetcode_2265/leetcode_2265.csproj --nologo
    dotnet run --no-build --project leetcode_2265/leetcode_2265.csproj

從 repository 根目錄則使用 `leetcode_2265/leetcode_2265/leetcode_2265.csproj`。先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_2265`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採 PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法與核心遞迴 helper 的繁體中文 XML summary；行內註解只說明後序彙總等高訊號不變量。

保留公開 API `public static int AverageOfSubtree(TreeNode root)` 與 public nested `TreeNode`。解法為純函式：不得輸出主控台、修改樹節點或依賴跨呼叫的可變全域狀態。`Traverse(TreeNode? node)` 必須以後序回傳子樹 `(Sum, Count, Matches)`；空節點回傳 `(0, 0, 0)`，非空節點只在 `node.val == sum / count` 時新增匹配數。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 9 個確定性 acceptance checks，包括重複呼叫、原樹拓撲不變與 1000 節點斜樹。全部成功時輸出必須以 `Summary: 9/9 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2265/`；不可加入 solution、測試專案、套件、額外公開 API 或題目契約外的行為。
