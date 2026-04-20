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

## 解法：方法二 — 貪心（Greedy）

### 解題概念與出發點

**關鍵觀察**：最大距離的配對，必然包含最左邊（index `0`）或最右邊（index `n-1`）的房子。

**證明思路**：設最優配對為 `(i, j)`（`i < j`），且兩者均不是端點。設端點共同顏色為 `c`：
- 若 `colors[j] ≠ c`：則 `colors[0] = c ≠ colors[j]`，配對 `(0, j)` 合法且距離 `j ≥ j - i`。
- 若 `colors[i] ≠ c`：則 `colors[n-1] = c ≠ colors[i]`，配對 `(i, n-1)` 合法且距離 `n-1-i ≥ j - i`。

無論哪種情況，都能找到含端點且距離不更小的合法配對，因此最優解必然包含 `0` 或 `n-1`。

**分兩種情況討論**：

**情況一**：若 `colors[0] ≠ colors[n-1]`，兩端顏色不同，最大距離直接為 `n-1`，無需進一步搜尋。

**情況二**：若 `colors[0] = colors[n-1] = c`（設此公共顏色為 `boundaryColor`），則：
- 對 **房子 0**（最左）：從右往左掃描，找到最遠（最右）的顏色不等於 `c` 的房子，索引為 `rightIdx`，距離 = `rightIdx`。
- 對 **房子 n-1**（最右）：從左往右掃描，找到最遠（最左）的顏色不等於 `c` 的房子，索引為 `leftIdx`，距離 = `n-1-leftIdx`。

答案為：

$$\max(\text{rightIdx},\; n-1-\text{leftIdx})$$

---

### 演算法步驟

1. 若 `colors[0] ≠ colors[n-1]`，直接回傳 `n-1`（情況一）。
2. 設 `boundaryColor = colors[0] = colors[n-1]`（情況二）。
3. 從索引 `n-2` 往左掃描，找到最大的 `rightIdx` 使得 `colors[rightIdx] ≠ boundaryColor`。
4. 從索引 `1` 往右掃描，找到最小的 `leftIdx` 使得 `colors[leftIdx] ≠ boundaryColor`。
5. 回傳 `max(rightIdx, n-1-leftIdx)`。

### 複雜度分析

| 複雜度 | 數值 | 說明 |
|--------|------|------|
| 時間複雜度 | $O(n)$ | 兩次線性掃描，各最多掃完整個陣列一次 |
| 空間複雜度 | $O(1)$ | 只使用常數額外空間 |

### 程式碼

```csharp
public int MaxDistance2(int[] colors)
{
    int n = colors.Length;

    // 記錄首尾共同顏色（情況一：首尾不同則直接回傳 n-1）
    int boundaryColor = colors[0];
    if (boundaryColor != colors[n - 1])
    {
        return n - 1;
    }

    // 情況二：首尾顏色均為 boundaryColor
    // 對於房子 0：從右往左掃描，找到最遠（最右）的顏色不同房子，索引為 rightIdx
    int rightIdx = n - 2;
    while (colors[rightIdx] == boundaryColor)
    {
        rightIdx--;
    }

    // 對於房子 n-1：從左往右掃描，找到最遠（最左）的顏色不同房子，索引為 leftIdx
    int leftIdx = 1;
    while (colors[leftIdx] == boundaryColor)
    {
        leftIdx++;
    }

    // 房子 0 到 rightIdx 的距離為 rightIdx；房子 n-1 到 leftIdx 的距離為 n-1-leftIdx
    return Math.Max(n - 1 - leftIdx, rightIdx);
}
```

---

## 範例演示（方法二）

### 範例 1

**輸入**：`colors = [1, 1, 1, 6, 1, 1, 1]`

`colors[0] = 1 = colors[6]`，進入情況二，`boundaryColor = 1`。

從右往左掃描 `rightIdx`：

| rightIdx | colors[rightIdx] | == boundaryColor? |
|:--------:|:----------------:|:-----------------:|
| 5        | 1                | ✓ 繼續            |
| 4        | 1                | ✓ 繼續            |
| 3        | 6                | ✗ 停止            |

`rightIdx = 3`，房子 `0` 到 `3` 的距離 = **3**

從左往右掃描 `leftIdx`：

| leftIdx | colors[leftIdx] | == boundaryColor? |
|:-------:|:---------------:|:-----------------:|
| 1       | 1               | ✓ 繼續            |
| 2       | 1               | ✓ 繼續            |
| 3       | 6               | ✗ 停止            |

`leftIdx = 3`，房子 `6` 到 `3` 的距離 = `6 - 3` = **3**

$$\max(3,\; 6 - 3) = \max(3,\; 3) = 3$$

**輸出**：`3`

```
索引:  0   1   2   3   4   5   6
顏色: [1]  [1] [1] [6] [1] [1] [1]
       ↑___________↑              掃描 rightIdx 停在 3，距離 = 3
               ↑___________↑     掃描 leftIdx 停在 3，距離 = 3
```

---

### 範例 2

**輸入**：`colors = [1, 8, 3, 8, 3]`

`colors[0] = 1 ≠ colors[4] = 3`，進入情況一，直接回傳 `n-1 = 4`。

**輸出**：`4`

---

### 範例 3（邊界情況）

**輸入**：`colors = [0, 1]`

`colors[0] = 0 ≠ colors[1] = 1`，進入情況一，直接回傳 `n-1 = 1`。

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
