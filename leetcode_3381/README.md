# LeetCode 3381 - 長度可被 K 整除的子陣列的最大元素和

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-3381-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/maximum-subarray-sum-with-length-divisible-by-k/)

## 題目描述

給定一個整數陣列 `nums` 與一個正整數 `k`，請回傳 `nums` 中**長度能被 `k` 整除**的**非空子陣列**的最大總和。

### 範例

**範例 1：**

```text
輸入：nums = [1, 2], k = 1
輸出：3
說明：子陣列 [1, 2] 長度為 2，可被 1 整除，總和為 3。
```

**範例 2：**

```text
輸入：nums = [-1, -2, -3, -4, -5], k = 4
輸出：-10
說明：子陣列 [-1, -2, -3, -4] 長度為 4，可被 4 整除，總和為 -10。
```

**範例 3：**

```text
輸入：nums = [-5, 1, 2, -3, 4], k = 2
輸出：4
說明：子陣列 [1, 2, -3, 4] 長度為 4，可被 2 整除，總和為 4。
```

### 限制條件

- $1 \leq k \leq nums.length \leq 2 \times 10^5$
- $-10^9 \leq nums[i] \leq 10^9$

## 解題思路

### 核心概念：前綴和 (Prefix Sum)

這道題目的關鍵在於利用**前綴和**搭配**同餘性質**來高效求解。

### 前綴和定義

令陣列 `nums` 的前綴和為：

$$prefixSum[i] = \sum_{j=0}^{i} nums[j] = nums[0] + nums[1] + \cdots + nums[i]$$

則區間 $[j, i]$ 的子陣列和為：

$$sum(j, i) = prefixSum[i] - prefixSum[j-1]$$

### 長度條件推導

題目要求子陣列的長度可以被 $k$ 整除，即：

$$(i - j + 1) \mod k = 0$$

這可以轉換為：

$$i - j + 1 \equiv 0 \pmod{k}$$

$$i + 1 \equiv j \pmod{k}$$

$$i \equiv j - 1 \pmod{k}$$

因此我們得到關鍵結論：

> **當 $i \mod k = (j-1) \mod k$ 時，子陣列 $[j, i]$ 的長度可被 $k$ 整除。**

### 最佳化策略

為了找到以索引 $i$ 為結尾的最大子陣列和，我們需要：

1. 計算當前的前綴和 $prefixSum[i]$
2. 找到所有與 $i$ 同餘的前綴和中的**最小值** $kSum[i \mod k]$
3. 最大子陣列和 = $prefixSum[i] - kSum[i \mod k]$

使用陣列 `kSum[l]` 記錄所有索引對 $k$ 取餘為 $l$ 的前綴和最小值，即可在 $O(1)$ 時間內查詢。

### 演算法步驟

1. 初始化 `kSum` 陣列，所有值設為極大值（避免溢位使用 `long.MaxValue / 2`）
2. 設定 `kSum[k-1] = 0`，代表「空前綴」的情況
3. 遍歷每個元素：
   - 累加前綴和
   - 更新最大值：`maxSum = max(maxSum, prefixSum - kSum[i % k])`
   - 更新同餘最小前綴和：`kSum[i % k] = min(kSum[i % k], prefixSum)`

### 為什麼 kSum[k-1] = 0？

當子陣列從索引 0 開始且長度為 $k$ 的倍數時，我們需要減去的「前一個前綴和」實際上是 0（空陣列的和）。

由於索引 $k-1$ 的前綴和對應到「前 $k$ 個元素」的情況，其前面的「空前綴」索引為 $-1$，而 $(-1) \mod k = k - 1$，所以 `kSum[k-1]` 初始化為 0。

## 複雜度分析

| 指標 | 複雜度 |
|------|--------|
| 時間複雜度 | $O(n)$ |
| 空間複雜度 | $O(k)$ |

- **時間複雜度**：只需遍歷陣列一次
- **空間複雜度**：只需要大小為 $k$ 的輔助陣列

## 執行專案

### 環境需求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 或更高版本

### 執行步驟

```bash
# 進入專案目錄
cd leetcode_3381

# 建構專案
dotnet build

# 執行程式
dotnet run
```

### 預期輸出

```text
範例 1: nums = [1, 2], k = 1
結果: 3

範例 2: nums = [-1, -2, -3, -4, -5], k = 4
結果: -10

範例 3: nums = [-5, 1, 2, -3, 4], k = 2
結果: 4
```

## 相關連結

- [LeetCode 原題 (英文)](https://leetcode.com/problems/maximum-subarray-sum-with-length-divisible-by-k/)
- [力扣 原題 (中文)](https://leetcode.cn/problems/maximum-subarray-sum-with-length-divisible-by-k/)
- [力扣官方題解](https://leetcode.cn/problems/maximum-subarray-sum-with-length-divisible-by-k/solutions/3837035/chang-du-ke-bei-k-zheng-chu-de-zi-shu-zu-dzxb/)
