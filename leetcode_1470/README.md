# LeetCode 1470. Shuffle the Array

[![LeetCode](https://img.shields.io/badge/LeetCode-1470-orange?style=flat-square)](https://leetcode.com/problems/shuffle-the-array/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/shuffle-the-array/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)

C# 實作 LeetCode 第 1470 題「重新排列陣列」的解法。

## 題目描述

給定一個長度為 `2n` 的陣列 `nums`，其形式為：

`[x₁, x₂, ..., xₙ, y₁, y₂, ..., yₙ]`

請將陣列重新排列為：

`[x₁, y₁, x₂, y₂, ..., xₙ, yₙ]`

### 範例

**範例 1：**

```text
輸入：nums = [2,5,1,3,4,7], n = 3
輸出：[2,3,5,4,1,7]
解釋：x = [2,5,1], y = [3,4,7]，結果為 [2,3,5,4,1,7]
```

**範例 2：**

```text
輸入：nums = [1,2,3,4,4,3,2,1], n = 4
輸出：[1,4,2,3,3,2,4,1]
```

**範例 3：**

```text
輸入：nums = [1,1,2,2], n = 2
輸出：[1,2,1,2]
```

### 限制條件

- `1 <= n <= 500`
- `nums.length == 2n`
- `1 <= nums[i] <= 10³`

## 解題思路

### 核心觀察

觀察原陣列與目標陣列的對應關係：

| 原始位置           | 元素   | 目標位置      |
|:------------------:|:------:|:-------------:|
| `i` (前半部)       | `xᵢ`   | `2 × i`       |
| `i + n` (後半部)   | `yᵢ`   | `2 × i + 1`   |

### 關鍵洞察

1. **前半部元素** `nums[0..n-1]` 代表 `x₁, x₂, ..., xₙ`
2. **後半部元素** `nums[n..2n-1]` 代表 `y₁, y₂, ..., yₙ`
3. 結果陣列中：
   - **偶數索引** `(0, 2, 4, ...)` 放置 `x` 元素
   - **奇數索引** `(1, 3, 5, ...)` 放置 `y` 元素

### 演算法步驟

1. 建立長度為 `2n` 的結果陣列
2. 遍歷 `i` 從 `0` 到 `n-1`：
   - 將 `nums[i]` 放到 `res[2×i]`
   - 將 `nums[i+n]` 放到 `res[2×i+1]`
3. 回傳結果陣列

## 流程演示

以 `nums = [2,5,1,3,4,7]`, `n = 3` 為例：

```text
原始陣列: [2, 5, 1, 3, 4, 7]
           ├─────┤  ├─────┤
           前半部x   後半部y

步驟分解：
┌─────────────────────────────────────────────────────────┐
│ i = 0:                                                  │
│   nums[0] = 2 (x₁) → res[0]                            │
│   nums[3] = 3 (y₁) → res[1]                            │
│   res = [2, 3, _, _, _, _]                             │
├─────────────────────────────────────────────────────────┤
│ i = 1:                                                  │
│   nums[1] = 5 (x₂) → res[2]                            │
│   nums[4] = 4 (y₂) → res[3]                            │
│   res = [2, 3, 5, 4, _, _]                             │
├─────────────────────────────────────────────────────────┤
│ i = 2:                                                  │
│   nums[2] = 1 (x₃) → res[4]                            │
│   nums[5] = 7 (y₃) → res[5]                            │
│   res = [2, 3, 5, 4, 1, 7]                             │
└─────────────────────────────────────────────────────────┘

最終結果: [2, 3, 5, 4, 1, 7] = [x₁, y₁, x₂, y₂, x₃, y₃]
```

## 複雜度分析

| 指標 | 複雜度 | 說明               |
|:----:|:------:|:-------------------|
| 時間 | O(n)   | 僅需一次遍歷       |
| 空間 | O(n)   | 需要額外的結果陣列 |

## 使用方式

### 前置需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) 或更高版本

### 執行程式

```bash
# 複製專案
git clone https://github.com/HyperLee/Leetcode_folder.git
cd Leetcode_folder/leetcode_1470

# 建構並執行
dotnet run --project leetcode_1470
```

### 預期輸出

```text
=== LeetCode 1470. Shuffle the Array ===

測試案例 1:
  輸入: nums = [2,5,1,3,4,7], n = 3
  輸出: [2,3,5,4,1,7]
  預期: [2,3,5,4,1,7]

測試案例 2:
  輸入: nums = [1,2,3,4,4,3,2,1], n = 4
  輸出: [1,4,2,3,3,2,4,1]
  預期: [1,4,2,3,3,2,4,1]

測試案例 3:
  輸入: nums = [1,1,2,2], n = 2
  輸出: [1,2,1,2]
  預期: [1,2,1,2]
```

## 相關連結

- [LeetCode 題目頁面 (英文)](https://leetcode.com/problems/shuffle-the-array/)
- [力扣題目頁面 (中文)](https://leetcode.cn/problems/shuffle-the-array/)
