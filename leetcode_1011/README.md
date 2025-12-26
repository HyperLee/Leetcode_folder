# LeetCode 1011. Capacity To Ship Packages Within D Days

[![LeetCode](https://img.shields.io/badge/LeetCode-1011-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/)
[![Language](https://img.shields.io/badge/Language-C%23-blue?style=flat-square&logo=csharp)](https://dotnet.microsoft.com/)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

使用**二分搜尋法 (Binary Search)** 解決「D 天內送達包裹的能力」問題。

## 題目描述

一條輸送帶上有數個包裹，必須在 `days` 天內從一個港口運送到另一個港口。輸送帶上第 `i` 個包裹的重量為 `weights[i]`。

每天我們按照 `weights` 的順序將包裹裝上船，但每天裝載的總重量不得超過船的最大載重能力。

請回傳能使所有包裹在 `days` 天內運完的**最小船載重能力**。

### 範例

**範例 1：**

```text
輸入：weights = [1,2,3,4,5,6,7,8,9,10], days = 5
輸出：15
解釋：船的最小載重能力為 15，可以在 5 天內運完所有包裹：
  第 1 天：1, 2, 3, 4, 5 (總重：15)
  第 2 天：6, 7 (總重：13)
  第 3 天：8 (總重：8)
  第 4 天：9 (總重：9)
  第 5 天：10 (總重：10)
```

**範例 2：**

```text
輸入：weights = [3,2,2,4,1,4], days = 3
輸出：6
解釋：船的最小載重能力為 6，可以在 3 天內運完所有包裹：
  第 1 天：3, 2 (總重：5)
  第 2 天：2, 4 (總重：6)
  第 3 天：1, 4 (總重：5)
```

**範例 3：**

```text
輸入：weights = [1,2,3,1,1], days = 4
輸出：3
解釋：船的最小載重能力為 3，可以在 4 天內運完所有包裹：
  第 1 天：1, 2 (總重：3)
  第 2 天：3 (總重：3)
  第 3 天：1, 1 (總重：2)
  第 4 天：(無包裹，提前完成)
```

### 限制條件

- `1 <= days <= weights.length <= 5 * 10⁴`
- `1 <= weights[i] <= 500`

## 解題思路

### 為什麼使用二分搜尋？

這道題要找的是**最小載重能力**，而載重能力有一個明確的範圍：

| 邊界 | 值 | 說明 |
|------|----|----|
| 最小值 | `max(weights)` | 船至少要能裝下最重的單一包裹 |
| 最大值 | `sum(weights)` | 一天就能運完全部包裹 |

在這個範圍內，存在一個**臨界點**：
- 載重 ≥ 臨界點：可以在 `days` 天內完成
- 載重 < 臨界點：無法在 `days` 天內完成

這種「單調性」正是二分搜尋的適用場景！

### 演算法步驟

```text
┌─────────────────────────────────────────────────────────────────┐
│ 步驟 1：確定搜尋範圍                                              │
│   left = max(weights)     // 最小可能載重                        │
│   right = sum(weights)    // 最大可能載重                        │
├─────────────────────────────────────────────────────────────────┤
│ 步驟 2：二分搜尋                                                  │
│   while (left < right):                                         │
│     mid = left + (right - left) / 2                             │
│     若以 mid 載重可在 days 天內完成:                               │
│       right = mid         // 嘗試更小的載重                       │
│     否則:                                                        │
│       left = mid + 1      // 需要更大的載重                       │
├─────────────────────────────────────────────────────────────────┤
│ 步驟 3：返回結果                                                  │
│   return left             // 最小可行載重                        │
└─────────────────────────────────────────────────────────────────┘
```

### 如何判斷載重是否足夠？

模擬運送過程，計算需要的天數：

```csharp
int need = 1, cur = 0;  // need: 需要天數, cur: 當天載重
for (int i = 0; i < weights.Length; i++)
{
    if (cur + weights[i] > capacity)  // 超過載重限制
    {
        need++;      // 需要新的一天
        cur = 0;     // 重置當天載重
    }
    cur += weights[i];  // 裝載當前包裹
}
// 若 need <= days，則載重足夠
```

## 複雜度分析

| 複雜度 | 值 | 說明 |
| -------- | ---- | ---- |
| 時間複雜度 | O(n × log(sum - max)) | 二分搜尋 log(sum - max) 次，每次遍歷 n 個包裹 |
| 空間複雜度 | O(1) | 只使用常數額外空間 |

## 範例演示流程

以 `weights = [1,2,3,4,5,6,7,8,9,10]`, `days = 5` 為例：

```text
初始化：
  left = 10 (最大包裹重量)
  right = 55 (總重量)

┌─────────┬──────┬──────┬──────┬────────────┬────────────┐
│ 迭代    │ left │ right│ mid  │ 需要天數   │ 操作       │
├─────────┼──────┼──────┼──────┼────────────┼────────────┤
│ 第 1 次 │ 10   │ 55   │ 32   │ 2 ≤ 5 ✓   │ right = 32 │
│ 第 2 次 │ 10   │ 32   │ 21   │ 3 ≤ 5 ✓   │ right = 21 │
│ 第 3 次 │ 10   │ 21   │ 15   │ 5 ≤ 5 ✓   │ right = 15 │
│ 第 4 次 │ 10   │ 15   │ 12   │ 6 > 5 ✗   │ left = 13  │
│ 第 5 次 │ 13   │ 15   │ 14   │ 6 > 5 ✗   │ left = 15  │
│ 結束    │ 15   │ 15   │  -   │     -      │ 返回 15    │
└─────────┴──────┴──────┴──────┴────────────┴────────────┘

驗證 mid = 15 的運送過程：
  第 1 天：1+2+3+4+5 = 15 (剛好等於載重)
  第 2 天：6+7 = 13 (8 會超過 15)
  第 3 天：8
  第 4 天：9
  第 5 天：10
  → 共需 5 天，符合要求！
```

## 執行專案

### 前置需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行測試
dotnet run --project leetcode_1011/leetcode_1011.csproj
```

### 預期輸出

```text
LeetCode 1011. Capacity To Ship Packages Within D Days
==================================================
測試案例 1: weights = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10], days = 5
結果: 15, 預期: 15, ✓ 通過

測試案例 2: weights = [3, 2, 2, 4, 1, 4], days = 3
結果: 6, 預期: 6, ✓ 通過

測試案例 3: weights = [1, 2, 3, 1, 1], days = 4
結果: 3, 預期: 3, ✓ 通過
```

## 相關題目

- [LeetCode 875. Koko Eating Bananas](https://leetcode.com/problems/koko-eating-bananas/) - 類似的二分搜尋應用
- [LeetCode 1482. Minimum Number of Days to Make m Bouquets](https://leetcode.com/problems/minimum-number-of-days-to-make-m-bouquets/)
- [LeetCode 410. Split Array Largest Sum](https://leetcode.com/problems/split-array-largest-sum/)

## 參考資料

- [LeetCode 官方題解](https://leetcode.cn/problems/capacity-to-ship-packages-within-d-days/solution/zai-d-tian-nei-song-da-bao-guo-de-neng-l-ntml/)
- [Binary Search 演算法詳解](https://en.wikipedia.org/wiki/Binary_search_algorithm)
