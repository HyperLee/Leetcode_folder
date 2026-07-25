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

## 解法一：兩個頻率字典

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

### 解法一的複雜度

令 `n`、`m` 分別為兩個陣列長度，`u`、`v` 分別為兩邊不同字串的數量。

| 項目 | 複雜度 | 說明 |
| --- | --- | --- |
| 時間 | `O(n + m)` | 建立兩個頻率字典並掃描第一個字典。 |
| 輔助空間 | `O(u + v)` | 兩個字典各保存一邊的不同字串。 |
| 結果空間 | `O(1)` | 僅回傳一個整數。 |

兩個字典直接對應兩個輸入陣列，比 `GroupBy`/join 寫法更容易看出「同一個鍵在兩邊都恰好
一次」的不變量，適合作為基礎教學版本。

### 解法一的 LeetCode 單次快照

2026-07-25 的單次提交結果為 Runtime `17 ms`、Beats `17.31%`。LeetCode 的 Runtime 與
Beats 會隨執行批次、平台及測資分布而波動，因此這些數字只代表該次執行，不能單獨證明演算法
漸進複雜度較差。

## 解法二：單一字典狀態機

公開 API：

```csharp
public static int CountWords2(string[] words1, string[] words2)
```

此版本只為 `words1` 出現過的字串建立一個 `Dictionary<string, WordState>`。第一輪將每個鍵
標記為在第一邊出現一次或重複；狀態一旦成為重複便不再累加精確次數，因為題目只關心是否恰好
一次。

第二輪掃描 `words2`：

- 不在字典中的字串未出現在 `words1`，直接忽略，也不配置新項目。
- `SeenOnceInFirst` 遇到一次時轉為 `SeenOnceInBoth`。
- `SeenOnceInBoth` 再遇到時轉為 `RepeatedInSecond`。
- `RepeatedInFirst` 與 `RepeatedInSecond` 都不可能成為答案，維持原狀。

最後只計算狀態為 `SeenOnceInBoth` 的鍵。這個唯一接受狀態同時保證同一個字串在兩邊都存在，
且各自恰好出現一次。方法同樣只讀取輸入，不修改陣列或輸出主控台。

### 狀態轉換範例

以 `words1=[a, b]`、`words2=[a, a, c]` 為例：

```plaintext
掃描 words1 後：a=SeenOnceInFirst, b=SeenOnceInFirst
words2 第一個 a：a=SeenOnceInBoth
words2 第二個 a：a=RepeatedInSecond
words2 的 c：不在第一邊，忽略
```

最終沒有任何鍵停在 `SeenOnceInBoth`，因此答案為 `0`。這也能避免只判斷字典中是否存在任意
單次值、卻沒有對齊目前字串鍵的錯誤。

### 方法比較

令 `u`、`v` 分別為兩邊不同字串的數量：

| 方法 | 時間 | 輔助空間 | 適用性與取捨 |
| --- | --- | --- | --- |
| 解法一：`CountWords` | `O(n + m)` | `O(u + v)` | 結構直接對應兩個輸入，最容易理解與驗證 |
| 解法二：`CountWords2` | `O(n + m)` | `O(u)` | 不保存第二邊獨有鍵，減少一個字典及配置 |

兩種方法的結果空間皆為 `O(1)`。解法二改善輔助空間與部分常數成本，但 LeetCode 的單次 Runtime
仍會受執行環境影響；尚未提供解法二的實際提交結果，因此不推測其 Runtime 或 Beats。

## Acceptance Harness

`Main` 執行九個確定性案例。每案以獨立副本分別呼叫 `CountWords` 與 `CountWords2`，各自
驗證答案以及 `words1`、`words2` 兩個輸入陣列的合併保存狀態，因此共有 36 個檢查；任何失敗
都會把 process exit code 設為 `1`。大型案例使用精簡輸入標籤，避免輸出兩千個元素。

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
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 2 | Actual: 2
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 2 - Official example 2
Input: words1=[b, bb, bbb], words2=[a, aa, aaa]
PASS CountWords result | Expected: 0 | Actual: 0
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 0 | Actual: 0
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 3 - Official example 3
Input: words1=[a, ab], words2=[a, a, a, ab]
PASS CountWords result | Expected: 1 | Actual: 1
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 1 | Actual: 1
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 4 - Minimum matching input
Input: words1=[a], words2=[a]
PASS CountWords result | Expected: 1 | Actual: 1
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 1 | Actual: 1
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 5 - Duplicate only in words1
Input: words1=[a, a, b], words2=[a, b]
PASS CountWords result | Expected: 1 | Actual: 1
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 1 | Actual: 1
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 6 - Duplicate only in words2
Input: words1=[a, b], words2=[a, a, b]
PASS CountWords result | Expected: 1 | Actual: 1
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 1 | Actual: 1
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 7 - Different duplicates in both arrays
Input: words1=[a, b, b, c], words2=[a, b, c, c]
PASS CountWords result | Expected: 1 | Actual: 1
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 1 | Actual: 1
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 8 - Dictionary key alignment regression
Input: words1=[a, b], words2=[a, a, c]
PASS CountWords result | Expected: 0 | Actual: 0
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 0 | Actual: 0
PASS CountWords2 input preserved | Expected: True | Actual: True

Case: 9 - Maximum array lengths
Input: words1=[a x 999, b], words2=[a x 999, b]
PASS CountWords result | Expected: 1 | Actual: 1
PASS CountWords input preserved | Expected: True | Actual: True
PASS CountWords2 result | Expected: 1 | Actual: 1
PASS CountWords2 input preserved | Expected: True | Actual: True

Summary: 36/36 checks passed.
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
