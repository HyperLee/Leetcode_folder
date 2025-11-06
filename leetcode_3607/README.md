# LeetCode 3607: Power Grid Maintenance（電網維護）

> **LeetCode 每日一題 - 2025/11/06**

這是 LeetCode 第 3607 題的 C# 解決方案，使用深度優先搜索（DFS）配合優先佇列（Priority Queue）來高效處理電網維護查詢。

## 執行結果

所有測試案例都通過了！兩種解法的結果完全一致：

```text
=== 解法一：使用 Vertex 類別封裝 ===

測試案例 1: [1, 1, 1] ✓
測試案例 2: [1, 2, 3] ✓
測試案例 3: [-1] ✓
測試案例 4: [1, 2, 3, 4] ✓

=== 解法二：使用陣列優化（記憶體效率更高） ===

測試案例 1: [1, 1, 1] ✓
測試案例 2: [1, 2, 3] ✓
測試案例 3: [-1] ✓
測試案例 4: [1, 2, 3, 4] ✓
```

## 執行專案

給定一個整數 `c`，表示 `c` 個發電站，每個發電站有一個從 1 到 `c` 的唯一識別碼（1 基索引）。這些發電站通過 `n` 條雙向電纜連接，表示為 2D 陣列 `connections`，其中每個元素 `connections[i] = [ui, vi]` 表示發電站 `ui` 和 `vi` 之間的連接。

**直接或間接連接的發電站形成一個電網。** 最初，所有發電站都在線（運作中）。

還給定一個 2D 陣列 `queries`，其中每個查詢是以下兩種類型之一：

1. **`[1, x]`**：請求對發電站 `x` 進行維護檢查
   - 如果發電站 `x` 在線，它自己解決檢查
   - 如果發電站 `x` 離線，檢查由同一電網中識別碼最小的運作發電站解決
   - 如果該電網中沒有運作發電站，則返回 `-1`

2. **`[2, x]`**：發電站 `x` 離線（即變為非運作）

返回一個整數陣列，表示每個類型 `[1, x]` 查詢的結果，按它們出現的順序。

> **注意**：電網保持其結構；離線（非運作）節點仍然是其電網的一部分，將其離線不會改變連接性。

### 範例

#### 範例 1

**輸入：**

```text
c = 4
connections = [[1,2],[2,3],[3,4]]
queries = [[1,1],[2,2],[1,2],[1,1]]
```

**輸出：** `[1, 1, 1]`

**說明：**

- 所有發電站形成一個電網
- `[1, 1]`：發電站 1 在線，返回 1
- `[2, 2]`：發電站 2 離線
- `[1, 2]`：發電站 2 已離線，電網中最小編號的在線發電站是 1，返回 1
- `[1, 1]`：發電站 1 在線，返回 1

#### 範例 2

**輸入：**

```text
c = 5
connections = [[1,2],[3,4]]
queries = [[1,1],[2,1],[1,1],[1,3]]
```

**輸出：** `[1, 2, 3]`

**說明：**

- 電網 1：發電站 1, 2
- 電網 2：發電站 3, 4
- 電網 3：發電站 5（孤立節點）
- `[1, 1]`：發電站 1 在線，返回 1
- `[2, 1]`：發電站 1 離線
- `[1, 1]`：發電站 1 已離線，電網中最小編號的在線發電站是 2，返回 2
- `[1, 3]`：發電站 3 在線，返回 3

### 限制條件

- `1 <= c <= 10^5`
- `0 <= connections.length <= min(c * (c - 1) / 2, 10^5)`
- `connections[i].length == 2`
- `1 <= ui, vi <= c`
- `ui != vi`
- `1 <= queries.length <= 10^5`
- `queries[i].length == 2`
- `1 <= queries[i][0] <= 2`
- `1 <= queries[i][1] <= c`

## 解法思路

這個問題可以分為兩個核心部分：**連通塊計算** 和 **在線電站維護**。本專案提供兩種解法。

## 解法一：使用 Vertex 類別封裝（物件導向）

### 核心思想

使用物件導向設計，將發電站的所有資訊封裝在 `Vertex` 類別中，使程式碼結構更清晰易懂。

### 1. 計算連通塊（DFS）

使用深度優先搜索（DFS）來識別圖中的所有連通分量（電網）：

- 遍歷所有尚未被訪問的節點，每個未訪問節點作為新電網的入口
- 從入口節點開始 DFS，所有能訪問到的節點都屬於同一個電網
- 為每個節點標記其所屬的電網 ID

### 2. 維護在線電站的最小編號（優先佇列）

使用**優先佇列（小根堆）**為每個電網維護在線發電站的編號：

- 每個電網對應一個優先佇列
- 佇列始終保持最小編號的發電站在堆頂
- 當發電站離線時，不立即從佇列中刪除

### 3. 惰性刪除技巧

關鍵優化：使用**惰性刪除**策略來處理離線操作：

- 當發電站離線（操作類型 2）時，僅標記該節點的 `offline` 屬性為 `true`
- 不立即從優先佇列中刪除該元素
- 在查詢操作（操作類型 1）時，才真正移除離線的發電站：
  - 持續從堆頂彈出元素，直到找到在線的發電站或佇列為空
  - 這樣避免了複雜的堆內刪除操作

### 演算法流程

```text
1. 建立圖的鄰接表表示
2. 使用 DFS 計算所有連通分量
3. 為每個連通分量建立優先佇列
4. 處理查詢：
   - 類型 1 [1, x]：
     * 如果 x 在線 → 返回 x
     * 如果 x 離線 → 從對應電網的優先佇列中找到最小編號的在線發電站
   - 類型 2 [2, x]：
     * 標記 x 為離線狀態
```

### 複雜度分析

- **時間複雜度**：`O(n + m + q log n)`
  - `n`：發電站數量
  - `m`：連接數（邊數）
  - `q`：查詢數量
  - DFS 遍歷：`O(n + m)`
  - 每個查詢最多需要 `O(log n)` 時間處理優先佇列

- **空間複雜度**：`O(n + m)`
  - 圖的鄰接表：`O(n + m)`
  - 優先佇列：`O(n)`
  - 節點資訊陣列：`O(n)`

## 解法二：使用陣列優化（記憶體效率更高）

### 核心思想

放棄物件封裝，改用原始陣列來管理節點狀態，並預先計算答案陣列大小，以提升記憶體效率和執行速度。

### 與解法一的主要差異

| 項目 | 解法一 | 解法二 |
|------|--------|--------|
| 節點資訊儲存 | `Vertex` 類別（封裝 `vertexId`, `offline`, `powerGridId`） | 分離的陣列（`belong[]`, `offline[]`） |
| 離線狀態管理 | `Vertex.offline` 屬性 | 獨立的 `bool[] offline` 陣列 |
| 答案陣列建立 | 使用 `List<int>` 動態添加，最後轉換 | 預先計算大小，直接分配 `int[]` |
| 記憶體開銷 | 較高（物件封裝） | 較低（原始陣列） |
| 程式碼可讀性 | 較高（物件導向） | 中等（效能導向） |

### 詳細演算法流程

#### 步驟 1：建立圖的鄰接表

```csharp
List<int>[] graph = new List<int>[c + 1];
for (int i = 0; i <= c; i++)
{
    graph[i] = new List<int>();
}

foreach (var edge in connections)
{
    int x = edge[0], y = edge[1];
    graph[x].Add(y);
    graph[y].Add(x);
}
```

**說明：**
- 建立大小為 `c+1` 的陣列（索引 0 不使用，因為發電站編號從 1 開始）
- 遍歷所有連接，為每條邊建立雙向關係

#### 步驟 2：初始化 belong 陣列並計算連通分量

```csharp
int[] belong = new int[c + 1];
Array.Fill(belong, -1);  // -1 表示尚未訪問

List<PriorityQueue<int, int>> heaps = new List<PriorityQueue<int, int>>();

for (int i = 1; i <= c; i++)
{
    if (belong[i] >= 0)  // 已訪問，跳過
        continue;
    
    PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
    DfsV2(i, graph, belong, heaps.Count, pq);
    heaps.Add(pq);
}
```

**說明：**
- `belong[i]`：記錄節點 `i` 屬於哪個電網（連通分量）
  - `-1`：尚未訪問
  - `>= 0`：電網編號
- 每發現一個新的連通分量，就為其建立一個新的優先佇列
- `heaps.Count` 作為新電網的 ID

#### 步驟 3：DFS 遍歷連通分量

```csharp
private void DfsV2(int x, List<int>[] graph, int[] belong, int compId, PriorityQueue<int, int> pq)
{
    belong[x] = compId;      // 標記節點 x 屬於哪個電網
    pq.Enqueue(x, x);        // 將節點加入該電網的優先佇列
    
    foreach (int y in graph[x])
    {
        if (belong[y] < 0)   // 相鄰節點尚未訪問
        {
            DfsV2(y, graph, belong, compId, pq);
        }
    }
}
```

**說明：**
- 標記當前節點所屬的電網
- 將節點編號加入優先佇列（編號同時作為元素和優先級）
- 遞迴訪問所有未訪問的相鄰節點

#### 步驟 4：預先計算答案陣列大小

```csharp
int ansSize = 0;
foreach (var q in queries)
{
    if (q[0] == 1)  // 只有類型 1 查詢需要返回結果
        ansSize++;
}

int[] ans = new int[ansSize];
int idx = 0;
```

**關鍵優化點：**
- 遍歷一次查詢陣列，統計類型 1 查詢的數量
- 預先分配答案陣列，避免動態擴容帶來的效能損耗
- 使用 `idx` 追蹤當前填充位置

#### 步驟 5：處理查詢

```csharp
bool[] offline = new bool[c + 1];  // 離線狀態陣列

foreach (var q in queries)
{
    int x = q[1];
    
    if (q[0] == 2)
    {
        // 類型 2：標記發電站 x 離線
        offline[x] = true;
        continue;
    }
    
    // 類型 1：查詢維護檢查由誰負責
    if (!offline[x])
    {
        ans[idx++] = x;  // x 在線，直接返回 x
        continue;
    }
    
    // x 離線，需要找到同一電網中最小編號的在線發電站
    PriorityQueue<int, int> pq = heaps[belong[x]];
    
    // 惰性刪除：移除堆頂的離線發電站
    while (pq.Count > 0 && offline[pq.Peek()])
    {
        pq.Dequeue();
    }
    
    ans[idx++] = pq.Count > 0 ? pq.Peek() : -1;
}
```

**查詢處理流程：**

1. **類型 2（離線操作）：**
   - 將 `offline[x]` 設為 `true`
   - 不修改優先佇列（惰性刪除）

2. **類型 1（查詢操作）：**
   - **情況 A：** 發電站 `x` 在線
     - 直接返回 `x`
   
   - **情況 B：** 發電站 `x` 離線
     1. 找到 `x` 所屬的電網：`belong[x]`
     2. 獲取該電網的優先佇列：`heaps[belong[x]]`
     3. 惰性刪除：不斷彈出堆頂的離線發電站
     4. 返回堆頂（最小編號的在線發電站）或 `-1`（電網全部離線）

### 惰性刪除的優勢

**為什麼不在類型 2 操作時立即從堆中刪除？**

1. **堆中刪除的複雜度高：**
   - 直接刪除堆中間元素需要 `O(n)` 時間
   - 需要重新建堆或調整堆結構

2. **惰性刪除只在需要時處理：**
   - 類型 2 操作：`O(1)` 時間（只標記）
   - 類型 1 操作：均攤 `O(log n)` 時間（每個元素最多被彈出一次）

3. **總體效率更高：**
   - 如果某個發電站離線後從未被查詢，就節省了刪除操作
   - 如果被查詢，只在那時才進行刪除

### 範例執行流程

**輸入：**
```text
c = 5
connections = [[1,2], [3,4]]
queries = [[1,1], [2,1], [1,1], [1,3]]
```

**步驟 1：建立圖**
```
電網 0: 1 - 2
電網 1: 3 - 4
電網 2: 5（孤立節點）
```

**步驟 2：初始化**
```
belong = [-, 0, 0, 1, 1, 2]  (索引 0 不使用)
heaps[0] = [1, 2]
heaps[1] = [3, 4]
heaps[2] = [5]
```

**步驟 3：處理查詢**

| 查詢 | 操作 | offline 狀態 | heaps 狀態 | 結果 |
|------|------|-------------|-----------|------|
| `[1,1]` | 查詢 1 | `[-, F, F, F, F, F]` | 未變 | `1`（1 在線） |
| `[2,1]` | 離線 1 | `[-, T, F, F, F, F]` | 未變（惰性） | - |
| `[1,1]` | 查詢 1 | `[-, T, F, F, F, F]` | heaps[0] = [2]（彈出 1） | `2`（1 離線，最小在線是 2） |
| `[1,3]` | 查詢 3 | `[-, T, F, F, F, F]` | 未變 | `3`（3 在線） |

**輸出：** `[1, 2, 3]`

### 複雜度分析

- **時間複雜度**：`O(n + m + q log n)`
  - 建圖：`O(m)`
  - DFS 遍歷：`O(n + m)`
  - 預先計算答案大小：`O(q)`
  - 處理查詢：`O(q log n)`（每個元素最多入堆一次、出堆一次）

- **空間複雜度**：`O(n + m)`
  - 圖的鄰接表：`O(n + m)`
  - `belong` 陣列：`O(n)`
  - `offline` 陣列：`O(n)`
  - 優先佇列：`O(n)`

### 解法二的優勢

1. **記憶體效率：** 使用原始陣列而非物件封裝，減少記憶體開銷
2. **執行效率：** 預先分配答案陣列，避免動態擴容
3. **適合大規模資料：** 當 `c` 和 `q` 很大時，效能優勢更明顯

### 解法選擇建議

- **解法一（Vertex 類別）：** 適合重視程式碼可讀性和維護性的場景
- **解法二（陣列優化）：** 適合追求極致效能，或資料規模很大的場景

## 兩種解法的程式碼結構對比

### 解法一：Vertex 類別

表示電網中的一個發電站節點：

```csharp
public class Vertex
{
    public int vertexId;        // 發電站的唯一識別碼
    public bool offline;        // 標記發電站是否離線
    public int powerGridId;     // 所屬的電網編號
}
```

### 解法二：陣列結構

使用多個陣列分離管理：

```csharp
int[] belong;        // belong[i] = 節點 i 所屬的電網編號
bool[] offline;      // offline[i] = 節點 i 是否離線
```

## 關鍵技術點

### 前置需求

- .NET 8.0 或更高版本

### 建構專案

```bash
dotnet build
```

### 執行程式

```bash
dotnet run
```

程式會執行多個測試案例，展示不同的電網維護查詢場景。

### 測試案例

專案包含以下測試案例：

1. **基本電網維護查詢**：單一連通電網，測試基本查詢和離線操作
2. **多個電網**：多個獨立的電網，測試跨電網查詢
3. **電網全部離線**：所有發電站都離線的情況
4. **複雜離線和查詢操作**：混合多種操作類型

## 關鍵技術點

### 1. 惰性刪除

避免直接在優先佇列中刪除元素（複雜度高），而是在查詢時才移除：

```csharp
while (powerGrid.Count > 0 && vertices[powerGrid.Peek()].offline)
{
    powerGrid.Dequeue();
}
```

### 2. 連通分量標記

使用 `powerGridId` 來快速識別節點所屬的電網：

```csharp
if (v.powerGridId == -1)
{
    Traverse(v, powerGridId, powerGrid, graph);
}
```

### 3. 優先佇列的使用

C# 的 `PriorityQueue<TElement, TPriority>` 自動維護最小元素在堆頂：

```csharp
powerGrid.Enqueue(u.vertexId, u.vertexId);  // 編號同時作為元素和優先級
```

## 相關連結

- [LeetCode 題目（英文）](https://leetcode.com/problems/power-grid-maintenance/description/?envType=daily-question&envId=2025-11-06)
- [LeetCode 題目（中文）](https://leetcode.cn/problems/power-grid-maintenance/description/?envType=daily-question&envId=2025-11-06)
- [官方解答](https://leetcode.cn/problems/power-grid-maintenance/solutions/3819897/dian-wang-wei-hu-by-leetcode-solution-d92h/?envType=daily-question&envId=2025-11-06)
- [灵茶山艾府解答](https://leetcode.cn/problems/power-grid-maintenance/solutions/3716402/dfs-lan-shan-chu-dui-pythonjavacgo-by-en-17gb/?envType=daily-question&envId=2025-11-06)
- [灵茶山艾府解答 - 视频讲解](https://www.bilibili.com/video/BV1GF3qzMEni/?t=5m42s)

## 學習資源

### 相關演算法概念

- **深度優先搜索（DFS）**：用於圖的遍歷和連通分量計算
- **廣度優先搜索（BFS）**：另一種圖遍歷方法，也可用於本題
- **優先佇列（Priority Queue）**：維護動態最小值的高效資料結構
- **惰性刪除**：延遲資料結構操作以提高效率的技巧
- **連通分量**：圖論中的基本概念

### 類似題目

- LeetCode 200: Number of Islands（島嶼數量）
- LeetCode 547: Number of Provinces（省份數量）
- LeetCode 684: Redundant Connection（冗餘連接）

---

*本專案為 LeetCode 每日一題的練習解答，僅供學習參考。*
