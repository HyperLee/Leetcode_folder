# LeetCode 705 — Design HashSet（設計雜湊集合）

以 C# .NET 10 實作不依賴內建 `HashSet<T>` 的整數雜湊集合。此專案保留「自己設計雜湊表」的學習重點：先把 key 映射到固定數量的桶，再以鏈結串列安全處理碰撞。

- 英文題目：[Design HashSet](https://leetcode.com/problems/design-hashset/)
- 中文題目：[設計雜湊集合](https://leetcode.cn/problems/design-hashset/)

## 題目說明

不使用任何內建雜湊表函式庫，實作 `MyHashSet`：

- `MyHashSet()`：建立空集合。
- `Add(int key)`：加入 key；重複加入不會產生重複成員。
- `Remove(int key)`：移除 key；key 不存在時不做任何事。
- `Contains(int key)`：回傳 key 是否存在。

限制條件：`0 <= key <= 1_000_000`，`Add`、`Remove`、`Contains` 合計最多呼叫 `10^4` 次。

## 設計：分離鏈結法

`MyHashSet` 配置 769 個桶。769 是質數，雜湊索引為 `key % 769`；每個桶各自保存一條 `LinkedList<int>`。不同 key 得到相同索引時，不會覆蓋彼此，而是在同一桶中逐一比較。

核心不變量：

1. 每個 key 只會位於 `key % 769` 對應的桶。
2. 桶內不保存重複 key，因此 `Add` 必須先確認不存在才加入。
3. `Remove` 只移除完全相同的 key，不會破壞與它碰撞的其他成員。

### 碰撞逐步示範

`1`、`770`、`1539` 的餘數都為 1：

```plaintext
Add(1)    -> bucket[1] = 1
Add(770)  -> bucket[1] = 1 -> 770
Add(1539) -> bucket[1] = 1 -> 770 -> 1539
Remove(770) -> bucket[1] = 1 -> 1539
```

因此移除中間的 `770` 後，`Contains(1)` 和 `Contains(1539)` 仍必須為 `true`。

## 複雜度

令 `k` 為目標桶內的鏈結串列長度。

| 操作 | 平均時間 | 碰撞最壞時間 | 說明 |
| --- | --- | --- | --- |
| `Add` | `O(1)` | `O(k)` | 先搜尋桶內是否已有 key，再視需要加入。 |
| `Remove` | `O(1)` | `O(k)` | 在目標桶中搜尋並移除 key。 |
| `Contains` | `O(1)` | `O(k)` | 只檢查目標桶。 |

桶陣列固定使用 769 個參考，已加入的 `n` 個 key 存在鏈結節點中，總儲存空間為 `O(769 + n)`，以固定桶數表示即為 `O(n)`；此題不建立額外結果集合。

## Acceptance harness

`Program.Main` 為每個案例建立全新的 `MyHashSet`，避免前一個案例的狀態影響後一個案例。所有輸出集中於 `Main`，核心 API 保持純粹。

| # | 案例 | 驗證重點 |
| --- | --- | --- |
| 1 | Official example | 官方操作序列與四個查詢結果。 |
| 2 | Minimum key boundary | key `0` 可新增、查詢、移除。 |
| 3 | Maximum key boundary | key `1_000_000` 可新增、查詢、移除。 |
| 4 | Same-bucket collisions | 同桶三個 key 中移除一個，不影響其餘兩個。 |
| 5 | Duplicate add is idempotent | 重複加入後移除一次即不存在。 |
| 6 | Missing removal preserves neighbors | 移除不存在但同桶的 key，不破壞既有 key。 |
| 7 | Reinsert after removal | 移除後可再次加入。 |
| 8 | 10000-operation spot check | 恰好 10,000 次操作後的存在性狀態正確。 |

## 建置與執行

在題目根目錄 `leetcode_705/` 執行：

```bash
dotnet build leetcode_705/leetcode_705.csproj --nologo
dotnet run --no-build --project leetcode_705/leetcode_705.csproj
```

若使用 VS Code，直接開啟 `leetcode_705/` 後選擇 `Debug leetcode_705`；設定會先執行 `build leetcode_705`，再啟動 .NET 10 DLL。

## 實際輸出

下列內容來自新鮮執行 `dotnet run --no-build --project leetcode_705/leetcode_705.csproj` 的完整逐字輸出：

```text
Case: Official example
Input: Add(1), Add(2), Contains(1), Contains(3), Add(2), Contains(2), Remove(2), Contains(2)
Expected: [true, false, true, false]
Actual: [true, false, true, false]
Result: PASS

Case: Minimum key boundary
Input: Add(0), Contains(0), Remove(0), Contains(0)
Expected: [true, false]
Actual: [true, false]
Result: PASS

Case: Maximum key boundary
Input: Add(1000000), Contains(1000000), Remove(1000000), Contains(1000000)
Expected: [true, false]
Actual: [true, false]
Result: PASS

Case: Same-bucket collisions
Input: Add(1), Add(770), Add(1539), Remove(770)
Expected: [true, false, true]
Actual: [true, false, true]
Result: PASS

Case: Duplicate add is idempotent
Input: Add(42), Add(42), Remove(42), Contains(42)
Expected: [false]
Actual: [false]
Result: PASS

Case: Missing removal preserves neighbors
Input: Add(1), Add(770), Remove(1539), Contains(1), Contains(770)
Expected: [true, true]
Actual: [true, true]
Result: PASS

Case: Reinsert after removal
Input: Add(5), Remove(5), Add(5), Contains(5)
Expected: [true]
Actual: [true]
Result: PASS

Case: 10000-operation spot check
Input: 3333 adds, 3333 contains checks, 3332 removals, 2 final contains checks
Expected: true
Actual: true
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_705/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_705/
    ├── leetcode_705.csproj
    └── Program.cs
```

舊式 `leetcode_705.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 已由 SDK-style .NET 10 專案取代。
