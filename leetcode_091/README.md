# LeetCode 91 — Decode Ways 解碼方法

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-91%20Decode%20Ways-FFA116)](https://leetcode.com/problems/decode-ways/)

這是一個以 C# 與 .NET 10 實作的主控台教學專案。專案保留完整的動態規劃陣列解法，並提供只使用常數額外空間的滾動變數解法；執行程式即可用相同固定案例驗證兩種實作。

## 快速連結

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：動態規劃陣列](#解法一動態規劃陣列)
- [解法二：滾動變數最佳化](#解法二滾動變數最佳化)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

一段由大寫英文字母組成的訊息，依照下列方式編碼：

```text
"1"  -> A
"2"  -> B
...
"25" -> Y
"26" -> Z
```

給定一個只含數字的字串 `s`，請計算整個字串共有多少種合法解碼方式。如果字串無法完整解碼，回傳 `0`。

例如，`"12"` 有兩種解碼方式：

1. `1, 2` → `A, B` → `"AB"`
2. `12` → `L`

因此答案是 `2`。

### 限制條件

依據 [LeetCode 91 官方題目](https://leetcode.com/problems/decode-ways/description/)：

- `1 <= s.Length <= 100`
- `s` 只包含數字。
- `s` 可能包含前導零。
- 測試資料保證答案可由 32 位元整數表示。

### `0` 為什麼特別重要？

`0` 沒有自己的字母映射，因此不能單獨解碼。它只有在與前一個數字組成 `10` 或 `20` 時才合法。

| 輸入 | 是否合法 | 原因 |
|---|---:|---|
| `"0"` | 否 | `0` 不能單獨對應任何字母 |
| `"06"` | 否 | `06` 不能視為 `6`，前導零不合法 |
| `"10"` | 是 | `10` 可對應 `J` |
| `"20"` | 是 | `20` 可對應 `T` |
| `"30"` | 否 | `30` 不在 `1–26` 的映射範圍 |

## 解題概念與出發點

從左到右讀取字串時，位置 `i` 的答案只取決於前一個位置與前兩個位置：

1. 如果目前的一位數是 `1–9`，它可以單獨成為一個字母。所有「前 `i - 1` 個字元」的解法都能在尾端接上這個字母。
2. 如果最近的兩位數是 `10–26`，它可以合併成一個字母。所有「前 `i - 2` 個字元」的解法都能在尾端接上這個字母。
3. 若兩個條件都成立，兩批解法互不重複，所以可以相加。
4. 若兩個條件都不成立，該位置的解法數就是 `0`。

定義：

```text
dp[i] = s 的前 i 個字元共有多少種合法解碼方式
```

狀態轉移：

```text
dp[i] = 0

若目前一位數介於 1–9：
    dp[i] += dp[i - 1]

若目前兩位數介於 10–26：
    dp[i] += dp[i - 2]
```

基礎狀態是 `dp[0] = 1`。這個 `1` 不是說空字串對應一個字母，而是表示「尚未選取任何字元」有一種有效組合方式，使第一個合法數字能從它延伸。

## 解法一：動態規劃陣列

方法：`NumDecodings(string s)`

### 設計說明

這個解法建立長度為 `n + 1` 的 `dp` 陣列，完整保存每個前綴的答案。

程式先在字串前面補上一個空白字元，讓原字串的第一個字元位於索引 `1`。如此一來，`dp[i]` 可以直接對應到「前 `i` 個數字」，也能一致地使用 `dp[i - 1]` 與 `dp[i - 2]`。

每一輪會取得：

- `singleDigit`：目前字元形成的一位數。
- `doubleDigits`：前一個字元與目前字元形成的兩位數。

如果 `singleDigit` 在 `1–9` 之間，將 `dp[i - 1]` 放入 `dp[i]`；如果 `doubleDigits` 在 `10–26` 之間，再加上 `dp[i - 2]`。

### 範例演示：`s = "226"`

初始狀態：

```text
dp[0] = 1
```

| i | 目前前綴 | 一位數判斷 | 兩位數判斷 | 計算 | dp[i] |
|---:|---|---|---|---|---:|
| 1 | `"2"` | `2` 合法 | 無有效前一位 | `dp[1] = dp[0]` | 1 |
| 2 | `"22"` | `2` 合法 | `22` 合法 | `dp[2] = dp[1] + dp[0]` | 2 |
| 3 | `"226"` | `6` 合法 | `26` 合法 | `dp[3] = dp[2] + dp[1]` | 3 |

最終 `dp[3] = 3`，三種分組是：

```text
2, 2, 6  -> B, B, F
2, 26    -> B, Z
22, 6    -> V, F
```

### 複雜度

- 時間複雜度：O(n)，每個字元只處理一次。
- 空間複雜度：O(n)，需要保存完整的 `dp` 陣列。

## 解法二：滾動變數最佳化

方法：`NumDecodings2(string s)`

### 設計說明

狀態轉移只會讀取 `dp[i - 1]` 與 `dp[i - 2]`，因此不必保留整個陣列。這個解法使用三個變數：

- `previousTwo`：進入本輪前的 `dp[i - 2]`。
- `previousOne`：進入本輪前的 `dp[i - 1]`。
- `current`：本輪正在計算的 `dp[i]`。

第一個字元如果是 `0`，`previousOne` 初始化為 `0`；否則初始化為 `1`。之後每輪仍使用與解法一完全相同的兩個合法性判斷。計算完成後，狀態向右滾動：

```text
previousTwo = previousOne
previousOne = current
```

這項最佳化只改變狀態的保存方式，不改變動態規劃的定義與答案。

### 範例演示：`s = "11106"`

第一個字元 `1` 可單獨解碼，因此初始狀態是：

```text
previousTwo = dp[0] = 1
previousOne = dp[1] = 1
```

| 處理字元 | 進入本輪的 previousTwo | 進入本輪的 previousOne | 一位數貢獻 | 兩位數貢獻 | current |
|---|---:|---:|---:|---:|---:|
| 第 2 個 `1` | 1 | 1 | `1` 合法，加入 1 | `11` 合法，加入 1 | 2 |
| 第 3 個 `1` | 1 | 2 | `1` 合法，加入 2 | `11` 合法，加入 1 | 3 |
| 第 4 個 `0` | 2 | 3 | `0` 不合法，不加入 | `10` 合法，加入 2 | 2 |
| 第 5 個 `6` | 3 | 2 | `6` 合法，加入 2 | `06` 不合法，不加入 | 2 |

最終答案為 `2`：

```text
1, 1, 10, 6 -> A, A, J, F
11, 10, 6   -> K, J, F
```

這個過程也顯示了為什麼 `"06"` 不能被獨立當成 `6`：最後一輪只能使用單一的 `6` 延續前面已完成的 `"1110"` 解碼，不能把 `0` 與 `6` 組成新字母。

### 複雜度

- 時間複雜度：O(n)，每個字元只處理一次。
- 空間複雜度：O(1)，只保存固定數量的整數狀態。

## 兩種解法比較

| 項目 | `NumDecodings` | `NumDecodings2` |
|---|---|---|
| 核心方法 | 動態規劃陣列 | 動態規劃滾動變數 |
| 時間複雜度 | O(n) | O(n) |
| 額外空間 | O(n) | O(1) |
| 優點 | 每個前綴答案都保留下來，容易觀察與除錯 | 記憶體使用固定，適合只需要最終答案時 |
| 取捨 | 字串越長，陣列越大 | 不再保留較早的 DP 狀態 |
| 適合用途 | 初學、教學、需要查看完整狀態 | 熟悉遞推後的空間最佳化 |

## 專案結構

```text
leetcode_091/
├── leetcode_091/
│   ├── leetcode_091.csproj
│   └── Program.cs
├── docs/
│   └── readme-template.md
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── leetcode_091.sln
└── README.md
```

## 建置與執行

需求：

- .NET 10 SDK

請在儲存庫根目錄執行：

```powershell
dotnet restore leetcode_091/leetcode_091.csproj
dotnet build leetcode_091/leetcode_091.csproj --nologo
dotnet run --project leetcode_091/leetcode_091.csproj
```

目前沒有獨立的自動測試專案。`Main` 會讓兩個解法分別執行九筆固定案例，並以 expected／actual 與 PASS／FAIL 輸出作為可重複執行的驗收檢查。

若要檢查 Git diff 中的多餘空白，可執行：

```powershell
git -c safe.directory=C:/GitHubFolder/Leetcode_folder diff --check -- .
```

命令沒有輸出且結束碼為 `0`，代表已追蹤差異未發現空白錯誤。

## 實際執行結果

以下內容來自：

```powershell
dotnet run --project leetcode_091/leetcode_091.csproj
```

```text
LeetCode 91. Decode Ways
[PASS] NumDecodings | s="12" | expected=2 | actual=2
[PASS] NumDecodings | s="226" | expected=3 | actual=3
[PASS] NumDecodings | s="06" | expected=0 | actual=0
[PASS] NumDecodings | s="0" | expected=0 | actual=0
[PASS] NumDecodings | s="10" | expected=1 | actual=1
[PASS] NumDecodings | s="27" | expected=1 | actual=1
[PASS] NumDecodings | s="11106" | expected=2 | actual=2
[PASS] NumDecodings | s="2101" | expected=1 | actual=1
[PASS] NumDecodings | s="123123" | expected=9 | actual=9
[PASS] NumDecodings2 | s="12" | expected=2 | actual=2
[PASS] NumDecodings2 | s="226" | expected=3 | actual=3
[PASS] NumDecodings2 | s="06" | expected=0 | actual=0
[PASS] NumDecodings2 | s="0" | expected=0 | actual=0
[PASS] NumDecodings2 | s="10" | expected=1 | actual=1
[PASS] NumDecodings2 | s="27" | expected=1 | actual=1
[PASS] NumDecodings2 | s="11106" | expected=2 | actual=2
[PASS] NumDecodings2 | s="2101" | expected=1 | actual=1
[PASS] NumDecodings2 | s="123123" | expected=9 | actual=9
Summary: 18/18 passed.
```
