# LeetCode 3607: Power Grid Maintenance（電網維護）

> **LeetCode 每日一題 - 2025/11/06**

這是 LeetCode 第 3607 題的 C# 解決方案，使用深度優先搜索（DFS）配合優先佇列（Priority Queue）來高效處理電網維護查詢。

## 題目描述

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

這個問題可以分為兩個核心部分：**連通塊計算** 和 **在線電站維護**。

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

## 程式碼結構

### `Vertex` 類別

表示電網中的一個發電站節點：

```csharp
public class Vertex
{
    public int vertexId;        // 發電站的唯一識別碼
    public bool offline;        // 標記發電站是否離線
    public int powerGridId;     // 所屬的電網編號
}
```

### `Traverse` 方法

使用 DFS 遍歷電網，標記所有屬於同一個連通分量的節點：

```csharp
private void Traverse(Vertex u, int powerGridId, 
                     PriorityQueue<int, int> powerGrid, 
                     List<List<int>> graph)
```

### `ProcessQueries` 方法

主要的查詢處理方法：

```csharp
public int[] ProcessQueries(int c, int[][] connections, int[][] queries)
```

## 執行專案

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
