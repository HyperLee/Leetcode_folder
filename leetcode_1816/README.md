# 1816. Truncate Sentence／截斷句子

給定一個由單一空格分隔單字、沒有前導或尾隨空格的句子 `s`，以及有效整數 `k`，
回傳只包含前 `k` 個單字的句子。本專案使用單次掃描空白邊界的純函式解法。

- [LeetCode English](https://leetcode.com/problems/truncate-sentence/)
- [LeetCode 中文](https://leetcode.cn/problems/truncate-sentence/)

## 題目說明與限制條件

句子中的每個單字只由大小寫英文字母組成。截斷結果必須保留原本前 `k` 個單字的內容、
大小寫與單一空格，不能包含第 `k` 個單字後面的分隔空格。

- `1 <= s.length <= 500`
- `1 <= k <= s` 中的單字數量
- `s` 只包含大小寫英文字母與空格
- 相鄰單字之間恰有一個空格
- `s` 沒有前導或尾隨空格
- `TruncateSentence` 只處理題目保證的有效輸入，不加入額外的無效輸入行為

## 解法：單次掃描單字邊界

從左至右掃描 `s` 並計數遇到的空格。第 `k` 個空格正好位於第 `k` 個單字之後，因此
在該索引建立不含空格的前綴切片即可。如果掃描結束仍未遇到第 `k` 個空格，依題目保證
可知句子恰好只有 `k` 個單字，直接回傳原字串。

核心不變量：掃描到索引 `i` 以前，`spacesSeen` 等於已完整跨過的單字邊界數；只有
`spacesSeen == k` 時，`s[0..i]` 才恰好包含前 `k` 個單字。

舊程式先以 `s.Length`（字元數）和 `k` 比較，再用該結果決定要組接幾個單字。題目
限制使字元數必定不少於單字數，因此有效輸入下仍會得到 `k`，但這個判斷混淆字元數與
單字數且會配置完整單字陣列。本次改為直接表達切點不變量的掃描。

容易出錯之處：

- 在第 `k - 1` 個空格切片，會少保留一個單字。
- 把第 `k` 個空格包含進結果，會留下尾隨空格。
- 句子恰有 `k` 個單字時沒有第 `k` 個空格，不能把「找不到切點」視為錯誤。
- 不需要 `Split` 整句；只要找到截斷邊界即可。

## 複雜度

| 方法 | 時間 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| `TruncateSentence`（空白邊界掃描） | `O(n)` | `O(m)` | `O(1)` |

`n` 是輸入句子長度，`m` 是回傳字串長度。.NET 的字串切片會建立結果字串；除此之外
只維護索引與空格計數。如果 `k` 等於總單字數，方法直接回傳原字串。

## 逐步走查

以 `s = "Hello how are you Contestant"`、`k = 4` 為例：

| 遇到的空格 | 空格後已完成的單字 | 動作 |
| ---: | --- | --- |
| 1 | `Hello` | 繼續掃描 |
| 2 | `Hello how` | 繼續掃描 |
| 3 | `Hello how are` | 繼續掃描 |
| 4 | `Hello how are you` | 在此空格前切片並回傳 |

結果為 `"Hello how are you"`，沒有尾隨空格。

## Acceptance Harness

`Main` 執行八個確定性案例。每案都印出 Case、Input、Expected、Actual 與 PASS/FAIL；
任何案例失敗會將 process exit code 設為 1。

| # | 輸入 | `k` | 預期 | 驗證目的 |
| ---: | --- | ---: | --- | --- |
| 1 | `Hello how are you Contestant` | 4 | `Hello how are you` | 官方範例 1、舊程式範例 |
| 2 | `What is the solution to this problem` | 4 | `What is the solution` | 官方範例 2 |
| 3 | `chopper is not a tanuki` | 5 | 原句 | 官方範例 3、保留全部單字 |
| 4 | `a` | 1 | `a` | 最小有效輸入 |
| 5 | `Hello World` | 1 | `Hello` | 第一個單字邊界 |
| 6 | `a B c D` | 3 | `a B c` | 大小寫保持不變 |
| 7 | `one two three four` | 3 | `one two three` | 第 `k` 個空格 off-by-one |
| 8 | 500 字元、250 個單字 | 1 | `aa` | 最大句長 spot check |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1816/leetcode_1816/leetcode_1816.csproj --nologo
dotnet run --no-build --project leetcode_1816/leetcode_1816/leetcode_1816.csproj
```

若直接開啟題目根目錄 `leetcode_1816/`，使用：

```bash
dotnet build leetcode_1816/leetcode_1816.csproj --nologo
dotnet run --no-build --project leetcode_1816/leetcode_1816.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: s="Hello how are you Contestant", k=4
Expected: "Hello how are you"
Actual: "Hello how are you"
Result: PASS

Case: 2 - Official example 2
Input: s="What is the solution to this problem", k=4
Expected: "What is the solution"
Actual: "What is the solution"
Result: PASS

Case: 3 - Official example 3
Input: s="chopper is not a tanuki", k=5
Expected: "chopper is not a tanuki"
Actual: "chopper is not a tanuki"
Result: PASS

Case: 4 - Minimum valid input
Input: s="a", k=1
Expected: "a"
Actual: "a"
Result: PASS

Case: 5 - Keep only the first word
Input: s="Hello World", k=1
Expected: "Hello"
Actual: "Hello"
Result: PASS

Case: 6 - Mixed letter casing
Input: s="a B c D", k=3
Expected: "a B c"
Actual: "a B c"
Result: PASS

Case: 7 - Cut at the kth word boundary
Input: s="one two three four", k=3
Expected: "one two three"
Actual: "one two three"
Result: PASS

Case: 8 - Maximum sentence length
Input: 500 characters, 250 words
Expected: "aa"
Actual: "aa"
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1816/
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
└── leetcode_1816/
    ├── Program.cs
    └── leetcode_1816.csproj
```
