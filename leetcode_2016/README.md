# Leetcode 2016 - 增量元素之間的最大差值

## 題目描述

給定一個 0 索引的整數陣列 `nums`，大小為 n，請找出 `nums[i]` 和 `nums[j]`(即 `nums[j] - nums[i]`) 的最大差值，
其中 0 <= i < j < n 且 nums [i] < nums [j]。

回傳最大差值。如果不存在這樣的 i 和 j，則回傳 -1。

[Leetcode 原題連結 (英文)](https://leetcode.com/problems/maximum-difference-between-increasing-elements/description/?envType=daily-question\&envId=2025-06-16)
[Leetcode 中文題目連結](https://leetcode.cn/problems/maximum-difference-between-increasing-elements/description/?envType=daily-question\&envId=2025-06-16)

---

## 解題思路

### 方法一：暴力雙層 for 迴圈

- 兩層 for 迴圈，枚舉所有 0 <= i < j < n 的組合，若 nums [i] < nums [j]，計算 nums [j] - nums [i] 並更新最大值。
- 時間複雜度：O (n^2)
- 空間複雜度：O (1)

### 方法二：一次遍歷維護最小值

- 只需一次 for 迴圈，維護目前為止遇到的最小值 min，對每個 nums [i]，若 nums [i] > min，計算差值並更新最大值。
- 若 nums [i] <= min，則更新 min。
- 時間複雜度：O (n)
- 空間複雜度：O (1)

---

## 程式碼片段

```csharp
// 方法一：暴力法
public int MaximumDifference(int[] nums)
{
    int n = nums.Length;
    int res = -1;
    for (int i = 0; i < n; i++)
    {
        for (int j = i + 1; j < n; j++)
        {
            if (nums[i] < nums[j])
            {
                int diff = nums[j] - nums[i];
                res = Math.Max(res, diff);
            }
        }
    }
    return res;
}

// 方法二：一次遍歷
public int MaximumDifference2(int[] nums)
{
    int min = nums[0];
    int maxDiff = -1;
    for (int i = 1; i < nums.Length; i++)
    {
        if (nums[i] > min)
        {
            maxDiff = Math.Max(maxDiff, nums[i] - min);
        }
        else
        {
            min = Math.Min(min, nums[i]);
        }
    }
    return maxDiff;
}
```

---

## 方法比較

| 方法    | 時間複雜度  | 空間複雜度 | 可讀性    | 維護性 | 優點          | 缺點          |
| ----- | ------ | ----- | ------ | --- | ----------- | ----------- |
| 暴力法   | O(n^2) | O(1)  | 高 (直觀) | 高   | 容易理解，適合新手   | 效率低，n 大時不適用 |
| 一次遍歷法 | O(n)   | O(1)  | 高 (簡潔) | 高   | 執行效率高，程式碼簡潔 | 需理解維護最小值的技巧 |

- **暴力法**：適合用於小型資料或初學者理解題意，程式碼直觀易懂。
- **一次遍歷法**：適合實際應用與大資料，效率高且程式碼簡潔，維護性佳。

---

## 小結

本題最佳解法為一次遍歷法，能在 O (n) 時間內解決問題，且程式碼簡潔易於維護。暴力法僅適合用於理解題意或驗證小型資料。

---

## 相關資源

- [Leetcode Discuss](https://leetcode.com/problems/maximum-difference-between-increasing-elements/discuss/)
- [C# 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
