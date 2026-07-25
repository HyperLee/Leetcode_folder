# LeetCode 2000 — Reverse Prefix of Word

> 反轉單字前綴｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/reverse-prefix-of-word/)
- [中文題目](https://leetcode.cn/problems/reverse-prefix-of-word/)

## 題目說明

給定從索引 0 開始的字串 `word` 與字元 `ch`，找出 `ch` 第一次出現的位置，並反轉從
索引 0 到該位置（含）的前綴。若 `word` 不含 `ch`，則原樣回傳 `word`。

題目限制：

- `1 <= word.length <= 250`
- `word` 只包含小寫英文字母。
- `ch` 是小寫英文字母。

## 核心不變量

反轉終點必須是 `ch` **第一次出現**的位置。索引 `0..firstIndex` 內的字元順序完全反轉，
`firstIndex + 1` 之後的後綴則保持原順序。

```plaintext
word = "abcdefd", ch = 'd'
firstIndex = 3
前綴 "abcd" → "dcba"
後綴 "efd" 保持不變
結果 "dcbaefd"
```

容易出錯的地方：

- 使用最後一次出現的位置，會反轉過多字元。
- 把終點視為不包含 `ch`，會產生 off-by-one 錯誤。
- `ch` 不存在時若直接使用索引 `-1`，可能造成錯誤範圍或例外。
- C# 的 `string` 不可變；即使在字元陣列內交換，最後仍需建立結果字串。

## 解法一：反向重組前綴

公開 API：

```csharp
public static string ReversePrefix(string word, char ch)
```

先以 `IndexOf` 找到第一個 `ch`。找到後，從該索引反向走到 0，把字元依序加入
`StringBuilder`，最後接回未反轉的後綴；找不到時直接回傳原字串。

- 時間複雜度：`O(n)`
- 結果空間：`O(n)`
- 輔助空間：`O(n)`

這個版本保留舊解法「反向建立前綴，再接回後綴」的教學結構，能直接看出輸出如何組成。

## 解法二：字元陣列雙指標

公開 API：

```csharp
public static string ReversePrefix2(string word, char ch)
```

先找到第一個 `ch`，再把字串複製成字元陣列。左右指標分別從索引 0 與
`firstIndex` 開始，向內交換直到相遇；`firstIndex` 後方從未被交換，因此自然保留原順序。

- 時間複雜度：`O(n)`
- 結果空間：`O(n)`
- 輔助空間：`O(n)`

這個版本把反轉操作集中在明確的閉區間 `[0, firstIndex]`，適合練習雙指標與索引邊界。

## Acceptance Harness

`Main` 對兩個公開方法執行相同九個確定性案例，每案產生兩項結果檢查，共 18 項；任何失敗
都會把 process exit code 設為 `1`。長度 250 的案例只顯示頭尾各 16 個字元，實際比較仍
使用完整字串。

| # | 輸入摘要 | 預期摘要 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `"abcdefd"`, `'d'` | `"dcbaefd"` | 官方案例；重複字元必取第一次 |
| 2 | `"xyxzxe"`, `'z'` | `"zxyxxe"` | 官方案例；中段切點 |
| 3 | `"abcd"`, `'z'` | `"abcd"` | 官方案例；找不到字元 |
| 4 | `"a"`, `'a'` | `"a"` | 最小有效輸入 |
| 5 | `"leetcode"`, `'l'` | `"leetcode"` | 字元位於首位 |
| 6 | `"abcd"`, `'d'` | `"dcba"` | 字元位於末位，反轉整個字串 |
| 7 | `"abcdef"`, `'c'` | `"cbadef"` | 奇數長度前綴與包含終點 |
| 8 | `"azbyzcz"`, `'z'` | `"zabyzcz"` | 多次出現時只使用第一個索引 |
| 9 | 249 個 `'a'` 加 `'b'`, `'b'` | `'b'` 加 249 個 `'a'` | 長度 250 上限 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2000/leetcode_2000/leetcode_2000.csproj --nologo
dotnet run --no-build --project leetcode_2000/leetcode_2000/leetcode_2000.csproj
```

若直接開啟題目根目錄 `leetcode_2000/`，使用：

```bash
dotnet build leetcode_2000/leetcode_2000.csproj --nologo
dotnet run --no-build --project leetcode_2000/leetcode_2000.csproj
```

以下是 fresh run 的完整輸出：

```text
LeetCode 2000 Acceptance Harness
Case: Official example 1
Input: word = "abcdefd", ch = 'd'
PASS ReversePrefix result | Expected: "dcbaefd" | Actual: "dcbaefd"
PASS ReversePrefix2 result | Expected: "dcbaefd" | Actual: "dcbaefd"

Case: Official example 2
Input: word = "xyxzxe", ch = 'z'
PASS ReversePrefix result | Expected: "zxyxxe" | Actual: "zxyxxe"
PASS ReversePrefix2 result | Expected: "zxyxxe" | Actual: "zxyxxe"

Case: Official example 3
Input: word = "abcd", ch = 'z'
PASS ReversePrefix result | Expected: "abcd" | Actual: "abcd"
PASS ReversePrefix2 result | Expected: "abcd" | Actual: "abcd"

Case: Minimum input
Input: word = "a", ch = 'a'
PASS ReversePrefix result | Expected: "a" | Actual: "a"
PASS ReversePrefix2 result | Expected: "a" | Actual: "a"

Case: Character at first position
Input: word = "leetcode", ch = 'l'
PASS ReversePrefix result | Expected: "leetcode" | Actual: "leetcode"
PASS ReversePrefix2 result | Expected: "leetcode" | Actual: "leetcode"

Case: Character at last position
Input: word = "abcd", ch = 'd'
PASS ReversePrefix result | Expected: "dcba" | Actual: "dcba"
PASS ReversePrefix2 result | Expected: "dcba" | Actual: "dcba"

Case: Odd-length prefix
Input: word = "abcdef", ch = 'c'
PASS ReversePrefix result | Expected: "cbadef" | Actual: "cbadef"
PASS ReversePrefix2 result | Expected: "cbadef" | Actual: "cbadef"

Case: First of repeated characters
Input: word = "azbyzcz", ch = 'z'
PASS ReversePrefix result | Expected: "zabyzcz" | Actual: "zabyzcz"
PASS ReversePrefix2 result | Expected: "zabyzcz" | Actual: "zabyzcz"

Case: Maximum-length input
Input: word = "aaaaaaaaaaaaaaaa...aaaaaaaaaaaaaaab" (length: 250), ch = 'b'
PASS ReversePrefix result | Expected: "baaaaaaaaaaaaaaa...aaaaaaaaaaaaaaaa" (length: 250) | Actual: "baaaaaaaaaaaaaaa...aaaaaaaaaaaaaaaa" (length: 250)
PASS ReversePrefix2 result | Expected: "baaaaaaaaaaaaaaa...aaaaaaaaaaaaaaaa" (length: 250) | Actual: "baaaaaaaaaaaaaaa...aaaaaaaaaaaaaaaa" (length: 250)

Summary: 18/18 checks passed.
```

## 專案結構

```plaintext
leetcode_2000/
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
└── leetcode_2000/
    ├── Program.cs
    └── leetcode_2000.csproj
```
