# LeetCode 1582 — Special Positions in a Binary Matrix

> Given an `m × n` binary matrix `mat`, return the number of **special positions** in `mat`.
>
> A position `(i, j)` is called **special** if `mat[i][j] == 1` and all other elements in row `i` and column `j` are `0`.

---

## Problem Description

### English

- LeetCode: [1582. Special Positions in a Binary Matrix](https://leetcode.com/problems/special-positions-in-a-binary-matrix/description/)
- Difficulty: **Easy**

Given an `m x n` binary matrix `mat`, return the number of special positions in `mat`.

A position `(i, j)` is called **special** if:
- `mat[i][j] == 1`
- All other elements in row `i` are `0`
- All other elements in column `j` are `0`

**Constraints:**
- `m == mat.length`
- `n == mat[i].length`
- `1 <= m, n <= 100`
- `mat[i][j]` is `0` or `1`

### 中文

- LeetCode CN: [1582. 二進位矩陣中的特殊位置](https://leetcode.cn/problems/special-positions-in-a-binary-matrix/description/)

給定一個 `m x n` 的二進位矩陣 `mat`，回傳 `mat` 中**特殊位置**的數量。

位置 `(i, j)` 被稱為**特殊位置**，當且僅當：
- `mat[i][j] == 1`
- 第 `i` 列（row）的其餘所有元素皆為 `0`
- 第 `j` 行（column）的其餘所有元素皆為 `0`

---

## Solution Approach — Simulation (模擬)

### 核心概念

由於矩陣中每個元素只能為 `0` 或 `1`，  
「特殊位置」的三個條件可以等價轉換為：

| 條件                      | 等價判斷                         |
|---------------------------|----------------------------------|
| `mat[i][j] == 1`          | 格子本身為 `1`                   |
| 第 `i` 列其餘元素皆為 `0` | 第 `i` 列的總和 `rowssum[i] == 1` |
| 第 `j` 行其餘元素皆為 `0` | 第 `j` 行的總和 `colsum[j] == 1`  |

因此，**預先計算每列與每行的總和**，即可在 O(1) 時間內判斷任意位置是否為特殊位置。

### 演算法步驟

**Step 1 — 預處理（Pre-computation）**

遍歷整個矩陣，分別累加：
- `rowssum[i]`：第 `i` 列的所有元素總和
- `colsum[j]`：第 `j` 行的所有元素總和

```
Time: O(m × n)  |  Space: O(m + n)
```

**Step 2 — 計數（Counting）**

再次遍歷矩陣，對每個位置 `(i, j)` 同時檢查三個條件：

```
if mat[i][j] == 1 AND rowssum[i] == 1 AND colsum[j] == 1
    → 找到一個特殊位置，結果 +1
```

```
Time: O(m × n)
```

**整體複雜度**

| 複雜度     | 值          |
|-----------|-------------|
| 時間複雜度 | O(m × n)    |
| 空間複雜度 | O(m + n)    |

---

## Walkthrough Example（範例演示）

### 範例 1

**Input:**
```
mat = [[1, 0, 0],
       [0, 0, 1],
       [1, 0, 0]]
```

**Step 1 — 計算列與行的總和：**

```
        行 0  行 1  行 2   rowssum
列 0  [  1    0    0  ]  →  1
列 1  [  0    0    1  ]  →  1
列 2  [  1    0    0  ]  →  1

colsum  →   2    0    1
```

**Step 2 — 逐格判斷：**

| (i, j) | mat[i][j] | rowssum[i] | colsum[j] | 特殊位置？ |
|--------|-----------|-----------|-----------|-----------|
| (0, 0) |     1     |     1     |     2     | ❌ colsum ≠ 1 |
| (0, 1) |     0     |     —     |     —     | ❌ mat ≠ 1 |
| (0, 2) |     0     |     —     |     —     | ❌ mat ≠ 1 |
| (1, 0) |     0     |     —     |     —     | ❌ mat ≠ 1 |
| (1, 1) |     0     |     —     |     —     | ❌ mat ≠ 1 |
| (1, 2) |     1     |     1     |     1     | ✅ 特殊位置！ |
| (2, 0) |     1     |     1     |     2     | ❌ colsum ≠ 1 |
| (2, 1) |     0     |     —     |     —     | ❌ mat ≠ 1 |
| (2, 2) |     0     |     —     |     —     | ❌ mat ≠ 1 |

**Output: `1`**

---

### 範例 2

**Input:**
```
mat = [[1, 0, 0],
       [0, 1, 0],
       [0, 0, 1]]
```

**Step 1 — 計算列與行的總和：**

```
        行 0  行 1  行 2   rowssum
列 0  [  1    0    0  ]  →  1
列 1  [  0    1    0  ]  →  1
列 2  [  0    0    1  ]  →  1

colsum  →   1    1    1
```

**Step 2 — 逐格判斷：**

| (i, j) | mat[i][j] | rowssum[i] | colsum[j] | 特殊位置？ |
|--------|-----------|-----------|-----------|-----------|
| (0, 0) |     1     |     1     |     1     | ✅ 特殊位置！ |
| (1, 1) |     1     |     1     |     1     | ✅ 特殊位置！ |
| (2, 2) |     1     |     1     |     1     | ✅ 特殊位置！ |

（其餘格子的 `mat[i][j] == 0`，直接跳過）

**Output: `3`**

---

## Implementation

本題 C# 實作請參閱 [leetcode_1582/Program.cs](leetcode_1582/Program.cs)。

```csharp
public int NumSpecial(int[][] mat)
{
    int m = mat.Length;
    int n = mat[0].Length;
    int[] rowssum = new int[m];
    int[] colsum  = new int[n];

    // Step 1: 預先計算每列與每行的總和
    for (int i = 0; i < m; i++)
        for (int j = 0; j < n; j++)
        {
            rowssum[i] += mat[i][j];
            colsum[j]  += mat[i][j];
        }

    int res = 0;
    // Step 2: 判斷每個格子是否為特殊位置
    for (int i = 0; i < m; i++)
        for (int j = 0; j < n; j++)
            if (mat[i][j] == 1 && rowssum[i] == 1 && colsum[j] == 1)
                res++;

    return res;
}
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Build & Run

```bash
dotnet build
dotnet run --project leetcode_1582
```

## References

- [C# Jagged Arrays — Microsoft Docs](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays)
- [LeetCode 1582 — Official Solution](https://leetcode.com/problems/special-positions-in-a-binary-matrix/editorial/)
