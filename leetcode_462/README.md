# LeetCode 462：最小操作次數使陣列元素相等 II

![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![LeetCode 462 Medium](https://img.shields.io/badge/LeetCode-462%20Medium-FFA116?logo=leetcode&logoColor=white)

給定整數陣列 `nums`，每次操作可以將任一元素遞增或遞減 `1`。本專案示範兩種找出最少操作次數的方法：排序後取中位數，以及使用 Quickselect 選出中位數。

[LeetCode 題目](https://leetcode.com/problems/minimum-moves-to-equal-array-elements-ii/description/) · [執行與驗證](#執行與驗證)

## 題目限制

- `1 <= nums.length <= 10^5`
- `-10^9 <= nums[i] <= 10^9`
- 最少操作次數保證可用 32 位元整數表示。

## 解題概念與出發點

若要把所有元素變成目標值 `x`，所需操作次數是每個元素到 `x` 的距離總和：

```text
sum(|nums[i] - x|)
```

這個總和在中位數處達到最小值。把數字排序後，奇數個元素的中間值是中位數；偶數個元素時，中間兩值之間的任一目標都能得到最小總和。本題使用 `nums[nums.Length / 2]`，也就是偶數長度時的右側中位數。

兩種解法都會原地重排輸入陣列。範例執行器會為每種解法複製輸入，確保兩者各自處理相同資料。

## 解法一：排序後取中位數

### 設計步驟

1. 使用 `Array.Sort` 將 `nums` 由小到大排序。
2. 取索引 `nums.Length / 2` 的元素作為中位數。
3. 逐一計算每個元素與中位數的絕對差，並加總後回傳。

排序讓中位數可直接由索引取得；其後的線性掃描計算將所有元素移至該值所需的操作數。`Array.Sort` 的時間複雜度為 `O(n log n)`。[Microsoft 文件](https://learn.microsoft.com/en-us/dotnet/api/system.array.sort?view=net-10.0)

### 範例演示

輸入 `[1, 2, 3]`：

1. 排序後仍為 `[1, 2, 3]`。
2. 中位數是 `nums[3 / 2] = 2`。
3. 距離總和為 `|1 - 2| + |2 - 2| + |3 - 2| = 1 + 0 + 1 = 2`。

輸入 `[1, 10, 2, 9]`：

1. 排序後為 `[1, 2, 9, 10]`。
2. 中位數是 `nums[4 / 2] = 9`。
3. 距離總和為 `|1 - 9| + |2 - 9| + |9 - 9| + |10 - 9| = 8 + 7 + 0 + 1 = 16`。

### 複雜度

- 時間：`O(n log n)`，由排序主導。
- 額外空間：`O(log n)`，用於排序實作的呼叫堆疊；不另建排序陣列。

## 解法二：三向分割 Quickselect

### 設計步驟

1. 設目標索引 `k = nums.Length / 2`，沿用排序解法選取的右側中位數。
2. 在目前搜尋範圍取中間索引的值作為 `pivot`，將範圍分成小於、等於、大於 `pivot` 的三段。
3. 若 `k` 位於小於或大於的區段，只在該區段繼續分割；若落在等值區段，`nums[k]` 即為中位數。等值區段可一次處理重複值。
4. 找到中位數後，掃描整個陣列並加總絕對差。

Quickselect 不必排好全部元素，只需反覆保留包含目標索引的區段。此實作固定取目前範圍的中間索引作為樞紐，因此演示流程可重現；平均時間為 `O(n)`，最壞情況仍為 `O(n²)`。

### 範例演示

輸入 `[1, 2, 3]`，目標索引 `k = 1`：

1. 中間索引的樞紐值為 `2`。
2. 分割成小於 `[1]`、等於 `[2]`、大於 `[3]`。
3. `k` 落在等值區段，因此中位數為 `2`；距離總和是 `1 + 0 + 1 = 2`。

輸入 `[1, 10, 2, 9]`，目標索引 `k = 2`：

1. 目前範圍的中間索引值為 `10`。分割後，小於區段為 `[1, 2, 9]`、等於區段為 `[10]`；`k` 位於小於區段，接著只搜尋該區段。
2. `[1, 2, 9]` 的中間索引值為 `2`。分割成 `[1]`、`[2]`、`[9]`，而 `k` 位於大於區段。
3. 剩餘區段只含 `9`，所以中位數為 `9`。距離總和是 `8 + 7 + 0 + 1 = 16`。

### 複雜度

- 平均時間：`O(n)`；最壞時間：`O(n²)`。
- 額外空間：`O(1)`，分割直接在輸入陣列上進行。

## 解法比較

| 解法 | 中位數取得方式 | 時間複雜度 | 額外空間 | 輸入陣列 |
| --- | --- | --- | --- | --- |
| 排序 | 完整排序後取索引 `n / 2` | `O(n log n)` | `O(log n)` | 原地排序 |
| Quickselect | 三向分割至索引 `n / 2` 落在等值區段 | 平均 `O(n)`，最壞 `O(n²)` | `O(1)` | 原地分割 |

## 執行與驗證

在儲存庫根目錄執行：

```powershell
dotnet build .\leetcode_462\leetcode_462.csproj
dotnet run --project .\leetcode_462\leetcode_462.csproj
```

此專案沒有獨立測試專案。執行程式會以兩種解法比對五組內建案例：題目範例、重複值、單一元素與負數輸入；任一結果不符預期時會拋出例外。成功時輸出如下：

```text
Input: [1, 2, 3] | Expected: 2 | MinMoves2: 2 | MinMoves2QuickSelect: 2
Input: [1, 10, 2, 9] | Expected: 16 | MinMoves2: 16 | MinMoves2QuickSelect: 16
Input: [7, 7, 7] | Expected: 0 | MinMoves2: 0 | MinMoves2QuickSelect: 0
Input: [-1000000000] | Expected: 0 | MinMoves2: 0 | MinMoves2QuickSelect: 0
Input: [-7, -3, -1] | Expected: 6 | MinMoves2: 6 | MinMoves2QuickSelect: 6
All 5 examples passed.
```

## 專案結構

- `leetcode_462/Program.cs`：兩種解法與內建範例執行器。
- `leetcode_462/leetcode_462.csproj`：目標為 .NET 10 的主控台專案。
- `docs/readme-template.md`：README 撰寫參考範本。
