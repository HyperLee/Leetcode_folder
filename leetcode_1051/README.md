# LeetCode 1051 — Height Checker

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)

本專案使用 C# 實作 LeetCode 1051「Height Checker」，提供「複製後排序」與
「計數排序」兩種解法，並內建七組可直接執行、會同時驗證答案與輸入保留性的
測試資料。

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：複製後排序](#解法一複製後排序)
- [解法二：計數排序](#解法二計數排序)
- [兩種解法比較](#兩種解法比較)
- [測試案例](#測試案例)
- [建置與執行](#建置與執行)

## 題目說明

學校要拍攝年度團體照，學生應按照身高由矮到高排成一列。給定整數陣列
`heights`，其中 `heights[i]` 表示索引 `i` 的學生目前身高。

將 `heights` 由小到大排列後可得到預期順序 `expected`。題目要求計算有多少個
索引滿足：

```text
heights[i] != expected[i]
```

只需要回傳位置不同的數量，不需要真的重新排列原始輸入。

### 官方範例一

```text
輸入：heights = [1, 1, 4, 2, 1, 3]
預期順序：       [1, 1, 1, 2, 3, 4]
輸出：3
```

索引 2、4、5 的目前身高與預期身高不同，因此答案是 3。

### 官方範例二

```text
輸入：heights = [5, 1, 2, 3, 4]
預期順序：       [1, 2, 3, 4, 5]
輸出：5
```

五個位置全部不同，因此答案是 5。

### 官方範例三

```text
輸入：heights = [1, 2, 3, 4, 5]
預期順序：       [1, 2, 3, 4, 5]
輸出：0
```

目前順序已符合預期，因此沒有不同的位置。

## 限制條件

依 [LeetCode 1051 官方題面](https://leetcode.com/problems/height-checker/description/)：

- `1 <= heights.length <= 100`
- `1 <= heights[i] <= 100`

本專案另外示範空陣列的防禦性行為；空陣列不是官方正式輸入，但兩種方法都會
自然回傳 0。方法假設輸入陣列不是 `null`，且非空輸入中的高度符合官方值域。

## 解題概念與出發點

題目不是問「有多少位學生需要移動」，而是問目前陣列與排序後陣列在多少個
索引上不同。因此核心工作只有兩步：

1. 取得由小到大的預期高度順序。
2. 逐一比較目前高度與同一位置的預期高度。

兩種解法的差別在於如何取得「下一個預期高度」：

- 解法一直接複製並排序整個陣列，寫法直觀。
- 解法二利用高度只會落在 1 到 100 的條件，先統計每種高度出現次數，再按值域
  由小到大展開預期順序。

兩個公開方法都不修改呼叫端傳入的 `heights`。可執行案例會為每種解法準備
獨立副本，並在比較答案時一併檢查輸入是否保持原樣。

## 解法一：複製後排序

### 設計說明

`HeightChecker` 先以集合運算式 `[.. heights]` 建立輸入副本 `expected`，再使用
`Array.Sort` 將副本遞增排序。原始 `heights` 保存目前順序，`expected` 表示正確
排隊順序，最後同步走訪兩個陣列並累計不同的位置。

流程如下：

```text
expected = heights 的副本
將 expected 遞增排序
mismatchCount = 0

對每個索引 i：
    如果 heights[i] != expected[i]：
        mismatchCount 加一

回傳 mismatchCount
```

複製是這個設計的重要部分。如果直接排序 `heights`，目前順序就會被覆蓋，既
無法再與 expected 比較，也會意外改動呼叫端資料。

### 正確性說明

排序完成後，`expected[i]` 正是索引 `i` 按非遞減身高排列時應出現的值。迴圈
檢查每一個有效索引，而且只在 `heights[i]` 與 `expected[i]` 不同時增加計數。
因此迴圈結束後，計數恰好等於題目要求的不相符索引數量。

### 複雜度

- 時間複雜度：`O(n log n)`，主要成本是排序副本。
- 輔助空間複雜度：`O(n)`，用來保存 `expected` 副本；排序實作的額外堆疊不會
  改變整體 `O(n)` 上界。
- 結果空間複雜度：`O(1)`，只回傳一個整數。

### 範例演示流程

以 `[1, 1, 4, 2, 1, 3]` 為例：

1. 複製目前順序：`expected = [1, 1, 4, 2, 1, 3]`。
2. 排序副本：`expected = [1, 1, 1, 2, 3, 4]`。
3. 逐位比較：

| 索引 | `heights[i]` | `expected[i]` | 是否不同 | 累計 |
|---:|---:|---:|:---:|---:|
| 0 | 1 | 1 | 否 | 0 |
| 1 | 1 | 1 | 否 | 0 |
| 2 | 4 | 1 | 是 | 1 |
| 3 | 2 | 2 | 否 | 1 |
| 4 | 1 | 3 | 是 | 2 |
| 5 | 3 | 4 | 是 | 3 |

最終回傳 3，且原始 `heights` 沒有被排序或修改。

## 解法二：計數排序

### 設計說明

`HeightChecker2` 利用 `1 <= heights[i] <= 100` 的有限值域，建立長度 101 的
`frequencies`。索引代表高度，陣列值代表該高度尚未放入預期順序的數量。

第一輪走訪統計每種高度：

```text
frequencies[height]++
```

第二輪沿著原始 `heights` 比較。`expectedHeight` 從 1 開始；當目前高度的頻率
為 0，就持續向右移到下一個仍有剩餘數量的高度。這個高度正是排序後目前索引
應出現的值。比較完成後將該頻率減一，代表已消耗一名這個高度的學生。

流程如下：

```text
統計每種高度的 frequencies
expectedHeight = 1
mismatchCount = 0

依序讀取每個 actualHeight：
    略過 frequency 為 0 的高度
    expectedHeight 即為目前位置的預期高度
    若 actualHeight != expectedHeight，mismatchCount 加一
    frequencies[expectedHeight] 減一

回傳 mismatchCount
```

這種方法沒有建立完整的排序陣列，也沒有修改輸入；它只維護每個高度尚未使用
的次數。

### 關鍵不變量

每次比較開始時，所有小於 `expectedHeight` 的頻率都已耗盡，而
`frequencies[expectedHeight]` 大於 0。因此 `expectedHeight` 必定是尚未放入
預期順序的最小高度，也就是目前索引在非遞減排序中應出現的高度。

### 正確性說明

頻率陣列完整記錄所有輸入高度的出現次數。第二輪每次都選擇尚未使用的最小
高度，並在使用後扣除一次，所以產生的預期高度序列與將 `heights` 遞增排序的
結果完全相同。程式再逐位置比較實際高度與這個預期高度，因此累計值就是所有
不相符索引的數量。

### 複雜度

令 `n` 為學生數量，`k` 為高度值域大小；本題 `k = 100`。

- 時間複雜度：`O(n + k)`。統計需要 `O(n)`，預期高度指標在整次執行中最多
  掃過 `k` 個桶，比較另需 `O(n)`。
- 輔助空間複雜度：`O(k)`，用來保存頻率陣列；在本題固定值域下也可視為
  `O(1)`。
- 結果空間複雜度：`O(1)`，只回傳一個整數。

### 範例演示流程

仍以 `[1, 1, 4, 2, 1, 3]` 為例，統計結果為：

```text
高度 1：3 次
高度 2：1 次
高度 3：1 次
高度 4：1 次
```

按頻率由小到大展開時，預期序列為 `1, 1, 1, 2, 3, 4`：

| 索引 | 實際高度 | 預期高度 | 使用後剩餘頻率 | 是否不同 | 累計 |
|---:|---:|---:|:---|:---:|---:|
| 0 | 1 | 1 | 高度 1 剩 2 | 否 | 0 |
| 1 | 1 | 1 | 高度 1 剩 1 | 否 | 0 |
| 2 | 4 | 1 | 高度 1 剩 0 | 是 | 1 |
| 3 | 2 | 2 | 高度 2 剩 0 | 否 | 1 |
| 4 | 1 | 3 | 高度 3 剩 0 | 是 | 2 |
| 5 | 3 | 4 | 高度 4 剩 0 | 是 | 3 |

最終同樣回傳 3。

## 兩種解法比較

| 項目 | 解法一：複製後排序 | 解法二：計數排序 |
|:---|:---|:---|
| 預期順序來源 | 排序完整副本 | 依高度頻率逐步展開 |
| 時間複雜度 | `O(n log n)` | `O(n + k)` |
| 輔助空間 | `O(n)` | `O(k)` |
| 是否修改輸入 | 否 | 否 |
| 優點 | 直觀、容易套用到一般整數值域 | 利用小值域避免比較排序 |
| 限制 | 排序成本較高 | 依賴已知且不大的高度值域 |

若沒有高度值域限制，複製排序法較通用；本題高度只在 1 到 100 之間，計數排序
則能更直接地利用題目條件。

## 測試案例

`RunSamples` 使用下列固定案例。每個案例會分別檢查兩種解法，所以共有 14 項
檢查：

| 案例 | 輸入 | 預期 | 驗證重點 |
|:---|:---|---:|:---|
| 官方範例一 | `[1, 1, 4, 2, 1, 3]` | 3 | 部分位置不同、重複高度 |
| 官方範例二 | `[5, 1, 2, 3, 4]` | 5 | 所有位置不同 |
| 官方範例三 | `[1, 2, 3, 4, 5]` | 0 | 已符合預期順序 |
| 單一最小值 | `[1]` | 0 | 最小正式輸入長度 |
| 重複值亂序 | `[2, 2, 1, 1]` | 4 | 重複值與全部位置不同 |
| 值域上下界 | `[100, 1, 100, 1]` | 2 | 高度 1 與 100 |
| 防禦性空陣列 | `[]` | 0 | 額外支援的空輸入 |

每個解法只有在「答案等於預期值」且「呼叫後輸入仍與原值相同」時才會顯示
`Result: PASS`。若任何檢查失敗，程式會設定非零結束碼。

## 專案結構

```text
leetcode_1051/
├── docs/
│   └── readme-template.md
├── leetcode_1051/
│   ├── leetcode_1051.csproj
│   └── Program.cs
├── leetcode_1051.sln
└── README.md
```

## 建置與執行

需求：

- .NET 10 SDK

從 `leetcode_1051` repository 根目錄依序執行：

```powershell
dotnet restore leetcode_1051/leetcode_1051.csproj
dotnet build leetcode_1051/leetcode_1051.csproj --nologo
dotnet run --project leetcode_1051/leetcode_1051.csproj
```

本專案目前沒有獨立的自動測試專案；`dotnet build` 加上可執行的自我驗證案例
就是目前的驗收方式。

### 執行結果

```text
Case: 官方範例一
Input: [1, 1, 4, 2, 1, 3]
Expected: 3
HeightChecker: 3 | Input preserved: PASS | Result: PASS
HeightChecker2: 3 | Input preserved: PASS | Result: PASS

Case: 官方範例二
Input: [5, 1, 2, 3, 4]
Expected: 5
HeightChecker: 5 | Input preserved: PASS | Result: PASS
HeightChecker2: 5 | Input preserved: PASS | Result: PASS

Case: 官方範例三：已排序
Input: [1, 2, 3, 4, 5]
Expected: 0
HeightChecker: 0 | Input preserved: PASS | Result: PASS
HeightChecker2: 0 | Input preserved: PASS | Result: PASS

Case: 單一最小值
Input: [1]
Expected: 0
HeightChecker: 0 | Input preserved: PASS | Result: PASS
HeightChecker2: 0 | Input preserved: PASS | Result: PASS

Case: 重複值亂序
Input: [2, 2, 1, 1]
Expected: 4
HeightChecker: 4 | Input preserved: PASS | Result: PASS
HeightChecker2: 4 | Input preserved: PASS | Result: PASS

Case: 值域上下界
Input: [100, 1, 100, 1]
Expected: 2
HeightChecker: 2 | Input preserved: PASS | Result: PASS
HeightChecker2: 2 | Input preserved: PASS | Result: PASS

Case: 防禦性空陣列
Input: []
Expected: 0
HeightChecker: 0 | Input preserved: PASS | Result: PASS
HeightChecker2: 0 | Input preserved: PASS | Result: PASS

Summary: 14/14 checks passed.
```
