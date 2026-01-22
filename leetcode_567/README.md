# LeetCode 567 - Permutation in String (字串的排列)

## 📋 題目描述

給定兩個字串 `s1` 與 `s2`，如果 `s2` 包含 `s1` 的某個**排列**則回傳 `true`，否則回傳 `false`。

換句話說，若 `s1` 的任一排列為 `s2` 的子字串則回傳 `true`。

> **排列 (Permutation)**：一個字串的排列是指由該字串所有字元重新排列後組成的另一個字串。例如 `"ab"` 的排列包括 `"ab"` 和 `"ba"`。

### 範例

| 範例 | 輸入 | 輸出 | 說明 |
|------|------|------|------|
| 1 | s1 = "ab", s2 = "eidbaooo" | true | s2 包含 s1 的排列 "ba" |
| 2 | s1 = "ab", s2 = "eidboaoo" | false | s2 不包含 s1 的任何排列 |

### 限制條件

- `1 <= s1.length, s2.length <= 10^4`
- `s1` 和 `s2` 僅包含小寫英文字母

### 參考連結

- [LeetCode 題目連結](https://leetcode.com/problems/permutation-in-string/description/)

---

## 💡 解題思路與核心概念

### 核心觀察

1. **排列的本質**：兩個字串互為排列 ⟺ 兩個字串中每個字元的出現次數完全相同
2. **子字串長度固定**：若 s2 包含 s1 的排列，則該子字串長度必定等於 s1 的長度
3. **滑動視窗應用**：我們需要在 s2 中找一個長度為 `len(s1)` 的子字串，其字元頻率與 s1 相同

### 解題出發點

```
s1 = "ab" (長度 2)
s2 = "eidbaooo"

我們需要檢查 s2 中每個長度為 2 的子字串：
  "ei" -> 不匹配
  "id" -> 不匹配
  "db" -> 不匹配
  "ba" -> ✓ 匹配！("ba" 是 "ab" 的排列)
```

由於暴力解法需要 O(n × m) 時間，我們使用**滑動視窗**技巧將時間複雜度優化至 O(n)。

---

## 🔧 解法一：滑動視窗 + 字元計數匹配法

### 演算法說明

此方法使用「字元種類匹配計數」來判斷視窗是否包含 s1 的排列：

1. **統計 s1 字元頻率**：記錄 s1 中每個字元出現次數，並計算有多少種不同字元
2. **維護滑動視窗**：使用雙指標 `left` 和 `right` 維護視窗
3. **匹配種類計數**：當視窗中某字元的次數恰好等於 s1 中該字元的次數時，匹配種類數 +1
4. **找到排列**：當所有字元種類都匹配且視窗長度等於 s1 長度時，找到排列

### 程式碼

```csharp
public bool CheckInclusion(string s1, string s2)
{
    char[] pattern = s1.ToCharArray();
    char[] text = s2.ToCharArray();

    int pLen = pattern.Length;
    int tLen = text.Length;

    int[] pFreq = new int[26];    // s1 字元頻率
    int[] winFreq = new int[26];  // 視窗字元頻率

    // 步驟 1: 統計 s1 字元頻率
    for (int i = 0; i < pLen; i++)
    {
        pFreq[pattern[i] - 'a']++;
    }

    // 步驟 2: 計算 s1 中有多少種不同字元
    int pCount = 0;
    for (int i = 0; i < 26; i++)
    {
        if (pFreq[i] > 0) pCount++;
    }

    // 步驟 3: 滑動視窗
    int left = 0, right = 0;
    int winCount = 0;  // 已匹配的字元種類數

    while (right < tLen)
    {
        // 擴展右邊界
        if (pFreq[text[right] - 'a'] > 0)
        {
            winFreq[text[right] - 'a']++;
            if (winFreq[text[right] - 'a'] == pFreq[text[right] - 'a'])
            {
                winCount++;
            }
        }
        right++;

        // 當所有字元種類都匹配時，嘗試縮小視窗
        while (winCount == pCount)
        {
            if (right - left == pLen)
            {
                return true;  // 找到排列！
            }

            // 收縮左邊界
            if (pFreq[text[left] - 'a'] > 0)
            {
                winFreq[text[left] - 'a']--;
                if (winFreq[text[left] - 'a'] < pFreq[text[left] - 'a'])
                {
                    winCount--;
                }
            }
            left++;
        }
    }

    return false;
}
```

### 複雜度分析

| 項目 | 複雜度 | 說明 |
|------|--------|------|
| 時間 | O(n) | 每個字元最多被存取兩次 (進入和離開視窗各一次) |
| 空間 | O(1) | 僅使用固定大小 26 的陣列 |

---

## 🔧 解法二：固定長度滑動視窗 + 字元頻率比對法

### 演算法說明

此方法維護一個**固定長度**的滑動視窗，每次移動時比對完整的字元頻率陣列：

1. **長度檢查**：若 s1 長度 > s2 長度，直接回傳 false
2. **初始化**：同時統計 s1 和 s2 前 `len(s1)` 個字元的頻率
3. **首個視窗比對**：若首個視窗頻率與 s1 相同，回傳 true
4. **滑動視窗**：視窗向右移動一位，移除最左字元、加入新字元，然後比對頻率

### 程式碼

```csharp
public bool CheckInclusion2(string s1, string s2)
{
    int length1 = s1.Length, length2 = s2.Length;

    // 步驟 1: 長度檢查
    if (length1 > length2)
    {
        return false;
    }

    int[] counts1 = new int[26];  // s1 字元頻率
    int[] counts2 = new int[26];  // s2 視窗字元頻率

    // 步驟 2: 初始化 - 統計 s1 和 s2 首個視窗的字元頻率
    for (int i = 0; i < length1; i++)
    {
        counts1[s1[i] - 'a']++;
        counts2[s2[i] - 'a']++;
    }

    // 步驟 3: 檢查首個視窗
    if (CheckEqual(counts1, counts2))
    {
        return true;
    }

    // 步驟 4: 滑動視窗
    for (int i = length1; i < length2; i++)
    {
        // 移除最左邊字元
        counts2[s2[i - length1] - 'a']--;
        // 加入新的右邊字元
        counts2[s2[i] - 'a']++;

        if (CheckEqual(counts1, counts2))
        {
            return true;
        }
    }

    return false;
}

// 輔助函式：比對兩個頻率陣列是否相同
public bool CheckEqual(int[] counts1, int[] counts2)
{
    for (int i = 0; i < 26; i++)
    {
        if (counts1[i] != counts2[i])
        {
            return false;
        }
    }
    return true;
}
```

### 複雜度分析

| 項目 | 複雜度 | 說明 |
|------|--------|------|
| 時間 | O(26 × n) = O(n) | 每次滑動需比對 26 個字元頻率 |
| 空間 | O(1) | 僅使用固定大小 26 的陣列 |

---

## 📊 兩種解法比較

| 比較項目 | 解法一 (字元計數匹配) | 解法二 (頻率比對) |
|----------|----------------------|------------------|
| **時間複雜度** | O(n) | O(26n) ≈ O(n) |
| **空間複雜度** | O(1) | O(1) |
| **實際效能** | 較快 (每次操作 O(1)) | 稍慢 (每次比對 O(26)) |
| **程式碼複雜度** | 較複雜 | 較簡單直觀 |
| **適用場景** | 字元集較大時效能更好 | 字元集較小時程式碼更易理解 |

> [!TIP]
> 解法一通過追蹤「已匹配的字元種類數」避免每次完整比對，在理論上更高效。
> 解法二邏輯更直觀，適合初學者理解滑動視窗的核心概念。

---

## 🎯 流程演示

### 範例：s1 = "ab", s2 = "eidbaooo"

#### 解法一演示

```
初始狀態：
  s1 = "ab" → pFreq = {a:1, b:1}, pCount = 2
  s2 = "eidbaooo"
  
步驟 1: right=0, char='e'
  'e' 不在 s1 中，跳過
  視窗: [e], winCount=0
  
步驟 2: right=1, char='i'
  'i' 不在 s1 中，跳過
  視窗: [ei], winCount=0
  
步驟 3: right=2, char='d'
  'd' 不在 s1 中，跳過
  視窗: [eid], winCount=0
  
步驟 4: right=3, char='b'
  'b' 在 s1 中，winFreq[b]=1, 等於 pFreq[b]=1
  winCount=1
  視窗: [eidb], winCount=1
  
步驟 5: right=4, char='a'
  'a' 在 s1 中，winFreq[a]=1, 等於 pFreq[a]=1
  winCount=2 ← 所有字元種類都匹配！
  視窗: [eidba], winCount=2
  
  進入內層迴圈：
    視窗長度=5 ≠ pLen=2，需要縮小視窗
    left=0, char='e' 不在 s1 中，跳過
    left=1
    
    視窗長度=4 ≠ pLen=2，繼續縮小
    left=1, char='i' 不在 s1 中，跳過
    left=2
    
    視窗長度=3 ≠ pLen=2，繼續縮小
    left=2, char='d' 不在 s1 中，跳過
    left=3
    
    視窗長度=2 == pLen=2 ✓
    找到排列！回傳 true
    
結果：視窗 [ba] 是 "ab" 的排列
```

#### 解法二演示

```
初始狀態：
  s1 = "ab" → counts1 = {a:1, b:1}
  s2 = "eidbaooo"
  length1 = 2, length2 = 8

步驟 1: 初始化首個視窗 (索引 0-1)
  視窗: "ei"
  counts2 = {e:1, i:1}
  比對: counts1 ≠ counts2 ✗

步驟 2: 滑動視窗 (i=2)
  移除 s2[0]='e': counts2 = {i:1}
  加入 s2[2]='d': counts2 = {i:1, d:1}
  視窗: "id"
  比對: counts1 ≠ counts2 ✗

步驟 3: 滑動視窗 (i=3)
  移除 s2[1]='i': counts2 = {d:1}
  加入 s2[3]='b': counts2 = {d:1, b:1}
  視窗: "db"
  比對: counts1 ≠ counts2 ✗

步驟 4: 滑動視窗 (i=4)
  移除 s2[2]='d': counts2 = {b:1}
  加入 s2[4]='a': counts2 = {b:1, a:1}
  視窗: "ba"
  比對: counts1 == counts2 ✓
  
結果：找到排列！回傳 true
```

---

## 🏃 執行方式

```bash
cd leetcode_567
dotnet run
```

### 預期輸出

```
測試 1: s1="ab", s2="eidbaooo"
  CheckInclusion:  True
  CheckInclusion2: True
測試 2: s1="ab", s2="eidboaoo"
  CheckInclusion:  False
  CheckInclusion2: False
測試 3: s1="adc", s2="dcda"
  CheckInclusion:  True
  CheckInclusion2: True
測試 4: s1="abcdef", s2="abc"
  CheckInclusion:  False
  CheckInclusion2: False
測試 5: s1="abc", s2="abc"
  CheckInclusion:  True
  CheckInclusion2: True
```

---

## 📁 專案結構

```
leetcode_567/
├── leetcode_567/
│   ├── Program.cs        # 主程式及解法實作
│   └── leetcode_567.csproj
├── leetcode_567.sln
└── README.md
```
