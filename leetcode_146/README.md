# LeetCode 146 — LRU Cache

以 C# 與 .NET 10 實作最近最少使用（Least Recently Used, LRU）快取。本專案保留兩種平均 O(1) 的教學解法，並以同一組可執行測資驗證行為一致。

- [題目與限制](#題目與限制)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：手寫雙向鏈結串列](#解法一手寫雙向鏈結串列)
- [解法二：NET-LinkedListT](#解法二net-linkedlistt)
- [解法比較](#解法比較)
- [建置與執行](#建置與執行)

## 題目與限制

設計 `LRUCache`，支援以下操作：

- `Get(key)`：鍵存在時回傳對應值，並把該鍵標記為最近使用；不存在時回傳 `-1`。
- `Put(key, value)`：鍵存在時更新值並標記為最近使用；不存在時新增。新增後若超過容量，淘汰最久未使用的鍵。

兩個操作都必須達到平均 O(1) 時間複雜度。

限制條件：

- `1 <= capacity <= 3000`
- `0 <= key <= 10^4`
- `0 <= value <= 10^5`
- `Get` 與 `Put` 合計最多呼叫 `2 * 10^5` 次

題目連結：[LeetCode 英文](https://leetcode.com/problems/lru-cache/description/)｜[LeetCode 中文](https://leetcode.cn/problems/lru-cache/description/)

## 解題概念與出發點

如果只使用 Dictionary，可以在平均 O(1) 找到 key，卻無法知道哪個 key 最久未使用。如果只使用一般串列，可以記錄使用順序，但尋找指定 key 需要 O(n)。

因此需要讓兩種資料結構各自負責最擅長的工作：

1. **雜湊表**：把 key 映射到串列節點，以平均 O(1) 完成定位。
2. **雙向鏈結串列**：在 O(1) 移除已知節點、插入表頭，以及淘汰表尾。

兩種解法都維持相同的不變量：

```text
[MRU 最近使用] <-> ... <-> [LRU 最久未使用]
```

Get 命中、更新既有 key 或新增 key 都會讓該節點成為 MRU；容量超限時移除 LRU。

## 解法一：手寫雙向鏈結串列

`LRUCache` 自行定義 `Node`，每個節點保存 `Key`、`Value`、`Prev` 與 `Next`。`_keyToNode` 負責由 key 定位節點，`_dummy` 則是環狀雙向鏈結串列的哨兵：

```text
                 MRU                       LRU
                  ↓                         ↓
_dummy <-> node <-> node <-> ... <-> node <-> _dummy
```

哨兵同時代表串列的前後界線：

- `_dummy.Next` 是 MRU。
- `_dummy.Prev` 是 LRU。
- 空串列時 `_dummy.Next` 與 `_dummy.Prev` 都指向自己。

這種環狀設計讓第一個、最後一個及唯一節點都能使用相同的連結操作，不必為 null 或邊界分支撰寫特殊處理。

### Get 流程

1. 使用 `_keyToNode.TryGetValue` 尋找節點。
2. 找不到時回傳 `-1`，不改變使用順序。
3. 找到時由 `Remove` 接回節點的前後鄰居。
4. 由 `PushFront` 把節點插到 `_dummy` 後方。
5. 回傳節點保存的值。

### Put 流程

1. 若 key 已存在，先透過 `GetNode` 把節點移到 MRU，再更新 `Value`。
2. 若 key 不存在，建立節點、加入 Dictionary，再插到 MRU。
3. 若鍵數超過容量，`_dummy.Prev` 即為 LRU；從 Dictionary 與串列同時移除。

### 官方範例演示

下表由左至右表示 MRU 到 LRU：

| 操作 | 回傳 | 操作後順序 | 淘汰 |
| --- | ---: | --- | --- |
| `Put(1, 1)` | — | `[1=1]` | — |
| `Put(2, 2)` | — | `[2=2, 1=1]` | — |
| `Get(1)` | `1` | `[1=1, 2=2]` | — |
| `Put(3, 3)` | — | `[3=3, 1=1]` | `2` |
| `Get(2)` | `-1` | `[3=3, 1=1]` | — |
| `Put(4, 4)` | — | `[4=4, 3=3]` | `1` |
| `Get(1)` | `-1` | `[4=4, 3=3]` | — |
| `Get(3)` | `3` | `[3=3, 4=4]` | — |
| `Get(4)` | `4` | `[4=4, 3=3]` | — |

### 複雜度

- Get：平均 O(1)
- Put：平均 O(1)
- 空間：O(capacity)

## 解法二：.NET `LinkedList<T>`

`LRUCache2` 使用 .NET 內建的 `LinkedList<(int Key, int Value)>` 管理使用順序，並以 `Dictionary<int, LinkedListNode<(int Key, int Value)>>` 保存 key 到實際串列節點的映射。

- `_usageOrder.First` 是 MRU。
- `_usageOrder.Last` 是 LRU。
- Dictionary 保存 `LinkedListNode`，而不是只保存 value，因此命中後可以直接移動既有節點，不必再次搜尋串列。

### Get 流程

1. 使用 Dictionary 尋找對應的 `LinkedListNode`。
2. 找不到時回傳 `-1`。
3. 找到時呼叫 `MoveToFront`：先由 LinkedList 移除節點，再把同一節點加入表頭。
4. 回傳節點 tuple 中的 `Value`。

### Put 流程

1. 若 key 已存在，更新節點的 `(Key, Value)`，再移到表頭。
2. 若 key 不存在，使用 `AddFirst` 建立 MRU 節點，並把節點存入 Dictionary。
3. 若超過容量，取得 `_usageOrder.Last`，由 LinkedList 與 Dictionary 同時移除。

### 官方範例演示

此解法的可見 LRU 行為與解法一相同，但節點連結由 .NET `LinkedList<T>` 維護：

| 操作 | 回傳 | 操作後順序 | LinkedList 動作 |
| --- | ---: | --- | --- |
| `Put(1, 1)` | — | `[1=1]` | `AddFirst(1)` |
| `Put(2, 2)` | — | `[2=2, 1=1]` | `AddFirst(2)` |
| `Get(1)` | `1` | `[1=1, 2=2]` | 移除節點 1，再 `AddFirst` |
| `Put(3, 3)` | — | `[3=3, 1=1]` | `AddFirst(3)`，移除 Last 2 |
| `Get(2)` | `-1` | `[3=3, 1=1]` | 無變動 |
| `Put(4, 4)` | — | `[4=4, 3=3]` | `AddFirst(4)`，移除 Last 1 |
| `Get(1)` | `-1` | `[4=4, 3=3]` | 無變動 |
| `Get(3)` | `3` | `[3=3, 4=4]` | 移除節點 3，再 `AddFirst` |
| `Get(4)` | `4` | `[4=4, 3=3]` | 移除節點 4，再 `AddFirst` |

### 複雜度

- Get：平均 O(1)
- Put：平均 O(1)
- 空間：O(capacity)

## 解法比較

| 面向 | 解法一：手寫串列 | 解法二：`LinkedList<T>` |
| --- | --- | --- |
| key 定位 | `Dictionary<int, Node>` | `Dictionary<int, LinkedListNode<...>>` |
| 順序容器 | 自訂環狀雙向鏈結串列 | .NET `LinkedList<T>` |
| MRU / LRU | `_dummy.Next` / `_dummy.Prev` | `First` / `Last` |
| Get / Put | 平均 O(1) / 平均 O(1) | 平均 O(1) / 平均 O(1) |
| 空間 | O(capacity) | O(capacity) |
| 優點 | 能完整理解指標與哨兵技巧；控制程度高 | 程式較短；邊界與連結交由標準函式庫處理 |
| 代價 | 必須正確維護四個相鄰指標 | 需要理解 `LinkedListNode<T>`，抽象層較高 |

兩者都符合題目要求。若目標是學習資料結構，解法一較有價值；若目標是應用程式中的可讀性與維護性，解法二通常更直接。

## 可執行測試

`Main` 透過共同的 `ILruCache` 契約，把完全相同的四組案例分別交給兩種解法：

| 案例 | 驗證重點 | 每種解法的 Get 數 |
| --- | --- | ---: |
| 官方範例 | 命中、未命中、更新順序與兩次淘汰 | 5 |
| 更新既有鍵 | 更新 value 也必須刷新使用順序 | 3 |
| 容量為 1 | 新增第二個 key 後立即淘汰舊 key | 3 |
| 鍵值邊界 | `(0, 0)` 與 `(10000, 100000)` | 2 |

每種解法共有 13 次 Get；兩種解法合計 8 組案例、26 次 Get。

## 建置與執行

需要 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。從此 README 所在的專案根目錄執行：

```bash
dotnet restore leetcode_146/leetcode_146.csproj
dotnet build leetcode_146/leetcode_146.csproj --nologo
dotnet run --project leetcode_146/leetcode_146.csproj --no-build
```

目前沒有獨立測試專案；建置加上 deterministic console harness 即為驗收方式。

### 實際執行輸出

```text
=== 解法一：手寫雙向鏈結串列 ===
[PASS] 官方範例 | Expected: [1, -1, -1, 3, 4] | Actual: [1, -1, -1, 3, 4]
[PASS] 更新既有鍵 | Expected: [10, -1, 3] | Actual: [10, -1, 3]
[PASS] 容量為 1 | Expected: [1, -1, 2] | Actual: [1, -1, 2]
[PASS] 鍵值邊界 | Expected: [0, 100000] | Actual: [0, 100000]

=== 解法二：.NET LinkedList<T> ===
[PASS] 官方範例 | Expected: [1, -1, -1, 3, 4] | Actual: [1, -1, -1, 3, 4]
[PASS] 更新既有鍵 | Expected: [10, -1, 3] | Actual: [10, -1, 3]
[PASS] 容量為 1 | Expected: [1, -1, 2] | Actual: [1, -1, 2]
[PASS] 鍵值邊界 | Expected: [0, 100000] | Actual: [0, 100000]

總結：8/8 組案例通過，26/26 次 Get 驗證通過。
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_146/
    ├── leetcode_146.csproj
    └── Program.cs
```

## 延伸閱讀

- [Cache replacement policies — LRU](https://en.wikipedia.org/wiki/Cache_replacement_policies#LRU)
- [LeetCode 官方題解](https://leetcode.cn/problems/lru-cache/solutions/259678/lruhuan-cun-ji-zhi-by-leetcode-solution/)
- [LRU Cache 圖解](https://leetcode.cn/problems/lru-cache/solutions/2456294/tu-jie-yi-zhang-tu-miao-dong-lrupythonja-czgt/)
