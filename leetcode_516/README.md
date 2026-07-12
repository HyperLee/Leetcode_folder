# LeetCode 516：Longest Palindromic Subsequence／最長回文子序列

這是一個以 C# 撰寫的 .NET 10 主控台專案，使用二維區間動態規劃找出字串中
最長回文子序列的長度。`LongestPalindromeSubseq` 只負責計算並回傳結果，`Main`
則執行十項可重複的 acceptance checks。

## 題目連結

- [English: 516. Longest Palindromic Subsequence](https://leetcode.com/problems/longest-palindromic-subsequence/)
- [中文：516. 最长回文子序列](https://leetcode.cn/problems/longest-palindromic-subsequence/)

## 題意與限制

給定字串 `s`，請找出其中最長的回文子序列，並回傳該子序列的長度。子序列可以
刪除部分字元，但不能改變剩餘字元的相對順序；因此它不要求像「回文子字串」一樣
連續出現。

本題有效輸入限制為：

- `1 <= s.length <= 1000`
- `s` 只包含小寫英文字母

實作遵循題目有效輸入契約，不額外定義 null、空字串或其他無效輸入的行為。

## 二維區間動態規劃

令 `dp[i, j]` 表示閉區間 `s[i..j]` 中最長回文子序列的長度。單一字元本身就是
長度 1 的回文，因此先設定：

```plaintext
dp[i, i] = 1
```

對於長度至少為 2 的區間：

- 若 `s[i] == s[j]`，兩端可以一起納入：`dp[i, j] = dp[i + 1, j - 1] + 2`。
- 若兩端不同，最優解必須捨棄左端或右端其中之一：
  `dp[i, j] = max(dp[i + 1, j], dp[i, j - 1])`。

因為目前區間依賴更短的區間，`i` 必須由右往左走，`j` 必須由左往右走。當兩端
相鄰時，`dp[i + 1, j - 1]` 對應空區間，二維陣列的預設值 0 會讓結果正確得到 2。

### 走查：`s = "abcda"`

```plaintext
外層字元 a 與 a 相同，可納入兩端。
中間區間 "bcd" 的最佳結果為 1，因此整體結果至少為 3。
可能的最長回文子序列是 "aca"，所以答案為 3。
```

## 複雜度

- 時間複雜度：`O(n²)`，每個 `(i, j)` 區間只計算一次。
- 輔助空間：`O(n²)`，二維表格保存所有區間結果。
- 公開 API 不修改輸入字串，也不輸出主控台內容。

## 公開 API

```csharp
public static int LongestPalindromeSubseq(string s)
```

## Acceptance Harness 案例

| # | 案例 | 預期 | 驗證重點 |
|---:|---|---:|---|
| 1 | `bbbab` | 4 | 官方範例與重複字元 |
| 2 | `cbbd` | 2 | 官方範例與偶數回文 |
| 3 | `a` | 1 | 最小有效輸入 |
| 4 | `aba` | 3 | 奇數長度完整回文 |
| 5 | `abcd` | 1 | 沒有重複字元 |
| 6 | `aaaaa` | 5 | 所有字元相同 |
| 7 | `agbdba` | 5 | 非連續回文子序列 |
| 8 | `abcda` | 3 | 兩端配對與中間區間 |
| 9 | `racecar` | 7 | 完整回文 |
| 10 | 1000 個 `a` | 1000 | 題目長度上限 spot check |

## 建置與執行

請從 README 所在的題目根目錄執行：

```bash
dotnet build leetcode_516/leetcode_516.csproj --nologo
dotnet run --no-build --project leetcode_516/leetcode_516.csproj
```

以下是 fresh `dotnet run --no-build` 的完整輸出；README 中只有這一個 `text` fence：

```text
LeetCode 516 acceptance harness

Case 1: Official example 1
Input: s = "bbbab"
Expected: 4
Actual: 4
Result: PASS

Case 2: Official example 2
Input: s = "cbbd"
Expected: 2
Actual: 2
Result: PASS

Case 3: Single character
Input: s = "a"
Expected: 1
Actual: 1
Result: PASS

Case 4: Odd-length palindrome
Input: s = "aba"
Expected: 3
Actual: 3
Result: PASS

Case 5: No repeated characters
Input: s = "abcd"
Expected: 1
Actual: 1
Result: PASS

Case 6: All characters equal
Input: s = "aaaaa"
Expected: 5
Actual: 5
Result: PASS

Case 7: Non-contiguous palindrome
Input: s = "agbdba"
Expected: 5
Actual: 5
Result: PASS

Case 8: Regression case
Input: s = "abcda"
Expected: 3
Actual: 3
Result: PASS

Case 9: Complete palindrome
Input: s = "racecar"
Expected: 7
Actual: 7
Result: PASS

Case 10: Maximum length
Input: s = "a" repeated 1000 times
Expected: 1000
Actual: 1000
Result: PASS

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/2026-07-12-leetcode-516-net10-migration.md
│       └── specs/2026-07-12-leetcode-516-net10-migration-design.md
├── leetcode_516/
│   ├── Program.cs
│   └── leetcode_516.csproj
└── README.md
```
