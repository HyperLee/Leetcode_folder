# LeetCode 3321: Find X-Sum of All K-Long Subarrays II

> **難度**: Hard  
> **標籤**: Sliding Window, Heap, Ordered Set, Hash Table

## 問題描述

給定一個包含 `n` 個整數的陣列 `nums` 和兩個整數 `k` 和 `x`。

陣列的 **x-sum** 通過以下程序計算：

1. 統計陣列中所有元素的出現次數
2. 僅保留前 `x` 個最頻繁元素的出現次數
   - 如果兩個元素出現次數相同，則**值較大**的元素被視為更頻繁
3. 計算結果陣列的總和（元素值 × 出現次數）

**注意**: 如果陣列的相異元素少於 `x` 個，則其 x-sum 為陣列的總和。

返回一個長度為 `n - k + 1` 的整數陣列 `answer`，其中 `answer[i]` 是子陣列 `nums[i..i + k - 1]` 的 x-sum。

### 範例

**範例 1:**

```text
輸入: nums = [1,1,2,2,3,4,2,3], k = 6, x = 2
輸出: [6,10,12]
解釋:
- 子陣列 [1,1,2,2,3,4]: 頻率 {1:2, 2:2, 3:1, 4:1}，前2頻繁 → 2(2次) + 1(2次) = 6
- 子陣列 [1,2,2,3,4,2]: 頻率 {1:1, 2:3, 3:1, 4:1}，前2頻繁 → 2(3次) + 4(1次) = 10
- 子陣列 [2,2,3,4,2,3]: 頻率 {2:3, 3:2, 4:1}，前2頻繁 → 2(3次) + 3(2次) = 12
```

**範例 2:**

```text
輸入: nums = [5,5,5,5,5], k = 3, x = 1
輸出: [15,15,15]
解釋: 每個長度為3的子陣列都包含3個5，x-sum = 5×3 = 15
```

### 限制條件

- `1 ≤ n == nums.length ≤ 50,000`
- `1 ≤ nums[i] ≤ 50`
- `1 ≤ x ≤ k ≤ n`

## 解法說明

### 為什麼 LeetCode 3318 的解法無法直接套用？

LeetCode 3318 和 3321 是姊妹題，主要差異在於**資料規模**：

| 題目 | 陣列長度 (n) | 回傳類型 | 時間複雜度要求 |
|------|-------------|----------|---------------|
| 3318 (Easy) | ≤ 1,000 | `int[]` | 寬鬆 |
| 3321 (Hard) | ≤ 50,000 | `long[]` | 嚴格 |

#### 3318 的暴力解法

```csharp
// ❌ 在 3321 會超時 (TLE)
public int[] FindXSum(int[] nums, int k, int x)
{
    for (int i = 0; i <= n - k; i++)
    {
        // 每次滑動都重新計算頻率和排序
        var freq = CountFrequency(subarray);  // O(k)
        var sorted = freq.OrderBy(...);        // O(k log k) ← 瓶頸
        result[i] = CalculateSum(sorted, x);   // O(x)
    }
}
```

**效能瓶頸**:

- 每次滑動都執行 **O(k log k)** 的排序
- 總時間複雜度: **O((n-k+1) × k log k)**
- 當 n=50,000, k=25,000 時 ≈ **20億次操作** → 超時

### 本題優化解法: 雙 SortedSet

核心思想是**避免每次都重新排序**，改用動態維護的有序集合。

#### 資料結構設計

```text
┌─────────────────────────────────────────────┐
│              當前滑動視窗                      │
│  [element₁, element₂, ..., elementₖ]        │
└─────────────────────────────────────────────┘
                    ↓
        ┌───────────────────────┐
        │    frequencyMap       │  ← 記錄每個元素的頻率
        │  {num → frequency}    │
        └───────────────────────┘
                    ↓
        ┌───────────────────────┐
        │ 按 (頻率↓, 數值↓) 排序  │
        └───────────────────────┘
                    ↓
    ┌──────────────┴──────────────┐
    ↓                              ↓
┌─────────┐                  ┌─────────┐
│ topSet  │                  │ restSet │
│ (前x個) │                  │ (其他)  │
│         │                  │         │
│ 用於計算 │                  │ 候補元素 │
│ x-sum   │                  │         │
└─────────┘                  └─────────┘
    ↓
  topSum ← 維護 topSet 的加權和
```

#### 演算法流程

##### 1. 初始化第一個視窗

```csharp
for (int i = 0; i < k; i++)
{
    Add(nums[i], ...);  // 建立頻率並維護 topSet/restSet
}
result[0] = topSum;
```

##### 2. 滑動視窗

```csharp
for (int i = 1; i < n - k + 1; i++)
{
    Remove(nums[i-1], ...);    // 移除左邊元素
    Add(nums[i+k-1], ...);     // 加入右邊元素
    result[i] = topSum;
}
```

##### 3. Add() 操作細節

```csharp
private static void Add(int num, ...)
{
    // 步驟1: 如果元素已存在，移除舊的 (freq, value)
    if (oldFreq > 0)
    {
        var oldPair = (oldFreq, num);
        if (topSet.Contains(oldPair))
            topSet.Remove(oldPair);
            topSum -= num * oldFreq;
        else
            restSet.Remove(oldPair);
    }
    
    // 步驟2: 更新頻率 (頻率+1)
    int newFreq = oldFreq + 1;
    frequencyMap[num] = newFreq;
    
    // 步驟3: 將新的 pair 加入 topSet
    topSet.Add((newFreq, num));
    topSum += num * newFreq;
    
    // 步驟4: 平衡兩個集合
    if (topSet.Count > x)
    {
        var smallest = topSet.Max;  // 取最不重要的
        topSet.Remove(smallest);
        topSum -= smallest.value * smallest.freq;
        restSet.Add(smallest);
    }
}
```

##### 4. Remove() 操作細節

```csharp
private static void Remove(int num, ...)
{
    // 步驟1: 移除當前的 (freq, value)
    var oldPair = (oldFreq, num);
    bool wasInTop = topSet.Contains(oldPair);
    
    if (wasInTop)
    {
        topSet.Remove(oldPair);
        topSum -= num * oldFreq;
    }
    else
    {
        restSet.Remove(oldPair);
    }
    
    // 步驟2: 更新頻率 (頻率-1)
    int newFreq = oldFreq - 1;
    if (newFreq == 0)
        frequencyMap.Remove(num);
    else
    {
        frequencyMap[num] = newFreq;
        restSet.Add((newFreq, num));
    }
    
    // 步驟3: 如果從 topSet 移除，從 restSet 補充
    if (wasInTop && restSet.Count > 0)
    {
        var largest = restSet.Min;  // 取最重要的
        restSet.Remove(largest);
        topSet.Add(largest);
        topSum += largest.value * largest.freq;
    }
}
```

#### 自訂比較器

關鍵在於正確的排序邏輯：

```csharp
IComparer<(int freq, int value)> comparer = Comparer<(int freq, int value)>.Create((a, b) =>
{
    // 優先級1: 頻率降序 (頻率高的排前面)
    if (a.freq != b.freq)
        return b.freq.CompareTo(a.freq);
    
    // 優先級2: 數值降序 (數值大的排前面)
    return b.value.CompareTo(a.value);
});
```

> **注意**: `SortedSet.Min` 取得的是比較器下的「最小值」，在降序排列中實際上是最重要的元素。

### 複雜度分析

**時間複雜度**: O((n-k+1) × log k)

- 初始化第一個視窗: O(k log k)
- 每次滑動操作:
  - `Remove()`: O(log k) - SortedSet 的移除操作
  - `Add()`: O(log k) - SortedSet 的插入操作
  - 平衡操作: O(log k)
- 總共 (n-k+1) 次滑動

**空間複雜度**: O(k)

- `frequencyMap`: 最多 k 個不同元素
- `topSet` + `restSet`: 最多 k 個元素

### 效能對比

| 資料規模 | 暴力解法 (3318) | 雙 SortedSet (3321) | 提升倍數 |
|---------|----------------|-------------------|---------|
| n=1,000, k=500 | 250萬次操作 | 4,500次操作 | 556× |
| n=10,000, k=5,000 | 2.5億次操作 | 6萬次操作 | 4,167× |
| n=50,000, k=25,000 | 20億次操作 | 37.5萬次操作 | 5,333× |

## 執行結果

```bash
$ dotnet run

3321. Find X-Sum of All K-Long Subarrays II
========================================

測試案例 1: 基本測試
輸入: nums = [1, 1, 2, 2, 3, 4, 2, 3], k = 6, x = 2
輸出: [6, 10, 12]

測試案例 2: 所有元素相同
輸入: nums = [5, 5, 5, 5, 5], k = 3, x = 1
輸出: [15, 15, 15]

測試案例 3: x 大於相異元素數量
輸入: nums = [1, 2, 3, 4, 5], k = 3, x = 5
輸出: [6, 9, 12]

測試案例 4: 頻率相同時數值決定順序
輸入: nums = [3, 1, 3, 1, 2], k = 5, x = 2
輸出: [8]

測試案例 5: 大數值測試
輸入: nums = [100000, 200000, 100000, 200000, 300000], k = 4, x = 2
輸出: [600000, 700000]

所有測試完成!
```

## 關鍵技巧總結

1. **雙集合分治**: 將元素分為「重要」和「候補」兩組，只維護必要的排序
2. **動態平衡**: 每次操作後自動調整兩個集合，保持 topSet 恰好有 x 個元素
3. **即時計算**: 使用 `topSum` 變數追蹤當前 x-sum，避免重複計算
4. **自訂比較器**: 正確實作「頻率優先，數值次之」的排序邏輯
5. **Tuple 作為 Key**: 使用 `(freq, value)` 作為集合元素，同時包含頻率和數值資訊

## 進階優化方向

- 使用 **Priority Queue (Heap)** 替代 SortedSet，在某些情況下可能更快
- 針對 `x = 1` 的特殊情況優化（只需追蹤最頻繁的一個元素）
- 使用 **Segment Tree** 或 **Fenwick Tree** 處理更複雜的區間查詢

## 相關題目

- [LeetCode 3318: Find X-Sum of All K-Long Subarrays I](https://leetcode.com/problems/find-x-sum-of-all-k-long-subarrays-i/) - 本題的簡化版本
- [LeetCode 295: Find Median from Data Stream](https://leetcode.com/problems/find-median-from-data-stream/) - 類似的雙堆技巧
- [LeetCode 480: Sliding Window Median](https://leetcode.com/problems/sliding-window-median/) - 滑動視窗 + 有序資料結構

## 專案結構

```text
leetcode_3321/
├── leetcode_3321/
│   ├── Program.cs          # 主要實作和測試案例
│   └── leetcode_3321.csproj
├── leetcode_3321.sln
└── README.md               # 本文件
```

## 執行方式

```bash
# 建構專案
dotnet build

# 執行測試
dotnet run
```

## 技術堆疊

- **.NET 8.0** - 目標框架
- **C# 13** - 使用最新語言特性
- **SortedSet** - 核心資料結構
- **Tuple (ValueType)** - 高效的複合鍵

## 授權

本專案為 LeetCode 練習題目的解答實作。
