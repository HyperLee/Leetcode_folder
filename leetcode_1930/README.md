# LeetCode 1930 — Unique Length-3 Palindromic Subsequences

> 長度為 3 的不同回文子序列｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/unique-length-3-palindromic-subsequences/)
- [中文題目](https://leetcode.cn/problems/unique-length-3-palindromic-subsequences/)

## 題目說明

給定一個只含小寫英文字母的字串 `s`，計算其中不同的長度三回文子序列數量。子序列可以
刪除部分字元，但保留下來的字元相對順序不可改變；即使多組索引產生相同的三字元序列，
該回文也只計算一次。

題目限制：

- `3 <= s.length <= 100000`
- `s` 僅由小寫英文字母組成

## 核心不變量

長度三回文一定具有 `x?x` 的形式。固定首尾字元 `x` 後，只需找出 `x` 的第一次與最後
一次出現位置；這兩個位置形成該首尾字元的最寬合法範圍。範圍內每一種不同字元 `y` 都能
形成恰好一種 `xyx`，而範圍外不可能提供更多中心字元。

容易出錯之處：

- 計算的是不同字元序列，不是可選索引組合數；相同的 `xyx` 只能計算一次。
- 首尾之間必須至少有一個位置；只有兩個相鄰的相同字元不能形成長度三回文。
- 不需要為每組索引建構實際回文字串。
- 舊實作先建立 `Substring` 再交給 HashSet，會配置最多接近輸入大小的暫存字串；新版
  直接掃描原字串索引。

## 解法一：HashSet 中心字元去重

公開 API：

```csharp
public static int CountPalindromicSubsequence(string s)
```

逐一枚舉 `'a'` 到 `'z'` 作為首尾，使用 `IndexOf` 與 `LastIndexOf` 取得最寬範圍，再
直接走訪中間索引並加入 `HashSet<char>`。HashSet 的元素數量就是該首尾字元能形成的不同
回文數量。

這個版本保留舊解法的教學結構，容易直接對照「固定首尾、中心去重」的不變量；代價是每個
有效首尾字元都會建立一個小型 HashSet，且雜湊操作具有較高常數成本。

## 解法二：固定 26 格布林陣列

公開 API：

```csharp
public static int CountPalindromicSubsequence2(string s)
```

題目保證只有小寫英文字母，因此可重用單一 `bool[26]`。每次處理新的首尾字元前清空陣列，
以 `s[index] - 'a'` 直接定位中心字元；只有第一次將某格從 `false` 改成 `true` 時才增加
答案。

這個版本避免 HashSet 與重複容器配置，在本題固定字母表契約下有較低常數成本；若字元集合
不再固定，HashSet 版本會更容易一般化。

### 複雜度比較

令 `n` 為字串長度：

| 方法 | 時間 | 輔助空間 | 取捨 |
| --- | --- | --- | --- |
| HashSet | `O(26n) = O(n)` | `O(26) = O(1)` | 教學直觀、容易一般化 |
| `bool[26]` | `O(26n) = O(n)` | `O(26) = O(1)` | 固定字母表下配置與常數成本較低 |

兩個方法的結果空間皆為 `O(1)`，因為只回傳一個整數。

## 逐步範例

以 `s = "aabca"` 為例：

```plaintext
固定首尾 a：第一次 a 在索引 0，最後一次 a 在索引 4。
中間範圍為 "abc"，不同中心字元是 a、b、c。
因此形成 aaa、aba、aca，共 3 種。
其他字元沒有足夠的首尾位置，總答案仍為 3。
```

`s = "aaaa"` 雖然可以用多組索引形成 `aaa`，但中心字元集合只有 `{a}`，因此答案是
`1`，不是索引組合數。

## Acceptance Harness

`Main` 對兩個公開方法執行相同十個確定性案例，每個案例各產生兩項結果檢查，共 20 項；
任何失敗都會把 process exit code 設為 `1`。大型案例只輸出精簡描述，不列出十萬個字元。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `"aabca"` | 3 | 官方範例與三種中心 |
| 2 | `"adc"` | 0 | 官方範例，沒有相同首尾 |
| 3 | `"bbcbaba"` | 4 | 官方範例，多種首尾 |
| 4 | `"aaa"` | 1 | 最小長度且全部相同 |
| 5 | `"aba"` | 1 | 最小長度且中心不同 |
| 6 | `"aaaa"` | 1 | 多組索引仍只計算一種 |
| 7 | `"abca"` | 2 | 同一首尾有兩種中心 |
| 8 | `"abccba"` | 3 | 多個首尾字元各自貢獻 |
| 9 | 十萬個 `a` | 1 | 最大長度、單一答案 |
| 10 | 循環字母表至十萬字元 | 676 | 最大長度與理論最大 `26 × 26` 種 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1930/leetcode_1930/leetcode_1930.csproj --nologo
dotnet run --no-build --project leetcode_1930/leetcode_1930/leetcode_1930.csproj
```

若直接開啟題目根目錄 `leetcode_1930/`，使用：

```bash
dotnet build leetcode_1930/leetcode_1930.csproj --nologo
dotnet run --no-build --project leetcode_1930/leetcode_1930.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: "aabca"
PASS CountPalindromicSubsequence result | Expected: 3 | Actual: 3
PASS CountPalindromicSubsequence2 result | Expected: 3 | Actual: 3

Case: 2 - Official example 2
Input: "adc"
PASS CountPalindromicSubsequence result | Expected: 0 | Actual: 0
PASS CountPalindromicSubsequence2 result | Expected: 0 | Actual: 0

Case: 3 - Official example 3
Input: "bbcbaba"
PASS CountPalindromicSubsequence result | Expected: 4 | Actual: 4
PASS CountPalindromicSubsequence2 result | Expected: 4 | Actual: 4

Case: 4 - Minimum all equal
Input: "aaa"
PASS CountPalindromicSubsequence result | Expected: 1 | Actual: 1
PASS CountPalindromicSubsequence2 result | Expected: 1 | Actual: 1

Case: 5 - Minimum distinct center
Input: "aba"
PASS CountPalindromicSubsequence result | Expected: 1 | Actual: 1
PASS CountPalindromicSubsequence2 result | Expected: 1 | Actual: 1

Case: 6 - Duplicate construction paths
Input: "aaaa"
PASS CountPalindromicSubsequence result | Expected: 1 | Actual: 1
PASS CountPalindromicSubsequence2 result | Expected: 1 | Actual: 1

Case: 7 - Two distinct centers
Input: "abca"
PASS CountPalindromicSubsequence result | Expected: 2 | Actual: 2
PASS CountPalindromicSubsequence2 result | Expected: 2 | Actual: 2

Case: 8 - Multiple boundary characters
Input: "abccba"
PASS CountPalindromicSubsequence result | Expected: 3 | Actual: 3
PASS CountPalindromicSubsequence2 result | Expected: 3 | Actual: 3

Case: 9 - Maximum length all equal
Input: 100000 x 'a'
PASS CountPalindromicSubsequence result | Expected: 1 | Actual: 1
PASS CountPalindromicSubsequence2 result | Expected: 1 | Actual: 1

Case: 10 - Maximum length repeating alphabet
Input: "abcdefghijklmnopqrstuvwxyz" repeated to 100000 characters
PASS CountPalindromicSubsequence result | Expected: 676 | Actual: 676
PASS CountPalindromicSubsequence2 result | Expected: 676 | Actual: 676

Summary: 20/20 checks passed.
```

## 專案結構

```plaintext
leetcode_1930/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1930/
    ├── Program.cs
    └── leetcode_1930.csproj
```
