# LeetCode 1480. Running Sum of 1d Array

[![LeetCode](https://img.shields.io/badge/LeetCode-1480-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/running-sum-of-1d-array/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/running-sum-of-1d-array/)
[![Language](https://img.shields.io/badge/Language-C%23-blue?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

一維數組的動態和 (Running Sum) 問題的 C# 解法實作。

## 題目描述

給定一個陣列 `nums`，定義陣列的動態和為：

$$runningSum[i] = \sum_{j=0}^{i} nums[j]$$

也就是說：`runningSum[i] = nums[0] + nums[1] + ... + nums[i]`

**目標：** 返回 `nums` 的動態和陣列。

### 範例

| 輸入 | 輸出 | 說明 |
|------|------|------|
| `[1, 2, 3, 4]` | `[1, 3, 6, 10]` | `[1, 1+2, 1+2+3, 1+2+3+4]` |
| `[1, 1, 1, 1, 1]` | `[1, 2, 3, 4, 5]` | 累加 1 的結果 |
| `[3, 1, 2, 10, 1]` | `[3, 4, 6, 16, 17]` | 依序累加 |

### 限制條件

- `1 <= nums.length <= 1000`
- `-10^6 <= nums[i] <= 10^6`

## 解題思路

### 核心概念

這道題的關鍵在於理解**前綴和 (Prefix Sum)** 的概念：

- `runningSum[0] = nums[0]`（第一個元素的動態和就是它本身）
- `runningSum[i] = runningSum[i-1] + nums[i]`（後續元素的動態和 = 前一個動態和 + 當前元素）

### 遞推公式推導

$$
runningSum[i] = 
\begin{cases} 
nums[0] & \text{if } i = 0 \\
runningSum[i-1] + nums[i] & \text{if } i > 0
\end{cases}
$$

## 解法：原地修改 (In-place Modification)

### 算法說明

由於 `runningSum[i-1]` 在計算完成後就存放在 `nums[i-1]` 中，我們可以直接在原陣列上進行修改，無需額外空間。

### 程式碼

```csharp
public int[] RunningSum(int[] nums)
{
    int n = nums.Length;
    
    // 從索引 1 開始（索引 0 的動態和就是它本身）
    for (int i = 1; i < n; i++)
    {
        // nums[i-1] 此時已經是 runningSum[i-1]
        nums[i] += nums[i - 1];
    }
    
    return nums;
}
```

### 複雜度分析

| 複雜度類型 | 數值 | 說明 |
|-----------|------|------|
| 時間複雜度 | O(n) | 只需遍歷陣列一次 |
| 空間複雜度 | O(1) | 原地修改，不需額外空間 |

## 演示流程

以 `nums = [1, 2, 3, 4]` 為例：

```
初始狀態: [1, 2, 3, 4]
         ↑
         i=0 (不處理，動態和就是它本身)

步驟 1:   [1, 2, 3, 4]
            ↑
            i=1: nums[1] = nums[0] + nums[1] = 1 + 2 = 3
         結果: [1, 3, 3, 4]

步驟 2:   [1, 3, 3, 4]
               ↑
               i=2: nums[2] = nums[1] + nums[2] = 3 + 3 = 6
         結果: [1, 3, 6, 4]

步驟 3:   [1, 3, 6, 4]
                  ↑
                  i=3: nums[3] = nums[2] + nums[3] = 6 + 4 = 10
         結果: [1, 3, 6, 10]

最終輸出: [1, 3, 6, 10] ✓
```

## 執行專案

### 環境需求

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) 或更高版本

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1480/leetcode_1480.csproj
```

### 預期輸出

```
範例 1: [1, 3, 6, 10]
範例 2: [1, 2, 3, 4, 5]
範例 3: [3, 4, 6, 16, 17]
```

## 相關題目

- [LeetCode 303. Range Sum Query - Immutable](https://leetcode.com/problems/range-sum-query-immutable/) - 區域和檢索
- [LeetCode 724. Find Pivot Index](https://leetcode.com/problems/find-pivot-index/) - 尋找陣列的中心下標
- [LeetCode 560. Subarray Sum Equals K](https://leetcode.com/problems/subarray-sum-equals-k/) - 和為 K 的子陣列

## 參考資料

- [LeetCode 題目連結 (英文)](https://leetcode.com/problems/running-sum-of-1d-array/)
- [力扣題目連結 (中文)](https://leetcode.cn/problems/running-sum-of-1d-array/)
