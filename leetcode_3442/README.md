# Leetcode 3442. Maximum Difference Between Even and Odd Frequency I

## 題目描述

給定一個只包含小寫英文字母的字串 `s`。
請找出滿足以下條件的最大差值 `diff = freq(a1) - freq(a2)`：

- `a1` 在字串中出現的頻次為奇數。
- `a2` 在字串中出現的頻次為偶數。
  返回這個最大差值。

[Leetcode 原題連結 (英文)](https://leetcode.com/problems/maximum-difference-between-even-and-odd-frequency-i/description/?envType=daily-question\&envId=2025-06-10)
[Leetcode 中文題目連結](https://leetcode.cn/problems/maximum-difference-between-even-and-odd-frequency-i/description/?envType=daily-question\&envId=2025-06-10)

---

## 解題思路

### 解法一 (陣列)

- 使用長度為 26 的陣列記錄每個字母的出現次數。
- 遍歷字串統計頻次，分別找出奇數頻次的最大值與偶數頻次的最小值。
- 回傳兩者的差值。
- 時間複雜度：O (n)，空間複雜度：O (1)。

### 解法二 (Dictionary)

- 使用 Dictionary 記錄每個字母的出現次數，適合字母範圍不固定時。
- 遍歷字串統計頻次，分別找出奇數頻次的最大值與偶數頻次的最小值。
- 回傳兩者的差值。
- 時間複雜度：O (n)，空間複雜度：O (1)(若字母種類有限)，否則 O (k)。

---

## 程式碼片段

```csharp
// 解法一
public int MaxDifference(string s)
{
    // 建立長度為 26 的陣列，記錄每個字母的出現次數
    int[] freq = new int[26];
    foreach (char c in s)
    {
        freq[c - 'a']++; // 統計每個字母的頻次
    }

    int oddMax = 1, evenMax = int.MaxValue;
    foreach (int count in freq)
    {
        if (count % 2 == 0)
        {
            // 如果頻次是偶數，更新偶數最小值
            evenMax = Math.Min(evenMax, count);
        }
        else
        {
            // 如果頻次是奇數，更新奇數最大值
            oddMax = Math.Max(oddMax, count);
        }
    }

    // 回傳奇數最大值與偶數最小值的差
    return oddMax - evenMax;
}

// 解法二
public int MaxDifference2(string s)
{
    // 使用 Dictionary 記錄每個字母的出現次數
    Dictionary<int, int> freq = new Dictionary<int, int>();
    foreach (char c in s)
    {
        int index = c - 'a';
        if (freq.ContainsKey(index))
        {
            freq[index]++; // 已存在則累加
        }
        else
        {
            freq[index] = 1; // 首次出現設為 1
        }
    }

    int oddMax = 1, evenMax = int.MaxValue;
    foreach (var kvp in freq)
    {
        if (kvp.Value % 2 == 0)
        {
            // 偶數頻次更新偶數最小值
            evenMax = Math.Min(evenMax, kvp.Value);
        }
        else
        {
            // 奇數頻次更新奇數最大值
            oddMax = Math.Max(oddMax, kvp.Value);
        }
    }

    // 回傳奇數最大值與偶數最小值的差
    return oddMax - evenMax;
}
```

---

## 解法比較

| 解法         | 時間複雜度 | 空間複雜度     | 可讀性       | 可維護性     | 適用情境           |
| ---------- | ----- | --------- | --------- | -------- | -------------- |
| 陣列         | O(n)  | O(1)      | 高 (程式碼簡潔) | 高 (結構單純) | 字母範圍固定 (如 a-z) |
| Dictionary | O(n)  | O(1)/O(k) | 中 (程式碼較長) | 高 (易於擴充) | 字母範圍不固定或需擴充    |

- 陣列方式效能最佳，適合字母種類固定時，程式碼簡潔易懂。
- Dictionary 方式可擴充性高，適合處理字母範圍不固定或需支援更多字元的情境。
- 兩者可讀性與可維護性皆佳，Dictionary 在需擴充時更具彈性。

---
