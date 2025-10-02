# leetcode_3100 — 3100. Water Bottles II

建立一本精簡的 README，說明如何建置與執行此練習題專案，以及詳細的解題說明。

## 簡介

此專案為一個使用 C# (.NET) 的控制台範例，實作 LeetCode 題目 3100. "Water Bottles II"。程式位於 `leetcode_3100/Program.cs`，包含：

- `MaxBottlesDrunk(int numBottles, int numExchange)`：用來計算在題目限制下，最多能喝到幾瓶水的主要演算法。

題目連結：

- https://leetcode.com/problems/water-bottles-ii/
- https://leetcode.cn/problems/water-bottles-ii/

## 專案目標

- 提供一個簡潔且可執行的解題實作（含說明與測資）。
- 以實作與範例展示如何模擬「每次以目前 numExchange 值最多兌換一瓶，再將 numExchange++」的規則。

## 建置與執行

此專案使用 .NET（在 workspace 中以 .csproj 管理）。在 macOS 或其他環境上，請先安裝 .NET SDK。

# leetcode_3100 — 3100. Water Bottles II

此專案為 LeetCode 題目 3100（Water Bottles II）的 C# 範例實作。程式碼位於 `leetcode_3100/Program.cs`，重點是 `MaxBottlesDrunk(int numBottles, int numExchange)` 方法，模擬題目中「每次以目前 numExchange 值最多兌換一瓶，兌換後 numExchange++」的規則。

本 README 以中文（繁體）撰寫，包含：快速上手、細節解題說明、演算法證明、範例、複雜度分析與後續建議。

## 快速上手

先確定已安裝 .NET SDK（範例使用 net8.0）。

在專案根目錄執行：

```bash
# 建置
dotnet build ./leetcode_3100/leetcode_3100.csproj

# 執行（會列印內建測資結果）
dotnet run --project ./leetcode_3100/leetcode_3100.csproj
```

程式會在主控台輸出內建測資結果（`Program.Main` 內定義數組），用以驗證演算法行為。

## 題目摘要

給定兩個整數：

- `numBottles`：初始擁有的滿水瓶數量。
- `numExchange`：當前需要的空瓶數以兌換一瓶滿瓶。

操作規則（每次可選擇一項）：

- 喝任意數量的滿瓶（喝後變成空瓶）。
- 用 `numExchange` 個空瓶換一個滿瓶；完成兌換後，`numExchange` 會增加 1。

限制：在同一個 `numExchange` 值下，不能分多批使用相同的 `numExchange` 做多次兌換（亦即每個 numExchange 值在整個過程中最多兌換一次）。

目標：回傳最多可以喝到的水瓶數量。

## 詳細解題說明（深入）

核心觀察：由於 `numExchange` 在每次兌換後都會增加，且同一個 `numExchange` 值只能兌換一次，因此無法直接以「空瓶數整除 numExchange」的方式一次兌換多瓶。正確的做法是模擬每一步：在當前 `numExchange` 值下，若空瓶足夠就兌換 1 瓶，喝掉後更新空瓶數與 `numExchange`，重複直到無法再兌換。

主要變數定義：

- `empty`：目前持有的空瓶數。
- `totalDrank`：累計已喝的瓶數。

算法步驟：

1. 初始化：`totalDrank = numBottles`；`empty = numBottles`（先把初始滿瓶喝掉，得到相同數量的空瓶）。
2. 迴圈：當 `empty >= numExchange` 時執行：
   - `totalDrank += 1`（兌換並喝掉一瓶）。
   - `empty = empty - numExchange + 1`（消耗 `numExchange` 個空瓶換來一瓶，喝掉後得到 1 個空瓶）。
   - `numExchange++`（兌換後限制提升）。
3. 當 `empty < numExchange` 時停止，回傳 `totalDrank`。

直觀理由（為何貪婪每次換 1 瓶是安全的）：

- 若在當前 `numExchange` 有足夠的空瓶可換，先兌換一瓶不會損害未來可能的兌換，因為：
  - 兌換會消耗 `numExchange` 空瓶並補回 1（喝掉後），淨減少 `numExchange - 1` 空瓶；且下一個 `numExchange` 會更大，先兌換能保留更多累積空瓶給後續更大門檻使用。

形式化不動量（invariant）：

- 在任一步之前，`totalDrank` 等於已消耗的滿瓶數，`empty` 等於目前持有的空瓶數；每次操作都不會漏掉可能兌換的機會，演算法會嘗試在每個可用的 `numExchange` 值下兌換一次，因此是完整搜尋（以單向模擬）且將所有可行兌換計入。

終止性：

- 每次兌換會使 `empty` 減少至少 1（因為 `numExchange >= 1`，且淨減 `numExchange - 1`），同時 `numExchange` 增加，因此迴圈不會無限進行，最終 `empty < numExchange` 時終止。

## 範例（逐步說明）

範例：`numBottles = 10, numExchange = 3`

1. 初始：`totalDrank = 10`，`empty = 10`。
2. `empty >= 3`：兌換 1 瓶，`totalDrank = 11`，`empty = 10 - 3 + 1 = 8`，`numExchange = 4`。
3. `empty >= 4`：兌換 1 瓶，`totalDrank = 12`，`empty = 8 - 4 + 1 = 5`，`numExchange = 5`。
4. `empty >= 5`：兌換 1 瓶，`totalDrank = 13`，`empty = 5 - 5 + 1 = 1`，`numExchange = 6`。
5. 現在 `empty = 1 < 6`，停止。答案為 `13`。

其他小例子：

- `numBottles = 3, numExchange = 1`：
  - 初始：`total = 3, empty = 3`
  - numExchange=1：可換一瓶，`total=4, empty=3 - 1 + 1 = 3, numExchange=2`
  - numExchange=2：可換一瓶，`total=5, empty=3 - 2 + 1 = 2, numExchange=3`
  - numExchange=3：可換一瓶，`total=6, empty=2 - 3 + 1 = 0` → 但注意當 empty < 0 不會發生，實作中會在 `empty >= numExchange` 條件下停止，實際模擬會得到正確值（範例中的程式以模擬為主）。

## 複雜度分析

- 時間複雜度：O(k)，k 為實際兌換次數；每次迴圈做常數時間操作。k 最壞情況為 O(numBottles)。
- 空間複雜度：O(1)，僅使用常數數量的整數變數。

## 常見邊界與注意事項

- `numBottles = 0`：直接回傳 0。
- `numExchange <= 0`：題目定義通常為正整數；若不保證，可在方法入口加入參數檢查並拋出 `ArgumentException`。
- `numExchange = 1`：需特別留意數學直覺，但模擬流程會正確處理（因為每次兌換淨減 `0` 空瓶，但會增大門檻，最終終止）。

## 檔案結構

```
Leetcode_folder/
├─ leetcode_3100/
│  ├─ Program.cs         # 本題實作與簡單測資
│  └─ leetcode_3100.csproj
├─ leetcode_3100.sln
└─ README.md             # 這份說明
```

## 測試建議與後續改進

- 建議加入單元測試（xUnit/NUnit）：覆蓋典型案例、邊界案例（0、1、大量）以及題目提供的反例。
- 可把 `MaxBottlesDrunk` 改為 `static`，讓呼叫更直觀。
- 若需要更詳細的 debug，可在模擬迴圈內印出每次兌換前後的 `empty`、`numExchange` 與 `totalDrank`。

---

需要我接著替你：

- 建立 xUnit 測試並新增幾個斷言（例如 `10,3 -> 13`）。
- 把 `MaxBottlesDrunk` 改成 `static` 並更新 `Main` 的呼叫方式。
