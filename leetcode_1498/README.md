# LeetCode 1498 滿足條件的子序列數目

## 題目說明

**1498. 滿足條件的子序列數目**

給定一個整數陣列 `nums` 和一個整數 `target`，要求回傳 `nums` 中非空子序列的數量，使得該子序列的**最小值與最大值之和 ≤ target**。

**關鍵觀察**：

- 子序列不需要連續，但要保持相對順序
- 我們只關心最小值和最大值，不關心中間元素的具體值
- 答案需要對 `10^9 + 7` 取餘

## 解題出發點概念

### 1. 排序優化

由於只關心子序列的**最小值和最大值**，不關心元素的相對順序，可以對陣列排序。這樣能將問題轉化為在排序陣列中尋找合法區間。

### 2. 計算貢獻法

固定最小值 `v_min`，然後計算所有以 `v_min` 為最小值的子序列的貢獻：

- 如果 `v_min + v_max ≤ target`，則 `v_max ≤ target - v_min`
- 這意味著最小值必須滿足 `2 × v_min ≤ target`，即 `v_min ≤ target/2`

### 3. 組合數學

對於區間 `[i, j]` 中的元素，如果 `nums[i]` 是必選的最小值：

- 其他 `j-i` 個元素可選可不選
- 總共有 `2^(j-i)` 種選擇方式

## 詳細解題流程

### 步驟 1: 預處理次方值

```csharp
// 預處理 2^i % MOD，避免重複計算快速幂
int[] f = new int[MaxN];
f[0] = 1;
for (int i = 1; i < MaxN; i++)
{
    f[i] = (f[i - 1] * 2) % MOD;
}
```

**為什麼這樣做？**

- 避免每次都計算 `2^k`，時間複雜度從 O (log k) 降到 O (1)
- 使用遞推：`f[i] = f[i-1] × 2`，確保結果正確且高效

### 步驟 2: 排序陣列

```csharp
Array.Sort(nums);
```

排序後，對於任意區間 `[i, j]`：

- `nums[i]` 是最小值
- `nums[j]` 是最大值

### 步驟 3: 枚舉所有合法的最小值

```csharp
for (int i = 0; i < nums.Length && nums[i] * 2 <= target; i++)
{
    // 當前最小值：nums[i]
    // 最大值上限：target - nums[i]
}
```

**關鍵判斷**：`nums[i] * 2 <= target`

- 如果 `nums[i] > target/2`，則即使最大值也是 `nums[i]`，和仍會超過 `target`
- 這是一個重要的剪枝條件

### 步驟 4: 二分查找最大合法位置

```csharp
int maxValue = target - nums[i];
int pos = BinarySearch(nums, maxValue) - 1;
```

**二分查找的巧妙設計**：

- `BinarySearch` 回傳**第一個大於 `maxValue`** 的位置
- `pos = BinarySearch(...) - 1` 得到**最後一個 ≤ `maxValue`** 的位置
- 這樣設計避免了邊界判斷的複雜性

### 步驟 5: 計算貢獻

```csharp
int contribute = (pos >= i) ? f[pos - i] : 0;
ans = (ans + contribute) % MOD;
```

**貢獻計算邏輯**：

- 區間 `[i, pos]` 有 `pos - i + 1` 個元素
- `nums[i]` 必選 (作為最小值)
- 其他 `pos - i` 個元素可選可不選
- 總貢獻：`2^(pos - i)`

## 程式碼中的細節與陷阱

### 1. 二分查找的邊界處理

```csharp
if (mid == nums.Length)
{
    return mid; // 防止陣列越界
}
```

### 2. 模運算的正確性

```csharp
ans = (ans + contribute) % MOD; // 每次加法後都取模
f[i] = (f[i - 1] * 2) % MOD;   // 預處理時也要取模
```

### 3. 時間複雜度分析

- 排序：O (n log n)
- 預處理次方：O (n)
- 主迴圈：O (n log n)(n 次二分查找)
- 總體：O (n log n)

這個解法巧妙地將子序列計數問題轉化為排序 + 二分查找 + 組合數學的問題，是動態規劃和貪心思想的完美結合。
