# LeetCode 1523 — Count Odd Numbers in an Interval Range (在區間範圍內統計奇數數目)

📌 本專案用 C#（.NET）實作 LeetCode 題目 1523：Count Odd Numbers in an Interval Range。目標是給定兩個非負整數 `low` 和 `high`，回傳區間 `[low, high]`（含邊界）內的奇數個數。

Constraints:

- 0 <= low <= high
Example:
- Input: low = 3, high = 7 → Output: 3 (3,5,7)
- Input: low = 4, high = 10 → Output: 3 (5,7,9)

-
- 0 <= low <= high
Constraints:

- 0 <= low <= high
- 0 <= low <= high
- 欄位型別視語言而定，但在一般情況下數值會在 Int 範圍內（LeetCode 常見上限約 10^9），若擔心溢位可改用長整數。

---

## 解題思路與概念

這個問題的核心在於「奇偶性」與「數學化簡」。有幾種簡單方式可以解決：

- 數學公式：透過計算從 `0` 到 `n` 有多少個奇數，並以此推導區間奇數數量的閉式解。
- 暴力枚舉：對區間內每個整數逐一檢查是否為奇數，適合範圍很小的情況或用於驗證及教學。
- 前綴計數（Prefix）：定義 Pre(x) 為 `[0, x]` 區間內的奇數數量，則 `[low, high]` 的奇數數量為 `Pre(high) - Pre(low - 1)`。

數學觀察：

- 在非負整數情況下，`0..n` 之間奇數個數等於 `(n + 1) / 2`（整數除法，向下取整的效果）
- 因此區間 `[low, high]` 的奇數數量 = `((high + 1) / 2) - (low / 2)`。

---

## 專案結構（精簡）

- `leetcode_1523/Program.cs`：本專案主要程式檔，包含三種解法的實作：`CountOdds`（數學公式）、`CountOdds2`（暴力）、`CountOdds3`（前綴計數 + 位移）。
- `leetcode_1523.csproj`：.NET 專案檔。

---

## 三種解法詳細說明

### 方法一：數學公式 (CountOdds)
- 核心概念：直接使用公式 `((high + 1) / 2) - (low / 2)`。
- 為什麼對？`(x + 1) / 2` 回傳 `[0, x]` 中奇數的數量，`low/2` 回傳 `[0, low-1]` 中奇數的數量差值即為 `[low, high]` 中奇數的數量。

範例推導：

- low = 3, high = 7： `(7 + 1) / 2 - (3 / 2)` = `8 / 2 - 1` = `4 - 1` = `3` → 對

時間複雜度：O(1)

空間複雜度：O(1)

優點：

- 常數時間計算，簡潔、效率高。

缺點/注意事項：

- 若語言中的整數會溢位（例如 high = Int32.MaxValue），則 `high + 1` 可能溢位，必要時改用更高精度型別（long / int64）。

### 方法二：暴力枚舉 (CountOdds2)
- 核心概念：從 `low` 走訪到 `high`，每個數檢查 `i % 2 != 0` 並累加。

Pseudocode:

```text
count = 0
for i = low to high:
    if i % 2 != 0:
        count++
return count
```

時間複雜度：O(n)（n = high - low + 1）

空間複雜度：O(1)

優點：
- 實作簡單、直覺且易於驗證。適用於小範圍或教學演示。

缺點：
- 當區間很大時效率太差，不適合在 LeetCode 的大型測資中使用。

### 方法三：前綴計數 + 位移 (CountOdds3)
- 核心概念：定義 Pre(x) = `[0, x]` 區間內奇數的數量（ = `(x + 1) / 2`），因此結果為 `Pre(high) - Pre(low - 1)`。
- 位移技巧：`(x + 1) / 2` 可改寫為 `(x + 1) >> 1`（位移運算）以在某些語言中略微優化速度，但在 C# 中一樣等價於整數除法。

程式碼要點：
- `Pre(x)` 需要在 `low = 0`、`low = 1`、`high` 很大等邊界情況下正確運作。

時間複雜度：O(1)

空間複雜度：O(1)

優點：
- 簡潔且常數時間完成。

缺點：
- 與方法一相似，需注意溢位（x + 1）在極端數值時的處理。

---

## 三種方法比較

| 方法 | 時間複雜度 | 空間複雜度 | 優點 | 缺點 | 適用情境 |
| --- | ---: | ---: | --- | --- | --- |
| 數學公式 (`CountOdds`) | O(1) | O(1) | 最簡潔、最快速 | 需注意溢位 | 一般最推薦使用，適合所有 LeetCode 測資 |
| 前綴 + 位移 (`CountOdds3`) | O(1) | O(1) | 清楚地表達前綴思想、等價於數學公式 | 需注意溢位 | 也是常用常數時間方法，相較 `CountOdds` 顯示前綴語意|
| 暴力列舉 (`CountOdds2`) | O(high-low) | O(1) | 易於理解與驗證 | 效能較差，區間大時不適用 | 教學、驗證或小輸入範例使用 |

---

## 執行與測試（在本地用 .NET / C#）

1. 建構（建立）專案：

```bash
# 在專案根目錄執行（適用於 macOS、Linux、Windows 的 Git Bash 或相容 shell）
dotnet build leetcode_1523/leetcode_1523.csproj -c Debug
```

2. 執行：

```bash
dotnet run --project leetcode_1523/leetcode_1523.csproj --configuration Debug
```

這個專案在 `Program.cs` 中有內建的測試範例（許多小範例），執行時會在主控台印出三個實作的結果，方便比較。

---

## 參考資料

- 題目連結（LeetCode）: [https://leetcode.com/problems/count-odd-numbers-in-an-interval-range/](https://leetcode.com/problems/count-odd-numbers-in-an-interval-range/)
- 中文題目連結（LeetCode CN）: [https://leetcode.cn/problems/count-odd-numbers-in-an-interval-range/](https://leetcode.cn/problems/count-odd-numbers-in-an-interval-range/)

---

## 備註

- 如果要避免整數溢位問題，請把 `int` 更換為 `long`（例如 `long`、`Int64`），並相對更改公式中的運算順序以避免 overflow 的中間結果。
- 在 `Program.cs` 中的 `CountOdds` 與 `CountOdds3` 都是 O(1) 的常數時間方法，實務上可直接選用 `CountOdds`，程式簡潔且可讀。

---

Happy coding! ✅
