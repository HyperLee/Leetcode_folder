# LeetCode 1475：商品折扣後的最終價格

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個可直接執行的 .NET 10 主控台教學專案，示範如何用「直接模擬」與「單調堆疊」解決 LeetCode 1475，並以固定案例自動比對 Expected、Actual 與輸入是否保持不變。

## 快速連結

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：直接模擬](#解法一直接模擬)
- [解法二：單調堆疊](#解法二單調堆疊)
- [兩種解法比較](#兩種解法比較)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

給定整數陣列 `prices`，`prices[i]` 代表商店中第 `i` 件商品的價格。

購買第 `i` 件商品時，從它右側尋找第一個索引 `j`，使得：

1. `j > i`
2. `prices[j] <= prices[i]`

若找到，商品 `i` 可獲得 `prices[j]` 的折扣，最終價格為 `prices[i] - prices[j]`；若找不到，最終價格仍為 `prices[i]`。回傳所有商品套用折扣後的價格陣列。

題目連結：[1475. Final Prices With a Special Discount in a Shop](https://leetcode.com/problems/final-prices-with-a-special-discount-in-a-shop/description/)

### 限制條件

- `1 <= prices.length <= 500`
- `1 <= prices[i] <= 1000`

> [!NOTE]
> 本專案另外測試空陣列，確認公開方法能安全回傳空結果。這是本機健全性案例，不是題目正式輸入範圍的一部分。

## 解題概念與出發點

題目最重要的字眼不是「右側最小價格」，而是「右側第一個不高於目前價格的商品」。因此不能只找右側區間的最小值，也不能任意排序；索引順序本身就是答案的一部分。

可以從兩個角度思考：

1. **以目前商品為中心向右找：** 對每個 `i` 逐一檢查 `i + 1`、`i + 2`……，找到第一個符合條件的 `j` 就停止。這就是直接模擬。
2. **以目前掃描到的價格替前面商品結算：** 從左往右讀取價格，保存所有尚未找到折扣的索引。當新價格不高於某些待處理商品時，新價格就是它們遇到的第一個合法折扣。這可以用單調堆疊完成。

兩個公開方法都建立新的結果陣列，不會修改呼叫者傳入的 `prices`。

## 解法一：直接模擬

方法：`FinalPrices(int[] prices)`

### 設計說明

為每件商品執行一次由左至右的搜尋：

1. 建立與輸入等長的結果陣列。
2. 對每個索引 `i`，先假設折扣為 `0`。
3. 從 `j = i + 1` 開始往右掃描。
4. 第一次遇到 `prices[j] <= prices[i]` 時記錄折扣並立刻 `break`。
5. 將 `prices[i] - discount` 寫入結果。

提前停止搜尋非常重要，因為題目要求的是第一個符合條件的 `j`，而不是所有合法價格中的最小值。

### 複雜度

- 時間複雜度：O(n²)。最壞情況下，每件商品都要掃描其右側全部商品。
- 額外空間複雜度：O(n)，用於必須回傳的結果陣列；若不計回傳結果，輔助空間為 O(1)。

### 優缺點

- 優點：流程直接對應題意，容易理解與驗證。
- 缺點：重複掃描右側區間，輸入較大時效率低於單調堆疊。

## 解法二：單調堆疊

方法：`FinalPrices2(int[] prices)`

### 設計說明

堆疊保存「尚未遇到合法折扣」的商品索引，而不是保存已完成的答案。掃描到索引 `i` 時：

1. 查看堆疊頂端索引 `top`。
2. 若 `prices[top] >= prices[i]`，目前價格符合折扣條件。
3. 因為掃描方向由左至右，`i` 必然是 `top` 遇到的第一個合法右側索引；彈出 `top` 並從其結果扣除 `prices[i]`。
4. 重複處理，直到堆疊為空，或頂端價格小於目前價格。
5. 將 `i` 推入堆疊，等待未來商品替它提供折扣。

處理完 `while` 後，堆疊內價格由底至頂嚴格遞增。每個索引最多推入與彈出各一次，因此即使程式中有巢狀 `while`，總操作次數仍是線性的。

### 為何保存索引

保存索引可以同時取得原始價格與答案寫入位置。若只保存價格，遇到重複值時就無法判斷應更新結果陣列中的哪一件商品。

### 複雜度

- 時間複雜度：O(n)。每個索引最多進出堆疊一次。
- 額外空間複雜度：O(n)，包含結果陣列與最壞情況下保存所有索引的堆疊。

### 優缺點

- 優點：消除重複向右搜尋，時間複雜度降為 O(n)。
- 缺點：需要理解「尚未解決的索引」與堆疊單調性，直覺性略低於直接模擬。

## 兩種解法比較

| 項目 | 直接模擬 `FinalPrices` | 單調堆疊 `FinalPrices2` |
| --- | --- | --- |
| 核心觀點 | 每件商品主動向右找折扣 | 目前商品替先前商品結算折扣 |
| 時間複雜度 | O(n²) | O(n) |
| 額外空間 | O(n) 結果；輔助 O(1) | O(n) 結果與堆疊 |
| 是否修改輸入 | 否 | 否 |
| 適合用途 | 初學、直接對照題意 | 效能最佳化、理解 next smaller or equal element |

## 範例演示流程

以下使用 `prices = [8, 4, 6, 2, 3]`，預期結果為 `[4, 2, 4, 2, 3]`。

### 解法一演示：直接模擬

| `i` | 原價 | 向右檢查 | 第一個合法折扣 | 最終價格 |
| ---: | ---: | --- | ---: | ---: |
| 0 | 8 | 4 符合 `4 <= 8` | 4 | 4 |
| 1 | 4 | 6 不符合；2 符合 | 2 | 2 |
| 2 | 6 | 2 符合 `2 <= 6` | 2 | 4 |
| 3 | 2 | 3 不符合 | 0 | 2 |
| 4 | 3 | 右側無商品 | 0 | 3 |

每個 `i` 一旦找到第一個合法折扣就停止，不再檢查更右側的價格。

### 解法二演示：單調堆疊

`result` 初始為輸入副本 `[8, 4, 6, 2, 3]`，堆疊以下列方式變化：

| 掃描索引與價格 | 結算動作 | 堆疊索引（底 → 頂） | `result` |
| --- | --- | --- | --- |
| `0 / 8` | 無待處理商品 | `[0]` | `[8, 4, 6, 2, 3]` |
| `1 / 4` | `8 >= 4`，結算索引 0 | `[1]` | `[4, 4, 6, 2, 3]` |
| `2 / 6` | `4 < 6`，不可結算索引 1 | `[1, 2]` | `[4, 4, 6, 2, 3]` |
| `3 / 2` | 依序結算索引 2、1 | `[3]` | `[4, 2, 4, 2, 3]` |
| `4 / 3` | `2 < 3`，不可結算索引 3 | `[3, 4]` | `[4, 2, 4, 2, 3]` |

最後仍在堆疊中的索引 3、4 沒有遇到合法右側折扣，因此保留原價。

### 其他案例涵蓋

- 嚴格遞增 `[1, 2, 3, 4, 5]`：所有商品都維持原價。
- 重複價格 `[10, 1, 1, 6]`：相等也符合 `<=`，第二件商品可被下一個 `1` 折抵為 `0`。
- 全部重複 `[5, 5, 5]`：前兩件分別使用緊鄰右側的 `5`，結果為 `[0, 0, 5]`。
- 空陣列 `[]`：兩種方法都回傳 `[]`。

## 建置與執行

請從 repository 根目錄執行：

```bash
dotnet restore leetcode_1475/leetcode_1475.csproj
dotnet build leetcode_1475/leetcode_1475.csproj --nologo
dotnet run --no-build --project leetcode_1475/leetcode_1475.csproj
```

目前沒有獨立的自動化測試專案；`Main` 會執行 5 組案例，每組驗證兩種解法的結果及輸入不變契約。任何檢查失敗時，程式會設定非零結束碼。

### 實際執行結果

```text
Case 1: 官方範例一：連續出現可用折扣
Input: [8, 4, 6, 2, 3]
Expected: [4, 2, 4, 2, 3]
FinalPrices Actual: [4, 2, 4, 2, 3]
FinalPrices Result: PASS
FinalPrices Input unchanged: PASS
FinalPrices2 Actual: [4, 2, 4, 2, 3]
FinalPrices2 Result: PASS
FinalPrices2 Input unchanged: PASS

Case 2: 官方範例二：右側價格皆較高
Input: [1, 2, 3, 4, 5]
Expected: [1, 2, 3, 4, 5]
FinalPrices Actual: [1, 2, 3, 4, 5]
FinalPrices Result: PASS
FinalPrices Input unchanged: PASS
FinalPrices2 Actual: [1, 2, 3, 4, 5]
FinalPrices2 Result: PASS
FinalPrices2 Input unchanged: PASS

Case 3: 官方範例三：相同價格可作為折扣
Input: [10, 1, 1, 6]
Expected: [9, 0, 1, 6]
FinalPrices Actual: [9, 0, 1, 6]
FinalPrices Result: PASS
FinalPrices Input unchanged: PASS
FinalPrices2 Actual: [9, 0, 1, 6]
FinalPrices2 Result: PASS
FinalPrices2 Input unchanged: PASS

Case 4: 重複值：每件商品取最近的相同價格
Input: [5, 5, 5]
Expected: [0, 0, 5]
FinalPrices Actual: [0, 0, 5]
FinalPrices Result: PASS
FinalPrices Input unchanged: PASS
FinalPrices2 Actual: [0, 0, 5]
FinalPrices2 Result: PASS
FinalPrices2 Input unchanged: PASS

Case 5: 防禦性案例：空陣列
Input: []
Expected: []
FinalPrices Actual: []
FinalPrices Result: PASS
FinalPrices Input unchanged: PASS
FinalPrices2 Actual: []
FinalPrices2 Result: PASS
FinalPrices2 Input unchanged: PASS

Summary: 20/20 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1475.sln
└── leetcode_1475/
    ├── leetcode_1475.csproj
    └── Program.cs
```