# LeetCode 1576: Replace All ?'s to Avoid Consecutive Repeating Characters

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-1576-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/replace-all-s-to-avoid-consecutive-repeating-characters/)

## 題目說明

給定一個字串 `s`，字元僅包含小寫英文字母與 `?`，請將所有 `?` 替換為小寫字母，並確保最終字串中**不會出現任何連續重複的字元**。

### 限制條件

- 不得修改原本不是 `?` 的字元
- 已保證原始字串（除了 `?`）中不包含連續重複字元
- 若有多種解，回傳任一個皆可

### 題目連結

- [LeetCode (English)](https://leetcode.com/problems/replace-all-s-to-avoid-consecutive-repeating-characters/)
- [力扣 (中文)](https://leetcode.cn/problems/replace-all-s-to-avoid-consecutive-repeating-characters/)

## 解題概念與出發點

### 核心觀察

這題的關鍵洞察在於：

1. **局部性原則**：每個 `?` 只需要關注其**左鄰居**和**右鄰居**，不需要考慮更遠的字元
2. **三字母足夠**：由於最多只有兩個鄰居，所以只需要 3 個不同的字母（`a`, `b`, `c`）就一定能找到一個不與左右重複的字母

### 為什麼三個字母就夠？

| 鄰居數量 | 可能情況 | 必定存在不重複的字母 |
|----------|----------|----------------------|
| 0 個鄰居 | `?` 在開頭或結尾 | `a`, `b`, `c` 都可以 |
| 1 個鄰居 | 左或右有字元 | 至少有 2 個字母可選 |
| 2 個鄰居 | 左右都有字元 | 至少有 1 個字母可選 |

> [!TIP]
> 這是一個典型的**貪婪演算法 (Greedy Algorithm)** 問題，我們不需要回溯或複雜的搜尋，只要從左到右依序處理每個 `?` 即可。

## 演算法詳解

### 步驟流程

```text
輸入: "j?qg??b"

步驟 1: 將字串轉為字元陣列 (因為 C# 字串不可變)
        ['j', '?', 'q', 'g', '?', '?', 'b']

步驟 2: 從左到右遍歷

  i=0: 'j' 不是 '?' → 跳過
  
  i=1: '?' 需要替換
       左='j', 右='q'
       'a' ≠ 'j' 且 'a' ≠ 'q' ✓ → 選 'a'
       結果: ['j', 'a', 'q', 'g', '?', '?', 'b']
  
  i=2: 'q' 不是 '?' → 跳過
  
  i=3: 'g' 不是 '?' → 跳過
  
  i=4: '?' 需要替換
       左='g', 右='?' (此時右邊還是 '?')
       'a' ≠ 'g' 且 'a' ≠ '?' ✓ → 選 'a'
       結果: ['j', 'a', 'q', 'g', 'a', '?', 'b']
  
  i=5: '?' 需要替換
       左='a', 右='b'
       'a' = 左 ✗
       'b' = 右 ✗
       'c' ≠ 'a' 且 'c' ≠ 'b' ✓ → 選 'c'
       結果: ['j', 'a', 'q', 'g', 'a', 'c', 'b']
  
  i=6: 'b' 不是 '?' → 跳過

輸出: "jaqgacb"
```

### 邊界處理技巧

使用 `(char?)null` 來表示不存在的鄰居：

```csharp
// 字串開頭：沒有左鄰居
var left = i > 0 ? arr[i - 1] : (char?)null;

// 字串結尾：沒有右鄰居  
var right = i < n - 1 ? arr[i + 1] : (char?)null;
```

> [!NOTE]
> `null != 'a'` 永遠為 `true`，這樣就不需要額外處理邊界情況的特殊邏輯。

## 複雜度分析

| 複雜度類型 | 值 | 說明 |
|-----------|-----|------|
| 時間複雜度 | O(n) | 只需遍歷字串一次 |
| 空間複雜度 | O(n) | 字元陣列需要 n 個空間 |

## 執行範例

```bash
cd leetcode_1576
dotnet run
```

### 測試輸出

```plaintext
輸入: "?zs" => 輸出: "azs"
輸入: "ubv?w" => 輸出: "ubvaw"
輸入: "j?qg??b" => 輸出: "jaqgacb"
輸入: "??????????" => 輸出: "ababababab"
輸入: "abcdefg" => 輸出: "abcdefg"
```

## 程式碼重點

```csharp
// 貪婪策略：依序嘗試 'a', 'b', 'c'
if (left != 'a' && right != 'a')
    arr[i] = 'a';
else if (left != 'b' && right != 'b')
    arr[i] = 'b';
else if (left != 'c' && right != 'c')
    arr[i] = 'c';
```

這段程式碼的精妙之處：

- 邏輯簡潔：三個 `if-else` 涵蓋所有情況
- 必定成功：數學上保證至少有一個分支會滿足條件
- 效率最佳：常數時間決定每個 `?` 的替換值

## 相關題目

- [LeetCode 1592: Rearrange Spaces Between Words](https://leetcode.com/problems/rearrange-spaces-between-words/)
- [LeetCode 1694: Reformat Phone Number](https://leetcode.com/problems/reformat-phone-number/)
