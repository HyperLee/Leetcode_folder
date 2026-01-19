# LeetCode 491 - Non-decreasing Subsequences

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-491-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/non-decreasing-subsequences/)

使用**深度優先搜尋（DFS）**與**回溯法（Backtracking）**解決 LeetCode 491 題：非遞減子序列。

## 題目說明

給定一個整數陣列 `nums`，找出所有**至少包含兩個元素**的**非遞減子序列**（不同的子序列）。

> [!NOTE]
> 子序列是從陣列中刪除一些元素（可以不刪除）後，剩餘元素保持原有順序所組成的序列。

### 範例

**範例 1：**
```
輸入: nums = [4, 6, 7, 7]
輸出: [[4,6], [4,6,7], [4,6,7,7], [4,7], [4,7,7], [6,7], [6,7,7], [7,7]]
```

**範例 2：**
```
輸入: nums = [4, 4, 3, 2, 1]
輸出: [[4, 4]]
```

### 限制條件

- `1 <= nums.length <= 15`
- `-100 <= nums[i] <= 100`

## 解題思路

### 核心概念

這道題目的關鍵在於：

1. **子序列 vs 子陣列**：子序列保持相對順序但不需要連續，而子陣列必須連續
2. **非遞減**：序列中每個元素都必須 ≥ 前一個元素
3. **去重**：不能產生重複的子序列

### 為什麼選擇回溯法？

回溯法非常適合這類「列舉所有可能組合」的問題：

- ✅ 需要找出**所有**符合條件的結果
- ✅ 每一步都有多種選擇（選或不選某個元素）
- ✅ 需要在過程中進行**剪枝**（排除不符合條件的路徑）

### 去重策略

> [!IMPORTANT]
> 這是本題最關鍵的部分！

由於陣列中可能存在重複元素，直接回溯會產生重複的子序列。解決方案是：

**在每一層遞迴中使用 HashSet 記錄已選擇的數字**

```
陣列: [4, 7, 7]

選擇第二個位置時:
  - 選第一個 7 → [4, 7]
  - 選第二個 7 → [4, 7]  ← 重複！

使用 HashSet 後:
  - 選第一個 7 → [4, 7] ✓
  - 第二個 7 已在 Set 中，跳過 ✓
```

## 演算法流程

### 步驟說明

```
1. 從陣列的每個位置開始，嘗試將元素加入當前子序列
2. 檢查條件：
   - 該元素是否 >= 子序列的最後一個元素（非遞減條件）
   - 該元素是否在當前層已被選過（去重條件）
3. 若符合條件：
   - 將元素加入子序列
   - 若子序列長度 >= 2，記錄到結果中
   - 遞迴處理下一個位置
   - 回溯：移除最後加入的元素
4. 重複直到遍歷完所有元素
```

### 圖解演示

以 `nums = [4, 6, 7]` 為例：

```
                          []
                    ┌──────┴──────┐
                   [4]           [6]           [7]
              ┌─────┴─────┐       │
            [4,6]       [4,7]   [6,7]
              │
           [4,6,7]

有效的非遞減子序列（長度 >= 2）:
✓ [4,6]
✓ [4,7]
✓ [4,6,7]
✓ [6,7]
```

### 帶重複元素的範例

以 `nums = [4, 7, 7]` 為例，展示去重機制：

```
起始: current=[], startIndex=0

第一層 (startIndex=0):
  Set = {}
  
  選 nums[0]=4: Set={4}, current=[4]
  │  第二層 (startIndex=1):
  │    Set = {}
  │    
  │    選 nums[1]=7: Set={7}, current=[4,7] → 記錄 [4,7] ✓
  │    │  第三層 (startIndex=2):
  │    │    選 nums[2]=7: current=[4,7,7] → 記錄 [4,7,7] ✓
  │    │  回溯: current=[4,7]
  │    回溯: current=[4]
  │    
  │    nums[2]=7 已在 Set 中 → 跳過（去重）
  │  回溯: current=[]
  
  選 nums[1]=7: Set={4,7}, current=[7]
  │  第二層 (startIndex=2):
  │    選 nums[2]=7: current=[7,7] → 記錄 [7,7] ✓
  │  回溯: current=[7]
  回溯: current=[]
  
  nums[2]=7 已在 Set 中 → 跳過（去重）

最終結果: [[4,7], [4,7,7], [7,7]]
```

## 複雜度分析

| 複雜度 | 說明 |
|--------|------|
| 時間複雜度 | O(2ⁿ × n)，最壞情況需要產生所有子序列 |
| 空間複雜度 | O(n)，遞迴堆疊深度 + 當前子序列長度 |

## 執行程式

### 前置需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_491
```

### 預期輸出

```
輸入: [4, 6, 7, 7]
輸出: [[4, 6], [4, 6, 7], [4, 6, 7, 7], [4, 7], [4, 7, 7], [6, 7], [6, 7, 7], [7, 7]]

輸入: [4, 4, 3, 2, 1]
輸出: [[4, 4]]

輸入: [1, 2, 3, 4, 5]
共找到 26 個非遞減子序列
```

## 相關題目

- [LeetCode 78 - Subsets](https://leetcode.com/problems/subsets/)
- [LeetCode 90 - Subsets II](https://leetcode.com/problems/subsets-ii/)
- [LeetCode 46 - Permutations](https://leetcode.com/problems/permutations/)
- [LeetCode 47 - Permutations II](https://leetcode.com/problems/permutations-ii/)

## 參考資源

- [LeetCode 題目連結（英文）](https://leetcode.com/problems/non-decreasing-subsequences/)
- [LeetCode 題目連結（中文）](https://leetcode.cn/problems/non-decreasing-subsequences/)
