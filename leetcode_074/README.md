# LeetCode 74 - Search a 2D Matrix

這個專案使用 C# console 程式實作 LeetCode 74「Search a 2D Matrix / 搜尋二維矩陣」。目標是在符合排序規則的 `m x n` 整數矩陣中判斷 `target` 是否存在，並展示暴力搜尋與二分搜尋兩種解法。

## 題目說明

給定一個 `m x n` 的整數矩陣 `matrix`，且此矩陣具有以下特性：

- 每一列都依照非遞減順序排序。
- 每一列的第一個整數都大於前一列的最後一個整數。

給定一個整數 `target`，如果 `target` 存在於 `matrix` 中則回傳 `true`，否則回傳 `false`。

題目要求撰寫時間複雜度為 `O(log(m * n))` 的解法。

範例：

```text
Input: matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 3
Output: true
```

```text
Input: matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 13
Output: false
```

## 限制條件

LeetCode 原題限制如下：

- `m == matrix.length`
- `n == matrix[i].length`
- `1 <= m, n <= 100`
- `-10^4 <= matrix[i][j], target <= 10^4`
- 每一列都依照非遞減順序排序。
- 每一列的第一個整數都大於前一列的最後一個整數。

目前程式中的兩個方法都假設輸入符合題目限制，也就是矩陣非空且每列至少有一個元素。

## 解題概念與出發點

這題的關鍵在於第二個排序條件：每一列的第一個值都大於前一列最後一個值。因此矩陣雖然是二維資料，但如果依照列由上到下、欄由左到右的順序讀取，整體等價於一個已排序的一維陣列。

例如：

```text
matrix = [
  [1,  3,  5,  7],
  [10, 11, 16, 20],
  [23, 30, 34, 60]
]
```

可以視為：

```text
[1, 3, 5, 7, 10, 11, 16, 20, 23, 30, 34, 60]
```

因此可以先用暴力搜尋理解問題，再用二分搜尋達到題目要求的 `O(log(m * n))`。

## 解法一：暴力搜尋

目前實作位於 `leetcode_074/Program.cs` 的 `SearchMatrix(int[][] matrix, int target)`。

設計說明：

- 使用兩層迴圈依序掃描每一個格子。
- 外層迴圈走訪矩陣的每一列，內層迴圈走訪該列中的每一欄。
- 如果目前元素等於 `target`，立即回傳 `true`。
- 如果全部元素都檢查完仍未找到，回傳 `false`。
- 此方法不利用題目提供的排序特性，因此容易理解，但不符合題目要求的 `O(log(m * n))` 時間複雜度。

複雜度：

- 時間複雜度：`O(m * n)`
- 空間複雜度：`O(1)`

### 範例演示：target = 13

使用矩陣：

```text
[
  [1,  3,  5,  7],
  [10, 11, 16, 20],
  [23, 30, 34, 60]
]
```

暴力搜尋會依序檢查：

| 步驟 | row | column | value | 判斷 |
| --- | ---: | ---: | ---: | --- |
| 1 | 0 | 0 | 1 | `1 != 13`，繼續 |
| 2 | 0 | 1 | 3 | `3 != 13`，繼續 |
| 3 | 0 | 2 | 5 | `5 != 13`，繼續 |
| 4 | 0 | 3 | 7 | `7 != 13`，繼續 |
| 5 | 1 | 0 | 10 | `10 != 13`，繼續 |
| 6 | 1 | 1 | 11 | `11 != 13`，繼續 |
| 7 | 1 | 2 | 16 | `16 != 13`，繼續 |
| 8 | 1 | 3 | 20 | `20 != 13`，繼續 |
| 9 | 2 | 0 | 23 | `23 != 13`，繼續 |
| 10 | 2 | 1 | 30 | `30 != 13`，繼續 |
| 11 | 2 | 2 | 34 | `34 != 13`，繼續 |
| 12 | 2 | 3 | 60 | `60 != 13`，搜尋結束 |

完整掃描後沒有找到 `13`，因此回傳 `false`。

## 解法二：攤平索引的二分搜尋

目前實作位於 `leetcode_074/Program.cs` 的 `SearchMatrix2(int[][] matrix, int target)`。

設計說明：

- 不真的建立新陣列，而是把矩陣「視為」長度 `m * n` 的排序陣列。
- 一維索引範圍為 `0` 到 `m * n - 1`。
- 每次取中間索引 `mid = left + (right - left) / 2`，避免索引加總時可能發生溢位。
- 用 `row = mid / n` 找出 `mid` 對應的矩陣列索引。
- 用 `column = mid % n` 找出 `mid` 對應的矩陣欄索引。
- 若 `matrix[row][column] == target`，回傳 `true`。
- 若目前值大於 `target`，代表目標只可能在攤平順序的左半邊，令 `right = mid - 1`。
- 若目前值小於 `target`，代表目標只可能在攤平順序的右半邊，令 `left = mid + 1`。
- 搜尋區間耗盡後仍未命中，回傳 `false`。

### 為什麼 `row = mid / n`、`column = mid % n` 可以成立

矩陣是依照「由左到右、由上到下」的順序被視為一維陣列。因為每一列固定有 `n` 個元素，所以攤平後可以想成「每 `n` 個元素為一組」，每一組剛好對應原本矩陣的一列。

如果已知矩陣座標是 `matrix[row][column]`，它在攤平後的一維索引會是：

```text
index = row * n + column
```

反過來，已知一維索引 `mid` 時，就可以用除法和餘數把它拆回矩陣座標：

```text
row = mid / n
column = mid % n
```

- `mid / n` 使用整數除法，意思是 `mid` 前面已經完整跨過幾組 `n` 個元素，因此得到列索引 `row`。
- `mid % n` 取得除以 `n` 後的餘數，意思是 `mid` 在目前這一列中往右偏移幾格，因此得到欄索引 `column`。

以範例矩陣來看，`n = 4`，代表每一列有 4 個元素：

| 一維索引 | 矩陣座標 | 值 |
| ---: | --- | ---: |
| 0 | `matrix[0][0]` | 1 |
| 1 | `matrix[0][1]` | 3 |
| 2 | `matrix[0][2]` | 5 |
| 3 | `matrix[0][3]` | 7 |
| 4 | `matrix[1][0]` | 10 |
| 5 | `matrix[1][1]` | 11 |

所以當 `mid = 5` 時：

```text
row = 5 / 4 = 1
column = 5 % 4 = 1
```

因此 `mid = 5` 對應到 `matrix[1][1]`，值是 `11`。

這裡使用 `n` 而不是 `m`，是因為 `n` 代表每一列的欄數，也就是攤平成一維後每幾個元素要換到下一列；`m` 只是總共有幾列，不能用來判斷單列的寬度。

複雜度：

- 時間複雜度：`O(log(m * n))`
- 空間複雜度：`O(1)`

### 範例演示：target = 3

使用矩陣：

```text
[
  [1,  3,  5,  7],
  [10, 11, 16, 20],
  [23, 30, 34, 60]
]
```

矩陣共有 `m = 3` 列、`n = 4` 欄，攤平後的索引範圍是 `0..11`。

| 步驟 | left | right | mid | row = mid / 4 | column = mid % 4 | value | 判斷 |
| --- | ---: | ---: | ---: | ---: | ---: | ---: | --- |
| 1 | 0 | 11 | 5 | 1 | 1 | 11 | `11 > 3`，往左半邊找 |
| 2 | 0 | 4 | 2 | 0 | 2 | 5 | `5 > 3`，往左半邊找 |
| 3 | 0 | 1 | 0 | 0 | 0 | 1 | `1 < 3`，往右半邊找 |
| 4 | 1 | 1 | 1 | 0 | 1 | 3 | `3 == 3`，回傳 `true` |

### 範例演示：target = 13

| 步驟 | left | right | mid | row = mid / 4 | column = mid % 4 | value | 判斷 |
| --- | ---: | ---: | ---: | ---: | ---: | ---: | --- |
| 1 | 0 | 11 | 5 | 1 | 1 | 11 | `11 < 13`，往右半邊找 |
| 2 | 6 | 11 | 8 | 2 | 0 | 23 | `23 > 13`，往左半邊找 |
| 3 | 6 | 7 | 6 | 1 | 2 | 16 | `16 > 13`，往左半邊找 |

此時 `left = 6`、`right = 5`，搜尋區間已耗盡，代表 `13` 不存在於矩陣中，回傳 `false`。

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_074/
│   ├── Program.cs
│   └── leetcode_074.csproj
└── README.md
```

## 建置與執行

從專案根目錄執行：

```bash
dotnet build leetcode_074/leetcode_074.csproj
```

執行內建範例資料：

```bash
dotnet run --project leetcode_074/leetcode_074.csproj
```

目前在本機 macOS/.NET 環境執行時完整輸出：

```text
CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.
SearchMatrix | target exists in first row | target = 3 | expected = True | actual = True | PASS
SearchMatrix2 | target exists in first row | target = 3 | expected = True | actual = True | PASS
SearchMatrix | target is missing between values | target = 13 | expected = False | actual = False | PASS
SearchMatrix2 | target is missing between values | target = 13 | expected = False | actual = False | PASS
SearchMatrix | single cell target exists | target = 1 | expected = True | actual = True | PASS
SearchMatrix2 | single cell target exists | target = 1 | expected = True | actual = True | PASS
SearchMatrix | target below matrix minimum | target = 0 | expected = False | actual = False | PASS
SearchMatrix2 | target below matrix minimum | target = 0 | expected = False | actual = False | PASS
SearchMatrix | target exists at last cell | target = 60 | expected = True | actual = True | PASS
SearchMatrix2 | target exists at last cell | target = 60 | expected = True | actual = True | PASS
```

> [!NOTE]
> 目前沒有獨立測試專案；範例資料放在 `Main` 進入點中，可透過 `dotnet run` 快速驗證主要案例與邊界案例。

## 驗證指令

本專案目前沒有獨立測試專案；使用 console 範例與 Git 空白檢查驗證：

```bash
dotnet build leetcode_074/leetcode_074.csproj
dotnet run --project leetcode_074/leetcode_074.csproj
git diff --check
```
