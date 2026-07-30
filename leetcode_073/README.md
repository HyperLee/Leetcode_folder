# LeetCode 73：矩陣置零

這個專案使用 C# 與 .NET 10 實作 LeetCode 73「Set Matrix Zeroes」，並以可直接執行的主控台案例比較兩種原地修改矩陣的解法。

- [LeetCode 英文題目](https://leetcode.com/problems/set-matrix-zeroes/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/set-matrix-zeroes/description/)

## 題目說明

給定一個 `m x n` 的整數矩陣 `matrix`。只要原始矩陣中的某個元素是 `0`，就必須把該元素所在的整行與整列全部設為 `0`。

題目要求直接修改傳入的矩陣，也就是使用原地演算法，而不是建立並回傳另一個結果矩陣。

例如：

```text
輸入：
1 1 1
1 0 1
1 1 1

輸出：
1 0 1
0 0 0
1 0 1
```

中間的 `0` 位於第 2 列、第 2 欄，因此該列與該欄都必須變成 `0`。

## 限制條件

- `m == matrix.Length`
- `n == matrix[0].Length`
- `1 <= m, n <= 200`
- `-2^31 <= matrix[i][j] <= 2^31 - 1`
- 輸入至少是 `1 x 1`，且每一列長度相同。
- 兩種公開解法都會直接修改輸入矩陣，沒有回傳值。

## 解題概念與出發點

最直覺的想法，是掃描到 `0` 時立刻把對應行列清成 `0`。但這樣會產生連鎖誤判：

1. 某個元素因為演算法執行而被改成 `0`。
2. 後續掃描無法分辨它是「原始零值」還是「剛剛寫入的零值」。
3. 新的 `0` 又觸發更多行列清零，最後可能把不應改動的位置也清掉。

因此兩種解法都採用相同原則：先完整記錄原始零值會影響哪些行與列，再進行第二階段的清零。差別只在於標記資料存放的位置。

## 解法比較

| 解法 | 標記方式 | 時間複雜度 | 額外空間複雜度 | 特點 |
| --- | --- | --- | --- | --- |
| `SetZeroes` | 借用第一行與第一列 | `O(mn)` | `O(1)` | 符合進階要求，但必須小心保存外框原始狀態 |
| `SetZeroes2` | `row`、`col` 布林陣列 | `O(mn)` | `O(m+n)` | 邏輯直觀、容易理解，但需要額外記憶體 |

## 解法一：第一行與第一列作為標記

### 設計說明

`SetZeroes` 不額外建立與矩陣大小相關的標記陣列，而是借用矩陣本身的第一行與第一列。

若內部位置 `matrix[i][j]` 是 `0`：

- 將 `matrix[i][0]` 設為 `0`，表示第 `i` 列最後需要全部清零。
- 將 `matrix[0][j]` 設為 `0`，表示第 `j` 欄最後需要全部清零。

第一行與第一列既是原始資料，也是標記區，因此必須先使用兩個布林值保存它們原本是否含有 `0`：

- `firstRowZero`：第一行原本是否含有 `0`。
- `firstColZero`：第一列原本是否含有 `0`。

執行順序如下：

1. 掃描第一行，保存 `firstRowZero`。
2. 掃描第一列，保存 `firstColZero`。
3. 從索引 `(1, 1)` 開始掃描內部區域，將影響資訊寫到第一行、第一列。
4. 根據標記清除內部區域。
5. 若 `firstRowZero` 為 `true`，清除第一行。
6. 若 `firstColZero` 為 `true`，清除第一列。

最後兩個步驟不能提前，否則第一行或第一列被清零後，原本的標記資訊會遭到破壞。

### 範例演示

初始矩陣：

```text
1 1 1
1 0 1
1 1 1
```

第一行與第一列原本都沒有 `0`：

```text
firstRowZero = false
firstColZero = false
```

掃描內部時在 `(1, 1)` 發現 `0`，因此將該列的第一個元素與該欄的第一個元素設為標記：

```text
1 0 1
0 0 1
1 1 1
```

接著只看第一行與第一列的標記：

- 第 2 列的列標記是 `0`，所以第 2 列內部全部清零。
- 第 2 欄的欄標記是 `0`，所以第 2 欄內部全部清零。

```text
1 0 1
0 0 0
1 0 1
```

因為兩個外框旗標都是 `false`，最後不需額外清除第一行或第一列，這就是最終結果。

### 正確性與取捨

每一個內部原始零值都會留下列標記與欄標記，因此第二階段一定能清除所有應受影響的位置。第一行、第一列的原始狀態已由兩個布林值獨立保存，所以即使外框兼作標記，也不會遺失它們是否需要清零的資訊。

這個方法只使用固定數量的區域變數，額外空間為 `O(1)`；代價是處理順序較嚴格，閱讀時也需要理解外框同時具有資料與標記兩種角色。

## 解法二：行列布林陣列

### 設計說明

`SetZeroes2` 使用兩個額外陣列：

- `row[i]` 表示第 `i` 列是否需要清零。
- `col[j]` 表示第 `j` 欄是否需要清零。

演算法分成兩次完整掃描：

1. 第一次掃描只尋找原始零值。遇到 `matrix[i][j] == 0` 時，設定 `row[i] = true` 與 `col[j] = true`。
2. 第二次掃描才修改矩陣。只要 `row[i]` 或 `col[j]` 為 `true`，就把 `matrix[i][j]` 設為 `0`。

因為第一次掃描完全不改矩陣，所以收集到的標記只可能來自原始輸入，不會產生連鎖誤判。

### 範例演示

同樣使用：

```text
1 1 1
1 0 1
1 1 1
```

第一次掃描在 `(1, 1)` 發現 `0`，得到：

```text
row = [false, true, false]
col = [false, true, false]
```

第二次掃描逐格判斷：

- 第 2 列因 `row[1]` 為 `true`，整列清零。
- 第 2 欄因 `col[1]` 為 `true`，整欄清零。
- 其他位置的列標記與欄標記都是 `false`，保留原值。

結果為：

```text
1 0 1
0 0 0
1 0 1
```

### 正確性與取捨

每個原始零值都會同時標記其所在的行與列。第二次掃描將所有具有任一標記的位置清零，正好涵蓋題目要求的所有位置，也不會清除沒有受任何原始零值影響的位置。

這個方法的控制流程較簡單，適合先理解題目；但 `row` 與 `col` 的大小會隨矩陣行列數增加，因此額外空間為 `O(m+n)`。

## 可執行驗收案例

`Main` 會讓兩種解法分別在輸入矩陣的深拷貝上執行，再逐列比較實際矩陣與手動定義的預期矩陣。這可避免第一種解法原地修改後，第二種解法取得已被改動的輸入。

目前涵蓋：

1. 內部位置含零。
2. 第一行與第一列含零。
3. 完全沒有零值。
4. `1 x 1` 零值矩陣。
5. 單列矩陣含零。
6. 單欄矩陣含零。

每個案例驗證兩種解法，共有 12 項檢查。

## 專案結構

```text
leetcode_073/
├─ docs/
│  └─ readme-template.md
├─ leetcode_073/
│  ├─ leetcode_073.csproj
│  └─ Program.cs
├─ leetcode_073.sln
└─ README.md
```

## 建置與執行

需求：

- .NET 10 SDK

從此儲存庫目錄執行：

```powershell
dotnet restore leetcode_073/leetcode_073.csproj
dotnet build leetcode_073/leetcode_073.csproj --nologo
dotnet run --project leetcode_073/leetcode_073.csproj
```

## 實際執行結果

以下內容來自 `dotnet run --project leetcode_073/leetcode_073.csproj`：

```text
案例 1：內部零值
Input:
1 1 1
1 0 1
1 1 1
Expected:
1 0 1
0 0 0
1 0 1
SetZeroes Actual:
1 0 1
0 0 0
1 0 1
SetZeroes: PASS
SetZeroes2 Actual:
1 0 1
0 0 0
1 0 1
SetZeroes2: PASS

案例 2：第一行、第一列零值
Input:
0 1 2 0
3 4 5 2
1 3 1 5
Expected:
0 0 0 0
0 4 5 0
0 3 1 0
SetZeroes Actual:
0 0 0 0
0 4 5 0
0 3 1 0
SetZeroes: PASS
SetZeroes2 Actual:
0 0 0 0
0 4 5 0
0 3 1 0
SetZeroes2: PASS

案例 3：無零值
Input:
1 2
3 4
Expected:
1 2
3 4
SetZeroes Actual:
1 2
3 4
SetZeroes: PASS
SetZeroes2 Actual:
1 2
3 4
SetZeroes2: PASS

案例 4：單一零值
Input:
0
Expected:
0
SetZeroes Actual:
0
SetZeroes: PASS
SetZeroes2 Actual:
0
SetZeroes2: PASS

案例 5：單列含零
Input:
1 0 3
Expected:
0 0 0
SetZeroes Actual:
0 0 0
SetZeroes: PASS
SetZeroes2 Actual:
0 0 0
SetZeroes2: PASS

案例 6：單欄含零
Input:
1
0
3
Expected:
0
0
0
SetZeroes Actual:
0
0
0
SetZeroes: PASS
SetZeroes2 Actual:
0
0
0
SetZeroes2: PASS

12/12 passed.
```
