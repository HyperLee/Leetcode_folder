# LeetCode 028 - Find the Index of the First Occurrence in a String

以 C# 實作 LeetCode 28，示範如何在 `haystack` 中找出 `needle` 第一次出現的位置。本專案目前提供兩種解法：

- 暴力法
- KMP（Knuth-Morris-Pratt）

主程式會直接執行固定測資，列出兩種解法的結果，方便比對輸出與理解解題流程。

## 題目說明

給定兩個字串 `haystack` 和 `needle`，回傳 `needle` 在 `haystack` 中第一次出現的索引；如果不存在，則回傳 `-1`。

題目連結：

- LeetCode: <https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/description/>
- LeetCode 中文站: <https://leetcode.cn/problems/find-the-index-of-the-first-occurrence-in-a-string/description/>

## 限制條件

- `1 <= haystack.length, needle.length <= 10^4`
- `haystack` 和 `needle` 只包含小寫英文字母

## 解題概念與出發點

最直接的想法，是把 `needle` 放到 `haystack` 的每一個可能起點上逐字比較；這樣容易寫，也容易驗證正確性，但一旦比對失敗，就會重複檢查很多已經看過的字元。

進一步觀察可發現，當模式字串前半段已經匹配成功時，若在中途失敗，並不一定要整段重來。KMP 正是利用這個性質，先對 `needle` 預處理出 LPS（Longest Prefix Suffix）陣列，讓指標可以跳到下一個仍然可能成立的位置，將時間複雜度降為線性。

## 解法一：暴力法

### 設計說明

- 從 `haystack` 的每個可能起點開始檢查
- 逐字比較對應位置是否和 `needle` 相同
- 一旦遇到不相同的字元，立即中止目前起點的比對，換下一個起點
- 第一個完整匹配的位置就是答案；全部檢查完仍未匹配則回傳 `-1`

### 複雜度

- 時間複雜度：`O(n * m)`
- 空間複雜度：`O(1)`

### 範例演示流程

以 `haystack = "sadbutsad"`、`needle = "sad"` 為例：

1. 從索引 `0` 開始，比較 `haystack[0..2]` 與 `"sad"`。
2. `s == s`、`a == a`、`d == d`，完整匹配成功。
3. 因為第一個起點就成功，所以直接回傳 `0`，不需要再檢查索引 `1` 之後的位置。

## 解法二：KMP

### 設計說明

- 先為 `needle` 建立 LPS 陣列，記錄每個位置結尾時「最長共同前綴與後綴長度」
- 使用兩個指標同步走訪 `haystack` 與 `needle`
- 若字元相同，兩個指標一起前進
- 若字元不同且 `needle` 指標不在起點，依照 LPS 回退 `needle` 指標，不重複比較已知可重用的部分
- 若 `needle` 指標走完整段字串，表示找到第一個完整匹配

### 複雜度

- 時間複雜度：`O(n + m)`
- 空間複雜度：`O(m)`

### 範例演示流程

以 `haystack = "mississippi"`、`needle = "issip"` 為例：

1. 先為 `needle = "issip"` 建立 LPS 陣列，結果為 `[0, 0, 0, 1, 0]`。
2. 從 `haystack[0] = 'm'` 開始比較，和 `needle[0] = 'i'` 不同，因此 `haystack` 指標前進。
3. 到 `haystack[1] = 'i'` 後，依序匹配 `i`、`s`、`s`、`i`、`p`。
4. 當 `needle` 指標走到結尾時，代表完整匹配成功。
5. 回傳起始索引 `6 - 5 + 1 = 4`，也就是 `"issip"` 第一次出現在 `haystack` 的位置。

## 目前實作

程式集中在 [leetcode_028/Program.cs](leetcode_028/Program.cs)，包含：

- `StrStrBruteForce`：暴力法
- `StrStrKmp`：KMP 解法
- `BuildLps`：建立 KMP 所需的 LPS 陣列
- `RunDemoCases`：執行固定測資並輸出比對結果

## 建置與執行

```bash
dotnet build leetcode_028/leetcode_028.csproj
dotnet run --project leetcode_028/leetcode_028.csproj
```

## 範例輸出

```text
LeetCode 28 - Find the Index of the First Occurrence in a String

Case 1: haystack = "sadbutsad", needle = "sad", expected = 0
Brute force: 0 (PASS)
KMP: 0 (PASS)

Case 2: haystack = "leetcode", needle = "leeto", expected = -1
Brute force: -1 (PASS)
KMP: -1 (PASS)

Case 3: haystack = "aaaaa", needle = "bba", expected = -1
Brute force: -1 (PASS)
KMP: -1 (PASS)

Case 4: haystack = "mississippi", needle = "issip", expected = 4
Brute force: 4 (PASS)
KMP: 4 (PASS)
```

## 驗證方式

本 repository 目前沒有獨立的測試專案，驗證方式如下：

- 先執行 `dotnet build` 確認可以成功編譯
- 再執行 `dotnet run` 確認固定測資輸出與預期答案一致
- 最後執行 `git diff --check` 確認沒有多餘空白或換行問題
