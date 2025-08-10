# Reordered Power of 2 (Leetcode 869)

> 判斷一個整數的數字重新排列後，是否能成為 2 的冪。

---

## 專案簡介

本專案為 Leetcode 第 869 題「Reordered Power of 2」的 C# 解法，實作了搜尋回溯與位元運算的組合，能有效判斷任意整數 n 是否能經由重新排列其數字後，變成 2 的冪。

- 題目連結：[Leetcode 869](https://leetcode.com/problems/reordered-power-of-2/)
- 中文說明：[Leetcode 869 (CN)](https://leetcode.cn/problems/reordered-power-of-2/)

## 解題思路

1. **全排列搜尋（回溯法/DFS）**：
  - 將 n 轉為字元陣列並排序，方便去除重複排列。
  - 使用回溯法（深度優先搜尋，DFS）枚舉所有可能的數字排列：
    - 每一層遞迴代表決定一個數字的位置。
    - 用一個布林陣列 `vis` 標記哪些數字已經被使用，避免重複。
    - 若目前組成的數字為 0，則不能選擇 '0' 作為首位（避免前導零）。
    - 若遇到重複數字，僅當前一個相同數字已被使用時才可選，確保不產生重複排列（這是 Leetcode 47 題的經典技巧）。
    - 每次選定一個數字後，遞迴進入下一層，組成新的數字。
    - 當所有位數都決定完（遞迴到底），就檢查這個排列是否為 2 的冪。
  - 只要有一種排列符合條件，立即 return true，否則全部排列都檢查完才 return false。

  **回溯法流程圖解：**
  以 n = 128 為例，排列過程如下：
  ```
  1. 選 '1' → 選 '2' → 選 '8' → 得到 128
  2. 選 '1' → 選 '8' → 選 '2' → 得到 182
  3. 選 '2' → 選 '1' → 選 '8' → 得到 218
  ...
  ```
  每一條路徑都會組成一個不同的數字，全部檢查是否為 2 的冪。

2. **判斷 2 的冪（位元運算）**：
  - 對每個排列出來的數字，利用 `n > 0 && (n & (n-1)) == 0` 判斷是否為 2 的冪。
  - 原理：2 的冪在二進位下只有一個位元為 1，減 1 後與原數字做 AND 運算必為 0。

3. **提前結束**：
  - 只要有一種排列符合條件，立即回傳 true，節省不必要的搜尋。

:::note
本專案程式碼有詳細中文註解，特別適合學習「回溯法全排列」與「位元運算判斷 2 的冪」的技巧。
:::

## 專案結構

```
leetcode_869.sln
leetcode_869/
  leetcode_869.csproj
  Program.cs   # 主程式與解題邏輯
```

## 如何執行

1. 安裝 [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. 在專案根目錄下執行：

```sh
# 建構專案
$ dotnet build

# 執行專案
$ dotnet run --project leetcode_869/leetcode_869.csproj
```

## 範例輸出

```
n=1, 是否可重排為2的冪: True
n=10, 是否可重排為2的冪: False
n=16, 是否可重排為2的冪: True
n=24, 是否可重排為2的冪: False
n=46, 是否可重排為2的冪: True
...
```

## 參考資料
- [Leetcode 官方題解](https://leetcode.cn/problems/reordered-power-of-2/solutions/)
- [C# 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/)

:::tip
如需學習更多 C# 回溯法與位元運算技巧，建議參考 Leetcode 47、231 題。
:::
