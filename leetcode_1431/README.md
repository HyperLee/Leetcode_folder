# LeetCode 1431：擁有最多糖果的孩子

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Language](https://img.shields.io/badge/C%23-Console-239120?logo=csharp)

這是一個可直接執行的 .NET 10 Console 教學專案。程式保留兩種既有線性解法，並加入逐一比較的直覺暴力法，方便觀察「先取得全域最大值」如何把重複比較從 `O(n²)` 降為 `O(n)`。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：兩趟線性掃描](#解法一兩趟線性掃描)
- [解法二：建立加糖後陣列](#解法二建立加糖後陣列)
- [解法三：逐一比較所有孩子](#解法三逐一比較所有孩子)
- [解法比較](#解法比較)
- [可執行驗證設計](#可執行驗證設計)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

有 `n` 位孩子，第 `i` 位孩子原本有 `candies[i]` 顆糖果。現在有 `extraCandies` 顆額外糖果；對每一位孩子分別假設「把全部額外糖果都給他」，判斷此時他的糖果數是否能成為所有孩子中的最大值。

回傳長度為 `n` 的布林清單：

- 第 `i` 個結果為 `true`：第 `i` 位孩子加糖後可達到最多糖果數。
- 第 `i` 個結果為 `false`：第 `i` 位孩子加糖後仍少於某位孩子。

題目允許多位孩子並列最多，因此「剛好追平」也必須回傳 `true`。

題目連結：[LeetCode 1431 - Kids With the Greatest Number of Candies](https://leetcode.com/problems/kids-with-the-greatest-number-of-candies/)

### 範例

```text
candies = [2, 3, 5, 1, 3]
extraCandies = 3
```

原本最多糖果數是 5。逐位加上 3 後會得到 `[5, 6, 8, 4, 6]`；其中只有 4 小於原本最大值 5，因此答案為：

```text
[true, true, true, false, true]
```

## 限制條件

- `n == candies.Length`
- `2 <= n <= 100`
- `1 <= candies[i] <= 100`
- `1 <= extraCandies <= 50`
- 三個公開方法皆依題目保證接收合法輸入，不另外處理空陣列或範圍外數值。
- 三個方法都不修改呼叫端傳入的 `candies`。

## 解題概念與出發點

### 關鍵判斷

若原本最大糖果數為 `maximumCandies`，第 `i` 位孩子取得全部額外糖果後的數量為：

```text
candies[i] + extraCandies
```

他能成為最多糖果者的充要條件是：

```text
candies[i] + extraCandies >= maximumCandies
```

比較對象仍是「其他孩子原本的糖果數」，因為每次假設只把額外糖果交給目前正在判斷的孩子。其他孩子不會同時取得額外糖果。

### 為什麼先找最大值？

最直覺的方法是：每判斷一位孩子，就和所有孩子逐一比較。這樣每位孩子最多比較 `n` 次，總計 `O(n²)`。

不過所有候選孩子面對的比較目標都相同：只要能追平原始陣列的最大值，就一定不會輸給任何人。因此可以先用一趟掃描取得最大值，再用第二趟掃描完成全部判斷，把時間降為 `O(n)`。

## 解法一：兩趟線性掃描

對應方法：`KidsWithCandies`

### 設計出發點

這是直接利用關鍵判斷的標準解法。第一趟只負責取得原始最大值；第二趟把每位孩子加上 `extraCandies`，並與最大值比較。它不需要建立加糖後的完整陣列，因此除回傳結果外只使用一個最大值變數。

### 演算法流程

1. 以第一個元素初始化 `maximumCandies`。
2. 掃描其餘元素，持續更新原始最大值。
3. 再掃描一次 `candies`。
4. 對每個 `candyCount` 判斷 `candyCount + extraCandies >= maximumCandies`。
5. 依原順序加入布林結果並回傳。

### 範例演示

輸入為 `candies = [2, 3, 5, 1, 3]`、`extraCandies = 3`。

第一趟掃描最大值：

| 讀取值 | 掃描後最大值 |
|---:|---:|
| 2 | 2 |
| 3 | 3 |
| 5 | 5 |
| 1 | 5 |
| 3 | 5 |

第二趟判斷：

| 孩子索引 | 原有糖果 | 加糖後 | 是否 `>= 5` | 結果 |
|---:|---:|---:|---|---|
| 0 | 2 | 5 | 是 | `true` |
| 1 | 3 | 6 | 是 | `true` |
| 2 | 5 | 8 | 是 | `true` |
| 3 | 1 | 4 | 否 | `false` |
| 4 | 3 | 6 | 是 | `true` |

最終回傳 `[true, true, true, false, true]`。

### 複雜度

- 時間：`O(n)`，兩次線性掃描仍為線性時間。
- 額外空間：`O(1)`，不計必須回傳的布林清單。

## 解法二：建立加糖後陣列

對應方法：`KidsWithCandies2`

### 設計出發點

這個版本先把「每位孩子若取得全部額外糖果」的結果具體存成 `candiesWithExtra`。它把資料轉換與布林判斷分成兩個清楚階段，適合用來觀察中間狀態；代價是多使用一個長度為 `n` 的陣列。

原始最大值只計算一次。若在每次判斷時重複呼叫 `Max()`，整體會不必要地退化成 `O(n²)`。

### 演算法流程

1. 計算原始陣列的 `maximumCandies`。
2. 建立長度相同的 `candiesWithExtra`。
3. 將 `candies[i] + extraCandies` 寫入新陣列的第 `i` 格。
4. 掃描新陣列，判斷每個值是否大於或等於 `maximumCandies`。
5. 依原順序回傳布林結果。

### 範例演示

輸入仍為 `candies = [2, 3, 5, 1, 3]`、`extraCandies = 3`。

1. 原始最大值為 `5`。
2. 建立加糖後陣列：

   ```text
   [2 + 3, 3 + 3, 5 + 3, 1 + 3, 3 + 3]
   = [5, 6, 8, 4, 6]
   ```

3. 每個新值和 `5` 比較：

   ```text
   [5 >= 5, 6 >= 5, 8 >= 5, 4 >= 5, 6 >= 5]
   = [true, true, true, false, true]
   ```

原始 `candies` 不會被覆寫；所有加糖後的資料都放在新陣列中。

### 複雜度

- 時間：`O(n)`，計算最大值、建立新陣列與產生答案皆為線性操作。
- 額外空間：`O(n)`，`candiesWithExtra` 需要保存所有中間結果。

## 解法三：逐一比較所有孩子

對應方法：`KidsWithCandies3`

### 設計出發點

這是最貼近題目文字的直覺方法：選定一位候選孩子，把額外糖果全部給他，再逐一確認他的總數是否至少等於每位孩子原本的糖果數。它不先計算全域最大值，因此會重複做許多比較，但能清楚展示最佳化前的基準思路。

### 演算法流程

1. 依序選擇每位孩子作為候選者。
2. 計算 `candidateTotal = candidateCandyCount + extraCandies`。
3. 從頭掃描所有孩子的原始糖果數。
4. 若發現 `candidateTotal` 小於任一原始糖果數，立即標記為 `false` 並停止此候選者的比較。
5. 若完整掃描都沒有落後，結果為 `true`。

### 範例演示

輸入仍為 `candies = [2, 3, 5, 1, 3]`、`extraCandies = 3`。

| 候選索引 | 候選總數 | 逐一比較重點 | 結果 |
|---:|---:|---|---|
| 0 | 5 | `5` 不小於 `2、3、5、1、3` | `true` |
| 1 | 6 | `6` 不小於所有原始值 | `true` |
| 2 | 8 | `8` 不小於所有原始值 | `true` |
| 3 | 4 | 比較到原始值 `5` 時失敗並提前停止 | `false` |
| 4 | 6 | `6` 不小於所有原始值 | `true` |

最終仍得到 `[true, true, true, false, true]`，但最壞情況下每位候選者都必須掃描完整陣列。

### 複雜度

- 時間：`O(n²)`，外層選擇 `n` 位候選者，內層最多再比較 `n` 次。
- 額外空間：`O(1)`，不計回傳結果；只保存目前候選總數與判斷狀態。

## 解法比較

| 解法 | 核心策略 | 時間 | 額外空間 | 輸入是否修改 | 教學重點 |
|---|---|---:|---:|---|---|
| `KidsWithCandies` | 先找最大值，再直接產生答案 | `O(n)` | `O(1)` | 否 | 標準且最節省空間 |
| `KidsWithCandies2` | 先建立每位孩子加糖後的陣列 | `O(n)` | `O(n)` | 否 | 將資料轉換與判斷分離 |
| `KidsWithCandies3` | 每位候選者逐一比較所有孩子 | `O(n²)` | `O(1)` | 否 | 從直覺暴力法理解線性最佳化 |

在實務提交時，解法一最直接，也同時具備最佳漸進時間與較低額外空間。解法二適合需要保留或展示中間轉換資料的情境；解法三則主要作為教學比較，不是本題的效能首選。

## 可執行驗證設計

`Main` 對三種解法執行相同的 6 組資料。每次呼叫都使用獨立輸入副本，並同時驗證：

1. `Actual` 是否與手工列出的 `Expected` 相同。
2. 解法執行後的輸入是否與執行前完全一致。

| 案例 | `candies` | `extraCandies` | 預期結果 | 驗證目的 |
|---|---|---:|---|---|
| 最小邊界 | `[1, 1]` | 1 | `[true, true]` | 最小長度與最小數值 |
| 官方範例一 | `[2, 3, 5, 1, 3]` | 3 | `[true, true, true, false, true]` | 一般混合結果 |
| 官方範例二 | `[4, 2, 1, 1, 2]` | 1 | `[true, false, false, false, false]` | 額外糖果不足以追平 |
| 官方範例三 | `[12, 1, 12]` | 10 | `[true, false, true]` | 重複最大值 |
| 剛好追平最大值 | `[1, 2]` | 1 | `[true, true]` | 驗證 `>=` 而不是 `>` |
| 最大數值邊界 | `[100, 1, 100]` | 50 | `[true, false, true]` | 最大糖果值與最大額外糖果 |

總計為 `6 個案例 × 3 種解法 = 18` 項檢查。任一結果錯誤或修改輸入時，該項會顯示 `FAIL`，程式也會設定非零結束碼。

## 建置與執行

請從本 README 所在的 repository root 執行：

```bash
dotnet restore leetcode_1431/leetcode_1431.csproj
dotnet build leetcode_1431/leetcode_1431.csproj --nologo
dotnet run --no-build --project leetcode_1431/leetcode_1431.csproj
```

專案沒有獨立的自動化測試專案；目前以成功建置及 `Main` 的 18 項自我檢查作為行為驗收。

## 實際執行結果

以下內容來自執行 `dotnet run --no-build --project leetcode_1431/leetcode_1431.csproj`：

<!-- RUN-OUTPUT-START -->
```text
LeetCode 1431 - Kids With the Greatest Number of Candies

案例：最小邊界
Input: candies = [1, 1], extraCandies = 1
KidsWithCandies | Expected: [true, true] | Actual: [true, true] | Input preserved: True | PASS
KidsWithCandies2 | Expected: [true, true] | Actual: [true, true] | Input preserved: True | PASS
KidsWithCandies3 | Expected: [true, true] | Actual: [true, true] | Input preserved: True | PASS

案例：官方範例一
Input: candies = [2, 3, 5, 1, 3], extraCandies = 3
KidsWithCandies | Expected: [true, true, true, false, true] | Actual: [true, true, true, false, true] | Input preserved: True | PASS
KidsWithCandies2 | Expected: [true, true, true, false, true] | Actual: [true, true, true, false, true] | Input preserved: True | PASS
KidsWithCandies3 | Expected: [true, true, true, false, true] | Actual: [true, true, true, false, true] | Input preserved: True | PASS

案例：官方範例二
Input: candies = [4, 2, 1, 1, 2], extraCandies = 1
KidsWithCandies | Expected: [true, false, false, false, false] | Actual: [true, false, false, false, false] | Input preserved: True | PASS
KidsWithCandies2 | Expected: [true, false, false, false, false] | Actual: [true, false, false, false, false] | Input preserved: True | PASS
KidsWithCandies3 | Expected: [true, false, false, false, false] | Actual: [true, false, false, false, false] | Input preserved: True | PASS

案例：官方範例三
Input: candies = [12, 1, 12], extraCandies = 10
KidsWithCandies | Expected: [true, false, true] | Actual: [true, false, true] | Input preserved: True | PASS
KidsWithCandies2 | Expected: [true, false, true] | Actual: [true, false, true] | Input preserved: True | PASS
KidsWithCandies3 | Expected: [true, false, true] | Actual: [true, false, true] | Input preserved: True | PASS

案例：剛好追平最大值
Input: candies = [1, 2], extraCandies = 1
KidsWithCandies | Expected: [true, true] | Actual: [true, true] | Input preserved: True | PASS
KidsWithCandies2 | Expected: [true, true] | Actual: [true, true] | Input preserved: True | PASS
KidsWithCandies3 | Expected: [true, true] | Actual: [true, true] | Input preserved: True | PASS

案例：最大數值邊界
Input: candies = [100, 1, 100], extraCandies = 50
KidsWithCandies | Expected: [true, false, true] | Actual: [true, false, true] | Input preserved: True | PASS
KidsWithCandies2 | Expected: [true, false, true] | Actual: [true, false, true] | Input preserved: True | PASS
KidsWithCandies3 | Expected: [true, false, true] | Actual: [true, false, true] | Input preserved: True | PASS

Summary: 18/18 checks passed.
```
<!-- RUN-OUTPUT-END -->

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1431.sln
└── leetcode_1431/
    ├── Program.cs
    └── leetcode_1431.csproj
```

- `Program.cs`：三種演算法、格式化輔助函式及可執行驗收案例。
- `docs/readme-template.md`：README 的建立與驗證準則。