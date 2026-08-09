# LeetCode 3488：距離最小相等元素查詢

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-console-239120?logo=csharp)

這個專案以 C# 示範 LeetCode 3488「Closest Equal Element Queries」。程式保留「索引分組＋二分搜尋」解法，另提供「預先計算每個索引的最近距離」解法。`Main` 會執行六組固定案例，對兩種方法做共 12 項可重跑驗證。

## 快速連結

- [題目說明](#題目說明)
- [核心觀察與出發點](#核心觀察與出發點)
- [兩種解法比較](#兩種解法比較)
- [解法一：索引分組＋二分搜尋](#解法一索引分組二分搜尋)
- [解法二：預先計算每個索引的距離](#解法二預先計算每個索引的距離)
- [測試設計](#測試設計)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一個環形陣列 `nums` 與查詢陣列 `queries`。對每個查詢索引 `queries[i]`，需要找到另一個索引 `j`，使得：

```text
nums[j] == nums[queries[i]]
```

並求出兩個索引在環形陣列上的最小距離。如果查詢位置的值在陣列中只出現一次，答案為 `-1`。

- [LeetCode 英文題目](https://leetcode.com/problems/closest-equal-element-queries/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/closest-equal-element-queries/description/)

### 環形距離

陣列長度為 `n`，索引 `a` 與 `b` 之間的環形距離為：

```text
directDistance = abs(a - b)
circularDistance = min(directDistance, n - directDistance)
```

例如 `n = 7`時，索引 `0` 與 `5` 的直線距離是 `5`，但反方向跨越首尾只需要 `2` 步，所以環形距離為 `2`。

### 限制條件

- `1 <= nums.length <= 10^5`
- `1 <= nums[i] <= 10^6`
- `1 <= queries.length <= 10^5`
- `0 <= queries[i] < nums.length`

### 官方範例

| 輸入 | 輸出 | 說明 |
|---|---|---|
| `nums = [1,3,1,4,1,3,2]`, `queries = [0,3,5]` | `[2,-1,3]` | 索引 0 最近的值 1 在索引 2；值 4 只出現一次；索引 5 可跨越首尾到索引 1。 |
| `nums = [1,2,3,4]`, `queries = [0,1,2,3]` | `[-1,-1,-1,-1]` | 所有值都只出現一次。 |

## 核心觀察與出發點

### 1. 不能為每筆查詢掃描整個陣列

`nums` 與 `queries` 的長度都可達 `10^5`。如果每筆查詢都重新掃描 `nums`，最壞時間複雜度是 O(nq)，可能進行 `10^10` 次比較。因此，必須先整理相同值的位置。

### 2. 只需要考慮同值位置的左右鄰居

將某個值出現的索引由小到大排列。對其中一個位置而言，往左遇到的第一個同值元素，以及往右遇到的第一個同值元素，就是唯一可能成為最近距離的兩個候選。

### 3. 環形首尾也是鄰居

對排序後位置清單的第一個元素，它的左鄰居是清單最後一個元素繞回前一圈；對最後一個元素，它的右鄰居則是第一個元素繞到後一圈。兩種解法都利用 `-n` 與 `+n` 的虛擬座標來統一處理這個邊界。

## 兩種解法比較

令 `n = nums.Length`、`q = queries.Length`。

| 方法 | API | 預處理 | 每筆查詢 | 總時間 | 額外空間 |
|---|---|---:|---:|---:|---:|
| 索引分組＋二分搜尋 | `SolveQueries` | O(n) | O(log n) | O(n + q log n) | O(n) |
| 預先計算距離 | `SolveQueriesByPrecomputedDistances` | O(n) | O(1) | O(n + q) | O(n) |

兩種 API 都不會修改 `nums`，但都會將答案就地寫回 `queries` 並回傳同一個陣列。

> [!IMPORTANT]
> 如果同一組查詢需要呼叫兩種方法做比較，必須分別傳入 `queries.ToArray()`，否則第一種方法寫回的答案會污染第二種方法的查詢索引。

## 解法一：索引分組＋二分搜尋

### 設計出發點

每筆查詢只關心「與查詢位置相同的值」。因此先建立 `Dictionary<int, List<int>>`，將數值對應到它出現的所有索引。由於建表時從左到右掃描 `nums`，每個位置清單天然已是升冪排列，不需要額外排序。

### 執行步驟

1. 遍歷 `nums`，建立「值 → 所有出現索引」的雜湊表。
2. 對每個位置清單：
   - 在頭部加入 `lastPosition - n`。
   - 在尾部加入 `firstPosition + n`。
3. 取出 `nums[queries[i]]` 對應的位置清單。
4. 如果加入兩個虛擬位置後的清單長度為 3，原本只有一個真實位置，答案是 `-1`。
5. 否則以二分搜尋找到查詢索引，比較它與左右鄰居的距離。

### 官方範例演示

輸入：

```text
nums    = [1, 3, 1, 4, 1, 3, 2]
queries = [0, 3, 5]
n = 7
```

建立位置分組：

```text
1 -> [0, 2, 4]
3 -> [1, 5]
4 -> [3]
2 -> [6]
```

在頭尾加入環形虛擬位置：

```text
1 -> [-3, 0, 2, 4, 7]
3 -> [-2, 1, 5, 8]
4 -> [-4, 3, 10]
2 -> [-1, 6, 13]
```

- 查詢索引 `0`：值為 `1`，在 `[-3,0,2,4,7]` 中找到 `0`。左距離為 `0 - (-3) = 3`，右距離為 `2 - 0 = 2`，答案為 `2`。
- 查詢索引 `3`：值為 `4`，位置清單加入虛擬位置後長度是 3，表示值 `4` 只出現一次，答案為 `-1`。
- 查詢索引 `5`：值為 `3`，在 `[-2,1,5,8]` 中找到 `5`。左距離為 `5 - 1 = 4`，右距離為 `8 - 5 = 3`，答案為 `3`。

最後將 `[2,-1,3]` 寫回 `queries`。

### 正確性說明

位置清單已按索引排序。從查詢位置向左或向右移動時，各方向遇到的第一個同值元素必然不比更遠的同值元素差。虛擬頭尾位置又將跨越邊界的鄰居轉成直線鄰居，所以左右距離的較小值必然是正確答案。

### 優缺點

- 優點：只對查詢真正需要的同值位置做搜尋；二分搜尋的設計直觀。
- 缺點：每筆查詢都要二分搜尋；當查詢數量很多時，會重複定位同一個索引。

## 解法二：預先計算每個索引的距離

### 設計出發點

某個索引的最近相同元素距離不會因查詢順序而改變。與其在每筆查詢中重新二分搜尋，可以先一次算好 `distanceByIndex[index]`，之後每筆查詢只需陣列存取。

### 執行步驟

1. 與第一種解法相同，先建立「值 → 所有出現索引」的雜湊表。
2. 建立長度為 `n` 的 `distanceByIndex`。
3. 如果某個值只有一個位置，將該位置設為 `-1`。
4. 如果有兩個以上位置，對組內每個索引求出：
   - 前驅：組內前一個位置；第一個位置則使用 `lastPosition - n`。
   - 後繼：組內後一個位置；最後一個位置則使用 `firstPosition + n`。
   - 將左右距離的較小值寫入 `distanceByIndex`。
5. 以 `distanceByIndex[queries[i]]` 在 O(1) 時間回答每筆查詢。

### 官方範例演示

使用相同輸入：

```text
nums    = [1, 3, 1, 4, 1, 3, 2]
queries = [0, 3, 5]
```

先建立位置分組：

```text
1 -> [0, 2, 4]
3 -> [1, 5]
4 -> [3]
2 -> [6]
```

逐組預先計算：

- 值 `1`、位置 `[0,2,4]`：
  - 索引 0 到左鄰居 `4 - 7 = -3` 的距離為 3，到右鄰居 2 的距離為 2，記錄 2。
  - 索引 2 的左右距離都是 2，記錄 2。
  - 索引 4 到左鄰居 2 的距離為 2，到右鄰居 `0 + 7 = 7` 的距離為 3，記錄 2。
- 值 `3`、位置 `[1,5]`：兩個位置直線距離為 4，跨越首尾的距離為 3，兩者都記錄 3。
- 值 `4` 與值 `2` 各只出現一次，對應位置記錄 `-1`。

因此完整的預計算陣列為：

```text
index:            [0, 1, 2,  3, 4, 5,  6]
distanceByIndex:  [2, 3, 2, -1, 2, 3, -1]
```

查詢 `[0,3,5]` 只需取出 `distanceByIndex[0]`、`distanceByIndex[3]` 與 `distanceByIndex[5]`，得到 `[2,-1,3]`。

### 正確性說明

對每個同值索引組，排序順序中的前驅與後繼分別是往左、往右最先遇到的同值元素。首尾使用 `-n` 與 `+n` 後，同樣包含跨越邊界的最近鄰居。所以 `distanceByIndex` 在預處理後已對每個索引儲存正確答案，查詢階段只是讀取該答案。

### 優缺點

- 優點：每個真實位置只處理一次，查詢為 O(1)，總時間複雜度降為 O(n + q)。
- 優點：重複查詢同一索引時不會重複搜尋或計算。
- 缺點：即使只查詢少數索引，仍會先計算所有 `n` 個位置的答案。

## 測試設計

專案沒有獨立測試專案，因此使用 `Main` 作為可重跑的驗收入口。每組案例都以人工推導的字面陣列作為 Expected，並為兩種會覆寫輸入的 API 各建立一份 `queries` 副本。若有任一項不符，程序將設定非零結束碼。

| 案例 | 驗證重點 |
|---|---|
| 官方範例 1 | 同時包含多次出現、單次出現與跨首尾最近距離。 |
| 官方範例 2 | 所有值都唯一，答案應全為 `-1`。 |
| 環形首尾為最近位置 | 索引 0 與最後索引在環形上距離為 1。 |
| 只有兩個相同元素 | 最小陣列中的前驅與後繼處理。 |
| 多組交錯重複值 | 雜湊分組彼此獨立，並正確處理多個查詢。 |
| 最大長度的相同元素 | `n = 100000` 時的處理與長陣列輸出縮寫。 |

## 專案結構

```text
leetcode_3488/
|-- README.md
|-- SOLUTION.md
|-- leetcode_3488.sln
`-- leetcode_3488/
    |-- Program.cs
    `-- leetcode_3488.csproj
```

## 建置與執行

需要安裝支援 `net10.0` 的 .NET 10 SDK。以下命令都從此題的 repository 根目錄執行。

還原相依套件：

```bash
dotnet restore leetcode_3488/leetcode_3488.csproj
```

建置：

```bash
dotnet build leetcode_3488/leetcode_3488.csproj --no-restore --nologo
```

執行固定案例：

```bash
dotnet run --no-build --project leetcode_3488/leetcode_3488.csproj
```

檢查 C# 格式與 Git 差異空白：

```bash
dotnet format leetcode_3488/leetcode_3488.csproj --verify-no-changes --no-restore
git diff --check
```

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_3488/leetcode_3488.csproj` 的實際輸出：

```text
LeetCode 3488 - Closest Equal Element Queries
兩種解法對照驗證

案例 1：官方範例 1
nums = [1, 3, 1, 4, 1, 3, 2]
queries = [0, 3, 5]
SolveQueries: Expected = [2, -1, 3], Actual = [2, -1, 3] => PASS
SolveQueriesByPrecomputedDistances: Expected = [2, -1, 3], Actual = [2, -1, 3] => PASS

案例 2：官方範例 2：全部唯一值
nums = [1, 2, 3, 4]
queries = [0, 1, 2, 3]
SolveQueries: Expected = [-1, -1, -1, -1], Actual = [-1, -1, -1, -1] => PASS
SolveQueriesByPrecomputedDistances: Expected = [-1, -1, -1, -1], Actual = [-1, -1, -1, -1] => PASS

案例 3：環形首尾為最近位置
nums = [1, 2, 3, 1]
queries = [0, 3]
SolveQueries: Expected = [1, 1], Actual = [1, 1] => PASS
SolveQueriesByPrecomputedDistances: Expected = [1, 1], Actual = [1, 1] => PASS

案例 4：只有兩個相同元素
nums = [7, 7]
queries = [0, 1]
SolveQueries: Expected = [1, 1], Actual = [1, 1] => PASS
SolveQueriesByPrecomputedDistances: Expected = [1, 1], Actual = [1, 1] => PASS

案例 5：多組交錯重複值
nums = [1, 2, 1, 2, 1, 2]
queries = [0, 1, 2, 5]
SolveQueries: Expected = [2, 2, 2, 2], Actual = [2, 2, 2, 2] => PASS
SolveQueriesByPrecomputedDistances: Expected = [2, 2, 2, 2], Actual = [2, 2, 2, 2] => PASS

案例 6：最大長度的相同元素
nums = [9, 9, 9, 9, 9, 9, ..., 9, 9, 9, 9, 9, 9] (length = 100000)
queries = [0, 50000, 99999]
SolveQueries: Expected = [1, 1, 1], Actual = [1, 1, 1] => PASS
SolveQueriesByPrecomputedDistances: Expected = [1, 1, 1], Actual = [1, 1, 1] => PASS

總結：12/12 項測試通過
```