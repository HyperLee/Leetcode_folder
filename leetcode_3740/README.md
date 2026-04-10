# LeetCode 3740 — Minimum Distance Between Three Equal Elements I

[![Build](https://img.shields.io/badge/build-passing-brightgreen?style=flat-square)](leetcode_3740/leetcode_3740.csproj)
[![Language](https://img.shields.io/badge/language-C%23%2014-512BD4?style=flat-square)](https://learn.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)
[![Difficulty](https://img.shields.io/badge/difficulty-Easy-5cb85c?style=flat-square)](https://leetcode.com/problems/minimum-distance-between-three-equal-elements-i/)

三個相等元素之間的最小距離（暴力法 / Brute Force）

[LeetCode EN](https://leetcode.com/problems/minimum-distance-between-three-equal-elements-i/description/?envType=daily-question&envId=2026-04-10) •
[LeetCode CN](https://leetcode.cn/problems/minimum-distance-between-three-equal-elements-i/description/?envType=daily-question&envId=2026-04-10)

---

## 題目說明

給定一個整數陣列 `nums`。

若三個**彼此不同**的索引 `(i, j, k)` 滿足 `nums[i] == nums[j] == nums[k]`，則此三元組稱為 **good tuple**。

**good tuple 的距離**定義為：

$$\text{distance}(i, j, k) = |i - j| + |j - k| + |k - i|$$

回傳所有 good tuple 中**可能的最小距離**；如果不存在任何 good tuple，則回傳 `-1`。

### 限制條件

- `3 <= nums.length <= 100`
- `1 <= nums[i] <= 100`

---

## 核心觀察：距離公式化簡

距離公式 $|i-j| + |j-k| + |k-i|$ 看起來需要三項計算，但實際上有一個精妙的規律：

> **不管三個索引的排列順序如何，距離恆等於「最左側索引到最右側索引距離的兩倍」。**

設三個索引排序後為 $i < j < k$，則：

$$|i-j| + |j-k| + |k-i| = (j-i) + (k-j) + (k-i) = 2(k-i)$$

這意味著：
- 中間索引 $j$ 的位置**不影響**距離大小。
- 要最小化距離，只需最小化**最左側與最右側索引之差**。

---

## 解法：暴力法（三重迴圈）

### 思路

1. 嚴格以 `i < j < k` 的順序枚舉所有三元組，確保索引彼此不同。
2. 若 `nums[i] == nums[j]`，繼續尋找最小的 `k > j` 使 `nums[k] == nums[j]`。
3. 找到後計算距離 `2 * (k - i)`，更新全域最小值後 `break`（對固定 `(i, j)`，`k` 越小距離越小）。
4. 若全程未找到任何 good tuple，回傳 `-1`。

### 時間與空間複雜度

| 複雜度 | 數值 |
|--------|------|
| 時間   | $O(n^3)$ |
| 空間   | $O(1)$ |

### 程式碼

```csharp
public int MinimumDistance(int[] nums)
{
    int n = nums.Length;
    int res = int.MaxValue;

    for (int i = 0; i < n - 2; i++)
    {
        for (int j = i + 1; j < n - 1; j++)
        {
            if (nums[i] != nums[j])
                continue;

            for (int k = j + 1; k < n; k++)
            {
                if (nums[j] == nums[k])
                {
                    res = Math.Min(res, 2 * (k - i));
                    break;
                }
            }
        }
    }

    return res == int.MaxValue ? -1 : res;
}
```

---

## 演示流程

以 `nums = [1, 2, 1, 3, 1]` 為例（陣列長度 n = 5）：

```
索引:  0   1   2   3   4
值  :  1   2   1   3   1
```

### 枚舉過程

| i | j | k | nums[i]=nums[j]? | nums[j]=nums[k]? | 距離 2*(k-i) |
|---|---|---|------------------|------------------|--------------|
| 0 | 1 | — | 1 ≠ 2，跳過      | —                | —            |
| 0 | 2 | 3 | 1 = 1 ✓          | 1 ≠ 3，繼續      | —            |
| 0 | 2 | 4 | 1 = 1 ✓          | 1 = 1 ✓          | 2*(4-0) = **8** |
| 1 | 2 | — | 2 ≠ 1，跳過      | —                | —            |
| 1 | 3 | — | 2 ≠ 3，跳過      | —                | —            |
| 2 | 3 | — | 1 ≠ 3，跳過      | —                | —            |
| 2 | 4 | — | j < n-1 = 4，j 最大為 3，不執行 | — | — |

> [!NOTE]
> 陣列中值為 `1` 的索引只有 `0, 2, 4`，因此唯一的 good tuple 為 `(0, 2, 4)`，最小距離為 **8**。

### 最終結果

$$\text{distance}(0, 2, 4) = |0-2| + |2-4| + |4-0| = 2 + 2 + 4 = 8 = 2 \times (4 - 0)$$

---

## 解法二：維護最近三次出現位置（滑動視窗）

### 思路

從「距離只與最左和最右位置有關」的核心觀察出發，可以更聰明地只追蹤每個數值**最近三次出現的索引**，以 $O(n)$ 一次掃描得出答案：

1. 為每個數值維護一個長度為 3 的索引快取（`cache[v, 0..2]`）及已記錄的次數（`count[v]`）。
2. 遇到數值 `v`：
   - 若 `count[v] < 3`：直接將當前索引 `i` 存入 `cache[v, count[v]]`，並將 `count[v]++`。
   - 若 `count[v] == 3`（快取已滿）：**視窗右移**——丟棄 slot 0 最舊索引，slot 內資料依序往前遞補（`cache[v,0] ← cache[v,1]`、`cache[v,1] ← cache[v,2]`），再將當前索引 `i` 寫入 slot 2，令視窗始終保留最近三個位置。
3. 每當 `count[v] == 3`，立即以 $2 \times (\text{cache}[v,2] - \text{cache}[v,0])$ 更新全域最小值。
4. 掃描完成後，若從未更新過最小值，回傳 `-1`；否則回傳最小值。

> [!TIP]
> 題目保證 `1 <= nums[i] <= n`，故直接用大小 `n+1` 的陣列代替 `Dictionary`，省去雜湊開銷。

### 時間與空間複雜度

| 複雜度 | 數值 |
|--------|------|
| 時間   | $O(n)$ |
| 空間   | $O(n)$ |

### 程式碼

```csharp
public int MinimumDistance2(int[] nums)
{
    int n = nums.Length;

    // cache[v, slot]：數值 v 最近三次出現的索引（slot = 0/1/2）
    int[,] cache = new int[n + 1, 3];

    // count[v]：數值 v 目前已記錄的索引數量，最大追蹤至 3
    int[] count = new int[n + 1];

    int res = int.MaxValue;

    for (int i = 0; i < n; i++)
    {
        int v = nums[i];

        if (count[v] < 3)
        {
            cache[v, count[v]] = i;
            count[v]++;
        }
        else
        {
            // 視窗右移：slot 資料依序往前遞補，slot 2 存入最新索引
            // 丟棄最舊（最左）位置，因為越靠近的三點，距離 2*(rightmost-leftmost) 必定越小
            cache[v, 0] = cache[v, 1];
            cache[v, 1] = cache[v, 2];
            cache[v, 2] = i;
        }

        if (count[v] == 3)
        {
            res = Math.Min(res, 2 * (cache[v, 2] - cache[v, 0]));
        }
    }

    return res == int.MaxValue ? -1 : res;
}
```

---

## 解法二演示流程

以 `nums = [1, 2, 1, 3, 1]` 為例（陣列長度 n = 5）：

```
索引:  0   1   2   3   4
值  :  1   2   1   3   1
```

初始狀態：`count` 全為 0，`cache` 全為 0，`res = int.MaxValue`

### 逐步執行

| i | v | count[v] 操作           | cache[v, 0..2] 更新後 | count[v] | count[v]==3? | res 更新                     |
|---|---|-------------------------|-----------------------|----------|--------------|------------------------------|
| 0 | 1 | 0 < 3，存入 slot 0，++  | `[0, -, -]`           | 1        | 否           | 不更新                       |
| 1 | 2 | 0 < 3，存入 slot 0，++  | `[1, -, -]`           | 1        | 否           | 不更新                       |
| 2 | 1 | 1 < 3，存入 slot 1，++  | `[0, 2, -]`           | 2        | 否           | 不更新                       |
| 3 | 3 | 0 < 3，存入 slot 0，++  | `[3, -, -]`           | 1        | 否           | 不更新                       |
| 4 | 1 | 2 < 3，存入 slot 2，++  | `[0, 2, 4]`           | 3        | **是**       | $2\times(4-0)=\mathbf{8}$    |

> [!NOTE]
> 值為 `2` 與 `3` 的 `count` 始終未達到 3，不會觸發距離計算。  
> 值為 `1` 在索引 `0`、`2`、`4` 各出現一次，恰好在 `i=4` 時累積到 3 個，立即計算距離。

### 最終結果

$$\text{distance} = 2 \times (\text{cache}[1,2] - \text{cache}[1,0]) = 2 \times (4 - 0) = 8$$

---

## 兩種解法比較

| 解法 | 時間複雜度 | 空間複雜度 | 適用場景 |
|------|-----------|-----------|----------|
| 解法一：暴力法（三重迴圈） | $O(n^3)$ | $O(1)$ | 資料規模小（$n \le 100$） |
| 解法二：維護最近三次位置 | $O(n)$ | $O(n)$ | 資料規模大，效能要求高 |

---

## 更多測試案例

| 輸入 `nums`       | 說明                            | 預期輸出 |
|-------------------|---------------------------------|----------|
| `[1, 1, 1, 2]`    | 三個 1 在索引 0,1,2             | `4`      |
| `[1, 2, 1, 3, 1]` | 三個 1 在索引 0,2,4             | `8`      |
| `[1, 2, 3]`       | 無 good tuple                   | `-1`     |
| `[1, 1, 1, 1]`    | 最短連續三個 1 在索引 0,1,2     | `4`      |
| `[4, 3, 4, 1, 4]` | 三個 4 在索引 0,2,4             | `8`      |

---

## 執行方式

```bash
dotnet run --project leetcode_3740/leetcode_3740.csproj
```

---

## 相關題目

- [3741. Minimum Distance Between Three Equal Elements II](https://leetcode.com/problems/minimum-distance-between-three-equal-elements-ii/) — 資料規模更大版本，需要更高效的解法（$O(n)$）
