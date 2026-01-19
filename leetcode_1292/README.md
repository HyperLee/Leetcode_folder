# LeetCode 1292 - 元素和小於等於閾值的正方形的最大邊長

> Maximum Side Length of a Square with Sum Less than or Equal to Threshold

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-1292-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/)

## 題目描述

給定一個 `m x n` 的矩陣 `mat` 與一個整數 `threshold`，回傳元素和小於或等於 `threshold` 的正方形之**最大邊長**；若不存在此類正方形，則回傳 `0`。

### 範例

**範例 1:**
```
輸入: mat = [[1,1,3,2,4,3,2],
            [1,1,3,2,4,3,2],
            [1,1,3,2,4,3,2]], threshold = 4
輸出: 2
解釋: 邊長為 2 的正方形 [[1,1],[1,1]]，元素和為 4，滿足 ≤ threshold
```

**範例 2:**
```
輸入: mat = [[2,2,2,2,2],
            [2,2,2,2,2],
            [2,2,2,2,2],
            [2,2,2,2,2],
            [2,2,2,2,2]], threshold = 1
輸出: 0
解釋: 最小元素為 2，沒有任何 1x1 正方形的和 ≤ 1
```

### 限制條件

- `m == mat.length`
- `n == mat[i].length`  
- `1 <= m, n <= 300`
- `0 <= mat[i][j] <= 10^4`
- `0 <= threshold <= 10^5`

## 解題思路

### 核心概念

本題結合了兩個重要技巧：
1. **二維前綴和** - 預處理後可在 O(1) 時間內計算任意子矩陣的元素和
2. **單調性優化** - 利用已找到的最大邊長，避免無意義的重複計算

### 為什麼需要前綴和？

如果每次都暴力計算正方形的元素和，時間複雜度會是 O(k²)（k 為邊長）。對於一個 300×300 的矩陣，這樣的計算量是不可接受的。

前綴和的核心思想是**預處理**：先花 O(m×n) 時間建立前綴和陣列，之後每次查詢只需 O(1) 時間。

### 前綴和陣列定義

設 `sum[i][j]` 表示從 `mat[0][0]` 到 `mat[i-1][j-1]` 的所有元素之和。

**建立公式（容斥原理）：**
```
sum[i+1][j+1] = sum[i+1][j] + sum[i][j+1] - sum[i][j] + mat[i][j]
```

**查詢公式：** 計算 `mat[r1..r2][c1..c2]` 的元素和：
```
query = sum[r2+1][c2+1] - sum[r2+1][c1] - sum[r1][c2+1] + sum[r1][c1]
```

視覺化說明：
```
+-------+-------+
|   A   |   B   |  
+-------+-------+
|   C   |   D   |  ← 我們要計算 D 區域
+-------+-------+

D = (A+B+C+D) - (A+B) - (A+C) + A
  = sum[r2+1][c2+1] - sum[r1][c2+1] - sum[r2+1][c1] + sum[r1][c1]
```

### 關鍵優化：從 ans+1 開始枚舉

暴力做法是對每個位置從邊長 1 開始枚舉，這樣時間複雜度為 O(m×n×min(m,n))。

**優化觀察：** 如果目前找到的最大邊長是 `ans`，那麼：
- 枚舉邊長 1, 2, ..., ans 是**毫無意義**的
- 因為即使找到了，也不會更新答案（需要比 ans 更大才有價值）

**優化策略：** 直接從 `ans+1` 開始枚舉！

這個簡單的改動將時間複雜度從 O(m×n×min(m,n)) 優化為 **O(m×n)**。

> [!TIP]
> 這個優化的關鍵在於：當 `ans` 增加時，整個搜索過程中 `ans` 最多只會增加 min(m,n) 次，因此總的內層迴圈執行次數是 O(m×n + min(m,n)) = O(m×n)。

## 演算法流程演示

以 `mat = [[1,1,3],[1,1,3],[1,1,3]]`，`threshold = 4` 為例：

### Step 1: 建立前綴和陣列

```
原始矩陣 mat:          前綴和 sum:
1  1  3               0  0  0  0
1  1  3      →        0  1  2  5
1  1  3               0  2  4  10
                      0  3  6  15
```

### Step 2: 枚舉過程

| 位置 (i,j) | 當前 res | 嘗試邊長 | 正方形範圍 | 元素和 | 結果 |
|-----------|---------|---------|-----------|-------|------|
| (0,0) | 0 | 1 | (0,0)→(0,0) | 1 | ✅ res=1 |
| (0,0) | 1 | 2 | (0,0)→(1,1) | 4 | ✅ res=2 |
| (0,0) | 2 | 3 | (0,0)→(2,2) | 15 | ❌ 停止 |
| (0,1) | 2 | 3 | (0,1)→(2,3) | 超界 | 跳過 |
| (0,2) | 2 | 3 | (0,2)→(2,4) | 超界 | 跳過 |
| (1,0) | 2 | 3 | (1,0)→(3,2) | 超界 | 跳過 |
| ... | 2 | - | - | - | 無更新 |

**最終答案：2**

## 複雜度分析

| 指標 | 複雜度 | 說明 |
|-----|-------|------|
| 時間複雜度 | O(m × n) | 建立前綴和 O(mn) + 優化後的枚舉 O(mn) |
| 空間複雜度 | O(m × n) | 前綴和陣列 |

## 執行專案

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1292
```

## 相關題目

- [LeetCode 304. Range Sum Query 2D - Immutable](https://leetcode.com/problems/range-sum-query-2d-immutable/) - 二維前綴和基礎題
- [LeetCode 221. Maximal Square](https://leetcode.com/problems/maximal-square/) - 最大正方形 (DP 解法)
- [LeetCode 1314. Matrix Block Sum](https://leetcode.com/problems/matrix-block-sum/) - 矩陣區塊和

## 參考資料

- [LeetCode 官方題解](https://leetcode.com/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/solutions/)
- [力扣官方題解](https://leetcode.cn/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/solutions/)
- [灵茶山艾府 - 圖解二維前綴和](https://leetcode.cn/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/solutions/3873775/wu-xu-er-fen-bao-li-mei-ju-jiu-shi-omnpy-r7yk/?envType=daily-question&envId=2026-01-19)
- [力扣官方题解](https://leetcode.cn/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/solutions/101737/yuan-su-he-xiao-yu-deng-yu-yu-zhi-de-zheng-fang-2/?envType=daily-question&envId=2026-01-19)
