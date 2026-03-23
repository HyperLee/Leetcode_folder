# LeetCode 1594. 矩陣的最大非負積

> Maximum Non Negative Product in a Matrix

LeetCode 每日一題（2026-03-23）的 C# 解法，使用動態規劃求解矩陣中從左上角到右下角的最大非負乘積。

- [題目連結（英文）](https://leetcode.com/problems/maximum-non-negative-product-in-a-matrix/description/)
- [題目連結（中文）](https://leetcode.cn/problems/maximum-non-negative-product-in-a-matrix/description/)

## 題目描述

給定一個 `m x n` 的整數矩陣 `grid`，初始位置在左上角 `(0, 0)`，每一步只能**向右**或**向下**移動。

在所有從 `(0, 0)` 到 `(m-1, n-1)` 的路徑中，找出**乘積最大的非負路徑**。路徑的乘積為沿途所有格子中整數的乘積。

回傳最大非負乘積對 $10^9 + 7$ 取模的結果。若最大乘積為負數，則回傳 `-1`。

### 限制條件

- `m == grid.length`
- `n == grid[i].length`
- `1 <= m, n <= 15`
- `-4 <= grid[i][j] <= 4`

## 解題概念與出發點

### 為什麼不能只追蹤最大值？

矩陣元素有正有負，若只追蹤乘積的最大值，會遺漏一個重要的情境：

> **一個很小的負數乘積，遇到負數元素後，可能翻轉成為最大的正數乘積。**

例如，目前最大乘積 `6`、最小乘積 `-8`，遇到 `-2` 時：
- `6 × (-2) = -12`（變成負數）
- `(-8) × (-2) = 16`（反而成為更大的正數！）

因此，**必須同時追蹤乘積的最大值與最小值**。

### 動態規劃定義

定義兩個 DP 陣列：

| 陣列 | 意義 |
|---|---|
| `maxProduct[i][j]` | 從 `(0,0)` 到 `(i,j)` 所有路徑中，乘積的**最大值** |
| `minProduct[i][j]` | 從 `(0,0)` 到 `(i,j)` 所有路徑中，乘積的**最小值** |

## 解法詳細說明

### 狀態轉移方程式

由於只能向右或向下移動，位置 `(i, j)` 只能從 `(i-1, j)` 或 `(i, j-1)` 到達。

**當 `grid[i][j] >= 0`（正數或零）時：**

$$
\text{maxProduct}[i][j] = \max(\text{maxProduct}[i-1][j],\ \text{maxProduct}[i][j-1]) \times \text{grid}[i][j]
$$

$$
\text{minProduct}[i][j] = \min(\text{minProduct}[i-1][j],\ \text{minProduct}[i][j-1]) \times \text{grid}[i][j]
$$

**當 `grid[i][j] < 0`（負數）時**，乘以負數會翻轉大小關係：

$$
\text{maxProduct}[i][j] = \min(\text{minProduct}[i-1][j],\ \text{minProduct}[i][j-1]) \times \text{grid}[i][j]
$$

$$
\text{minProduct}[i][j] = \max(\text{maxProduct}[i-1][j],\ \text{maxProduct}[i][j-1]) \times \text{grid}[i][j]
$$

### 邊界條件

| 情況 | 處理方式 |
|---|---|
| `i = 0, j = 0` | `maxProduct[0][0] = minProduct[0][0] = grid[0][0]` |
| `i = 0`（第一列） | 只能從左方 `(0, j-1)` 轉移 |
| `j = 0`（第一行） | 只能從上方 `(i-1, 0)` 轉移 |

### 最終結果

$$
\text{answer} =
\begin{cases}
-1 & \text{if } \text{maxProduct}[m-1][n-1] < 0 \\
\text{maxProduct}[m-1][n-1] \mod (10^9 + 7) & \text{otherwise}
\end{cases}
$$

### 複雜度

- **時間複雜度**：$O(m \times n)$，遍歷矩陣一次
- **空間複雜度**：$O(m \times n)$，兩個 DP 陣列

## 範例演示

以 `grid = [[1, -2, 1], [1, -2, 1], [3, -4, 1]]` 為例，預期輸出 `8`。

### Step 1：初始化起點

```
maxProduct[0][0] = 1,  minProduct[0][0] = 1
```

### Step 2：初始化第一列（只能向右）

```
maxProduct[0][1] = 1 × (-2) = -2,  minProduct[0][1] = -2
maxProduct[0][2] = -2 × 1 = -2,    minProduct[0][2] = -2
```

### Step 3：初始化第一行（只能向下）

```
maxProduct[1][0] = 1 × 1 = 1,   minProduct[1][0] = 1
maxProduct[2][0] = 1 × 3 = 3,   minProduct[2][0] = 3
```

### Step 4：填充 DP 表格

**位置 (1,1)，`grid[1][1] = -2`（負數，翻轉）：**

```
maxProduct[1][1] = min(minProduct[0][1], minProduct[1][0]) × (-2) = min(-2, 1) × (-2) = (-2) × (-2) = 4
minProduct[1][1] = max(maxProduct[0][1], maxProduct[1][0]) × (-2) = max(-2, 1) × (-2) = 1 × (-2) = -2
```

**位置 (1,2)，`grid[1][2] = 1`（正數）：**

```
maxProduct[1][2] = max(maxProduct[0][2], maxProduct[1][1]) × 1 = max(-2, 4) × 1 = 4
minProduct[1][2] = min(minProduct[0][2], minProduct[1][1]) × 1 = min(-2, -2) × 1 = -2
```

**位置 (2,1)，`grid[2][1] = -4`（負數，翻轉）：**

```
maxProduct[2][1] = min(minProduct[1][1], minProduct[2][0]) × (-4) = min(-2, 3) × (-4) = (-2) × (-4) = 8
minProduct[2][1] = max(maxProduct[1][1], maxProduct[2][0]) × (-4) = max(4, 3) × (-4) = 4 × (-4) = -16
```

**位置 (2,2)，`grid[2][2] = 1`（正數）：**

```
maxProduct[2][2] = max(maxProduct[1][2], maxProduct[2][1]) × 1 = max(4, 8) × 1 = 8
minProduct[2][2] = min(minProduct[1][2], minProduct[2][1]) × 1 = min(-2, -16) × 1 = -16
```

### Step 5：取得結果

```
maxProduct[2][2] = 8 >= 0，回傳 8 % (10^9 + 7) = 8
```

### 最終 DP 表格

**maxProduct：**

| | j=0 | j=1 | j=2 |
|---|---|---|---|
| **i=0** | 1 | -2 | -2 |
| **i=1** | 1 | 4 | 4 |
| **i=2** | 3 | 8 | **8** |

**minProduct：**

| | j=0 | j=1 | j=2 |
|---|---|---|---|
| **i=0** | 1 | -2 | -2 |
| **i=1** | 1 | -2 | -2 |
| **i=2** | 3 | -16 | **-16** |

> [!TIP]
> 關鍵觀察：位置 `(2,1)` 的 `maxProduct = 8` 正是利用了 `(1,1)` 的最小值 `-2` 乘以負數 `-4` 翻轉而來，這就是為什麼需要同時追蹤最大值與最小值。

## 執行

```bash
dotnet run --project leetcode_1594
```
