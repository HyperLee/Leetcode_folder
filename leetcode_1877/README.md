# LeetCode 1877. Minimize Maximum Pair Sum in Array

[![LeetCode](https://img.shields.io/badge/LeetCode-1877-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/minimize-maximum-pair-sum-in-array/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/minimize-maximum-pair-sum-in-array/)
[![C#](https://img.shields.io/badge/Language-C%23-239120?style=flat-square&logo=c-sharp)](https://docs.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

> 最小化陣列中最大數對和 — 使用排序 + 貪心策略

## 📋 題目描述

### English

The **pair sum** of a pair `(a, b)` is equal to `a + b`. The **maximum pair sum** is the largest pair sum in a list of pairs.

- For example, if we have pairs `(1,5)`, `(2,3)`, and `(4,4)`, the maximum pair sum would be `max(1+5, 2+3, 4+4) = max(6, 5, 8) = 8`.

Given an array `nums` of **even** length `n`, pair up the elements of `nums` into `n / 2` pairs such that:

1. Each element of `nums` is in **exactly one** pair, and
2. The **maximum pair sum** is **minimized**.

Return the **minimized maximum pair sum** after optimally pairing up the elements.

### 繁體中文

對於一個數對 `(a, b)`，該數對的和為 `a + b`。在一組數對中，最大的數對和稱為「**最大數對和**」。

- 例如：數對 `(1,5)`、`(2,3)` 與 `(4,4)` 的最大數對和為 `max(1+5, 2+3, 4+4) = max(6, 5, 8) = 8`。

給定一個長度為**偶數** `n` 的陣列 `nums`，將 `nums` 中的元素兩兩配對成 `n / 2` 個數對，要求：

1. 每個元素**恰好**屬於一個數對；
2. 要使配對後的**最大數對和最小**。

回傳最佳配對下的**最小最大數對和**。

## 🎯 解題概念與出發點

### 核心洞察

這是一道典型的**貪心（Greedy）** 問題。關鍵觀察：

> 若要讓「最大數對和」盡可能小，應該讓各個數對的和盡可能「平均」分布。

直覺上：

- 如果把兩個大的數配在一起 → 這個數對和會非常大 ❌
- 如果把兩個小的數配在一起 → 剩下的大數必須互配，還是會產生很大的和 ❌
- **最佳策略：最小配最大、次小配次大...** → 讓每對的和趨於平衡 ✅

## 📐 解法詳解

### 演算法步驟

```text
1. 將陣列 nums 排序（升序）
2. 使用雙指標：左指標從最小值開始，右指標從最大值開始
3. 每次將 nums[i] 與 nums[n-1-i] 配對
4. 追蹤所有配對中的最大和
5. 回傳該最大值
```

### C# 實作

```csharp
public int MinPairSum(int[] nums)
{
    // Step 1: 排序陣列
    Array.Sort(nums);

    int res = 0;
    int n = nums.Length;

    // Step 2: 最小配最大，計算最大數對和
    for (int i = 0; i < n / 2; i++)
    {
        int pairSum = nums[i] + nums[n - 1 - i];
        res = Math.Max(res, pairSum);
    }

    return res;
}
```

### 複雜度分析

| 指標 | 複雜度       | 說明                           |
| ---- | ------------ | ------------------------------ |
| 時間 | O(n log n)   | 排序主導                       |
| 空間 | O(1)         | 原地排序，僅使用常數額外空間   |

## 🔬 範例演示流程

### 範例 1：`nums = [3, 5, 2, 3]`

```text
原始陣列: [3, 5, 2, 3]
     ↓ 排序
排序後:   [2, 3, 3, 5]
           ↑        ↑
           i=0    n-1-i=3
           
配對過程:
┌─────────────────────────────────────┐
│ i=0: nums[0] + nums[3] = 2 + 5 = 7  │  ← 最小配最大
│ i=1: nums[1] + nums[2] = 3 + 3 = 6  │  ← 次小配次大
└─────────────────────────────────────┘

最大數對和 = max(7, 6) = 7 ✓
```

### 範例 2：`nums = [3, 5, 4, 2, 4, 6]`

```text
原始陣列: [3, 5, 4, 2, 4, 6]
     ↓ 排序
排序後:   [2, 3, 4, 4, 5, 6]
           ↑              ↑
           
配對過程:
┌─────────────────────────────────────┐
│ i=0: nums[0] + nums[5] = 2 + 6 = 8  │
│ i=1: nums[1] + nums[4] = 3 + 5 = 8  │
│ i=2: nums[2] + nums[3] = 4 + 4 = 8  │
└─────────────────────────────────────┘

最大數對和 = max(8, 8, 8) = 8 ✓
```

> [!NOTE]
> 注意這個範例中，所有配對的和都相等！這正是貪心策略讓結果「平衡」的體現。

## 📚 數學原理推導與證明

### 定理

對於排序後的陣列 $a_1 \leq a_2 \leq \cdots \leq a_n$，配對方式 $(a_1, a_n), (a_2, a_{n-1}), \ldots$ 可使最大數對和最小。

### 證明（交換論證法）

**目標：** 證明 $a_1$ 與 $a_n$ 配對是最優的。

**設定：** 假設存在另一個配對方案，其中：

- $a_1$ 與 $a_i$ 配對
- $a_j$ 與 $a_n$ 配對（$1 < i, j < n$）

**比較兩種方案：**

| 方案   | 配對                           | 最大數對和                                            |
| ------ | ------------------------------ | ----------------------------------------------------- |
| 方案 A | $(a_1, a_i)$ 與 $(a_j, a_n)$   | $M_1 = \max(a_1 + a_i, a_j + a_n, \text{其他})$       |
| 方案 B | $(a_1, a_n)$ 與 $(a_i, a_j)$   | $M_2 = \max(a_1 + a_n, a_i + a_j, \text{其他})$       |

**推導：**

由於 $a_1 \leq a_j$ 且 $a_i \leq a_n$：

$$a_1 + a_i \leq a_j + a_n$$

因此：
$$M_1 = \max(a_j + a_n, \text{其他})$$

同時：
$$a_1 + a_n \leq a_j + a_n$$
$$a_i + a_j \leq a_j + a_n$$

所以：
$$\max(a_1 + a_n, a_i + a_j) \leq a_j + a_n$$

**結論：**
$$M_2 \leq M_1$$

這意味著將 $a_1$ 與 $a_n$ 配對**不會使結果變差**。

### 歸納推廣

移除已配對的 $a_1$ 與 $a_n$ 後，剩餘 $n-2$ 個元素形成子問題。  
對子問題遞迴應用同樣的論證，可得：

$$\text{最優配對} = \{(a_k, a_{n+1-k}) \mid k = 1, 2, \ldots, n/2\}$$

### 反證法補充證明

假設存在更優的配對方案，其最大數對和為 $M^* < M_{\text{greedy}}$。

設貪心方案中最大的數對和為 $a_{k'} + a_{n+1-k'}$。

在任意配對方案中，滿足 $v \geq n+1-k'$ 的 $v$ 有 $k'$ 個。  
這些 $v$ 配對的 $u$ 若要使 $a_u + a_v < a_{k'} + a_{n+1-k'}$，則需 $a_u < a_{k'}$。  
但滿足此條件的 $u$ 只有 $k'-1$ 個（即 $u \in [1, k'-1]$）。

**矛盾！** $k'$ 個 $v$ 無法都找到對應的 $u$。

因此不存在比貪心策略更優的方案。 **Q.E.D.**

## 🚀 執行方式

### 前置需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1877
```

### 預期輸出

```text
Test 1: [3, 5, 2, 3] => 7
Test 2: [3, 5, 4, 2, 4, 6] => 8
Test 3: [1, 2] => 3
Test 4: [1, 1, 1, 1] => 2
```

## 📖 相關資源

- [LeetCode 1877 - English](https://leetcode.com/problems/minimize-maximum-pair-sum-in-array/)
- [力扣 1877 - 简体中文](https://leetcode.cn/problems/minimize-maximum-pair-sum-in-array/)

## 📁 專案結構

```text
leetcode_1877/
├── leetcode_1877.sln          # 方案檔
├── README.md                  # 本文件
└── leetcode_1877/
    ├── leetcode_1877.csproj   # 專案檔
    └── Program.cs             # 主程式與解法
```
