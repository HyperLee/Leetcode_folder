# LeetCode 947. Most Stones Removed with Same Row or Column

> 移除最多的同行或同列石頭

| 難度 | 標籤 |
|------|------|
| Medium | 並查集 (Union-Find)、深度優先搜尋、圖 |

- [English](https://leetcode.com/problems/most-stones-removed-with-same-row-or-column/description/)
- [中文](https://leetcode.cn/problems/most-stones-removed-with-same-row-or-column/description/)

## 題目描述

在二維平面上，我們在整數座標點上放置了 `n` 顆石頭，每個座標點最多只能有一顆石頭。

如果一顆石頭與另一顆**尚未被移除**的石頭共享**同一行**或**同一列**，則該石頭可以被移除。

給定一個長度為 `n` 的陣列 `stones`，其中 `stones[i] = [xi, yi]` 表示第 `i` 顆石頭的位置，請回傳**最多可以移除的石頭數量**。

### 限制條件

- `1 <= stones.length <= 1000`
- `0 <= xi, yi <= 10^4`
- 不會有兩顆石頭位於同一座標

## 解題概念與出發點

### 將石頭視為圖的頂點

把二維座標平面上的石頭想像成**圖的頂點**：若兩顆石頭的橫座標相同或縱座標相同，它們之間就存在一條**邊**。

### 連通分量與最大移除數

根據移除規則，只要一顆石頭的同行或同列上還有其他石頭存在，就可以移除它。由此可以推導出：

> **一個連通分量中的所有石頭，最終一定可以只留下一顆。**

原因是：既然這些頂點在同一個連通圖裡，可以透過遍歷（DFS / BFS）到達每個頂點。按照遍歷的**逆序**移除石頭，最後只剩一顆。

因此核心公式為：

$$
\text{最多可移除石頭數} = \text{石頭總數} - \text{連通分量個數}
$$

## 解法一：並查集 — 懶初始化 (Dictionary)

### 為什麼用並查集？

題目不需要給出具體的移除方案，只需計算連通分量的個數。並查集天生適合動態合併、查詢連通性的場景。

### 合併的語義

遍歷每顆石頭 `[x, y]`，在並查集中將 `x`（行）與 `y`（列）合併，語義是：

> **所有橫座標為 x 的石頭** 以及 **所有縱座標為 y 的石頭** 都屬於同一個連通分量。

### 如何在一維並查集中區分行與列？

石頭的座標是二維有序對 `(x, y)`，但並查集底層只有一維的鍵。若直接存入，行 `3` 和列 `3` 會被誤認為同一個節點。

由於 `0 <= x, y <= 10^4`，我們將**行座標**映射到 `[10001, 20001]` 區間（即 `x + 10001`），使之與列座標 `[0, 10000]` 完全不重疊。

### 實作要點

| 功能 | 說明 |
|------|------|
| **懶初始化** | 使用 `Dictionary<int, int>` 儲存父節點，遇到新元素時自動建立節點並將連通分量數 +1 |
| **路徑壓縮** | `Find` 遞迴時將沿途節點直接指向根節點，使後續查詢接近 O(1) |
| **合併操作** | `Union` 找到兩個元素的根節點，若不同則合併並將連通分量數 −1 |

### 程式碼

```csharp
public int RemoveStones(int[][] stones)
{
    UnionFind unionFind = new UnionFind();

    foreach (int[] stone in stones)
    {
        // 行座標加 10001 以區分列座標
        unionFind.Union(stone[0] + 10001, stone[1]);
    }

    // 石頭總數 − 連通分量個數 = 最多可移除石頭數
    return stones.Length - unionFind.Count;
}
```

## 範例演示流程

以 `stones = [[0,0],[0,1],[1,0],[1,2],[2,1],[2,2]]` 為例，預期輸出 **5**。

### 步驟拆解

| 步驟 | 石頭 | Union 操作 | 說明 |
|:----:|:-----:|:----------:|------|
| 1 | `[0,0]` | `Union(10001, 0)` | 新建節點 10001（行0）和 0（列0），合併 → 連通分量數：1 |
| 2 | `[0,1]` | `Union(10001, 1)` | 新建節點 1（列1），行0 已存在 → 將列1 併入行0 所在分量 → 連通分量數：1 |
| 3 | `[1,0]` | `Union(10002, 0)` | 新建節點 10002（行1），列0 已存在 → 將行1 併入列0 所在分量 → 連通分量數：1 |
| 4 | `[1,2]` | `Union(10002, 2)` | 新建節點 2（列2），行1 已存在 → 將列2 併入行1 所在分量 → 連通分量數：1 |
| 5 | `[2,1]` | `Union(10003, 1)` | 新建節點 10003（行2），列1 已存在 → 將行2 併入列1 所在分量 → 連通分量數：1 |
| 6 | `[2,2]` | `Union(10003, 2)` | 行2 與列2 已在同一分量中 → 不合併 → 連通分量數：1 |

### 結果

- 石頭總數：**6**
- 連通分量數：**1**（全部石頭互相連通）
- 最多可移除：`6 - 1 = 5` ✅

### 複雜度分析

| | 複雜度 |
|---|--------|
| **時間** | $O(n \cdot \alpha(n))$，其中 $\alpha$ 為反阿克曼函式，近似 $O(n)$ |
| **空間** | $O(n)$，並查集所使用的 Dictionary 儲存空間 |

## 解法二：並查集 — 預先初始化 + 按秩合併 (Array)

### 與解法一的差異

| | 解法一 | 解法二 |
|---|--------|--------|
| **並查集節點** | 行座標與列座標 | 石頭索引 (0 ~ n-1) |
| **儲存結構** | `Dictionary<int, int>`（懶初始化） | `int[]`（預先初始化） |
| **合併策略** | 僅路徑壓縮 | 路徑壓縮 + 按秩合併 |
| **合併語義** | 將行與列合併 | 將同行 / 同列的石頭合併 |

### 設計思路

解法一將「行」與「列」作為並查集的節點，巧妙但較抽象。解法二採取更直觀的作法：

1. **以石頭索引為節點**：建立大小為 `n` 的並查集，每顆石頭對應一個節點。
2. **分組合併**：使用 `Dictionary<int, List<int>>` 將石頭按行座標和列座標分組，同一組內的石頭全部合併。
3. **雙重優化**：同時使用路徑壓縮與按秩合併，均攤時間複雜度為 $O(\alpha(n))$。

### 分組合併的過程

將石頭按行和列分組後，只需將**每組的第一顆石頭與其餘石頭逐一合併**，即可確保同行或同列的所有石頭位於同一連通分量中。

以 `stones = [[0,0],[0,1],[1,0],[1,2],[2,1],[2,2]]` 為例：

**按行分組：**

| 行 | 石頭索引 |
|-----|------------|
| 0 | 0, 1 |
| 1 | 2, 3 |
| 2 | 4, 5 |

**按列分組：**

| 列 | 石頭索引 |
|-----|------------|
| 0 | 0, 2 |
| 1 | 1, 4 |
| 2 | 3, 5 |

**合併過程：**

| 步驟 | 操作 | 來源 | 說明 |
|:----:|:----:|:----:|------|
| 1 | `Union(0, 1)` | 行 0 | 石頭 0 與石頭 1 同行，合併 → 連通分量數：5 |
| 2 | `Union(2, 3)` | 行 1 | 石頭 2 與石頭 3 同行，合併 → 連通分量數：4 |
| 3 | `Union(4, 5)` | 行 2 | 石頭 4 與石頭 5 同行，合併 → 連通分量數：3 |
| 4 | `Union(0, 2)` | 列 0 | 石頭 0 與石頭 2 同列，合併 → 連通分量數：2 |
| 5 | `Union(1, 4)` | 列 1 | 石頭 1 與石頭 4 同列，合併 → 連通分量數：1 |
| 6 | `Union(3, 5)` | 列 2 | 石頭 3 與石頭 5 已在同一分量 → 連通分量數：1 |

**結果**：`6 - 1 = 5` ✅

### 程式碼

```csharp
public int RemoveStones2(int[][] stones)
{
    int n = stones.Length;
    UnionFind unionFind = new UnionFind(n);

    // 使用 Dictionary 將同行或同列的石頭索引分組
    Dictionary<int, List<int>> rowGroups = new();
    Dictionary<int, List<int>> colGroups = new();

    for (int i = 0; i < n; i++)
    {
        int row = stones[i][0];
        int col = stones[i][1];

        if (!rowGroups.ContainsKey(row))
            rowGroups[row] = new List<int>();
        rowGroups[row].Add(i);

        if (!colGroups.ContainsKey(col))
            colGroups[col] = new List<int>();
        colGroups[col].Add(i);
    }

    // 將同行的石頭合併至同一連通分量
    foreach (List<int> group in rowGroups.Values)
    {
        for (int i = 1; i < group.Count; i++)
        {
            unionFind.Union(group[0], group[i]);
        }
    }

    // 將同列的石頭合併至同一連通分量
    foreach (List<int> group in colGroups.Values)
    {
        for (int i = 1; i < group.Count; i++)
        {
            unionFind.Union(group[0], group[i]);
        }
    }

    // 石頭總數 − 連通分量個數 = 最多可移除石頭數
    return n - unionFind.Count;
}
```

### 複雜度分析

| | 複雜度 |
|---|--------|
| **時間** | $O(n \cdot \alpha(n))$，其中 $\alpha$ 為反阿克曼函式，近似 $O(n)$ |
| **空間** | $O(n)$，並查集陣列與分組 Dictionary 儲存空間 |

---

## 補充說明：並查集 (Union-Find) 完整詳解

### 什麼是並查集？

並查集（Union-Find），又稱**不相交集合**（Disjoint Set Union, DSU），是一種專門用於管理**元素分組**的資料結構。它高效地支援兩種核心操作：

| 操作 | 說明 |
|------|------|
| **Find（查找）** | 判斷某個元素屬於哪一個集合（透過找到該集合的**代表元素 / 根節點**） |
| **Union（合併）** | 將兩個不同的集合合併為一個集合 |

並查集常用於解決**連通性問題**，例如：
- 判斷圖中兩個頂點是否連通
- 計算圖的連通分量個數
- Kruskal 最小生成樹演算法
- 社交網路中的朋友圈問題

### 基本原理

並查集使用**樹（森林）結構**來表示多個不相交的集合，每棵樹代表一個集合：

```
集合 A        集合 B
  1             4
 / \            |
2   3           5
```

- 每個節點有一個指向**父節點**的指標
- 樹的**根節點**是該集合的代表元素（根節點的父節點指向自身）
- 若兩個元素的根節點相同，代表它們屬於同一個集合

### 核心操作詳解

#### 1. 初始化

一開始每個元素獨立成一棵樹（自己是自己的根），即每個元素各自為一個集合：

```
parent[x] = x  （對所有元素 x）
```

```
初始狀態（5 個元素）：
  0   1   2   3   4       ← 每個元素都是獨立的根節點
```

#### 2. Find（查找根節點）

從節點 `x` 開始，沿著父節點指標一路向上，直到找到根節點（`parent[root] == root`）：

```
Find(5)：
  1
  |
  3
  |
  5  ← 從這裡出發，沿父節點往上
  
路徑：5 → 3 → 1（根節點）
回傳：1
```

**未優化版本**的時間複雜度為 $O(h)$，其中 $h$ 是樹高。最壞情況下樹退化為鏈，$h = n$。

#### 3. Union（合併兩個集合）

將元素 `x` 和元素 `y` 所屬的集合合併：先分別找到兩者的根節點，若根節點不同，則將其中一個根指向另一個：

```
合併前：
  集合 A: 1 ← 2    集合 B: 4 ← 5
          ↑
          3

Union(3, 5)：
  Find(3) = 1, Find(5) = 4
  將根節點 1 指向根節點 4（或反向皆可）

合併後：
        4
       / \
      1   5
     / \
    2   3
```

### 優化策略

未優化的並查集在最壞情況下效能退化為 $O(n)$。以下兩種經典優化可將操作的均攤時間降至近乎常數：

#### 優化一：路徑壓縮 (Path Compression)

**核心思想**：在執行 `Find` 的過程中，將沿途所有節點**直接掛到根節點**下，使樹變得極度扁平。

```
路徑壓縮前：           路徑壓縮後（Find(5) 之後）：
    1                      1
    |                    / | \
    2                   2  3  5
    |
    3
    |
    5
```

**遞迴實作**（本題所用）：

```csharp
public int Find(int x)
{
    if (parent[x] != x)
    {
        parent[x] = Find(parent[x]);  // 遞迴壓縮：將 x 直接指向根
    }
    return parent[x];
}
```

**迭代實作**：

```csharp
public int Find(int x)
{
    int root = x;
    while (parent[root] != root)
    {
        root = parent[root];
    }
    // 第二輪：將沿途所有節點直接指向根
    while (parent[x] != root)
    {
        int next = parent[x];
        parent[x] = root;
        x = next;
    }
    return root;
}
```

路徑壓縮後，下次對同一路徑上的任何節點呼叫 `Find`，只需 $O(1)$。

#### 優化二：按秩合併 (Union by Rank)

**核心思想**：合併時，總是將**較矮的樹**掛到**較高的樹**下方，以避免樹高度持續增長。

```
按秩合併示意：
  rank=2      rank=1
    1           4         →  將較矮的 4 掛到較高的 1 下
   / \          |
  2   3         5

合併後：
      1          ← rank 維持 2
    / | \
   2  3  4
         |
         5
```

```csharp
public void Union(int x, int y)
{
    int rootX = Find(x);
    int rootY = Find(y);
    if (rootX == rootY) return;

    // 將較矮的樹掛到較高的樹下
    if (rank[rootX] < rank[rootY])
    {
        parent[rootX] = rootY;
    }
    else if (rank[rootX] > rank[rootY])
    {
        parent[rootY] = rootX;
    }
    else
    {
        parent[rootY] = rootX;
        rank[rootX]++;  // 等高時合併，高度 +1
    }
}
```

> **本題的實作**為了簡潔，僅使用路徑壓縮而未使用按秩合併，因為題目規模（$n \le 1000$）下已足夠高效。

### 兩種優化的綜合效果

| 優化策略 | 單次操作時間複雜度 |
|----------|-------------------|
| 無優化 | $O(n)$（最壞情況） |
| 僅路徑壓縮 | 均攤 $O(\log n)$ |
| 僅按秩合併 | $O(\log n)$ |
| **路徑壓縮 + 按秩合併** | **均攤 $O(\alpha(n))$** |

其中 $\alpha(n)$ 是**反阿克曼函式**（Inverse Ackermann Function），增長極其緩慢：

| $n$ | $\alpha(n)$ |
|-----|-------------|
| $1$ | $0$ |
| $2$ | $1$ |
| $\le 2^{65536}$ | $\le 4$ |

對於所有實際可能遇到的 $n$ 值，$\alpha(n) \le 4$，因此可視為**常數時間**。

### 懶初始化 vs 預先初始化

| 方式 | 適用場景 | 資料結構 |
|------|----------|----------|
| **預先初始化** | 元素範圍已知且連續（如 `0` ~ `n-1`） | 陣列 `int[] parent` |
| **懶初始化** | 元素範圍未知、稀疏、或需要動態新增 | `Dictionary<int, int> parent` |

**本題使用懶初始化**，原因是：
- 行座標映射後範圍為 `[10001, 20001]`，列座標範圍為 `[0, 10000]`
- 實際用到的節點遠少於整個範圍（最多 `2n` 個節點，$n \le 1000$）
- 使用 Dictionary 可以按需建立節點，避免浪費空間

```csharp
// 懶初始化：第一次 Find 某個元素時自動建立
public int Find(int x)
{
    if (!parent.ContainsKey(x))
    {
        parent[x] = x;   // 新節點，自己是自己的根
        Count++;          // 新增一個連通分量
    }
    if (parent[x] != x)
    {
        parent[x] = Find(parent[x]);
    }
    return parent[x];
}
```

### 並查集的經典應用場景

| 應用場景 | 說明 |
|----------|------|
| **連通分量計數** | 如本題：計算圖中有幾個連通分量 |
| **判斷環路** | 若 `Union(u, v)` 時 `Find(u) == Find(v)`，則加入邊 `(u, v)` 會形成環 |
| **Kruskal 最小生成樹** | 將邊按權重排序，用並查集判斷是否形成環，不形成則加入生成樹 |
| **動態連通性** | 持續加入邊，即時回答「兩點是否連通？」 |
| **等價類別劃分** | 將具有等價關係的元素歸為同一組（如帳號合併、句子相似性） |

### 完整並查集模板（含路徑壓縮 + 按秩合併）

```csharp
/// <summary>
/// 通用並查集模板，支援路徑壓縮與按秩合併。
/// </summary>
public class UnionFind
{
    private readonly int[] parent;
    private readonly int[] rank;
    public int Count { get; private set; }

    /// <summary>
    /// 初始化並查集，建立 n 個獨立的集合（元素 0 ~ n-1）。
    /// </summary>
    /// <param name="n">元素總數</param>
    public UnionFind(int n)
    {
        Count = n;
        parent = new int[n];
        rank = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }
    }

    /// <summary>
    /// 查找元素 x 的根節點，使用路徑壓縮優化。
    /// </summary>
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]);
        }
        return parent[x];
    }

    /// <summary>
    /// 合併元素 x 與元素 y 所屬集合，使用按秩合併優化。
    /// </summary>
    /// <returns>是否實際執行了合併（false 表示已在同一集合）</returns>
    public bool Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);
        if (rootX == rootY) return false;

        if (rank[rootX] < rank[rootY])
        {
            parent[rootX] = rootY;
        }
        else if (rank[rootX] > rank[rootY])
        {
            parent[rootY] = rootX;
        }
        else
        {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
        Count--;
        return true;
    }

    /// <summary>
    /// 判斷元素 x 與元素 y 是否屬於同一集合。
    /// </summary>
    public bool Connected(int x, int y) => Find(x) == Find(y);
}
```

---

### reference

- [leetcode](https://leetcode.cn/problems/most-stones-removed-with-same-row-or-column/solutions/560400/947-yi-chu-zui-duo-de-tong-xing-huo-tong-ezha/)
- [力扣官方题解](https://leetcode.cn/problems/most-stones-removed-with-same-row-or-column/solutions/560363/yi-chu-zui-duo-de-tong-xing-huo-tong-lie-m50r/)
- [灵茶山艾府](https://leetcode.cn/problems/most-stones-removed-with-same-row-or-column/solutions/3931977/zhong-jie-bing-cha-ji-pythonjavacgo-by-e-ulrj/)
