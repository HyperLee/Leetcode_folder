# LeetCode 1461 - Check If a String Contains All Binary Codes of Size K

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-1461-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/check-if-a-string-contains-all-binary-codes-of-size-k/)

## 題目描述

給定一個二進位字串 `s` 和一個整數 `k`，如果 `s` 包含所有長度為 `k` 的二進位子串，則返回 `true`；否則返回 `false`。

**題目連結：**

- [LeetCode (英文)](https://leetcode.com/problems/check-if-a-string-contains-all-binary-codes-of-size-k/)
- [力扣 (中文)](https://leetcode.cn/problems/check-if-a-string-contains-all-binary-codes-of-size-k/)

### 範例

```text
輸入: s = "00110110", k = 2
輸出: true
說明: 長度為 2 的二進位字串有 "00", "01", "10", "11"，它們都是 s 的子串。

輸入: s = "0110", k = 1
輸出: true
說明: 長度為 1 的二進位字串有 "0" 和 "1"，它們都是 s 的子串。

輸入: s = "0110", k = 2
輸出: false
說明: 長度為 2 的二進位字串有 "00", "01", "10", "11"，但 "00" 不是 s 的子串。
```

### 限制條件

- `1 <= s.length <= 5 * 10^5`
- `s[i]` 為 `'0'` 或 `'1'`
- `1 <= k <= 20`

## 解題概念與出發點

### 問題分析

首先，我們需要理解題目的核心問題：

1. **長度為 k 的二進位字串有多少種？**
   - 每個位置可以是 `0` 或 `1`，共有 `k` 個位置
   - 因此總共有 `2^k` 種不同的二進位字串

2. **如何判斷是否包含所有可能的子串？**
   - 我們需要找出字串 `s` 中所有長度為 `k` 的子串
   - 然後檢查是否恰好有 `2^k` 種不同的子串

### 關鍵觀察

> [!TIP]
> 如果字串 `s` 要包含所有 `2^k` 種長度為 `k` 的二進位字串，那麼 `s` 的長度至少需要 `2^k + k - 1`。

**為什麼？**

想像我們要依序排列這 `2^k` 個不重疊的子串：

- 第一個子串佔用 `k` 個字元
- 之後每增加一個新子串，至少需要 1 個新字元（透過滑動視窗）
- 因此最少需要 `k + (2^k - 1) = 2^k + k - 1` 個字元

## 解法詳解：雜湊表 (Hash Set)

### 演算法思路

1. **提前檢查長度**：如果 `s.Length < 2^k + k - 1`，直接返回 `false`
2. **滑動視窗收集子串**：遍歷字串，取出所有長度為 `k` 的子串
3. **使用 HashSet 去重**：將子串加入 HashSet，自動去除重複
4. **比較數量**：最後判斷 HashSet 的大小是否等於 `2^k`

### 程式碼實作

```csharp
public bool HasAllCodes(string s, int k)
{
    // 提前檢查：字串長度是否足夠
    if (s.Length < (1 << k) + k - 1)
    {
        return false;
    }

    // 使用雜湊集合儲存所有長度為 k 的子串
    HashSet<string> exists = new HashSet<string>();

    // 使用滑動視窗遍歷字串
    for (int i = 0; i + k <= s.Length; ++i)
    {
        exists.Add(s.Substring(i, k));
    }

    // 判斷是否收集到了所有 2^k 種不同的子串
    return exists.Count == (1 << k);
}
```

### 程式碼細節說明

#### 迴圈條件：`i + k <= s.Length`

迴圈條件 `i + k <= s.Length` 是為了**防止索引超出範圍**。

**具體範例說明：**

假設 `s = "0110"`, `k = 2`：
- `s.Length = 4`
- `s.Substring(i, k)` 會從索引 `i` 開始取 `k` 個字元

迴圈執行過程：

```text
i = 0: 取 s.Substring(0, 2) = "01" ✓  (0 + 2 = 2 ≤ 4)
i = 1: 取 s.Substring(1, 2) = "11" ✓  (1 + 2 = 3 ≤ 4)
i = 2: 取 s.Substring(2, 2) = "10" ✓  (2 + 2 = 4 ≤ 4)
i = 3: 停止，因為 3 + 2 = 5 > 4 (會超出字串範圍)
```

**關鍵點：**
- 如果從索引 `i` 開始取 `k` 個字元，最後一個字元的索引是 `i + k - 1`
- 這個索引必須 `< s.Length`，即 `i + k - 1 < s.Length`
- 整理後就是 `i + k ≤ s.Length`

**等價寫法：**

```csharp
// 以下三種寫法邏輯相同
i + k <= s.Length      // 目前使用的寫法
i <= s.Length - k      // 常見的替代寫法
i < s.Length - k + 1   // 另一種表示方式
```

這樣可以確保 `Substring` 方法不會拋出 `ArgumentOutOfRangeException` 例外。

### 複雜度分析

| 複雜度     | 數值       | 說明                                   |
| ---------- | ---------- | -------------------------------------- |
| 時間複雜度 | O(n × k)   | n 為字串長度，每次取子串需要 O(k)      |
| 空間複雜度 | O(2^k × k) | HashSet 最多儲存 2^k 個長度為 k 的字串 |

## 範例演示流程

以 `s = "00110110"`, `k = 2` 為例，詳細演示演算法執行過程：

### 步驟 1：長度檢查

```text
s.Length = 8
所需最小長度 = 2^2 + 2 - 1 = 5
8 >= 5 ✓ 通過檢查
```

### 步驟 2：滑動視窗收集子串

```text
索引 i=0: s[0..1] = "00" → HashSet: {"00"}
索引 i=1: s[1..2] = "01" → HashSet: {"00", "01"}
索引 i=2: s[2..3] = "11" → HashSet: {"00", "01", "11"}
索引 i=3: s[3..4] = "10" → HashSet: {"00", "01", "11", "10"}
索引 i=4: s[4..5] = "01" → HashSet: {"00", "01", "11", "10"} (重複，不變)
索引 i=5: s[5..6] = "11" → HashSet: {"00", "01", "11", "10"} (重複，不變)
索引 i=6: s[6..7] = "10" → HashSet: {"00", "01", "11", "10"} (重複，不變)
```

### 步驟 3：比較數量

```text
HashSet.Count = 4
2^k = 2^2 = 4
4 == 4 ✓ 返回 true
```

### 視覺化流程圖

```text
字串:    0  0  1  1  0  1  1  0
索引:    0  1  2  3  4  5  6  7

滑動視窗 (k=2):
        [0  0]                    → "00" ✓
           [0  1]                 → "01" ✓
              [1  1]              → "11" ✓
                 [1  0]           → "10" ✓
                    [0  1]        → "01" (已存在)
                       [1  1]     → "11" (已存在)
                          [1  0]  → "10" (已存在)

收集到的不同子串: "00", "01", "10", "11"
總數 = 4 = 2^2 ✓
```

## 執行專案

### 前置需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1461/leetcode_1461.csproj
```

### 預期輸出

```text
測試案例 1: s = "00110110", k = 2
結果: True

測試案例 2: s = "0110", k = 1
結果: True

測試案例 3: s = "0110", k = 2
結果: False

測試案例 4: s = "0000000001011100", k = 4
結果: False
```

## 相關資源

- [LeetCode 官方題解](https://leetcode.com/problems/check-if-a-string-contains-all-binary-codes-of-size-k/solutions/)
- [HashSet 文件](https://docs.microsoft.com/dotnet/api/system.collections.generic.hashset-1)
- [位元運算](https://docs.microsoft.com/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators)
