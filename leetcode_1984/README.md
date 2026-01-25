# LeetCode 1984. Minimum Difference Between Highest and Lowest of K Scores

[![LeetCode](https://img.shields.io/badge/LeetCode-1984-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/minimum-difference-between-highest-and-lowest-of-k-scores/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/minimum-difference-between-highest-and-lowest-of-k-scores/)
[![Language](https://img.shields.io/badge/Language-C%23-purple?style=flat-square&logo=csharp)](https://dotnet.microsoft.com/)

K 個分數中最高與最低分數的最小差值

## 題目描述

給定一個以 **0 為起點**的整數陣列 `nums`，其中 `nums[i]` 表示第 `i` 位學生的分數，並給定整數 `k`。

從陣列中選出任意 `k` 位學生的分數，使這 `k` 個分數的**最大值與最小值之差最小化**。

回傳可能的最小差值。

### 範例

**範例 1：**

```text
輸入: nums = [90], k = 1
輸出: 0
解釋: 只有一個學生，選擇他的分數，最高分與最低分相同，差值為 0。
```

**範例 2：**

```text
輸入: nums = [9,4,1,7], k = 2
輸出: 2
解釋: 選擇分數為 7 和 9 的學生，差值為 9 - 7 = 2。
```

### 限制條件

- `1 <= k <= nums.length <= 1000`
- `0 <= nums[i] <= 10^5`

## 解題思路

### 核心觀察

要最小化選擇的 `k` 名學生中最高分和最低分的差值，我們**必定是在排序後的陣列中連續選擇** `k` 個元素。

### 為什麼必須連續選擇？

#### 直觀理解

假設我們跳過了某個下標 `i`，而選擇了不連續的元素：

```text
排序後陣列: [1, 4, 7, 9]
選擇 k=3:   [1, _, 7, 9]  ← 跳過了 4
差值: 9 - 1 = 8
```

此時，如果我們將最高分 `9` 替換成被跳過的 `4`：

```text
新選擇:     [1, 4, 7, _]  ← 改選 1, 4, 7
差值: 7 - 1 = 6  ← 差值變小了！
```

因此，跳過元素**永遠不會**讓結果更好，最優解必定是連續選擇排序後陣列中的 `k` 個元素。

#### 嚴謹證明

**定理**：在排序後的陣列中，選擇 k 個元素使得最大值與最小值差值最小，則這 k 個元素必定是連續的。

**證明（反證法）**：

設排序後的陣列為 $a_1 \leq a_2 \leq \cdots \leq a_n$。

假設存在一個最優解 $S = \{a_{i_1}, a_{i_2}, \ldots, a_{i_k}\}$，其中 $i_1 < i_2 < \cdots < i_k$，且這些元素**不連續**（即存在某個 $j$ 使得 $i_{j+1} > i_j + 1$）。

此時：

- 最小值為 $a_{i_1}$
- 最大值為 $a_{i_k}$
- 差值為 $a_{i_k} - a_{i_1}$

由於元素不連續，存在一個被跳過的元素 $a_m$，其中 $i_1 < m < i_k$ 且 $m \notin \{i_1, i_2, \ldots, i_k\}$。

構造新的選擇 $S' = S \cup \{a_m\} \setminus \{a_{i_k}\}$（用 $a_m$ 替換 $a_{i_k}$）：

- $S'$ 仍包含 k 個元素
- 新的最小值仍為 $a_{i_1}$（因為 $a_m \geq a_{i_1}$）
- 新的最大值為 $\max(a_{i_{k-1}}, a_m) < a_{i_k}$（因為 $a_m < a_{i_k}$ 且 $a_{i_{k-1}} < a_{i_k}$）

因此，$S'$ 的差值**嚴格小於** $S$ 的差值，這與 $S$ 是最優解矛盾。

**結論**：最優解中的元素必定是排序後陣列中**連續**的 k 個元素。$\square$

#### 另一種理解方式：貪心思想

| 選擇策略 | 差值範圍 | 結論 |
| --------- | --------- | ------ |
| 連續選擇 $[a_i, a_{i+1}, \ldots, a_{i+k-1}]$ | $a_{i+k-1} - a_i$ | 差值由視窗大小決定 |
| 不連續選擇（跳過中間元素） | $\geq a_{i+k-1} - a_i$ | 差值只會更大或相等 |

跳過中間元素不會改變最小值和最大值（除非我們也換掉它們），因此不連續選擇**沒有任何優勢**。

## 演算法

1. **排序**：將陣列 `nums` 升序排序
2. **滑動視窗**：使用大小為 `k` 的視窗遍歷排序後的陣列
3. **計算差值**：對每個視窗，計算 `nums[i+k-1] - nums[i]`（視窗內最大值減最小值）
4. **取最小值**：回傳所有視窗差值中的最小值

### 複雜度分析

| 項目       | 複雜度     |
|------------|------------|
| 時間複雜度 | O(n log n) |
| 空間複雜度 | O(1)       |

> [!NOTE]
> 時間複雜度主要來自排序，滑動視窗遍歷僅需 O(n)。
> 空間複雜度為 O(1)，因為我們使用原地排序且僅使用常數額外空間。

## 範例演示

以 `nums = [9, 4, 1, 7]`, `k = 2` 為例：

### 步驟 1：排序

```text
原始陣列: [9, 4, 1, 7]
排序後:   [1, 4, 7, 9]
           ↑  ↑  ↑  ↑
           0  1  2  3  (索引)
```

### 步驟 2：滑動視窗遍歷

```text
視窗 1 (i=0): [1, 4] → 差值 = 4 - 1 = 3
               └──┘
               
視窗 2 (i=1): [4, 7] → 差值 = 7 - 4 = 3
                  └──┘
                  
視窗 3 (i=2): [7, 9] → 差值 = 9 - 7 = 2  ✓ 最小！
                     └──┘
```

### 步驟 3：回傳結果

```text
所有差值: [3, 3, 2]
最小差值: 2
```

## 執行專案

### 前置需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download) 或更新版本

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1984/leetcode_1984.csproj
```

### 預期輸出

```text
測試案例 1: nums = [90], k = 1
結果: 0

測試案例 2: nums = [9, 4, 1, 7], k = 2
結果: 2

測試案例 3: nums = [87063, 61094, 44530, 21297, 95857, 93551, 9918], k = 6
結果: 74560
```

## 程式碼結構

```text
leetcode_1984/
├── leetcode_1984.sln          # 解決方案檔
├── README.md                   # 說明文件
└── leetcode_1984/
    ├── leetcode_1984.csproj   # 專案檔
    └── Program.cs              # 主程式（包含解題實作）
```

## 相關題目

- [LeetCode 561. Array Partition](https://leetcode.com/problems/array-partition/)
- [LeetCode 1509. Minimum Difference Between Largest and Smallest Value in Three Moves](https://leetcode.com/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/)
