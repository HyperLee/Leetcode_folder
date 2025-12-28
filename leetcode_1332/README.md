# LeetCode 1332. Remove Palindromic Subsequences

刪除回文子序列

[![LeetCode](https://img.shields.io/badge/LeetCode-1332-orange?style=flat-square)](https://leetcode.com/problems/remove-palindromic-subsequences/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/remove-palindromic-subsequences/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)

## 題目描述

給定一個僅由字母 `'a'` 和 `'b'` 組成的字串 `s`。一次操作可以從 `s` 中刪除一個**回文子序列**。

回傳使給定字串變為空字串所需的**最小操作次數**。

### 名詞解釋

- **子序列 (Subsequence)**：由刪除原字串中部分字元（不需要連續）且保持相對順序所得到的字串。
- **回文字串 (Palindrome)**：正讀與反讀相同的字串。

### 範例

**範例 1:**

```text
輸入: s = "ababa"
輸出: 1
解釋: 字串本身就是回文，所以只需要一次操作就可以刪除整個字串。
```

**範例 2:**

```text
輸入: s = "abb"
輸出: 2
解釋: "abb" -> "bb" -> ""
先刪除回文子序列 "a"，再刪除 "bb"。
```

**範例 3:**

```text
輸入: s = "baabb"
輸出: 2
解釋: "baabb" -> "b" -> ""
先刪除回文子序列 "baab"，再刪除 "b"。
或者：先刪除所有 'a'，再刪除所有 'b'。
```

### 限制條件

- `1 <= s.length <= 1000`
- `s` 僅包含字母 `'a'` 和 `'b'`

---

## 解題概念與思路

### 關鍵觀察

這道題的核心在於理解**子序列**的定義以及字串只包含兩種字元的特性：

1. **子序列不需要連續**：我們可以從字串中挑選任意位置的字元，只要保持相對順序即可。

2. **相同字元組成的子序列必為回文**：
   - 所有的 `'a'` 組成的子序列 `"aaa..."` 必定是回文
   - 所有的 `'b'` 組成的子序列 `"bbb..."` 必定是回文

3. **最多只需要 2 次操作**：因為我們可以先刪除所有的 `'a'`，再刪除所有的 `'b'`。

### 解題邏輯

根據以上觀察，答案只有兩種可能：

| 情況             | 操作次數 | 說明                                  |
| ---------------- | -------- | ------------------------------------- |
| 字串本身是回文   | 1        | 一次刪除整個字串                      |
| 字串本身不是回文 | 2        | 先刪所有 `'a'`，再刪所有 `'b'`        |

> [!TIP]
> 題目保證字串長度至少為 1，所以不需要處理空字串的情況。

---

## 解法詳解

### 方法：直接判斷

只需要判斷字串是否為回文即可決定答案。

```csharp
public int RemovePalindromeSub(string s)
{
    int n = s.Length;

    // 使用雙指標從兩端向中間檢查是否為回文
    for (int i = 0; i < n / 2; i++)
    {
        // 如果對稱位置的字元不相等，則不是回文
        if (s[i] != s[n - 1 - i])
        {
            return 2;  // 非回文需要 2 次
        }
    }

    return 1;  // 是回文只需 1 次
}
```

### 複雜度分析

- **時間複雜度**：$O(n)$，其中 $n$ 為字串長度，只需遍歷一半字串。
- **空間複雜度**：$O(1)$，只使用常數額外空間。

---

## 演示流程

### 範例 1: `s = "ababa"` (回文字串)

```text
步驟 1: 檢查是否為回文
        a b a b a
        ↑       ↑
        i=0   n-1-i=4
        'a' == 'a' ✓

        a b a b a
          ↑   ↑
        i=1 n-1-i=3
        'b' == 'b' ✓

        (i=2 已到達中點，停止檢查)

步驟 2: 字串是回文 → 回傳 1

結果: 只需 1 次操作，刪除整個回文字串 "ababa"
```

### 範例 2: `s = "abb"` (非回文字串)

```text
步驟 1: 檢查是否為回文
        a b b
        ↑   ↑
        i=0 n-1-i=2
        'a' != 'b' ✗

步驟 2: 字串不是回文 → 回傳 2

結果: 需要 2 次操作
  操作 1: 刪除子序列 "a"  → 剩餘 "bb"
  操作 2: 刪除子序列 "bb" → 剩餘 ""
```

### 範例 3: `s = "baabb"` (非回文字串)

```text
步驟 1: 檢查是否為回文
        b a a b b
        ↑       ↑
        i=0   n-1-i=4
        'b' == 'b' ✓

        b a a b b
          ↑   ↑
        i=1 n-1-i=3
        'a' != 'b' ✗

步驟 2: 字串不是回文 → 回傳 2

結果: 需要 2 次操作
  方法 A:
    操作 1: 刪除子序列 "aab" (選取位置 1,2,3) → 剩餘 "bb"
    操作 2: 刪除子序列 "bb" → 剩餘 ""

  方法 B (更直觀):
    操作 1: 刪除所有 'a' → 剩餘 "bbb"
    操作 2: 刪除所有 'b' → 剩餘 ""
```

---

## 執行專案

### 前置需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) 或更高版本

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行專案
dotnet run --project leetcode_1332/leetcode_1332.csproj
```

### 預期輸出

```text
輸入: "ababa"
輸出: 1
預期: 1 (字串本身是回文，一次刪除即可)

輸入: "abb"
輸出: 2
預期: 2 (先刪除所有 'a'，再刪除所有 'b')

輸入: "baabb"
輸出: 2
預期: 2 (非回文字串)

輸入: "a"
輸出: 1
預期: 1 (單一字元必為回文)

輸入: "aaaa"
輸出: 1
預期: 1 (全部相同字元必為回文)
```

---

## 相關連結

- [LeetCode 1332 - Remove Palindromic Subsequences](https://leetcode.com/problems/remove-palindromic-subsequences/)
- [力扣 1332 - 删除回文子序列](https://leetcode.cn/problems/remove-palindromic-subsequences/)
