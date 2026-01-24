# LeetCode 647. Palindromic Substrings

計算字串中所有回文子字串的數量。

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-647-FFA116)](https://leetcode.com/problems/palindromic-substrings/)

## 題目描述

給定一個字串 `s`，回傳其中**回文子字串**的數量。

- **回文**：一個字串若正讀與反讀相同，稱為回文（例如 `"aba"`、`"bb"`）
- **子字串**：字串中連續的字元序列

### 範例

```
輸入: s = "abc"
輸出: 3
說明: 三個回文子字串為 "a", "b", "c"

輸入: s = "aaa"
輸出: 6
說明: 六個回文子字串為 "a", "a", "a", "aa", "aa", "aaa"
```

### 題目連結

- [LeetCode (English)](https://leetcode.com/problems/palindromic-substrings/description/)
- [力扣 (中文)](https://leetcode.cn/problems/palindromic-substrings/description/)

## 解題思路

### 核心概念：中心擴展法 (Expand Around Center)

回文字串具有**對稱性**，這是解題的關鍵突破點。我們可以從「中心」出發，向兩側擴展來判斷是否為回文。

### 為什麼選擇中心擴展法？

1. **直觀易懂**：符合回文「左右對稱」的本質特性
2. **空間效率高**：不需要額外的二維陣列，空間複雜度為 O(1)
3. **實作簡潔**：程式碼結構清晰，容易理解和維護

### 兩種回文中心

回文字串依長度可分為兩類，需要分別處理：

| 類型 | 中心 | 範例 | 擴展起點 |
|------|------|------|----------|
| 奇數長度 | 單一字元 | `"aba"` 中心為 `b` | `(i, i)` |
| 偶數長度 | 兩個相鄰字元 | `"abba"` 中心為 `bb` | `(i, i+1)` |

## 演算法詳解

### 步驟說明

```
對於字串中的每個位置 i：
  1. 以 s[i] 為中心，向兩側擴展，計算奇數長度回文數量
  2. 以 s[i] 和 s[i+1] 為中心，向兩側擴展，計算偶數長度回文數量
  3. 將兩種情況的結果相加
```

### 擴展邏輯

從中心位置 `(start, end)` 開始：

```
while (start >= 0 && end < 長度 && s[start] == s[end]):
    找到一個回文，計數 +1
    start--  // 左移
    end++    // 右移
```

### 複雜度分析

| 複雜度 | 值 | 說明 |
|--------|-----|------|
| 時間複雜度 | O(n²) | 遍歷 n 個中心，每個中心最多擴展 n 次 |
| 空間複雜度 | O(1) | 只使用常數個變數 |

## 範例演示

以字串 `"aaa"` 為例，展示完整執行流程：

### 位置 i = 0

| 擴展類型 | 擴展過程 | 找到的回文 |
|----------|----------|------------|
| 奇數 `(0,0)` | `s[0]='a'` ✓ → `s[-1]` 超界 | `"a"` (1個) |
| 偶數 `(0,1)` | `s[0]='a'==s[1]='a'` ✓ → `s[-1]` 超界 | `"aa"` (1個) |

**小計：2 個**

### 位置 i = 1

| 擴展類型 | 擴展過程 | 找到的回文 |
|----------|----------|------------|
| 奇數 `(1,1)` | `s[1]='a'` ✓ → `s[0]='a'==s[2]='a'` ✓ → `s[-1]` 超界 | `"a"`, `"aaa"` (2個) |
| 偶數 `(1,2)` | `s[1]='a'==s[2]='a'` ✓ → `s[0]` vs `s[3]` 超界 | `"aa"` (1個) |

**小計：3 個**

### 位置 i = 2

| 擴展類型 | 擴展過程 | 找到的回文 |
|----------|----------|------------|
| 奇數 `(2,2)` | `s[2]='a'` ✓ → `s[1]` vs `s[3]` 超界 | `"a"` (1個) |
| 偶數 `(2,3)` | `s[3]` 超界，不執行 | (0個) |

**小計：1 個**

### 總計

```
2 + 3 + 1 = 6 個回文子字串
```

## 快速開始

### 前置需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) 或更高版本

### 執行程式

```bash
# 複製專案
git clone https://github.com/HyperLee/Leetcode_folder.git

# 進入專案目錄
cd Leetcode_folder/leetcode_647

# 建構並執行
dotnet run
```

### 預期輸出

```
Input: "abc" -> Output: 3
Input: "aaa" -> Output: 6
Input: "aba" -> Output: 4
Input: "" -> Output: 0
Input: "abba" -> Output: 6
```

## 專案結構

```
leetcode_647/
├── leetcode_647.sln          # 解決方案檔案
├── README.md                 # 專案說明文件
└── leetcode_647/
    ├── leetcode_647.csproj   # 專案配置檔
    └── Program.cs            # 主程式（包含演算法實作）
```

## 相關題目

- [5. Longest Palindromic Substring](https://leetcode.com/problems/longest-palindromic-substring/) - 最長回文子字串
- [516. Longest Palindromic Subsequence](https://leetcode.com/problems/longest-palindromic-subsequence/) - 最長回文子序列
- [131. Palindrome Partitioning](https://leetcode.com/problems/palindrome-partitioning/) - 回文分割
