# LeetCode 374. Guess Number Higher or Lower

使用 **二分查找 (Binary Search)** 解決猜數字遊戲問題的 C# 實作。

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-374-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/guess-number-higher-or-lower/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-00AF9C?style=flat-square)](https://leetcode.com/problems/guess-number-higher-or-lower/)

## 題目描述

我們正在玩一個猜數字遊戲：我從 `1` 到 `n` 選一個數字（選定不變），你要猜出我選的數字。

每次你猜錯，我會告訴你猜的數字是**太大**還是**太小**。

使用預先定義的 API：

```csharp
int guess(int num)
```

回傳值說明：

| 回傳值 | 意義                                 |
| ------ | ------------------------------------ |
| `-1`   | 你猜的數字比答案大（`num > pick`）   |
| `1`    | 你猜的數字比答案小（`num < pick`）   |
| `0`    | 你猜的數字等於答案（`num == pick`）  |

**目標**：回傳我選的數字 `pick`。

### 範例

```text
輸入：n = 10, pick = 6
輸出：6
```

### 限制條件

- `1 <= n <= 2³¹ - 1`
- `1 <= pick <= n`

## 解題思路

### 核心概念：二分查找

這道題目是經典的**二分查找**應用場景。由於答案在 `[1, n]` 的有序區間內，且我們可以透過 `guess()` API 得知猜測值與答案的大小關係，因此可以每次將搜尋範圍減半。

### 關鍵觀察

設選出的數字為 `pick`，猜測的數字為 `x`：

- 若 `guess(x) ≤ 0`：表示 `x ≥ pick`，答案在 **左半區間**
- 若 `guess(x) > 0`：表示 `x < pick`，答案在 **右半區間**

### 演算法步驟

```text
1. 初始化搜尋區間 [left, right] = [1, n]
2. 當 left < right：
   a. 計算中間值 mid = left + (right - left) / 2
   b. 呼叫 guess(mid)：
      - 若 ≤ 0：right = mid（答案在 [left, mid]）
      - 若 > 0：left = mid + 1（答案在 [mid+1, right]）
3. 回傳 left（此時 left == right）
```

> [!TIP]
> 使用 `left + (right - left) / 2` 而非 `(left + right) / 2` 可以**避免整數溢位**，這在 `n` 接近 `2³¹ - 1` 時特別重要。

## 解法實作

```csharp
public int GuessNumber(int n)
{
    int left = 1, right = n;
    
    while (left < right)
    {
        int mid = left + (right - left) / 2;
        
        if (guess(mid) <= 0)
        {
            right = mid;    // 答案在 [left, mid]
        }
        else
        {
            left = mid + 1; // 答案在 [mid+1, right]
        }
    }
    
    return left;
}
```

## 複雜度分析

| 類型       | 複雜度         | 說明                   |
| ---------- | -------------- | ---------------------- |
| 時間複雜度 | **O(log n)**   | 每次迭代將搜尋範圍減半 |
| 空間複雜度 | **O(1)**       | 只使用常數額外空間     |

## 範例演示

以 `n = 10, pick = 6` 為例，演示二分查找過程：

```text
初始狀態：[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                          ↑
                        pick=6

第 1 次迭代：
├── left=1, right=10
├── mid = 1 + (10-1)/2 = 5
├── guess(5) = 1 (5 < 6，太小)
└── left = 5 + 1 = 6
    搜尋範圍：[6, 7, 8, 9, 10]

第 2 次迭代：
├── left=6, right=10
├── mid = 6 + (10-6)/2 = 8
├── guess(8) = -1 (8 > 6，太大)
└── right = 8
    搜尋範圍：[6, 7, 8]

第 3 次迭代：
├── left=6, right=8
├── mid = 6 + (8-6)/2 = 7
├── guess(7) = -1 (7 > 6，太大)
└── right = 7
    搜尋範圍：[6, 7]

第 4 次迭代：
├── left=6, right=7
├── mid = 6 + (7-6)/2 = 6
├── guess(6) = 0 (6 == 6，正確！)
└── right = 6
    搜尋範圍：[6]

結束：left == right == 6 ✓
```

**總迭代次數**：4 次（⌈log₂(10)⌉ ≈ 4）

## 執行專案

### 前置需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_374/leetcode_374.csproj
```

### 預期輸出

```text
=== LeetCode 374. Guess Number Higher or Lower ===

測試案例 1: n = 10, pick = 6
結果: 6, 預期: 6, 通過: True

測試案例 2: n = 1, pick = 1
結果: 1, 預期: 1, 通過: True

測試案例 3: n = 2, pick = 1
結果: 1, 預期: 1, 通過: True

測試案例 4: n = 100, pick = 73
結果: 73, 預期: 73, 通過: True

測試案例 5: n = 2147483647, pick = 2147483647
結果: 2147483647, 預期: 2147483647, 通過: True

=== 所有測試完成 ===
```

## 相關題目

- [LeetCode 278. First Bad Version](https://leetcode.com/problems/first-bad-version/) - 類似的二分查找應用
- [LeetCode 35. Search Insert Position](https://leetcode.com/problems/search-insert-position/) - 二分查找基礎題
- [LeetCode 704. Binary Search](https://leetcode.com/problems/binary-search/) - 標準二分查找

## 參考資源

- [LeetCode 題目連結（英文）](https://leetcode.com/problems/guess-number-higher-or-lower/)
- [LeetCode 題目連結（中文）](https://leetcode.cn/problems/guess-number-higher-or-lower/)
- [二分查找演算法 - Wikipedia](https://zh.wikipedia.org/wiki/二分搜尋演算法)
