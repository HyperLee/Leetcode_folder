# LeetCode 1897 — Redistribute Characters to Make All Strings Equal

> 重新分配字元使所有字串相等｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/redistribute-characters-to-make-all-strings-equal/)
- [中文題目](https://leetcode.cn/problems/redistribute-characters-to-make-all-strings-equal/)

## 題目說明

給定字串陣列 `words`。每次可從任一字串取出一個字元，並將它加入任一字串。判斷是否能
重新分配所有字元，使得最後所有字串完全相同。

題目限制：

- `1 <= words.length <= 100`
- `1 <= words[i].length <= 100`
- `words[i]` 僅由小寫英文字母組成

## 解法：Dictionary 字元計數

公開 API：

```csharp
public static bool MakeEqual(string[] words)
```

可自由搬移字元，因此字串原本的順序與各字串目前持有的字元都不重要。以 `Dictionary<char,
int>` 統計所有字元的總出現次數後，逐一檢查它們是否都能平均分配給 `words.Length` 個
字串。

核心不變量是：

> 每種字元的總次數都可被字串數量整除，當且僅當存在一種重新分配方法讓所有字串相同。

例如 `["abc", "aabc", "bc"]` 有三個字串，`a`、`b`、`c` 的總次數各為 `3`，
皆可平均分為每個字串一個，因此答案為 `true`。反之 `["ab", "a"]` 的 `a` 出現
`2` 次、`b` 出現 `1` 次，無法平均分給兩個字串，答案為 `false`。

方法只讀取輸入陣列及其中字串，不修改輸入也不進行主控台輸出。

### 複雜度

令 `C` 為所有字串字元總數，`k` 為不同字元數量：

| 項目 | 複雜度 | 說明 |
| --- | --- | --- |
| 時間 | `O(C)` | 每個字元只統計一次，再檢查最多 `k` 種字元 |
| 結果空間 | `O(1)` | 只回傳一個布林值 |
| 輔助空間 | `O(k)`，且 `k <= 26` | Dictionary 儲存各小寫英文字母的計數 |

## Acceptance Harness

`Main` 執行八個確定性案例。每案同時驗證答案與原始輸入陣列未被修改；任何失敗都會把
process exit code 設為 `1`。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `["abc", "aabc", "bc"]` | `true` | 官方範例 |
| 2 | `["ab", "a"]` | `false` | 字元總次數不可整除 |
| 3 | `["a"]` | `true` | 單一字串 |
| 4 | `["abc", "abc"]` | `true` | 已經相同 |
| 5 | `["ab", "ab", "aa"]` | `false` | 多種字元餘數 |
| 6 | `["aa", "bb", "ab"]` | `true` | 跨全部字元重新分配 |
| 7 | 100 個 `"a"` | `true` | 最大字串數量 |
| 8 | 一個長度 100 的 `"a"` 與一個長度 100 的 `"b"` | `true` | 最大字串長度 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1897/leetcode_1897/leetcode_1897.csproj --nologo
dotnet run --no-build --project leetcode_1897/leetcode_1897/leetcode_1897.csproj
```

若直接開啟題目根目錄 `leetcode_1897/`，使用：

```bash
dotnet build leetcode_1897/leetcode_1897.csproj --nologo
dotnet run --no-build --project leetcode_1897/leetcode_1897.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example
Input: ["abc", "aabc", "bc"]
Expected: True
Actual: True
Input preserved: True
Result: PASS

Case: 2 - Character count is not divisible
Input: ["ab", "a"]
Expected: False
Actual: False
Input preserved: True
Result: PASS

Case: 3 - Single string
Input: ["a"]
Expected: True
Actual: True
Input preserved: True
Result: PASS

Case: 4 - Already equal strings
Input: ["abc", "abc"]
Expected: True
Actual: True
Input preserved: True
Result: PASS

Case: 5 - Multiple remainders
Input: ["ab", "ab", "aa"]
Expected: False
Actual: False
Input preserved: True
Result: PASS

Case: 6 - Redistribution across all characters
Input: ["aa", "bb", "ab"]
Expected: True
Actual: True
Input preserved: True
Result: PASS

Case: 7 - One hundred copies
Input: ["a" x 100]
Expected: True
Actual: True
Input preserved: True
Result: PASS

Case: 8 - Two strings of maximum length
Input: ["a" x 100 chars, "b" x 100 chars]
Expected: True
Actual: True
Input preserved: True
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1897/
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
└── leetcode_1897/
    ├── Program.cs
    └── leetcode_1897.csproj
```
