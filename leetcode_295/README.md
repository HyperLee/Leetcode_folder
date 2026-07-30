# LeetCode 295 — Find Median from Data Stream

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Language](https://img.shields.io/badge/language-C%23-239120?logo=csharp)

以 .NET 10 實作「資料流的中位數」。本專案使用最大堆與最小堆，在資料持續加入時維持兩側平衡，讓每次加入的時間複雜度為 `O(log n)`，查詢中位數則為 `O(1)`。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法：雙優先佇列](#解法雙優先佇列)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)
- [驗證案例與實際輸出](#驗證案例與實際輸出)

## 題目說明

中位數是排序後整數列表的中間值：

- 元素數量為奇數時，中位數是正中央的元素。
- 元素數量為偶數時，中位數是中央兩個元素的平均值。

需要實作 `MedianFinder`：

- `MedianFinder()`：建立空的資料結構。
- `AddNum(int num)`：將一個整數加入資料流。
- `FindMedian()`：回傳目前所有元素的中位數。

官方範例依序加入 `1`、`2`、`3`：

1. 加入 `1` 後，排序結果為 `[1]`，中位數為 `1.0`。
2. 加入 `2` 後，排序結果為 `[1, 2]`，中位數為 `1.5`。
3. 加入 `3` 後，排序結果為 `[1, 2, 3]`，中位數為 `2.0`。

題目連結：[295. Find Median from Data Stream](https://leetcode.com/problems/find-median-from-data-stream/description/)

## 限制條件

- `-10^5 <= num <= 10^5`
- 呼叫 `FindMedian()` 前，資料結構中至少有一個元素。
- `AddNum()` 與 `FindMedian()` 的呼叫總數最多為 `5 * 10^4`。
- 回傳結果與正確答案的誤差在 `10^-5` 以內即可接受。

## 解題概念與出發點

如果每加入一個數字就重新排序全部資料，第 `n` 次加入可能需要 `O(n log n)`；當資料流持續增長且中位數被頻繁查詢時，會重複做大量不必要的排序工作。

真正需要維護的資訊只有排序後中央附近的元素，因此可以把資料分成兩半：

- 較小的一半：只需要快速取得其中最大值。
- 較大的一半：只需要快速取得其中最小值。

這正好對應最大堆與最小堆。兩個堆不必完整呈現排序後陣列，只要維持分界與數量平衡，就能直接由堆頂求出中位數。

| 方法 | 加入數字 | 查詢中位數 | 額外空間 | 本專案是否採用 |
| --- | --- | --- | --- | --- |
| 每次查詢前完整排序 | 視儲存方式而定 | `O(n log n)` | `O(n)` | 否 |
| 維護有序陣列 | `O(n)` | `O(1)` | `O(n)` | 否 |
| 最大堆 + 最小堆 | `O(log n)` | `O(1)` | `O(n)` | 是 |

## 解法：雙優先佇列

### 資料結構

- `maxHeap` 是最大堆，保存較小的一半；堆頂是這一半的最大值。
- `minHeap` 是最小堆，保存較大的一半；堆頂是這一半的最小值。

程式固定維持兩個不變量：

1. `maxHeap.Count == minHeap.Count`，或 `maxHeap.Count == minHeap.Count + 1`。
2. 當兩個堆都有元素時，`maxHeap.Peek() <= minHeap.Peek()`。

因此：

- 奇數筆資料時，最大堆比最小堆多一個元素，中位數就是 `maxHeap.Peek()`。
- 偶數筆資料時，兩堆大小相同，中位數是兩個堆頂的平均值。

### `AddNum(int num)` 的設計

#### 兩堆大小相同

加入前共有偶數筆資料，加入後最大堆應多一個元素：

1. 先把新數字放進 `minHeap`。
2. 從 `minHeap` 取出最小值。
3. 把該值放進 `maxHeap`。

新數字先經過最小堆篩選，可確保搬到最大堆的是所有候選值中較小的值，繼續維持左右兩半的順序。

#### 最大堆多一個元素

加入前共有奇數筆資料，加入後兩堆應恢復相同大小：

1. 先把新數字放進 `maxHeap`。
2. 從 `maxHeap` 取出最大值。
3. 把該值放進 `minHeap`。

新數字先經過最大堆篩選，可確保搬到最小堆的是所有候選值中較大的值。

### `FindMedian()` 的設計

- `maxHeap.Count > minHeap.Count`：資料筆數為奇數，回傳最大堆頂。
- 兩堆大小相同：資料筆數為偶數，回傳兩個堆頂的平均值。

題目保證呼叫 `FindMedian()` 前至少加入過一個元素，因此不需要定義空資料流的中位數。

### 複雜度

令 `n` 為目前已加入的資料筆數：

- `AddNum`：每次只執行固定次數的堆加入與移除，時間複雜度為 `O(log n)`。
- `FindMedian`：只讀取一或兩個堆頂，時間複雜度為 `O(1)`。
- 空間複雜度：所有元素分別保存在兩個堆中，合計為 `O(n)`。

## 範例演示流程

以下以官方資料流 `[1, 2, 3]` 說明。表格中的堆內容以方便理解的排序形式表示，不代表 `PriorityQueue` 內部陣列順序。

| 步驟 | 加入 | `maxHeap`（較小一半） | `minHeap`（較大一半） | 判斷 | 中位數 |
| ---: | ---: | --- | --- | --- | ---: |
| 1 | `1` | `[1]` | `[]` | 奇數筆，取最大堆頂 | `1.0` |
| 2 | `2` | `[1]` | `[2]` | 偶數筆，平均兩個堆頂 | `1.5` |
| 3 | `3` | `[2, 1]` | `[3]` | 奇數筆，取最大堆頂 | `2.0` |

詳細搬移過程：

1. 初始兩堆大小相同。將 `1` 放入最小堆後再取出，移到最大堆。
2. 最大堆目前多一個元素。將 `2` 放入最大堆，再把最大值 `2` 移到最小堆。
3. 兩堆再次同大小。將 `3` 放入最小堆，再把其中最小值 `2` 移到最大堆。

即使輸入順序改為遞減、包含重複值或混合正負數，相同的搬移規則仍會維持兩個不變量。

## 專案結構

```text
leetcode_295/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_295/
    ├── Program.cs
    └── leetcode_295.csproj
```

- `Program.cs`：包含 `MedianFinder` 雙堆實作與可執行驗證案例。
- `leetcode_295.csproj`：目標框架為 `net10.0` 的主控台專案。
- `docs/readme-template.md`：本 README 使用的初始文件指引。

## 建置與執行

需求：安裝支援 `net10.0` 的 .NET SDK。

從此 repository 根目錄執行：

```bash
dotnet restore leetcode_295/leetcode_295.csproj
dotnet build leetcode_295/leetcode_295.csproj --nologo
dotnet run --project leetcode_295/leetcode_295.csproj --no-build
```

本專案目前沒有獨立的自動化測試專案。驗收方式是成功建置，並執行 `Main` 中的固定案例，確認 21 次 Expected/Actual 比較全部通過。

## 驗證案例與實際輸出

| 案例 | 資料流 | 驗證重點 |
| --- | --- | --- |
| 官方範例 | `[1, 2, 3]` | 基本奇偶筆數切換 |
| 遞減資料 | `[5, 4, 3, 2, 1]` | 新值持續進入較小一半 |
| 重複值 | `[2, 2, 2, 2]` | 相等元素與穩定中位數 |
| 負數 | `[-5, -1, -3]` | 全負數排序與平均 |
| 正負混合 | `[-10, 0, 10, 20]` | 跨越零的奇偶中位數 |
| 題目上下界 | `[-100000, 100000]` | 合法輸入邊界 |

以下輸出來自實際執行 `dotnet run --project leetcode_295/leetcode_295.csproj --no-build`：

```text
LeetCode 295: Find Median from Data Stream
==========================================

案例 1：官方範例
資料流：[1, 2, 3]
步驟 1：加入 1 | Expected: 1.0 | Actual: 1.0 | PASS
步驟 2：加入 2 | Expected: 1.5 | Actual: 1.5 | PASS
步驟 3：加入 3 | Expected: 2.0 | Actual: 2.0 | PASS

案例 2：遞減資料
資料流：[5, 4, 3, 2, 1]
步驟 1：加入 5 | Expected: 5.0 | Actual: 5.0 | PASS
步驟 2：加入 4 | Expected: 4.5 | Actual: 4.5 | PASS
步驟 3：加入 3 | Expected: 4.0 | Actual: 4.0 | PASS
步驟 4：加入 2 | Expected: 3.5 | Actual: 3.5 | PASS
步驟 5：加入 1 | Expected: 3.0 | Actual: 3.0 | PASS

案例 3：重複值
資料流：[2, 2, 2, 2]
步驟 1：加入 2 | Expected: 2.0 | Actual: 2.0 | PASS
步驟 2：加入 2 | Expected: 2.0 | Actual: 2.0 | PASS
步驟 3：加入 2 | Expected: 2.0 | Actual: 2.0 | PASS
步驟 4：加入 2 | Expected: 2.0 | Actual: 2.0 | PASS

案例 4：負數
資料流：[-5, -1, -3]
步驟 1：加入 -5 | Expected: -5.0 | Actual: -5.0 | PASS
步驟 2：加入 -1 | Expected: -3.0 | Actual: -3.0 | PASS
步驟 3：加入 -3 | Expected: -3.0 | Actual: -3.0 | PASS

案例 5：正負混合
資料流：[-10, 0, 10, 20]
步驟 1：加入 -10 | Expected: -10.0 | Actual: -10.0 | PASS
步驟 2：加入 0 | Expected: -5.0 | Actual: -5.0 | PASS
步驟 3：加入 10 | Expected: 0.0 | Actual: 0.0 | PASS
步驟 4：加入 20 | Expected: 5.0 | Actual: 5.0 | PASS

案例 6：題目上下界
資料流：[-100000, 100000]
步驟 1：加入 -100000 | Expected: -100000.0 | Actual: -100000.0 | PASS
步驟 2：加入 100000 | Expected: 0.0 | Actual: 0.0 | PASS

總結：21/21 項驗證通過
```

提交前可再執行：

```bash
git diff --check
```
