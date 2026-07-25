# LeetCode 2085 — Count Common Words With One Occurrence

> 統計出現過一次的公共字串｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/count-common-words-with-one-occurrence/)
- [中文題目](https://leetcode.cn/problems/count-common-words-with-one-occurrence/)

## 題目說明

給定兩個字串陣列 `words1` 與 `words2`，回傳在兩個陣列中都恰好出現一次的字串數量。
同一個字串只要在任一陣列出現兩次以上，就不能計入答案。

題目限制：

- `1 <= words1.length, words2.length <= 1000`
- `1 <= words1[i].length, words2[j].length <= 30`
- 所有字串都只包含小寫英文字母

## 解法：兩個頻率字典

公開 API：

```csharp
public static int CountWords(string[] words1, string[] words2)
```

分別掃描 `words1` 與 `words2`，以兩個 `Dictionary<string, int>` 保存每個字串在各自陣列中的
出現次數。接著掃描第一個字典；只有目前鍵的第一邊頻率為 `1`，且在第二個字典中找到同一個鍵、
第二邊頻率也為 `1`，才增加答案。

核心不變量是：判斷頻率時必須保留字串鍵與次數的對應關係。`ContainsValue(1)` 只能證明第二個
字典中存在某個出現一次的字串，不能證明目前檢查的字串也出現一次；因此必須以目前字串作為鍵
執行 `TryGetValue`。

公開方法只讀取兩個輸入陣列，不排序、不改寫元素、不輸出主控台，也不加入題目契約外的
invalid-input 行為。

### 逐步範例

以官方第一個範例為例：

```plaintext
words1 = [leetcode, is, amazing, as, is]
words2 = [amazing, leetcode, is]
```

兩個頻率字典中的相關項目為：

- `leetcode`：第一邊 `1`、第二邊 `1`，計入。
- `amazing`：第一邊 `1`、第二邊 `1`，計入。
- `is`：第一邊 `2`、第二邊 `1`，不計入。
- `as`：第一邊 `1`、第二邊不存在，不計入。

因此答案是 `2`。

### 複雜度與取捨

令 `n`、`m` 分別為兩個陣列長度，`u`、`v` 分別為兩邊不同字串的數量。

| 項目 | 複雜度 | 說明 |
| --- | --- | --- |
| 時間 | `O(n + m)` | 建立兩個頻率字典並掃描第一個字典。 |
| 輔助空間 | `O(u + v)` | 兩個字典各保存一邊的不同字串。 |
| 結果空間 | `O(1)` | 僅回傳一個整數。 |

兩個字典比把兩邊次數壓進單一複合狀態更直接呈現題意，也比 `GroupBy`/join 寫法更容易看出
「同一個鍵在兩邊都恰好一次」的不變量。

## Acceptance Harness

`Main` 執行九個確定性案例。每案驗證答案，以及 `words1`、`words2` 兩個輸入陣列合併後的
保存狀態，因此共有 18 個檢查；任何失敗都會把 process exit code 設為 `1`。大型案例使用精簡
輸入標籤，避免輸出兩千個元素。

| # | 輸入摘要 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | 官方範例 1 | 2 | 共同單次、單邊重複及單邊存在 |
| 2 | 官方範例 2 | 0 | 完全沒有共同字串 |
| 3 | 官方範例 3 | 1 | 第二個陣列中的重複字串 |
| 4 | `words1=[a]`, `words2=[a]` | 1 | 最小有效輸入 |
| 5 | `words1=[a,a,b]`, `words2=[a,b]` | 1 | 只在第一邊重複 |
| 6 | `words1=[a,b]`, `words2=[a,a,b]` | 1 | 只在第二邊重複 |
| 7 | 兩邊分別重複不同字串 | 1 | 每一邊頻率獨立統計 |
| 8 | `words1=[a,b]`, `words2=[a,a,c]` | 0 | 字典鍵與頻率對齊回歸 |
| 9 | 兩邊皆為 999 個 `a` 加一個 `b` | 1 | 陣列長度 1000 上限 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2085/leetcode_2085/leetcode_2085.csproj --nologo
dotnet run --no-build --project leetcode_2085/leetcode_2085/leetcode_2085.csproj
```

若直接開啟題目根目錄 `leetcode_2085/`，使用：

```bash
dotnet build leetcode_2085/leetcode_2085.csproj --nologo
dotnet run --no-build --project leetcode_2085/leetcode_2085.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: words1=[leetcode, is, amazing, as, is], words2=[amazing, leetcode, is]
PASS CountWords result | Expected: 2 | Actual: 2
PASS Input preserved | Expected: True | Actual: True

Case: 2 - Official example 2
Input: words1=[b, bb, bbb], words2=[a, aa, aaa]
PASS CountWords result | Expected: 0 | Actual: 0
PASS Input preserved | Expected: True | Actual: True

Case: 3 - Official example 3
Input: words1=[a, ab], words2=[a, a, a, ab]
PASS CountWords result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 4 - Minimum matching input
Input: words1=[a], words2=[a]
PASS CountWords result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 5 - Duplicate only in words1
Input: words1=[a, a, b], words2=[a, b]
PASS CountWords result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 6 - Duplicate only in words2
Input: words1=[a, b], words2=[a, a, b]
PASS CountWords result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 7 - Different duplicates in both arrays
Input: words1=[a, b, b, c], words2=[a, b, c, c]
PASS CountWords result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Case: 8 - Dictionary key alignment regression
Input: words1=[a, b], words2=[a, a, c]
PASS CountWords result | Expected: 0 | Actual: 0
PASS Input preserved | Expected: True | Actual: True

Case: 9 - Maximum array lengths
Input: words1=[a x 999, b], words2=[a x 999, b]
PASS CountWords result | Expected: 1 | Actual: 1
PASS Input preserved | Expected: True | Actual: True

Summary: 18/18 checks passed.
```

## 專案結構

```plaintext
leetcode_2085/
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
└── leetcode_2085/
    ├── Program.cs
    └── leetcode_2085.csproj
```
