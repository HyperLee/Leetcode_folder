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

### 方法二的出發點：從字串到整數

> [!TIP]
> 方法一使用字串作為 HashSet 的鍵，每次取子串都需要 O(k) 的時間和空間開銷。
> 方法二改用**位元運算**，將子串直接轉換為整數，消除字串操作的開銷。

**核心想法：**

- 長度為 `k` 的二進位字串可以直接對應一個 `k` 位元的整數（範圍 `0` 到 `2^k - 1`）
- 透過滑動視窗 + 位元移位，每次 O(1) 就能計算出新視窗對應的整數值
- 用布林陣列（而非 HashSet）記錄是否出現，索引存取 O(1) 且無雜湊碰撞開銷

**位元移位滑動公式：**

$$x_{\text{new}} = (x_{\text{old}} \ll 1 \mathbin{\&} \text{Mask}) \mathbin{|} (c \mathbin{\&} 1)$$

其中 $\text{Mask} = 2^k - 1$（二進位為 k 個連續的 1），確保只保留最低 k 個位元。

**小優化：** 一旦 `count` 達到 `2^k`，立即跳出迴圈，无需遍歷剩餘字元。

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

## 解法詳解：位元運算滑動視窗 (Bit Manipulation)

### 演算法思路

1. **建立位元遮罩**：`Mask = (1 << k) - 1`，二進位為 `k` 個連續的 `1`
2. **建立布林陣列**：`bool[] has = new bool[1 << k]`，以整數值為索引
3. **滑動視窗轉整數**：逐字元用位元運算更新整數 `x`
4. **暖身階段**：當 `i >= k-1` 時視窗才湊滿 k 個字元，開始記錄
5. **提前終止**：`count` 達到 `2^k` 時立即跳出迴圈

### 核心位元運算解析

#### `x = (x << 1 & Mask) | (c & 1)`

以 `k = 3`，視窗從 `"011"` → `"110"` 為例：

```text
目前 x = 011 (整數 3)
新字元 c = '0'

步驟 1: x << 1     = 0110  (左移一位)
步驟 2: 0110 & 111 = 110   (Mask = 111，保留低 3 位，進位被截斷)
步驟 3: c & 1      = 0     ('0' & 1 = 0)
步驟 4: 110 | 0    = 110   (整數 6)

結果：新視窗 "110" 對應整數 6 ✓
```

#### 為何 `c & 1` 能取出位元值？

| 字元 | ASCII | 二進位   | `& 1` |
|------|-------|----------|-------|
| `'0'` | 48    | 00110000 | 0     |
| `'1'` | 49    | 00110001 | 1     |

ASCII `'0'` 和 `'1'` 的最低位元恰好對應其數値，因此 `c & 1` 即可直接取出位元。

### 程式碼實作

```csharp
public bool HasAllCodes2(string s, int k)
{
    int Mask = (1 << k) - 1;        // 2^k - 1，位元遮罩
    bool[] has = new bool[1 << k];  // 布林陣列取代 HashSet
    int count = 0;                  // 已收集不同子串數
    int x = 0;                      // 目前視窗的整數值

    for (int i = 0; i < s.Length && count < (1 << k); i++)
    {
        char c = s[i];
        x = (x << 1 & Mask) | (c & 1); // 滑動更新視窗整數值
        if (i >= k - 1 && !has[x])     // 視窗湊滿 k 個字元後才記錄
        {
            has[x] = true;
            count++;
        }
    }

    return count == (1 << k);
}
```

### 複雜度分析（方法二）

| 複雜度     | 數値     | 說明                                         |
| ---------- | -------- | -------------------------------------------- |
| 時間複雜度 | O(n)     | 每個字元只處理一次，位元運算為 O(1)          |
| 空間複雜度 | O(2^k)   | 布林陣列大小為 2^k，無需儲存字串             |

> **與方法一的比較：**
> - 時間：O(n × k) → **O(n)**（消除字串複製開銷）
> - 空間：O(2^k × k) → **O(2^k)**（無需儲存字串，只存整數）

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

## 方法二範例演示流程

以 `s = "00110110"`, `k = 2` 為例，詳細演示位元運算滑動視窗過程：

### 前置準備

```text
k = 2
Mask = (1 << 2) - 1 = 0b11 = 3
has  = [false, false, false, false]  (索引 0~3 對應 "00", "01", "10", "11")
count = 0
x = 0
```

### 逐步執行

```text
i=0: c='0'，x = (0<<1 & 3) | ('0'&1) = 0 | 0 = 0
     i < k-1 (0 < 1)，視窗未湊，不記錄

i=1: c='0'，x = (0<<1 & 3) | ('0'&1) = 0 | 0 = 0
     i >= k-1 (1 >= 1)，has[0]=false → has[0]=true，count=1
     視窗 "00" → 整數 0 ✓

i=2: c='1'，x = (0<<1 & 3) | ('1'&1) = 0 | 1 = 1
     i >= k-1，has[1]=false → has[1]=true，count=2
     視窗 "01" → 整數 1 ✓

i=3: c='1'，x = (1<<1 & 3) | ('1'&1) = (2 & 3) | 1 = 2 | 1 = 3
     i >= k-1，has[3]=false → has[3]=true，count=3
     視窗 "11" → 整數 3 ✓

i=4: c='0'，x = (3<<1 & 3) | ('0'&1) = (6 & 3) | 0 = 2 | 0 = 2
     i >= k-1，has[2]=false → has[2]=true，count=4
     視窗 "10" → 整數 2 ✓
     count = 4 = 2^2，迴圈條件 count < (1<<k) 為 false，提前結束！
```

### 結果

```text
count = 4 = 2^2
返回 true ✓（且在 i=4 時提前終止，無需遍歷至 i=7）
```

### 整數與二進位子串的對應關係

```text
整數 0 → "00"
整數 1 → "01"
整數 2 → "10"
整數 3 → "11"
```

### 視覺化位元滑動過程

```text
字串:         0   0   1   1   0   1   1   0
索引:         0   1   2   3   4   5   6   7

滑動視窗 (k=2):
            [ 0   0 ]                        → x=0  ("00") ✓
                [ 0   1 ]                    → x=1  ("01") ✓
                    [ 1   1 ]                → x=3  ("11") ✓
                        [ 1   0 ]            → x=2  ("10") ✓ 提前結束

所有 2^2=4 種已收集完同，返回 true ✓
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
結果 (方法一): True
結果 (方法二): True

測試案例 2: s = "0110", k = 1
結果 (方法一): True
結果 (方法二): True

測試案例 3: s = "0110", k = 2
結果 (方法一): False
結果 (方法二): False

測試案例 4: s = "0000000001011100", k = 4
結果 (方法一): False
結果 (方法二): False
```

## 相關資源

- [LeetCode 官方題解](https://leetcode.com/problems/check-if-a-string-contains-all-binary-codes-of-size-k/solutions/)
- [HashSet 文件](https://docs.microsoft.com/dotnet/api/system.collections.generic.hashset-1)
- [位元運算](https://docs.microsoft.com/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators)
