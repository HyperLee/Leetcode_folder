# leetcode_684 開發指引

## 專案位置與常用指令

- 題目根目錄是 `leetcode_684/`，實際的巢狀 .NET 10 主控台專案是 `leetcode_684/leetcode_684.csproj`。
- 請在題目根目錄執行下列指令：

```plaintext
dotnet build leetcode_684/leetcode_684.csproj --nologo
dotnet run --no-build --project leetcode_684/leetcode_684.csproj
```

- 本題沒有正式測試專案；`Program.Main` 內的 deterministic acceptance harness 是交付前的行為驗證，成功結尾必須是 `Summary: 8/8 checks passed.`。
- VS Code 直接開啟 `leetcode_684/` 後，使用 `Debug leetcode_684`；它會先執行 `build leetcode_684`。

## 實作契約

- 對外的核心 API 是 `FindRedundantConnection(int[][] edges)`。它必須保持純粹：不輸出主控台、不改變 `edges` 的順序或其中端點，僅回傳第一條形成環的原始邊；回傳端點方向必須與輸入完全一致。
- Union-Find 維持兩個不變量：`parent` 的根節點是集合代表元，且只有根節點的 `componentSize` 保存該集合大小。合併時以 size 較大的根為代表元，`Find` 必須維持路徑壓縮。
- 所有 C# 縮排使用四個空白。維持目前的 XML 文件與少量高訊號演算法註解；不要把主控台輸出移出 `Main`。
- 不要更動既有演算法或 acceptance harness，除非任務明確要求；文件中的 8 項案例、複雜度與輸出必須和實作同步。

## Git 範圍

- Git 中繼資料位於父層的 LeetCode repository；執行狀態、暫存與提交時應以該 repository 根目錄為基準，而不是巢狀 `.csproj` 目錄。
- 此類遷移只暫存本題目目標檔案，例如 `git add leetcode_684/README.md leetcode_684/AGENTS.md leetcode_684/.vscode/ leetcode_684/docs/readme-template.md`。不要暫存其他題目或工作區狀態檔；未追蹤的設計／計畫檔也僅在交付範圍明列時才納入。
- 提交前至少執行建置、acceptance harness、README 實際輸出比對與 `git diff --check`。
