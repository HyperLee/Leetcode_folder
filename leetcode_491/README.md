# LeetCode 491 - Non-decreasing Subsequences

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-491-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/non-decreasing-subsequences/)

提供三種解法解決 LeetCode 491 題：非遞減子序列。

| 解法 | 方法 | 特點 |
|:----:|------|------|
| 一 | 回溯法（HashSet 去重） | 標準寫法，易理解 |
| 二 | 優化版回溯法（陣列去重） | **效能最佳** |
| 三 | 位元遮罩枚舉法 | 無遞迴，程式碼簡潔 |

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

## 解法一：回溯法（HashSet 去重）

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

---

## 解法二：優化版回溯法（陣列去重 + stackalloc）

### 優化策略

利用題目的限制條件 `-100 ≤ nums[i] ≤ 100`，可以用固定大小的布林陣列取代 HashSet 進行去重：

1. **使用陣列取代 HashSet**：建立大小為 201 的布林陣列（索引 0~200 對應值 -100~100）
2. **使用 `stackalloc`**：在堆疊上分配記憶體，避免堆積記憶體分配與 GC 開銷
3. **使用索引運算子 `[^1]`**：取代 LINQ 的 `Last()` 方法，減少函式呼叫開銷

### 核心程式碼

```csharp
// 使用 stackalloc 在堆疊上分配 201 個 bool 的空間
// 索引 0~200 對應數值 -100~100
Span<bool> used = stackalloc bool[201];

for (int index = startIndex; index < nums.Length; index++)
{
    int mappedIndex = nums[index] + 100;

    // 去重：檢查當前層是否已選過此數值
    if (used[mappedIndex])
    {
        continue;
    }

    // 非遞減條件檢查
    if (current.Count > 0 && current[^1] > nums[index])
    {
        continue;
    }

    // 標記當前層已選取此數值
    used[mappedIndex] = true;

    current.Add(nums[index]);
    BacktrackOptimized(index + 1, nums, current, result);
    current.RemoveAt(current.Count - 1);
}
```

### 優化效果

| 項目 | 原版（HashSet） | 優化版（陣列） |
|------|----------------|---------------|
| 去重查詢 | O(1) 雜湊運算 | O(1) 陣列索引 |
| 記憶體分配 | 堆積記憶體 | 堆疊記憶體 |
| GC 壓力 | 每層遞迴建立新物件 | 無額外物件 |

---

## 解法三：位元遮罩枚舉法（Bitmask Enumeration）

### 解題思路

對於長度為 n 的陣列，共有 2ⁿ 個子序列。使用整數的位元表示來枚舉所有可能的子序列組合。

### 演算法說明

1. **位元表示**：若整數 `mask` 的第 i 位為 1，表示選取 `nums[i]`
2. **枚舉所有子集**：從 1 到 2ⁿ-1 枚舉所有可能的 mask
3. **驗證條件**：檢查每個子序列是否長度 ≥ 2 且為非遞減
4. **去重處理**：使用 HashSet 儲存已見過的序列

### 圖解演示

以 `nums = [4, 6, 7]` 為例（n=3，共 2³=8 個子集）：

```text
mask = 001 (1) → 選 nums[0]=4       → [4]       長度 < 2，跳過
mask = 010 (2) → 選 nums[1]=6       → [6]       長度 < 2，跳過
mask = 011 (3) → 選 nums[0,1]=4,6   → [4,6]     ✓ 非遞減
mask = 100 (4) → 選 nums[2]=7       → [7]       長度 < 2，跳過
mask = 101 (5) → 選 nums[0,2]=4,7   → [4,7]     ✓ 非遞減
mask = 110 (6) → 選 nums[1,2]=6,7   → [6,7]     ✓ 非遞減
mask = 111 (7) → 選 nums[0,1,2]     → [4,6,7]   ✓ 非遞減
```

### 核心程式碼

```csharp
int totalSubsets = 1 << n;  // 2^n

for (int mask = 1; mask < totalSubsets; mask++)
{
    var subsequence = new List<int>();

    // 根據位元遮罩選取元素
    for (int i = 0; i < n; i++)
    {
        if ((mask & (1 << i)) != 0)
        {
            subsequence.Add(nums[i]);
        }
    }

    // 檢查是否符合條件
    if (subsequence.Count >= 2 && IsNonDecreasing(subsequence))
    {
        string key = string.Join(",", subsequence);
        if (seen.Add(key))
        {
            result.Add(subsequence);
        }
    }
}
```

### 適用場景

- ✅ 當 n ≤ 15 時效能尚可接受（題目限制正好是 15）
- ✅ 程式碼簡潔，易於理解和維護
- ✅ 不需要遞迴，避免堆疊溢位風險
- ⚠️ 空間複雜度較高，需要額外儲存用於去重

---

## 三種解法比較

### 複雜度比較

| 解法 | 時間複雜度 | 空間複雜度 | 去重方式 |
|:----:|:----------:|:----------:|:--------:|
| 解法一 | O(2ⁿ × n) | O(n) | HashSet（每層） |
| 解法二 | O(2ⁿ × n) | O(n) | 陣列（stackalloc） |
| 解法三 | O(2ⁿ × n) | O(2ⁿ × n) | HashSet（全域） |

### 效能比較

| 項目 | 解法一 | 解法二 | 解法三 |
|------|:------:|:------:|:------:|
| 執行速度 | 中等 | **最快** | 較慢 |
| 記憶體使用 | 中等 | **最少** | 最多 |
| GC 壓力 | 中等 | **最低** | 較高 |

### 特點比較

| 項目 | 解法一 | 解法二 | 解法三 |
|------|--------|--------|--------|
| 實作難度 | ⭐⭐ 中等 | ⭐⭐⭐ 較高 | ⭐ 簡單 |
| 程式碼可讀性 | 高 | 中等 | **最高** |
| 遞迴深度 | O(n) | O(n) | **無遞迴** |
| 適用場景 | 通用 | **效能優先** | 小規模資料 |

### 推薦選擇

```text
┌─────────────────────────────────────────────────────────────┐
│  🎯 面試/競賽推薦：解法二（優化版回溯法）                     │
│     - 效能最佳，常數因子最小                                 │
│     - 展現對底層記憶體管理的理解                             │
├─────────────────────────────────────────────────────────────┤
│  📚 學習/教學推薦：解法一（標準回溯法）                       │
│     - 程式碼清晰，易於理解回溯概念                           │
│     - 是解決此類問題的標準範式                               │
├─────────────────────────────────────────────────────────────┤
│  🔧 快速實作推薦：解法三（位元遮罩法）                        │
│     - 程式碼最簡潔，不易出錯                                 │
│     - 適合 n ≤ 15 的小規模資料                              │
└─────────────────────────────────────────────────────────────┘
```

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
