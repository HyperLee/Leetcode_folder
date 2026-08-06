# LeetCode 1768：交替合併字串

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-00AF9B)](https://leetcode.com/problems/merge-strings-alternately/)

這是一個以 .NET 10 Console App 實作的教學專案，示範如何從第一個字串開始，依序交替取出兩個字串的字元，並在其中一個字串先用完時，將另一個字串剩餘的字元接到結果尾端。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：共同長度後追加尾端](#解法一共同長度後追加尾端)
- [解法二：雙指標單一迴圈](#解法二雙指標單一迴圈)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定兩個字串 `word1` 與 `word2`，從 `word1` 開始輪流取出一個字元，組成新的字串。

若其中一個字串較長，在另一個字串的字元全部使用完畢後，必須將較長字串尚未使用的字元依原順序附加到結果尾端。

### 官方範例

| 範例 | `word1` | `word2` | 輸出 | 說明 |
| --- | --- | --- | --- | --- |
| 1 | `"abc"` | `"pqr"` | `"apbqcr"` | 兩字串等長，全程成對交替加入 |
| 2 | `"ab"` | `"pqrs"` | `"apbqrs"` | 交替加入 `a、p、b、q` 後，補上 `word2` 的 `rs` |
| 3 | `"abcd"` | `"pq"` | `"apbqcd"` | 交替加入 `a、p、b、q` 後，補上 `word1` 的 `cd` |

題目連結：[LeetCode 1768. Merge Strings Alternately](https://leetcode.com/problems/merge-strings-alternately/description/)

## 限制條件

官方題目限制如下：

- `1 <= word1.length, word2.length <= 100`
- `word1` 與 `word2` 僅由小寫英文字母組成。

本專案的兩個方法遵循 C# nullable contract，預期輸入為非 `null` 字串，不另外處理 `null`。Console acceptance harness 額外加入其中一個字串為空字串的案例；這是用來驗證方法在官方範圍外仍能自然處理邊界輸入，不代表題目本身允許長度為 0。

## 解題概念與出發點

設 `m = word1.Length`、`n = word2.Length`。無論兩個字串長度是否相同，合併過程都必須維持以下規則：

1. 只要兩個字串在目前位置都還有字元，就先加入 `word1` 的字元，再加入 `word2` 的字元。
2. 每個輸入字元恰好被加入一次，原本在各自字串中的順序不能改變。
3. 當其中一個字串先用完時，另一個字串的剩餘部分直接依序接到尾端。
4. 最終結果長度必定為 `m + n`。

### 為什麼使用 `StringBuilder`

C# 的 `string` 是不可變物件。若在迴圈中反覆使用 `result += character`，每次串接都可能建立新的字串並複製既有內容。兩種解法都先以 `m + n` 作為 `StringBuilder` 容量，再逐字加入結果，能清楚表達演算法並避免不必要的中間字串。

## 解法一：共同長度後追加尾端

### 設計說明

`MergeAlternately` 將問題拆成兩個階段：

1. 計算 `commonLength = Math.Min(m, n)`。
2. 在索引 `0` 到 `commonLength - 1` 之間，每輪依序加入 `word1[i]` 與 `word2[i]`。
3. 共同區段完成後，檢查哪一個字串較長。
4. 將較長字串從 `commonLength` 開始的完整尾端一次加入 `StringBuilder`。

這種寫法把「可以成對交替的區段」與「只剩單一字串的區段」明確分開，適合用來理解題目的兩段式結構。

### 範例演示流程

#### 範例 1：`word1 = "abc"`、`word2 = "pqr"`

`commonLength = 3`，兩字串沒有剩餘尾端。

| 索引 `i` | 加入內容 | 累積結果 |
| ---: | --- | --- |
| 0 | `a`、`p` | `ap` |
| 1 | `b`、`q` | `apbq` |
| 2 | `c`、`r` | `apbqcr` |

#### 範例 2：`word1 = "ab"`、`word2 = "pqrs"`

`commonLength = 2`，先完成兩輪交替，再補上 `word2[2..]`。

| 階段 | 加入內容 | 累積結果 |
| --- | --- | --- |
| 共同索引 0 | `a`、`p` | `ap` |
| 共同索引 1 | `b`、`q` | `apbq` |
| 追加 `word2` 尾端 | `rs` | `apbqrs` |

#### 範例 3：`word1 = "abcd"`、`word2 = "pq"`

`commonLength = 2`，先完成兩輪交替，再補上 `word1[2..]`。

| 階段 | 加入內容 | 累積結果 |
| --- | --- | --- |
| 共同索引 0 | `a`、`p` | `ap` |
| 共同索引 1 | `b`、`q` | `apbq` |
| 追加 `word1` 尾端 | `cd` | `apbqcd` |

### 複雜度

- 時間複雜度：`O(m + n)`，每個字元只會被加入結果一次。
- 空間複雜度：`O(m + n)`，用於儲存回傳結果；不計回傳結果本身時，額外狀態為 `O(1)`。

## 解法二：雙指標單一迴圈

### 設計說明

`MergeAlternately2` 以同一個索引 `i` 表示兩個字串目前要嘗試讀取的位置：

1. 迴圈執行到 `Math.Max(m, n) - 1`。
2. 若 `i < m`，加入 `word1[i]`。
3. 若 `i < n`，加入 `word2[i]`。
4. 較短字串用盡後，其範圍判斷會變成 `false`；較長字串則繼續在後續輪次加入剩餘字元。

這種寫法不需要另外切分尾端區段。每一輪只處理「目前索引是否仍有效」，適合用來理解雙指標與邊界判斷如何把一般流程和剩餘字元整合在同一個迴圈中。

### 範例演示流程

#### 範例 1：`word1 = "abc"`、`word2 = "pqr"`

`maxLength = 3`，每輪兩個範圍判斷都成立。

| `i` | `i < word1.Length` | `i < word2.Length` | 累積結果 |
| ---: | --- | --- | --- |
| 0 | 加入 `a` | 加入 `p` | `ap` |
| 1 | 加入 `b` | 加入 `q` | `apbq` |
| 2 | 加入 `c` | 加入 `r` | `apbqcr` |

#### 範例 2：`word1 = "ab"`、`word2 = "pqrs"`

`maxLength = 4`。從 `i = 2` 開始，`word1` 的判斷不成立，但 `word2` 仍會繼續加入字元。

| `i` | `word1` 動作 | `word2` 動作 | 累積結果 |
| ---: | --- | --- | --- |
| 0 | 加入 `a` | 加入 `p` | `ap` |
| 1 | 加入 `b` | 加入 `q` | `apbq` |
| 2 | 超出範圍 | 加入 `r` | `apbqr` |
| 3 | 超出範圍 | 加入 `s` | `apbqrs` |

#### 範例 3：`word1 = "abcd"`、`word2 = "pq"`

`maxLength = 4`。從 `i = 2` 開始，只有 `word1` 的判斷成立。

| `i` | `word1` 動作 | `word2` 動作 | 累積結果 |
| ---: | --- | --- | --- |
| 0 | 加入 `a` | 加入 `p` | `ap` |
| 1 | 加入 `b` | 加入 `q` | `apbq` |
| 2 | 加入 `c` | 超出範圍 | `apbqc` |
| 3 | 加入 `d` | 超出範圍 | `apbqcd` |

### 複雜度

- 時間複雜度：`O(m + n)`；雖然迴圈次數是 `max(m, n)`，每輪最多處理兩個字元，總處理量仍與輸入總長度成正比。
- 空間複雜度：`O(m + n)`，用於儲存回傳結果；不計回傳結果本身時，額外狀態為 `O(1)`。

## 兩種解法比較

| 比較項目 | `MergeAlternately` | `MergeAlternately2` |
| --- | --- | --- |
| 核心概念 | 共同長度區段加上剩餘尾端 | 單一迴圈搭配範圍判斷 |
| 尾端處理 | 共同區段結束後一次追加 | 在後續迴圈中逐字追加 |
| 主要教學重點 | 問題分段、共同範圍、尾端切片 | 雙指標、索引邊界、統一流程 |
| 時間複雜度 | `O(m + n)` | `O(m + n)` |
| 空間複雜度 | `O(m + n)` | `O(m + n)` |
| 是否修改輸入 | 否 | 否 |

兩種方法的漸進複雜度相同，差異主要在流程表達。第一種把題目拆成容易觀察的兩個階段；第二種以邊界條件消除獨立的尾端處理階段。

## 測試設計

`Main` 以 6 組固定案例驗證兩種解法，共執行 12 次檢查：

| 案例 | `word1` | `word2` | 預期結果 | 驗證目的 |
| --- | --- | --- | --- | --- |
| Equal lengths | `"abc"` | `"pqr"` | `"apbqcr"` | 一般等長輸入 |
| Second word is longer | `"ab"` | `"pqrs"` | `"apbqrs"` | 第二個字串有剩餘字元 |
| First word is longer | `"abcd"` | `"pq"` | `"apbqcd"` | 第一個字串有剩餘字元 |
| Single-character words | `"a"` | `"z"` | `"az"` | 官方限制下的最小長度 |
| Empty first word | `""` | `"xyz"` | `"xyz"` | 延伸驗證第一個字串為空 |
| Empty second word | `"xyz"` | `""` | `"xyz"` | 延伸驗證第二個字串為空 |

每次檢查都輸出案例名稱、解法名稱、輸入、`Expected`、`Actual` 與 `PASS/FAIL`。只要任一檢查失敗，程式便將結束碼設為非 0，方便在終端或自動化環境中辨識失敗。

## 建置與執行

請從 repository 根目錄執行：

```bash
dotnet restore leetcode_1768/leetcode_1768.csproj
dotnet build leetcode_1768/leetcode_1768.csproj --nologo
dotnet run --no-build --project leetcode_1768/leetcode_1768.csproj
```

可使用以下命令確認格式與差異品質：

```bash
dotnet format leetcode_1768/leetcode_1768.csproj --verify-no-changes --no-restore
git diff --check
```

## 實際執行結果

以下內容來自完成建置後實際執行 `dotnet run --no-build --project leetcode_1768/leetcode_1768.csproj` 的輸出：

```text
Case: Equal lengths | Solution: MergeAlternately
Input: word1="abc", word2="pqr"
Expected: apbqcr
Actual: apbqcr
Result: PASS

Case: Equal lengths | Solution: MergeAlternately2
Input: word1="abc", word2="pqr"
Expected: apbqcr
Actual: apbqcr
Result: PASS

Case: Second word is longer | Solution: MergeAlternately
Input: word1="ab", word2="pqrs"
Expected: apbqrs
Actual: apbqrs
Result: PASS

Case: Second word is longer | Solution: MergeAlternately2
Input: word1="ab", word2="pqrs"
Expected: apbqrs
Actual: apbqrs
Result: PASS

Case: First word is longer | Solution: MergeAlternately
Input: word1="abcd", word2="pq"
Expected: apbqcd
Actual: apbqcd
Result: PASS

Case: First word is longer | Solution: MergeAlternately2
Input: word1="abcd", word2="pq"
Expected: apbqcd
Actual: apbqcd
Result: PASS

Case: Single-character words | Solution: MergeAlternately
Input: word1="a", word2="z"
Expected: az
Actual: az
Result: PASS

Case: Single-character words | Solution: MergeAlternately2
Input: word1="a", word2="z"
Expected: az
Actual: az
Result: PASS

Case: Empty first word | Solution: MergeAlternately
Input: word1="", word2="xyz"
Expected: xyz
Actual: xyz
Result: PASS

Case: Empty first word | Solution: MergeAlternately2
Input: word1="", word2="xyz"
Expected: xyz
Actual: xyz
Result: PASS

Case: Empty second word | Solution: MergeAlternately
Input: word1="xyz", word2=""
Expected: xyz
Actual: xyz
Result: PASS

Case: Empty second word | Solution: MergeAlternately2
Input: word1="xyz", word2=""
Expected: xyz
Actual: xyz
Result: PASS

Summary: 12/12 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1768.sln
└── leetcode_1768/
    ├── leetcode_1768.csproj
    └── Program.cs
```