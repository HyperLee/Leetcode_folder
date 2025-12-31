# LeetCode 1704. 判斷字串的兩半是否相似

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-1704-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/determine-if-string-halves-are-alike/)

## 題目描述

給定一個長度為**偶數**的字串 `s`。將字串分成兩個等長的子字串：`a` 為前半部，`b` 為後半部。

如果兩個字串中**元音字母**（`a`, `e`, `i`, `o`, `u`, `A`, `E`, `I`, `O`, `U`）的數量相同，則稱它們「相似」。

> [!NOTE]
> 字串 `s` 可能包含大小寫字母，元音判斷需同時考慮大小寫。

### 範例

| 輸入         | 輸出    | 說明                                                         |
| ------------ | ------- | ------------------------------------------------------------ |
| `"book"`     | `true`  | `a = "bo"` (1 個元音 o)，`b = "ok"` (1 個元音 o)             |
| `"textbook"` | `false` | `a = "text"` (1 個元音 e)，`b = "book"` (2 個元音 o, o)      |

### 限制條件

- `2 <= s.length <= 1000`
- `s.length` 為偶數
- `s` 僅包含大寫和小寫英文字母

## 解題思路

### 核心概念：計數法

題目的核心在於**統計與比較**：

1. 將字串平均分成前後兩半
2. 分別計算兩半中的元音字母數量
3. 比較兩個計數是否相等

```text
字串: "textbook"
       ├── 前半: "text" ──→ 元音數: 1 (e)
       └── 後半: "book" ──→ 元音數: 2 (o, o)
       
比較: 1 ≠ 2 → 回傳 false
```

### 解法詳細說明

```csharp
public bool HalvesAreAlike(string s)
{
    // Step 1: 分割字串
    string a = s.Substring(0, s.Length / 2);  // 前半
    string b = s.Substring(s.Length / 2);     // 後半
    
    // Step 2: 定義元音集合（包含大小寫）
    string vowels = "aeiouAEIOU";
    
    // Step 3: 計數
    int sum1 = 0, sum2 = 0;
    
    foreach (char c in a)
        if (vowels.Contains(c)) sum1++;
        
    foreach (char c in b)
        if (vowels.Contains(c)) sum2++;
    
    // Step 4: 比較
    return sum1 == sum2;
}
```

### 複雜度分析

| 類型       | 複雜度 | 說明                                         |
| ---------- | ------ | -------------------------------------------- |
| 時間複雜度 | O(n)   | 遍歷整個字串一次，n 為字串長度               |
| 空間複雜度 | O(1)   | 只使用常數額外空間（兩個計數器和元音字串）   |

## 流程演示

以 `"textbook"` 為例：

```text
原始字串: t e x t b o o k
          └─前半─┘ └─後半─┘

Step 1: 分割
┌─────────┬─────────┐
│ a="text"│ b="book"│
└─────────┴─────────┘

Step 2: 計算前半元音數
t → 非元音
e → ✓ 元音，sum1 = 1
x → 非元音
t → 非元音
前半元音總數: 1

Step 3: 計算後半元音數
b → 非元音
o → ✓ 元音，sum2 = 1
o → ✓ 元音，sum2 = 2
k → 非元音
後半元音總數: 2

Step 4: 比較結果
sum1 (1) ≠ sum2 (2)
回傳: false
```

## 執行方式

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1704
```

## 參考連結

- [LeetCode 1704 - Determine if String Halves Are Alike](https://leetcode.com/problems/determine-if-string-halves-are-alike/)
- [力扣 1704 - 判断字符串的两半是否相似](https://leetcode.cn/problems/determine-if-string-halves-are-alike/)
