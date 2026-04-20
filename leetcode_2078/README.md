# LeetCode 2078 — Two Furthest Houses With Different Colors

> **Daily Question** · 2026-04-20

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-brightgreen)](https://leetcode.com/problems/two-furthest-houses-with-different-colors/)

---

## 題目說明

- 題目連結（英文）：[LeetCode 2078](https://leetcode.com/problems/two-furthest-houses-with-different-colors/description/?envType=daily-question&envId=2026-04-20)
- 題目連結（中文）：[力扣 2078](https://leetcode.cn/problems/two-furthest-houses-with-different-colors/description/?envType=daily-question&envId=2026-04-20)

街道上有 `n` 棟房屋排成一排，每棟房屋都塗上了一種顏色。  
給定一個長度為 `n` 的 **0 索引**整數陣列 `colors`，其中 `colors[i]` 表示第 `i` 棟房屋的顏色。

請回傳**兩棟顏色不同的房屋之間的最大距離**。

第 `i` 棟和第 `j` 棟房屋之間的距離定義為 `|i - j|`。

### 限制條件

| 條件 | 範圍 |
|------|------|
| `n == colors.length` | — |
| `2 <= n <= 100` | 房屋數量上限 100 |
| `0 <= colors[i] <= 100` | 顏色值範圍 |
| 至少存在兩棟顏色不同的房屋 | 保證有效答案 |

---

## 解題概念與出發點

### 問題分析

要找「顏色不同且距離最遠」的兩棟房屋，關鍵在於同時滿足兩個條件：

1. `colors[i] != colors[j]`（顏色不同）
2. `|i - j|` 盡可能大（距離最遠）

### 出發點：暴力枚舉的可行性

由於 `n ≤ 100`，所有房屋對的總數最多為：

$$C(100, 2) = \frac{100 \times 99}{2} = 4950 \text{ 對}$$

這個規模完全在可接受範圍內，因此採用「枚舉所有房屋對」的暴力解法即可高效通過。

---

## 解法：方法一 — 枚舉（Brute Force）

### 演算法步驟

1. **外層迴圈**：枚舉左側房屋 `i`（索引從 `0` 到 `n-1`）。
2. **內層迴圈**：枚舉右側房屋 `j`（索引從 `i+1` 到 `n-1`），確保 `j > i`，距離恆為正值。
3. **條件判斷**：若 `colors[i] != colors[j]`，以 `j - i` 嘗試更新最大距離。
4. **回傳結果**：所有枚舉結束後，返回最大距離。

### 複雜度分析

| 複雜度 | 數值 | 說明 |
|--------|------|------|
| 時間複雜度 | $O(n^2)$ | 枚舉所有房屋對 |
| 空間複雜度 | $O(1)$ | 只使用常數額外空間 |

### 程式碼

```csharp
public int MaxDistance(int[] colors)
{
    int n = colors.Length;
    int maxDistance = 0;

    // 枚舉所有 (i, j) 對，i < j，確保只計算正向距離
    for (int i = 0; i < n; i++)
    {
        for (int j = i + 1; j < n; j++)
        {
            // 僅當兩棟房子顏色不同時，才更新最大距離
            if (colors[i] != colors[j])
            {
                maxDistance = Math.Max(maxDistance, j - i);
            }
        }
    }

    return maxDistance;
}
```

---

## 範例演示

### 範例 1

**輸入**：`colors = [1, 1, 1, 6, 1, 1, 1]`

逐步枚舉過程（僅列出顏色不同的配對）：

| i | j | colors[i] | colors[j] | 顏色不同? | 距離 (j - i) | 當前最大距離 |
|:-:|:-:|:---------:|:---------:|:--------:|:------------:|:------------:|
| 0 | 3 | 1         | 6         | ✓        | **3**        | 3            |
| 1 | 3 | 1         | 6         | ✓        | 2            | 3            |
| 2 | 3 | 1         | 6         | ✓        | 1            | 3            |
| 3 | 4 | 6         | 1         | ✓        | 1            | 3            |
| 3 | 5 | 6         | 1         | ✓        | 2            | 3            |
| 3 | 6 | 6         | 1         | ✓        | **3**        | 3            |

**輸出**：`3`

```
索引:  0   1   2   3   4   5   6
顏色: [1]  [1] [1] [6] [1] [1] [1]
       ↑_______________↑              距離 = 3
               ↑___________↑         距離 = 3
```

---

### 範例 2

**輸入**：`colors = [1, 8, 3, 8, 3]`

| i | j | colors[i] | colors[j] | 顏色不同? | 距離 (j - i) | 當前最大距離 |
|:-:|:-:|:---------:|:---------:|:--------:|:------------:|:------------:|
| 0 | 1 | 1         | 8         | ✓        | 1            | 1            |
| 0 | 2 | 1         | 3         | ✓        | 2            | 2            |
| 0 | 3 | 1         | 8         | ✓        | 3            | 3            |
| 0 | 4 | 1         | 3         | ✓        | **4**        | **4**        |
| 1 | 2 | 8         | 3         | ✓        | 1            | 4            |
| … | … | …         | …         | …        | …            | 4            |

**輸出**：`4`

```
索引:  0   1   2   3   4
顏色: [1]  [8] [3] [8] [3]
       ↑___________________↑    距離 = 4  ← 最大
```

---

### 範例 3（邊界情況）

**輸入**：`colors = [0, 1]`

只有兩棟房屋，顏色不同，距離為 `1`。

**輸出**：`1`

---

## 執行測試

```bash
dotnet run --project leetcode_2078/leetcode_2078.csproj
```

預期輸出：

```
Test 1: 3
Test 2: 4
Test 3: 1
```

---

## 建構專案

```bash
dotnet build leetcode_2078/leetcode_2078.csproj --configuration Debug
```
