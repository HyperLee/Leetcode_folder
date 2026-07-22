# Repository Guidelines

## Project Structure and Commands

此資料夾是題目根目錄；可執行的巢狀 .NET 10 專案是
`leetcode_1721/leetcode_1721.csproj`。從本資料夾執行：

```bash
dotnet build leetcode_1721/leetcode_1721.csproj --nologo
dotnet run --no-build --project leetcode_1721/leetcode_1721.csproj
```

使用 `--no-build` 前必須先建置。VS Code 請使用 `Debug leetcode_1721` 設定。此題沒有
solution、根專案或正式測試專案；`Main` 的 acceptance harness 是可執行驗證方式。

## Coding Style and Solution Contract

遵守 `.editorconfig`：C# 使用四個空白縮排、控制流程保留大括號、方法採 PascalCase、區域
變數採 camelCase。保留 `Main` 的雙語 XML 題述，以及 `SwapNodes`、`SwapNodes2` 的繁體中文
XML 摘要。

公開 API 必須維持 `public static ListNode SwapNodes(ListNode head, int k)` 與
`public static ListNode SwapNodes2(ListNode head, int k)`，且不得輸出。兩種解法只交換正數與
倒數第 `k` 個節點的值，回傳原 `head`，不得重排節點或改變鏈結 topology。不要新增題目未
要求的 invalid-input 行為；所有主控台輸出只在 `Main`。

## Testing and Git Scope

Harness 有八個確定性案例。每個案例為兩種解法建立獨立串列，並驗證答案、原 head identity、
節點 identity 與 topology；成功時必須精確結尾為 `Summary: 8/8 checks passed.`，exit code 為
0。Git metadata 位於 parent repository；commit 與 PR 的變更必須只限 `leetcode_1721/`。
