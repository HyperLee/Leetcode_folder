# leetcode_705 開發指引

## 專案位置與常用指令

- 題目根目錄是 `leetcode_705/`；實際的巢狀 .NET 10 主控台專案是 `leetcode_705/leetcode_705.csproj`。
- 請在題目根目錄執行：

```plaintext
dotnet build leetcode_705/leetcode_705.csproj --nologo
dotnet run --no-build --project leetcode_705/leetcode_705.csproj
```

- 本題沒有正式測試專案。`Program.Main` 的 deterministic acceptance harness 是交付前的行為驗證，成功結尾必須是 `Summary: 8/8 checks passed.`。
- 在 VS Code 直接開啟 `leetcode_705/`，以 `Debug leetcode_705` 啟動；它會先執行 `build leetcode_705`。

## 實作契約

- 公開 API 是 `MyHashSet()`, `Add(int key)`, `Remove(int key)` 與 `Contains(int key)`；`MyHashSet` 不得使用內建 `HashSet<T>`，也不得輸出主控台。
- 有效輸入是 `0 <= key <= 1_000_000`。實作使用 769 個桶與 `LinkedList<int>` 分離鏈結法；相同餘數的 key 可共用同一桶，但每個 key 只能保存一次。
- `Add` 必須是 idempotent，`Remove` 找不到 key 時不得影響同桶其他成員，`Contains` 只回傳存在性。
- C# 使用四個空白縮排、file-scoped namespace、繁體中文 XML 文件與少量說明不變量的註解。所有 console 輸出必須集中在 `Main`。

## 驗證與 Git 範圍

- 修改後執行 build 與 run；README 的完整輸出必須與 fresh run 完全一致。
- Git metadata 位於 parent repository `Leetcode_folder/`；commit、PR 與所有 tracked changes 都必須限制在 `leetcode_705/`。
- 不要修改 acceptance harness、公開 API 或 README 案例數，除非任務明確要求同步更新實作、文件與驗證輸出。
