# 2906. Construct Product Matrix

> LeetCode 每日一題 (2026-03-24)
>
> - [English](https://leetcode.com/problems/construct-product-matrix/description/?envType=daily-question&envId=2026-03-24)
> - [中文](https://leetcode.cn/problems/construct-product-matrix/description/?envType=daily-question&envId=2026-03-24)

## 題目描述

給定一個大小為 `n × m` 的 **0 索引** 二維整數矩陣 `grid`，定義一個同樣大小的矩陣 `p` 為 `grid` 的 **乘積矩陣**，需滿足：

> 每個元素 `p[i][j]` 的值等於 `grid` 中 **除了** `grid[i][j]` 以外所有元素的乘積，對 **12345** 取模。

回傳 `grid` 的乘積矩陣。

### 限制條件

- `1 <= n == grid.length <= 10⁵`
- `1 <= m == grid[i].length <= 10⁵`
- `2 <= n * m <= 10⁵`
- `1 <= grid[i][j] <= 10⁹`

### 範例

**範例 1：**

```
輸入：grid = [[1,2],[3,4]]
輸出：[[24,12],[8,6]]
```

**範例 2：**

```
輸入：grid = [[12345],[2],[1]]
輸出：[[2],[0],[0]]
```

## 解題概念與出發點

本題要求「除了自身以外所有元素的乘積」，這與 [238. Product of Array Except Self](https://leetcode.com/problems/product-of-array-except-self/) 的概念幾乎相同，差異僅在於從一維陣列推廣到二維矩陣，並額外需要取模 12345。

### 為什麼不能直接算？

最直覺的做法是先算出所有元素的總乘積，再逐個除以 `grid[i][j]`。但本題有兩個障礙：

1. **取模運算下除法不直接適用**——模運算與除法不像加減乘那樣直接相容。
2. **元素可能為 12345 的倍數**——此時模運算後為 0，無法用除法還原。

因此我們採用 **前綴積 × 後綴積** 的經典技巧，完全避免除法。

### 核心想法

將二維矩陣想像成一條展開的一維陣列（按列優先順序），對每個元素而言：

$$
p[i][j] = \underbrace{\text{前綴積（自身之前所有元素的乘積）}}_{\text{prefix}} \times \underbrace{\text{後綴積（自身之後所有元素的乘積）}}_{\text{suffix}} \mod 12345
$$

## 解法：前綴積 × 後綴積

### 演算法步驟

1. **倒序遍歷**：從矩陣最末元素 `grid[n-1][m-1]` 往回掃到 `grid[0][0]`，累積後綴積並存入結果矩陣 `p`。
2. **順序遍歷**：從矩陣首元素 `grid[0][0]` 往後掃到 `grid[n-1][m-1]`，累積前綴積，並將 `p[i][j]` 乘上前綴積，得到最終答案。

> [!TIP]
> 空間最佳化：後綴積直接存入結果矩陣 `p`，前綴積僅需一個變數，不需額外開陣列。

### 複雜度分析

| 項目 | 複雜度 |
|------|--------|
| 時間 | $O(n \times m)$，兩次遍歷 |
| 空間 | $O(1)$（不計輸出矩陣） |

### 程式碼

```csharp
public int[][] ConstructProductMatrix(int[][] grid)
{
    const int MOD = 12345;
    int n = grid.Length;
    int m = grid[0].Length;

    int[][] p = new int[n][];
    for (int i = 0; i < n; i++)
        p[i] = new int[m];

    // 第一輪：倒序遍歷，計算後綴積
    long suffixProduct = 1;
    for (int i = n - 1; i >= 0; i--)
    {
        for (int j = m - 1; j >= 0; j--)
        {
            p[i][j] = (int)suffixProduct;
            suffixProduct = (suffixProduct * grid[i][j]) % MOD;
        }
    }

    // 第二輪：順序遍歷，計算前綴積並與後綴積相乘
    long prefixProduct = 1;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            p[i][j] = (int)((long)p[i][j] * prefixProduct % MOD);
            prefixProduct = (prefixProduct * grid[i][j]) % MOD;
        }
    }

    return p;
}
```

## 舉例演示流程

以 `grid = [[1, 2], [3, 4]]` 為例（MOD = 12345），展開後為 `[1, 2, 3, 4]`。

### 第一輪：倒序計算後綴積

從最後一個元素往前掃，`suffixProduct` 初始為 1：

| 遍歷順序 | 位置 | `grid[i][j]` | 存入 `p[i][j]` = suffixProduct | 更新 suffixProduct |
|:---:|:---:|:---:|:---:|:---:|
| 1 | `[1][1]` | 4 | **1** | 1 × 4 = 4 |
| 2 | `[1][0]` | 3 | **4** | 4 × 3 = 12 |
| 3 | `[0][1]` | 2 | **12** | 12 × 2 = 24 |
| 4 | `[0][0]` | 1 | **24** | 24 × 1 = 24 |

此時 `p = [[24, 12], [4, 1]]`（每格存的是「自身之後所有元素的乘積」）。

### 第二輪：順序計算前綴積，並乘入結果

從第一個元素往後掃，`prefixProduct` 初始為 1：

| 遍歷順序 | 位置 | `grid[i][j]` | 原 `p[i][j]`（後綴積） | 最終 `p[i][j]` = 後綴積 × prefixProduct | 更新 prefixProduct |
|:---:|:---:|:---:|:---:|:---:|:---:|
| 1 | `[0][0]` | 1 | 24 | 24 × 1 = **24** | 1 × 1 = 1 |
| 2 | `[0][1]` | 2 | 12 | 12 × 1 = **12** | 1 × 2 = 2 |
| 3 | `[1][0]` | 3 | 4 | 4 × 2 = **8** | 2 × 3 = 6 |
| 4 | `[1][1]` | 4 | 1 | 1 × 6 = **6** | 6 × 4 = 24 |

### 最終結果

$$
p = \begin{bmatrix} 24 & 12 \\ 8 & 6 \end{bmatrix}
$$

驗證：
- `p[0][0]` = 2 × 3 × 4 = **24** ✓
- `p[0][1]` = 1 × 3 × 4 = **12** ✓
- `p[1][0]` = 1 × 2 × 4 = **8** ✓
- `p[1][1]` = 1 × 2 × 3 = **6** ✓
