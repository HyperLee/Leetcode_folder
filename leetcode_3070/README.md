# LeetCode 3070. Count Submatrices with Top-Left Element and Sum Less Than k

> 元素和小於等於 k 的子矩陣的數目

## 題目描述

給定一個以 0 為索引的整數矩陣 `grid` 以及一個整數 `k`。

回傳包含矩陣**左上角元素**，且元素總和**小於等於 k** 的子矩陣數目。

- LeetCode: [3070. Count Submatrices with Top-Left Element and Sum Less Than k](https://leetcode.com/problems/count-submatrices-with-top-left-element-and-sum-less-than-k/)
- 力扣: [3070. 元素和小於等於 k 的子矩陣的數目](https://leetcode.cn/problems/count-submatrices-with-top-left-element-and-sum-less-than-k/)

### 範例

**範例 1：**

```
輸入: grid = [[1,1,1],[1,1,1],[1,1,1]], k = 4
輸出: 6
```

**範例 2：**

```
輸入: grid = [[7,6,3],[6,6,1]], k = 18
輸出: 4
```

### 限制條件

- `m == grid.length`
- `n == grid[i].length`
- `1 <= n, m <= 1000`
- `0 <= grid[i][j] <= 1000`
- `1 <= k <= 10^9`

## 解題概念與出發點

所有符合條件的子矩陣都必須**以 `(0, 0)` 為左上角**。因此子矩陣完全由其**右下角 `(i, j)`** 決定——我們只需遍歷每一個 `(i, j)`，快速算出對應子矩陣的元素總和，並判斷是否 ≤ k。

關鍵觀察：若我們逐行、由左到右遍歷 `(i, j)`，可以用一個一維陣列 `cols[]` 來累計每一列的「縱向前綴和」，再在每一行中由左到右累加得到完整的子矩陣和，實現 **O(n × m) 時間、O(m) 空間** 的高效解法。

## 解法：二維前綴和（滾動陣列優化）

### 核心思路

1. 維護陣列 `cols[j]`：紀錄第 `j` 列從第 `0` 行到當前第 `i` 行的元素總和。
2. 遍歷每一行 `i` 時，用變數 `rows` 由左到右逐列累加 `cols[j]`。
3. 此時 `rows` 恰好等於子矩陣 `(0, 0) ~ (i, j)` 的元素和。
4. 若 `rows ≤ k`，答案加 1。

### 為什麼可行？

子矩陣 `(0, 0) ~ (i, j)` 的元素和可以展開為：

$$
\text{sum}(i, j) = \sum_{c=0}^{j} \sum_{r=0}^{i} \text{grid}[r][c]
= \sum_{c=0}^{j} \text{cols}[c]
$$

而 `rows` 正是 $\sum_{c=0}^{j} \text{cols}[c]$，因此等效於二維前綴和。

### 複雜度

| 項目 | 複雜度 |
|------|--------|
| 時間 | O(n × m) |
| 空間 | O(m) |

## 範例演示流程

以 `grid = [[1,1,1],[1,1,1],[1,1,1]]`, `k = 4` 為例：

### 初始狀態

```
cols = [0, 0, 0]
res  = 0
```

### 第 0 行 (i = 0)

| j | grid[0][j] | cols[j] | rows | ≤ 4? | res |
|---|------------|---------|------|------|-----|
| 0 | 1          | 1       | 1    | ✅   | 1   |
| 1 | 1          | 1       | 2    | ✅   | 2   |
| 2 | 1          | 1       | 3    | ✅   | 3   |

### 第 1 行 (i = 1)

| j | grid[1][j] | cols[j] | rows | ≤ 4? | res |
|---|------------|---------|------|------|-----|
| 0 | 1          | 2       | 2    | ✅   | 4   |
| 1 | 1          | 2       | 4    | ✅   | 5   |
| 2 | 1          | 2       | 6    | ❌   | 5   |

### 第 2 行 (i = 2)

| j | grid[2][j] | cols[j] | rows | ≤ 4? | res |
|---|------------|---------|------|------|-----|
| 0 | 1          | 3       | 3    | ✅   | 6   |
| 1 | 1          | 3       | 6    | ❌   | 6   |
| 2 | 1          | 3       | 9    | ❌   | 6   |

### 最終結果：`6`

> [!TIP]
> 一旦某一行中 `rows > k`，該行後續的 `j` 一定也會超過 k（因為 `grid[i][j] ≥ 0`），可以提前 `break` 做進一步優化。

## 執行專案

```bash
dotnet run --project leetcode_3070/leetcode_3070.csproj
```

### 前置需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
