# leetcode_1518

題目：1518. Water Bottles

原題連結：

- [LeetCode - English](https://leetcode.com/problems/water-bottles/)
- [LeetCode - 中文](https://leetcode.cn/problems/water-bottles/)

簡短描述

給定兩個整數 `numBottles`（初始滿瓶數）與 `numExchange`（兌換所需的空瓶數），每喝完一瓶滿水會得到一個空瓶。當你累積到 `numExchange` 個空瓶時，可以在商店用這些空瓶換成一瓶滿的水。請回傳你最多可以喝到的水瓶數。

此專案包含一個以 C# 撰寫的簡潔模擬解法，程式檔案為 `leetcode_1518/Program.cs`。

## 重點摘要

- 類型：模擬（Simulation）/ 貪婪。
- 核心想法：追蹤「空瓶數」隨時間的變化，重複兌換與飲用直到無法再兌換為止。
- 時間複雜度：$O(k)$，其中 $k$ 為實際喝到的總瓶數（迴圈每次至少縮減空瓶數 $numExchange-1$）。
- 空間複雜度：$O(1)$。

## 詳細解法說明（中文）

### 直觀思路

一開始有 `numBottles` 瓶滿水。當你喝掉一瓶滿水時，你會得到一個空瓶，因此初始時可以視為你已經喝掉了 `numBottles` 瓶，並且擁有 `numBottles` 個空瓶。

接下來，只要空瓶數 `empty` 大於等於 `numExchange`，你就可以用 `numExchange` 個空瓶換 1 瓶滿水。換到的新滿瓶喝掉之後又會得到新的空瓶，可以繼續參與後續兌換。重複這個過程直到 `empty < numExchange`。

用簡單的遞迴或迴圈模擬即可：

1. totalDrank += numBottles；empty += numBottles
2. while empty >= numExchange:
   - newFull = empty / numExchange（整數除法，代表可兌換到幾瓶）
   - totalDrank += newFull
   - empty = (empty % numExchange) + newFull（兌換後剩下的空瓶 + 喝掉新滿瓶後得到的空瓶）
3. 回傳 totalDrank。

### 為什麼這個模擬是正確的？

每次兌換我們都採取最大可能的兌換數（整數除法），沒有任何延遲或保存空瓶的策略能夠增加最終喝到的瓶數，因為空瓶只能在整數倍的 `numExchange` 才能兌換成新瓶；把可兌換的空瓶分散到後面也不會提高總數。因此貪婪地每次兌換最多瓶數是最優的。

另外，狀態可以由一個整數 `empty` 完全描述：手上目前有多少空瓶可用。把整個過程視為對 `empty` 一次次的變換，直到無法再前進。

### 邊界情況

- `numBottles == 0`：沒有初始的滿瓶，結果為 0。
- `numExchange <= 1`：理論上若 `numExchange == 1`，表示一個空瓶就能換一瓶，會導致無限循環；但依題目條件 `numExchange >= 2`，在此不處理無限的情況。若輸入不受限，可在程式加上保護判斷直接回傳 `int.MaxValue` 或其他處理。

## 程式碼逐行說明（對應 `leetcode_1518/Program.cs` 的 `NumWaterBottles`）

程式採用命名清楚、註解充分的方式撰寫。以下為重點說明（以程式內註解為主）：

- 初始化：
  - `int empty = 0;` 用來追蹤目前手上的空瓶數。
  - `int totalDrank = 0;` 用來累計已喝到的瓶數。

- 先把初始滿瓶喝掉（就等於你直接把初始數量加入結果與空瓶）：
  - `totalDrank += numBottles;`
  - `empty += numBottles;`

- 主要迴圈：`while (empty >= numExchange)`
  - `int newFull = empty / numExchange;`：使用整數除法取得可兌換的新瓶數。
  - `totalDrank += newFull;`：喝掉新兌換到的那幾瓶。
  - `empty = (empty % numExchange) + newFull;`：剩餘空瓶為未被用來兌換的空瓶（餘數）加上喝掉新瓶後得到的空瓶數。

最後回傳 `totalDrank`。

### 範例推演（numBottles=9, numExchange=3）

- 初始：totalDrank=9, empty=9
- 迴圈一：newFull=9/3=3 -> totalDrank=12；empty = 9%3 + 3 = 0 + 3 = 3
- 迴圈二：newFull=3/3=1 -> totalDrank=13；empty = 3%3 + 1 = 0 + 1 = 1
- 無法繼續（empty=1 < numExchange），結果為 13。

## 複雜度分析

- 時間複雜度：$O(k)$，其中 $k$ 為實際喝到的瓶數（也可以視為迴圈執行次數乘以常數操作）。每次迭代至少使得 `empty` 減少 $numExchange - 1$（因為交換後 `empty` = (empty % numExchange) + newFull，newFull = floor(empty/numExchange)，整體會往下收斂）。因此實際執行次數非常少，遠小於線性到初始瓶數的上界時常。

- 空間複雜度：$O(1)$，只使用固定數量的變數來追蹤狀態。

## 其他解法 / 推論

- 數學推導：可以把問題視為不斷把空瓶轉換為新瓶與消耗，最終結果等價於初始瓶數加上從空瓶累積兌換得到的瓶數。模擬法已足夠簡潔且高效。
- 若想要少些迴圈，可用一個公式變形或遞迴式，但程式上回傳的複雜度並不會比模擬顯著改善。

## 如何執行（在此專案）

此專案是以 .NET (C#) 撰寫，請在包含 `leetcode_1518.sln` 的工作目錄下執行：

```bash
# 進入範例專案資料夾
cd leetcode_1518

# 建構
dotnet build

# 執行
dotnet run
```

程式會在主程式內跑幾組示範測試並印出結果。

## 檔案清單

- `leetcode_1518/Program.cs`：C# 範例實作與測試。

## 注意事項

> [!note]
>
> - 本題目假設 `numExchange >= 2`。如果輸入不合法（例如 `numExchange <= 1`），請在呼叫前檢查或在函式內加入額外保護。
> - 本 README 已依專案現況撰寫，不包含授權（LICENSE）或貢獻指南等，這些會另置於專案根目錄的專用檔案。

---

若你想要，我可以：

- 把這份 README 同步加入到 git，而不自動做 commit（由你決定）。
- 將 `Program.cs` 加入更多測試案例或單元測試檔案。
