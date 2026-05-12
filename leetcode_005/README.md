# LeetCode 005 - Longest Palindromic Substring

![C#](https://img.shields.io/badge/C%23-console-512BD4)
![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)

這個專案示範 LeetCode 005「Longest Palindromic Substring / 最長回文子字串」的 C# 解法。Console 進入點會執行多組測試資料，並比較三種解法的輸出結果。

## 快速開始

```powershell
dotnet restore leetcode_005\leetcode_005.csproj
dotnet build leetcode_005\leetcode_005.csproj
dotnet run --project leetcode_005\leetcode_005.csproj
```

執行後會輸出 5 組案例，每組都會顯示輸入、可接受答案、三種解法的實際結果與流程摘要。若最長回文有多個合法答案，例如 `babad` 的 `bab` 與 `aba`，任一最長答案都視為正確。

## 題目說明

給定一個字串 `s`，回傳 `s` 中最長的回文子字串。

回文是指從左到右與從右到左讀起來都相同的字串，例如 `aba`、`bb`、`geeksskeeg`。

## 限制條件

- `1 <= s.Length <= 1000`
- `s` 只包含數字與英文字母
- 若存在多個同長度的最長回文子字串，回傳其中任一個即可

## 解題概念與出發點

核心觀察是：回文子字串必須在左右兩端字元相同時才能繼續向內或向外成立。

本專案保留三種解法：

| 方法 | 函式 | 時間複雜度 | 空間複雜度 | 出發點 |
| --- | --- | --- | --- | --- |
| 方法一 | `LongestPalindrome` | `O(n^3)` | `O(1)` | 枚舉所有子字串，再檢查是否為回文 |
| 方法二 | `LongestPalindrome2` | `O(n^2)` | `O(1)` | 從每個奇數與偶數中心向左右擴展 |
| 方法三 | `LongestPalindrome3` | `O(n^2)` | `O(1)` | 用 `2n - 1` 個中心統一處理奇偶長度回文 |

## 解法設計

### 方法一：暴力法

`LongestPalindrome` 會使用左右邊界 `i`、`j` 枚舉所有子字串。每固定一段子字串，就用雙指標 `p`、`q` 從兩端往中間檢查字元是否相同；如果是回文且長度大於目前答案，就更新結果。

範例演示流程，以 `babad` 為例：

1. 從 `b`、`ba`、`bab` 等子字串開始枚舉。
2. 檢查 `bab` 時，左右字元 `b` 相同，中間 `a` 自然成立，因此得到回文。
3. `bab` 長度為 3，大於先前答案，更新為目前最長結果。
4. 後續也可能找到 `aba`，同樣是合法答案；目前實作保留先找到的 `bab`。

### 方法二：中心擴展法

`LongestPalindrome2` 利用每個回文都有中心的特性。中心可能是單一字元，例如 `aba` 的 `b`；也可能是兩個字元中間，例如 `bb` 的兩個 `b` 中間。因此每個索引都會各自嘗試奇數中心與偶數中心，再呼叫 `ExpendCenter` 向左右擴展。

範例演示流程，以 `cbbd` 為例：

1. 掃描到第一個 `b` 時，嘗試單字元中心與雙字元中心。
2. 雙字元中心落在兩個 `b` 中間，左右字元相同，形成 `bb`。
3. 再向外擴展會比較 `c` 與 `d`，兩者不同，因此停止。
4. `bb` 是目前最長回文，最後回傳 `bb`。

### 方法三：優化中心擴展法

`LongestPalindrome3` 將長度為 `n` 的字串視為有 `2n - 1` 個中心。整數中心代表單一字元，`.5` 中心代表兩個字元中間，再透過 `Math.Floor` 與 `Math.Ceiling` 換算實際左右邊界。這樣可以讓奇數與偶數回文共用同一段擴展流程。

範例演示流程，以 `forgeeksskeegfor` 為例：

1. 逐一掃描 `2n - 1` 個可能中心。
2. 當中心落在 `ss` 中間時，左右邊界會先對到兩個 `s`。
3. 持續向外擴展，依序比對 `k/k`、`e/e`、`e/e`、`g/g`。
4. 再往外遇到不同字元後停止，得到 `geeksskeeg`。
5. 該結果長度大於其他回文，因此最後回傳 `geeksskeeg`。

## 範例執行摘要

| Input | 可接受答案 | 暴力法 | 中心擴展法 | 優化中心擴展法 |
| --- | --- | --- | --- | --- |
| `babad` | `bab` 或 `aba` | `bab` | `bab` | `bab` |
| `cbbd` | `bb` | `bb` | `bb` | `bb` |
| `a` | `a` | `a` | `a` | `a` |
| `ac` | `a` 或 `c` | `a` | `a` | `a` |
| `forgeeksskeegfor` | `geeksskeeg` | `geeksskeeg` | `geeksskeeg` | `geeksskeeg` |

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_005/
│   ├── leetcode_005.csproj
│   └── Program.cs
├── leetcode_005.slnx
└── README.md
```

目前 repository 尚未建立測試專案，因此驗證方式以 `dotnet build` 與 `dotnet run` 的範例輸出為主。
