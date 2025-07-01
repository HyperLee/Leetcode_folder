# leetcode_3330

## 題目說明

3330. 找到初始輸入字串 I ([LeetCode 連結](https://leetcode.com/problems/find-the-original-typed-string-i/description/?envType=daily-question&envId=2025-07-01))

Alice 嘗試在電腦上輸入一個特定字串，但她有時會因為手殘，某個按鍵按太久，導致某個字元被輸入多次。雖然 Alice 很專心，但她知道自己最多只會出現一次這種情況。給定一個字串 word，代表 Alice 螢幕上最終顯示的內容。請回傳 Alice 可能原本想輸入的原始字串總數。

## 專案結構

- `Program.cs`：主程式與解題邏輯，包含 for 迴圈解法。
- `.vscode/launch.json`、`.vscode/tasks.json`：VS Code 偵錯與建構設定。
- `leetcode_3330.csproj`：C# 專案檔。

## 執行方式

1. 於專案根目錄執行：
   ```sh
   dotnet build
   dotnet run --project leetcode_3330/leetcode_3330.csproj
   ```
2. 或於 VS Code 直接按 F5 啟動偵錯。

## 範例輸出

```
word: aabb, 可能原始字串總數: 3
word: abc, 可能原始字串總數: 1
word: aabbaa, 可能原始字串總數: 3
word: a, 可能原始字串總數: 1
word: aa, 可能原始字串總數: 2
word: abcc, 可能原始字串總數: 2
```

## 解題說明

### 解題思路

本題要找出 Alice 可能原本想輸入的所有原始字串數量。由於 Alice 最多只會有一次「某個字元連續重複」的情況，因此：
- 每一段連續重複的字元（例如 "aa"、"bbb"）都代表 Alice 可能多按了一次該字元。
- 對於每一段長度為 n 的連續重複字元，可以產生 n-1 種「移除其中一個重複字元」的原始字串。
- 最後還要加上原字串本身（即 Alice 沒有手殘的情況）。

### 實作方式

1. **for 迴圈法**
   - 用 for 迴圈遍歷字串，遇到連續重複字元時累加計數。
   - 每遇到一個連續重複區段，計算該區段可產生的原始字串數量。
   - 時間複雜度：O(n)，空間複雜度：O(1)。

### 範例說明

以 word = "aabb" 為例：
- "aa" 可移除一個 a，得到 "abb"
- "bb" 可移除一個 b，得到 "aab"
- 原字串本身 "aabb" 也算
- 總共 3 種

### 複雜度分析
- for 迴圈法：
  - 時間複雜度 O(n)：只需遍歷一次字串。
  - 空間複雜度 O(1)：只用常數變數。

---
