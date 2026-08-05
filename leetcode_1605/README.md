# LeetCode 1605：給定行和列的和求可行矩陣

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/10.0)

本專案以 .NET 10 console application 示範 LeetCode 1605。給定每一列與每一欄的總和，程式建立一個符合條件的非負整數矩陣。題目允許回傳任意一個合法矩陣，因此同一組輸入可能有多種正確答案。

專案目前提供兩種可直接執行與比較的貪婪解法：

- `RestoreMatrix`：依矩陣格子順序逐格填入。
- `RestoreMatrix2`：以雙指標只追蹤尚未完成的列與欄。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：逐格貪婪](#解法一逐格貪婪)
- [解法二：雙指標貪婪](#解法二雙指標貪婪)
- [兩種解法比較](#兩種解法比較)
- [可執行測試資料](#可執行測試資料)
- [執行方式](#執行方式)
- [專案結構](#專案結構)

## 題目說明

官方題目：

- [LeetCode 英文題目](https://leetcode.com/problems/find-valid-matrix-given-row-and-column-sums/)
- [LeetCode 中文題目](https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/description/)

給定兩個非負整數陣列：

- `rowSum[i]` 代表答案矩陣第 `i` 列所有元素的總和。
- `colSum[j]` 代表答案矩陣第 `j` 欄所有元素的總和。

請建立一個尺寸為 `rowSum.Length x colSum.Length` 的非負整數矩陣，使得：

1. 每一列的總和等於對應的 `rowSum[i]`。
2. 每一欄的總和等於對應的 `colSum[j]`。
3. 所有矩陣元素都大於或等於 `0`。

例如：

```text
rowSum = [3, 8]
colSum = [4, 7]
```

可以建立：

```text
[
  [3, 0],
  [1, 7]
]
```

列和為 `[3, 8]`，欄和為 `[4, 7]`，所以這是一個合法答案。

## 限制條件

以下為題目保證的輸入範圍：

| 條件 | 範圍或要求 |
| --- | --- |
| `rowSum.Length` | `1 <= rowSum.Length <= 500` |
| `colSum.Length` | `1 <= colSum.Length <= 500` |
| 每個總和 | `0 <= rowSum[i], colSum[j] <= 10^8` |
| 可行性 | `sum(rowSum) == sum(colSum)` |
| 矩陣元素 | 必須是非負整數 |

因為空陣列不符合題目限制，所以 executable harness 不加入空輸入案例；改以單一儲存格、零總和列欄與不同矩陣形狀測試邊界行為。

## 解題概念與出發點

### 1. 只看「剩餘需求」

一開始每一列與每一欄都有一個尚未滿足的總和。處理矩陣位置 `(i, j)` 時，真正重要的不是原始總和，而是目前剩下的：

```text
rowRemaining = rowSum[i]
columnRemaining = colSum[j]
```

這一格最多只能放入兩者中較小的數值：

```text
value = min(rowRemaining, columnRemaining)
```

放入後，同時從列和欄的剩餘需求扣除 `value`。

### 2. 為什麼可以貪婪地取最小值

取最小值有三個重要效果：

1. 不會超過目前列的剩餘需求。
2. 不會超過目前欄的剩餘需求。
3. 至少有一方會剛好歸零，表示該列或欄已經完成。

因此不需要回溯或猜測後續配置。題目又保證所有列和的總和等於所有欄和的總和，持續消耗剩餘需求後，就能完成一個合法矩陣。

### 3. 這份實作的輸入行為

`RestoreMatrix` 與 `RestoreMatrix2` 都會直接修改傳入的 `rowSum`、`colSum`，把已分配的數值扣除。成功完成時，這兩個工作陣列會被消耗到全為 `0`。

如果呼叫端還需要保留原始總和，請先複製輸入。程式中的 harness 對每一個解法都使用獨立複本，避免第一個解法的扣減結果影響第二個解法。

## 解法一：逐格貪婪

### 設計方式

`RestoreMatrix` 先配置 `m x n` 的矩陣，再以兩層迴圈依序處理每一個 `(i, j)`：

```text
建立 m x n 的全零矩陣

for 每一列 i:
    for 每一欄 j:
        value = min(rowSum[i], colSum[j])
        matrix[i][j] = value
        rowSum[i] -= value
        colSum[j] -= value
```

即使某一列或某一欄已經歸零，逐格版本仍會走過剩下的矩陣位置，這些位置會填入 `0`。這種寫法直接、容易對照矩陣座標，也保留了原本專案的基準解法。

### 範例演示

輸入：

```text
rowSum = [3, 8]
colSum = [4, 7]
```

處理順序如下：

| 位置 | 剩餘列和 | 剩餘欄和 | 填入值 | 更新後的需求 |
| --- | --- | --- | --- | --- |
| `(0, 0)` | `3` | `4` | `min(3, 4) = 3` | 第 0 列剩 `0`，第 0 欄剩 `1` |
| `(0, 1)` | `0` | `7` | `0` | 第 0 列維持 `0`，第 1 欄維持 `7` |
| `(1, 0)` | `8` | `1` | `min(8, 1) = 1` | 第 1 列剩 `7`，第 0 欄剩 `0` |
| `(1, 1)` | `7` | `7` | `min(7, 7) = 7` | 第 1 列與第 1 欄都完成 |

結果：

```text
[
  [3, 0],
  [1, 7]
]
```

### 複雜度

- 時間：`O(m x n)`，逐一處理所有矩陣位置。
- 結果空間：`O(m x n)`，需要回傳完整矩陣。
- 額外工作空間：除了輸入陣列與回傳矩陣外為 `O(1)`。

## 解法二：雙指標貪婪

### 設計方式

`RestoreMatrix2` 使用：

- `rowIndex`：目前尚未完成的列。
- `columnIndex`：目前尚未完成的欄。

每次迴圈先跳過已經歸零的列或欄，只在兩者都有剩餘需求時填值：

```text
rowIndex = 0
columnIndex = 0

while rowIndex 尚未超出列數 且 columnIndex 尚未超出欄數:
    如果目前列剩餘為 0，rowIndex 向後移動
    如果目前欄剩餘為 0，columnIndex 向後移動

    value = min(目前列剩餘, 目前欄剩餘)
    matrix[rowIndex][columnIndex] = value
    扣除目前列與欄的剩餘需求

    如果列歸零，rowIndex 向後移動
    如果欄歸零，columnIndex 向後移動
```

這個版本的重點不是改變貪婪規則，而是把「哪一列或欄還需要處理」明確保存為指標。當一方完成後，另一方可以直接與下一個未完成的索引配對。

### 範例演示

仍使用：

```text
rowSum = [3, 8]
colSum = [4, 7]
```

雙指標流程如下：

1. `rowIndex = 0`、`columnIndex = 0`，分配 `min(3, 4) = 3`；第 0 列完成，所以 `rowIndex` 移到 `1`。
2. 目前為第 1 列與第 0 欄，分配 `min(8, 1) = 1`；第 0 欄完成，所以 `columnIndex` 移到 `1`。
3. 目前為第 1 列與第 1 欄，分配 `min(7, 7) = 7`；兩個需求同時完成。
4. 得到與第一種解法相同的合法矩陣：

```text
[
  [3, 0],
  [1, 7]
]
```

### 複雜度

- 指標分配決策：最多讓列指標與欄指標各向後移動一次，因此為 `O(m + n)`。
- 總時間：`O(m x n)`，因為仍需配置並回傳完整的 `m x n` 矩陣。
- 結果空間：`O(m x n)`。
- 額外工作空間：除了輸入陣列與回傳矩陣外為 `O(1)`。

## 兩種解法比較

| 比較項目 | `RestoreMatrix` | `RestoreMatrix2` |
| --- | --- | --- |
| 核心策略 | 逐格取兩個剩餘總和的最小值 | 只追蹤尚未完成的列與欄，再取最小值 |
| 控制流程 | 固定走過所有 `m x n` 位置 | 由列、欄指標控制有效分配位置 |
| 是否修改輸入 | 是，會扣減 `rowSum`、`colSum` | 是，會扣減 `rowSum`、`colSum` |
| 總時間複雜度 | `O(m x n)` | `O(m x n)`，其中分配決策為 `O(m + n)` |
| 結果空間 | `O(m x n)` | `O(m x n)` |
| 教學重點 | 直接展示逐格貪婪不變量 | 展示「完成一方就移動指標」的狀態機 |

兩種方法的貪婪選擇相同，因此會在本專案的固定案例中產生相同矩陣；差異在於如何表示與推進尚未完成的列欄狀態。

## 可執行測試資料

`Main` 會呼叫 `RunSamples()`，每個案例分別執行兩種解法。驗證內容包括：

- 矩陣列數與欄數是否正確。
- 所有元素是否為非負整數。
- 每一列總和是否等於原始 `rowSum`。
- 每一欄總和是否等於原始 `colSum`。
- 供解法使用的輸入複本是否已扣減為零。

固定案例涵蓋兩個官方範例、單一儲存格、零總和邊界、零列／零欄交錯，以及不同形狀與重複總和。因答案不唯一，harness 驗證矩陣性質，而不是只比較某一個硬編碼矩陣。

目前實際執行輸出如下：

```text

案例：1. 官方範例一
輸入：rowSum = [3, 8], colSum = [4, 7]
解法一：RestoreMatrix（逐格貪婪）
Expected：非負 2 x 2 矩陣，列和 = [3, 8]，欄和 = [4, 7]
Actual：
  [3, 0]
  [1, 7]
Result：PASS
解法二：RestoreMatrix2（雙指標貪婪）
Expected：非負 2 x 2 矩陣，列和 = [3, 8]，欄和 = [4, 7]
Actual：
  [3, 0]
  [1, 7]
Result：PASS

案例：2. 官方範例二
輸入：rowSum = [5, 7, 10], colSum = [8, 6, 8]
解法一：RestoreMatrix（逐格貪婪）
Expected：非負 3 x 3 矩陣，列和 = [5, 7, 10]，欄和 = [8, 6, 8]
Actual：
  [5, 0, 0]
  [3, 4, 0]
  [0, 2, 8]
Result：PASS
解法二：RestoreMatrix2（雙指標貪婪）
Expected：非負 3 x 3 矩陣，列和 = [5, 7, 10]，欄和 = [8, 6, 8]
Actual：
  [5, 0, 0]
  [3, 4, 0]
  [0, 2, 8]
Result：PASS

案例：3. 單一儲存格
輸入：rowSum = [7], colSum = [7]
解法一：RestoreMatrix（逐格貪婪）
Expected：非負 1 x 1 矩陣，列和 = [7]，欄和 = [7]
Actual：
  [7]
Result：PASS
解法二：RestoreMatrix2（雙指標貪婪）
Expected：非負 1 x 1 矩陣，列和 = [7]，欄和 = [7]
Actual：
  [7]
Result：PASS

案例：4. 零總和邊界
輸入：rowSum = [0, 5], colSum = [2, 3]
解法一：RestoreMatrix（逐格貪婪）
Expected：非負 2 x 2 矩陣，列和 = [0, 5]，欄和 = [2, 3]
Actual：
  [0, 0]
  [2, 3]
Result：PASS
解法二：RestoreMatrix2（雙指標貪婪）
Expected：非負 2 x 2 矩陣，列和 = [0, 5]，欄和 = [2, 3]
Actual：
  [0, 0]
  [2, 3]
Result：PASS

案例：5. 多個零列與零欄
輸入：rowSum = [4, 0, 3], colSum = [0, 2, 5]
解法一：RestoreMatrix（逐格貪婪）
Expected：非負 3 x 3 矩陣，列和 = [4, 0, 3]，欄和 = [0, 2, 5]
Actual：
  [0, 2, 2]
  [0, 0, 0]
  [0, 0, 3]
Result：PASS
解法二：RestoreMatrix2（雙指標貪婪）
Expected：非負 3 x 3 矩陣，列和 = [4, 0, 3]，欄和 = [0, 2, 5]
Actual：
  [0, 2, 2]
  [0, 0, 0]
  [0, 0, 3]
Result：PASS

案例：6. 不同形狀與重複總和
輸入：rowSum = [2, 2, 2], colSum = [3, 3]
解法一：RestoreMatrix（逐格貪婪）
Expected：非負 3 x 2 矩陣，列和 = [2, 2, 2]，欄和 = [3, 3]
Actual：
  [2, 0]
  [1, 1]
  [0, 2]
Result：PASS
解法二：RestoreMatrix2（雙指標貪婪）
Expected：非負 3 x 2 矩陣，列和 = [2, 2, 2]，欄和 = [3, 3]
Actual：
  [2, 0]
  [1, 1]
  [0, 2]
Result：PASS

總結：12/12 項測試通過
```

## 執行方式

請從本專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_1605` 執行，使用明確的巢狀 project path：

```bash
dotnet restore leetode_1605/leetode_1605.csproj
dotnet build leetode_1605/leetode_1605.csproj --nologo
dotnet run --project leetode_1605/leetode_1605.csproj
```

本專案目前沒有自動化測試專案，因此建置加上可執行的 `Main` harness 是驗收方式。若要檢查 Git 差異中的多餘空白，執行：

```bash
git diff --check
```

成功執行時，console 會以 exit code `0` 結束，並顯示 `總結：12/12 項測試通過`。

## 專案結構

```text
leetcode_1605/
├── leetode_1605/
│   ├── Program.cs                 # 兩種演算法與可執行案例 harness
│   └── leetode_1605.csproj        # .NET 10 console project
├── docs/
│   └── readme-template.md         # README 初次建立模板
├── leetode_1605.sln
├── AGENTS.md
└── README.md
```

`bin/` 與 `obj/` 是建置產物，應維持未追蹤狀態。
