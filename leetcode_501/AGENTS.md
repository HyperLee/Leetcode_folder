# Repository Guidelines

## 專案結構

本題根目錄可直接作為 VS Code workspace。可執行的巢狀 .NET 10 專案位於
`leetcode_501/leetcode_501.csproj`，其 `Program.cs` 同時保留題目雙語摘要、公開解法
API 與確定性的 acceptance harness。`.vscode/tasks.json` 與 `.vscode/launch.json` 均假設
workspaceFolder 是此根目錄；`docs/` 存放 README 範本與本次遷移的設計／計畫紀錄。

## 建置、執行與驗證

從本題根目錄執行：

```bash
dotnet build leetcode_501/leetcode_501.csproj --nologo
dotnet run --no-build --project leetcode_501/leetcode_501.csproj
```

先成功建置，才可使用 `--no-build`。本題沒有正式測試專案；主控台 acceptance harness
是唯一的可重複驗證入口，必須以 exit code 0 結束並顯示
`Summary: 8/8 checks passed.`。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四個空白縮排與明確的區域變數型別；公開成員採
PascalCase，區域變數及參數採 camelCase。`public static int[] FindMode(TreeNode? root)`
必須是 console-free API，不得輸出、不得保留跨呼叫的演算法靜態狀態。

解法以迭代中序走訪取得非遞減序列，因此每個相同值必形成連續區段。只要維護前一值、
目前連續次數、最大頻率與眾數清單，就能在新最大值時替換結果、在平手時保留所有值。
維持此 contiguous-run invariant，並讓所有 `Console.WriteLine` 留在 `Main`。

## Git 範圍

Git metadata 位於父層 repository，而非本題根目錄。從父層檢視本題範圍時，使用
`git diff --check -- leetcode_501` 與 `git status --short`。提交時僅暫存
`leetcode_501/`，不要把其他題目或父層的未相關變更一併納入。
