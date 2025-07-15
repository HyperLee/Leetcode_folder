# leetcode_3136 解題說明

## 題目簡介

本專案針對 LeetCode 第 3136 題「有效單詞」（Valid Word）進行解題與程式碼實作。

- 題目連結：[LeetCode 英文版](https://leetcode.com/problems/valid-word/description/?envType=daily-question&envId=2025-07-15)
- 題目連結：[LeetCode 中文版](https://leetcode.cn/problems/valid-word/description/?envType=daily-question&envId=2025-07-15)

### 題目描述

一個單詞被認為是有效的，需同時滿足以下條件：
1. 至少包含 3 個字元。
2. 僅包含數字 (0-9) 與英文字母（大小寫）。
3. 至少包含一個母音字母（a, e, i, o, u 及其大寫）。
4. 至少包含一個子音字母（非母音的英文字母）。

## 專案結構

- `leetcode_3136.sln`：Visual Studio 解決方案檔案。
- `leetcode_3136/leetcode_3136.csproj`：C# 專案檔案。
- `leetcode_3136/Program.cs`：主程式，包含解題邏輯與測試案例。

## 解法說明

### 主邏輯

- 主要解法在 `IsValidWord` 函式中實作。
- 只需一次 for 迴圈遍歷字串，於迴圈中同時判斷所有條件，提升效率。
- 先檢查字串長度與 null，若不符直接回傳 false。
- 迴圈中：
  - 若遇到非英文字母或數字，直接回傳 false。
  - 判斷是否為母音字母（a, e, i, o, u，含大小寫），若是則標記。
  - 若是英文字母且不是母音，則標記為子音。
- 迴圈結束後，若同時包含母音與子音則回傳 true，否則回傳 false。

#### 程式碼片段

```csharp
public static bool IsValidWord(string word)
{
    if (word is null || word.Length < 3)
    {
        return false;
    }
    bool hasVowel = false;
    bool hasConsonant = false;
    foreach (var ch in word)
    {
        if (!char.IsLetterOrDigit(ch))
        {
            return false;
        }
        if (IsVowel(ch))
        {
            hasVowel = true;
        }
        else if (char.IsLetter(ch))
        {
            hasConsonant = true;
        }
    }
    return hasVowel && hasConsonant;
}
```

### 複雜度分析

- **時間複雜度**：O(n)
  - 其中 n 為字串長度。僅需一次 for 迴圈遍歷所有字元。
- **空間複雜度**：O(1)
  - 僅使用常數空間（兩個布林值）。

## 執行方式

1. 使用 .NET 8 或以上版本。
2. 於專案根目錄執行：
   ```pwsh
   dotnet build leetcode_3136/leetcode_3136.csproj
   dotnet run --project leetcode_3136/leetcode_3136.csproj
   ```

## 測試案例

主程式已內建多組測試字串，執行後會輸出每個字串是否有效。

## 其他說明

- 程式碼遵循 C# 13 最新語法與最佳實踐。
- 具備詳細註解，便於理解每個步驟。
- 若需擴充或整合至其他專案，可直接複製 `IsValidWord` 函式。

---

如有任何問題或建議，歡迎提出！
