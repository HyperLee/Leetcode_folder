# 1759. Count Number of Homogenous Substrings／統計同質子字串的數目

給定一個只含小寫英文字母的字串，計算其中所有字元完全相同的連續子字串數量，並將
答案對 `10^9 + 7` 取模。本專案保留單一、純函式的連續段落三角數解法。

- [LeetCode English](https://leetcode.com/problems/count-number-of-homogenous-substrings/)
- [LeetCode 中文](https://leetcode.cn/problems/count-number-of-homogenous-substrings/)

## 題目說明與限制條件

同質子字串的每個字元都必須相同，而且子字串必須是原字串中的連續區間。同一段長度為
`L` 的連續相同字元可提供長度 1 到 `L` 的所有合法子字串。

- `1 <= s.length <= 100000`
- `s` 僅包含小寫英文字母。
- 答案須對 `1_000_000_007` 取模。
- `CountHomogenous` 只處理題目保證的有效輸入，不加入額外的無效輸入行為。

## 保留解法：連續段落三角數

從左至右維護目前連續同字元段落的字元與長度。遇到不同字元時，長度為 `L` 的已封閉
段落共有 `L + (L - 1) + ... + 1 = L * (L + 1) / 2` 個同質子字串；將其加入總數後，
再從新字元開始下一段。掃描結束後必須額外結算最後一段。

核心不變量：每次開始新段落前，所有已結束段落的同質子字串都已恰好計入一次；目前
段落尚未計入，避免重複或遺漏。總數使用 `long`，最後才取模並轉回 `int`。

容易出錯之處：

- 忘記掃描結束後結算最後一段，例如 `abb` 會漏掉 `bb` 與其中兩個 `b`。
- 將不同位置但字元相同的段落合併，例如 `aaabaaa` 的兩段 `a` 中間隔著 `b`。
- 使用 `int` 計算最大段落的三角數會溢位；100,000 個相同字元產生
  `5,000,050,000` 個子字串。
- 忘記對最終答案取模；上述最大案例的正確回傳值是 `49,965`。

## 複雜度

| 方法 | 時間 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| `CountHomogenous`（段落三角數） | `O(n)` | `O(1)` | `O(1)` |

方法只回傳一個整數；除了累加值、目前字元與段落長度外，不建立與輸入長度相關的資料
結構。

## 逐步走查

以 `abbcccaa` 為例：

| 段落 | 長度 | 貢獻 | 累計 |
| --- | ---: | ---: | ---: |
| `a` | 1 | 1 | 1 |
| `bb` | 2 | 3 | 4 |
| `ccc` | 3 | 6 | 10 |
| `aa` | 2 | 3 | 13 |

最後共有 13 個同質子字串。

## Acceptance Harness

`Main` 執行九個確定性案例。每案都印出 Case、Input、Expected、Actual 與 PASS/FAIL；
任何案例失敗會將 process exit code 設為 1。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `abbcccaa` | 13 | 官方範例：多個不同長度段落 |
| 2 | `xy` | 2 | 官方範例：每段長度皆為 1 |
| 3 | `zzzzz` | 15 | 官方範例：單一長段落 |
| 4 | `a` | 1 | 最小有效輸入 |
| 5 | `bb` | 3 | 保留舊程式範例 |
| 6 | `aaabaaa` | 13 | 相同字元的分離段落不可合併 |
| 7 | `abb` | 4 | 掃描結束後結算最後段落 |
| 8 | 100,000 個 `a` | 49,965 | 最大長度、`long` 與取模 |
| 9 | `ab` 重複 50,000 次 | 100,000 | 最大長度、每段皆為 1 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1759/leetcode_1759/leetcode_1759.csproj --nologo
dotnet run --no-build --project leetcode_1759/leetcode_1759/leetcode_1759.csproj
```

若直接開啟題目根目錄 `leetcode_1759/`，使用：

```bash
dotnet build leetcode_1759/leetcode_1759.csproj --nologo
dotnet run --no-build --project leetcode_1759/leetcode_1759.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: "abbcccaa"
Expected: 13
Actual: 13
Result: PASS

Case: 2 - Official example 2
Input: "xy"
Expected: 2
Actual: 2
Result: PASS

Case: 3 - Official example 3
Input: "zzzzz"
Expected: 15
Actual: 15
Result: PASS

Case: 4 - Minimum input
Input: "a"
Expected: 1
Actual: 1
Result: PASS

Case: 5 - Legacy sample
Input: "bb"
Expected: 3
Actual: 3
Result: PASS

Case: 6 - Separated runs of the same character
Input: "aaabaaa"
Expected: 13
Actual: 13
Result: PASS

Case: 7 - Trailing run finalization
Input: "abb"
Expected: 4
Actual: 4
Result: PASS

Case: 8 - Maximum length all equal with modulo
Input: 100000 x 'a'
Expected: 49965
Actual: 49965
Result: PASS

Case: 9 - Maximum length alternating characters
Input: 'ab' x 50000
Expected: 100000
Actual: 100000
Result: PASS

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_1759/
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
└── leetcode_1759/
    ├── Program.cs
    └── leetcode_1759.csproj
```
