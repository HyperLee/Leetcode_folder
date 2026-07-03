# LeetCode 274 - H-Index

使用 .NET Console 專案整理 LeetCode 274 `H-Index` 的三種常見解法：排序、二分搜尋、計數桶。專案將題解、可執行範例與驗證輸出集中在 `leetcode_274/Program.cs`，方便直接 build、run，並對照每一種解法的設計思路。

## 題目連結

- [LeetCode 274. H-Index](https://leetcode.com/problems/h-index/description/)
- [LeetCode CN 274. H 指数](https://leetcode.cn/problems/h-index/description/)

## 題目說明

給定一個整數陣列 `citations`，其中 `citations[i]` 表示研究者第 `i` 篇論文的引用次數，請回傳這位研究者的 `h-index`。

`h-index` 的定義是：

- 至少有 `h` 篇論文
- 而且這 `h` 篇論文每一篇都至少被引用 `h` 次
- 要求的是滿足條件的最大 `h`

例如 `citations = [3, 0, 6, 1, 5]`：

- 至少有 `3` 篇論文被引用至少 `3` 次，成立
- 但沒有至少 `4` 篇論文被引用至少 `4` 次，不成立
- 所以答案是 `3`

## 限制條件

- `1 <= citations.length <= 5000`
- `0 <= citations[i] <= 1000`
- 輸入陣列中的值代表非負整數引用次數

## 解題概念與出發點

這題的核心不是單純找最大值，而是找一個「門檻」：

- 如果我們猜一個 `h`
- 只要能找到至少 `h` 篇論文，它們的引用次數都大於等於 `h`
- 這個 `h` 就是可行答案

因此整體思考方向可以分成三種：

1. 先排序，直接從高引用次數往回驗證能支撐多少篇論文。
2. 把答案 `h` 當成搜尋目標，用二分搜尋判斷某個 `h` 是否可行。
3. 利用 `h-index` 不可能超過論文總數 `n`，把引用次數壓進 `0..n` 的計數桶裡做反向累加。

## 解法比較

| 解法 | 核心想法 | 時間複雜度 | 空間複雜度 | 特性 |
| --- | --- | --- | --- | --- |
| `HIndex` | 排序後由大到小檢查可支撐的篇數 | `O(n log n)` | 依排序實作而定 | 最直觀，但會改動原陣列 |
| `HIndex2` | 對答案 `h` 做二分搜尋 | `O(n log n)` | `O(1)` | 保留原始資料，思路偏「可行性判斷」 |
| `HIndex3` | 用計數桶累積至少被引用 `i` 次的篇數 | `O(n)` | `O(n)` | 時間效率最佳，適合利用題目上界 |

> [!NOTE]
> `HIndex` 會先呼叫 `Array.Sort`，因此會原地改動輸入陣列。`Main` 裡的示例執行會先複製陣列，再分別交給三種解法，避免彼此互相影響。

## 解法一：排序法 `HIndex`

### 設計說明

排序法是最容易直覺理解的版本。

先把 `citations` 由小到大排序後，從陣列尾端開始往前看：

- 最尾端是目前最高引用次數的論文
- 如果它的引用次數大於目前的 `h`
- 代表我們至少還能再多納入一篇符合條件的論文

每成功納入一篇，`h` 就加一。直到遇到某篇論文的引用次數已經無法再支撐更大的 `h`，就停止。

這個方法的關鍵在於：

- 排序後，高引用論文會集中在尾端
- 我們只需要檢查「目前已經累積幾篇高引用論文」
- 不必一一重算每個 `h` 是否成立

### 範例演示流程

以 `citations = [3, 0, 6, 1, 5]` 為例：

1. 排序後得到 `[0, 1, 3, 5, 6]`
2. 從尾端開始，`6 > 0`，所以 `h = 1`
3. 下一個是 `5 > 1`，所以 `h = 2`
4. 下一個是 `3 > 2`，所以 `h = 3`
5. 下一個是 `1 > 3` 不成立，停止
6. 最終答案為 `3`

這種方法的優點是很容易看懂，缺點是需要排序，因此時間複雜度是 `O(n log n)`。

## 解法二：二分搜尋 `HIndex2`

### 設計說明

這個方法把問題改成：

- 假設答案是 `mid`
- 檢查是否至少有 `mid` 篇論文的引用次數大於等於 `mid`

如果成立，代表 `mid` 是可行答案，甚至可以再試更大；
如果不成立，代表 `mid` 太大，答案必須更小。

因為答案一定落在 `0..n` 之間，所以可以直接對答案範圍做二分搜尋，而不是對陣列值做搜尋。

這個方法的重點是：

- 搜的是「答案空間」而不是排序後的位置
- 每次掃描整個陣列，計算有幾篇論文達到 `mid`
- 利用可行與不可行之間的單調性收斂答案

### 範例演示流程

以 `citations = [1, 3, 1]` 為例，`n = 3`：

1. 初始搜尋範圍為 `left = 0`、`right = 3`
2. 取上中位數 `mid = 2`
3. 陣列中只有 `3` 這一篇達到至少 `2` 次引用，所以 `count = 1`
4. 因為 `count < mid`，表示 `h = 2` 不可行，縮小為 `right = 1`
5. 再取 `mid = 1`
6. 三篇論文都至少被引用 `1` 次，所以 `count = 3`
7. 因為 `count >= mid`，表示 `h = 1` 可行，更新 `left = 1`
8. 此時 `left == right == 1`，答案就是 `1`

這種方法保留原陣列、不需排序，但每輪仍要掃描整個陣列，因此總時間複雜度同樣是 `O(n log n)`。

## 解法三：計數桶 `HIndex3`

### 設計說明

`h-index` 最大只可能等於論文總數 `n`，因為不可能有超過 `n` 篇論文同時達標。

因此即使某篇論文被引用了 `100` 次、`1000` 次，對 `h-index` 來說，它的意義都只是：

- 這篇論文至少能支撐到 `n`

基於這個觀察，我們可以建立長度 `n + 1` 的計數桶：

- `counter[i]` 表示恰好有多少篇論文被引用 `i` 次
- 所有 `>= n` 的引用次數都壓到 `counter[n]`

接著從 `n` 反向累加到 `0`：

- 當前累積值表示「至少被引用 `i` 次」的論文數量
- 第一次出現累積篇數大於等於 `i` 的位置，就是最大可行的 `h`

### 範例演示流程

以 `citations = [0, 1, 3, 5, 6]` 為例，`n = 5`：

1. 建立 `counter[0..5]`
2. 將引用次數放入桶子後：
   - `counter[0] = 1`
   - `counter[1] = 1`
   - `counter[3] = 1`
   - `counter[5] = 2`，因為 `5` 與 `6` 都歸到最後一桶
3. 從 `i = 5` 開始反向累加，累積篇數為 `2`，尚未達到 `5`
4. `i = 4` 時累積篇數仍為 `2`，尚未達到 `4`
5. `i = 3` 時再加上 `counter[3]`，累積篇數變成 `3`
6. 因為此時 `3 >= 3`，所以答案就是 `3`

這個方法是三種方案中時間複雜度最好的版本，代價是要使用額外的計數陣列。

## 專案中的可執行示例

`Main` 目前固定執行以下五組案例，並讓三種解法各自輸出結果與 `PASS/FAIL`：

- `[3, 0, 6, 1, 5] -> 3`
- `[1, 3, 1] -> 1`
- `[0] -> 0`
- `[100] -> 1`
- `[0, 1, 3, 5, 6] -> 3`

這些案例涵蓋：

- 題目代表案例
- 小型陣列
- 最小邊界
- 超大引用次數
- 已接近排序完成的分布

## 專案結構

```text
.
├─ README.md
├─ docs/
│  └─ readme-template.md
└─ leetcode_274/
   ├─ Program.cs
   └─ leetcode_274.csproj
```

## 建置與執行

請在 `C:\GitHubFolder\Leetcode_folder\leetcode_274` 這個 repo 目錄下執行：

```powershell
dotnet build .\leetcode_274\leetcode_274.csproj
dotnet run --project .\leetcode_274\leetcode_274.csproj
```

目前沒有獨立的測試專案，因此這題的驗證方式是：

- 確認 `dotnet build` 成功
- 確認 `dotnet run` 能跑出五組案例
- 確認三種解法在每組案例都顯示 `PASS`

## 範例輸出

以下區塊應與目前 `Program.cs` 的實際執行結果一致：

```text
LeetCode 274 - H-Index
================================

Case 1
Input: [3, 0, 6, 1, 5]
Expected: 3
HIndex  (Sort)           : 3 PASS
HIndex2 (Binary Search)  : 3 PASS
HIndex3 (Counting)       : 3 PASS

Case 2
Input: [1, 3, 1]
Expected: 1
HIndex  (Sort)           : 1 PASS
HIndex2 (Binary Search)  : 1 PASS
HIndex3 (Counting)       : 1 PASS

Case 3
Input: [0]
Expected: 0
HIndex  (Sort)           : 0 PASS
HIndex2 (Binary Search)  : 0 PASS
HIndex3 (Counting)       : 0 PASS

Case 4
Input: [100]
Expected: 1
HIndex  (Sort)           : 1 PASS
HIndex2 (Binary Search)  : 1 PASS
HIndex3 (Counting)       : 1 PASS

Case 5
Input: [0, 1, 3, 5, 6]
Expected: 3
HIndex  (Sort)           : 3 PASS
HIndex2 (Binary Search)  : 3 PASS
HIndex3 (Counting)       : 3 PASS
```

## 驗證重點

- `HIndex`、`HIndex2`、`HIndex3` 三種解法都保留在同一個 `Program.cs` 中，方便直接閱讀與比對。
- `RunSampleCase` 會先複製輸入陣列，確保排序法不會污染後續解法的輸入。
- README 中記錄的建置與執行指令，應與實際驗證結果完全一致。
- 完成後還會額外執行 `git diff --check`，確認沒有多餘空白或換行問題。
