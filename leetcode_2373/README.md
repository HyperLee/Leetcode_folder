# LeetCode 2373：矩陣中的局部最大值

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Language](https://img.shields.io/badge/C%23-Console-239120?logo=csharp)

這是一個使用 .NET 10 撰寫的主控台教學專案，示範如何找出正方形矩陣中每個 `3 x 3` 區域的最大值。專案保留直觀的固定視窗掃描法，並加入單調佇列的兩階段滑動視窗解法，方便比較兩種設計的思考方式。

## 快速連結

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：固定 3 x 3 視窗掃描](#解法一固定-3-x-3-視窗掃描)
- [解法二：單調佇列兩階段滑動視窗](#解法二單調佇列兩階段滑動視窗)
- [複雜度比較](#複雜度比較)
- [建置與執行](#建置與執行)
- [測試案例與實際輸出](#測試案例與實際輸出)

## 題目說明

給定一個大小為 `n x n` 的整數矩陣 `grid`，建立大小為 `(n - 2) x (n - 2)` 的結果矩陣 `maxLocal`。結果中的 `maxLocal[row][column]`，必須等於輸入矩陣中以 `(row, column)` 為左上角之 `3 x 3` 區域的最大值。

```text
輸入：
[
  [9, 9, 8, 1],
  [5, 6, 2, 6],
  [8, 6, 2, 4],
  [6, 2, 2, 2]
]

輸出：
[
  [9, 9],
  [8, 6]
]
```

### 限制條件

- `n == grid.Length`
- `n == grid[i].Length`
- `3 <= n <= 100`
- `1 <= grid[i][j] <= 100`
- 輸入是每列長度相同的正方形矩陣。

本專案依照題目保證處理合法輸入，不另外加入空矩陣、非正方形矩陣或 `n < 3` 的防禦性檢查。

## 解題概念與出發點

長度為 `n` 的一列中，寬度為 `3` 的連續視窗有 `n - 3 + 1 = n - 2` 個合法起點。列方向也是相同情況，因此輸出大小為 `(n - 2) x (n - 2)`。

每一個輸出位置都能直接映射回輸入視窗：

```text
result[row][column]
    ↕
grid[row .. row + 2][column .. column + 2]
```

由此可得到兩種方向：

1. 直接走訪每個輸出位置，再掃描對應的九個元素。
2. 利用「二維固定視窗最大值可以拆成橫向最大值，再做縱向最大值」的性質，套用一維滑動視窗最大值演算法。

兩個公開方法都只讀取 `grid`，不會排序、覆寫或重組輸入資料。

## 解法一：固定 3 x 3 視窗掃描

```csharp
public static int[][] LargestLocal(int[][] grid)
```

### 設計說明

這是最直接、最容易從題意推導的解法：

1. 建立 `(n - 2) x (n - 2)` 的結果矩陣。
2. 使用 `row` 與 `column` 枚舉每個輸出位置。
3. 對每個輸出位置，再使用兩層迴圈掃描對應的 `3 x 3` 輸入視窗。
4. 將走訪到的九個元素與目前最大值比較。
5. 把最後的最大值保存在 `result[row][column]`。

題目保證元素至少為 `1`，所以新建陣列的初始值 `0` 可以安全地作為每個視窗最大值的起始值。

### 範例演示流程

| 輸出位置 | 對應輸入範圍 | 九個元素 | 最大值 |
| --- | --- | --- | ---: |
| `[0][0]` | 列 `0..2`、欄 `0..2` | `9,9,8 / 5,6,2 / 8,6,2` | 9 |
| `[0][1]` | 列 `0..2`、欄 `1..3` | `9,8,1 / 6,2,6 / 6,2,4` | 9 |
| `[1][0]` | 列 `1..3`、欄 `0..2` | `5,6,2 / 8,6,2 / 6,2,2` | 8 |
| `[1][1]` | 列 `1..3`、欄 `1..3` | `6,2,6 / 6,2,4 / 2,2,2` | 6 |

依輸出座標排列後得到 `[[9, 9], [8, 6]]`。

### 正確性說明

外層兩個迴圈完整枚舉 `(n - 2) x (n - 2)` 個合法視窗起點；內層兩個迴圈恰好走訪該起點涵蓋的九個元素。每個輸出格保存這九個值的最大值，因此符合題目對所有輸出位置的定義。

## 解法二：單調佇列兩階段滑動視窗

```csharp
public static int[][] LargestLocal2(int[][] grid)
```

核心 helper：

```csharp
private static int[] GetSlidingWindowMaximums(int[] values, int windowSize)
```

### 設計說明

這個解法將二維 `3 x 3` 最大值拆成兩次一維滑動視窗：

1. **橫向階段**：對每一列計算寬度為 `3` 的視窗最大值，產生 `n x (n - 2)` 中間矩陣。
2. **縱向階段**：逐欄取出中間矩陣的數值，再計算高度為 `3` 的視窗最大值，填入 `(n - 2) x (n - 2)` 結果矩陣。

一維 helper 使用陣列保存索引，並以 `head`、`tail` 表示單調遞減佇列的有效範圍。佇列遵守三個不變條件：

- 只保留目前視窗內的索引。
- 從首端到尾端，索引對應的數值保持嚴格遞減。
- 首端永遠是目前視窗最大值的索引。

處理新元素時，先淘汰離開視窗的首端索引，再從尾端移除所有小於或等於目前值的索引，最後加入目前索引。較小且更早離開視窗的元素不可能再成為後續最大值，因此可以安全移除。每個索引最多加入與移除各一次，一列或一欄的處理時間為線性。

### 範例演示流程

先對每列求寬度為 3 的最大值：

```text
[9, 9, 8, 1] -> [9, 9]
[5, 6, 2, 6] -> [6, 6]
[8, 6, 2, 4] -> [8, 6]
[6, 2, 2, 2] -> [6, 2]
```

中間矩陣為：

```text
[
  [9, 9],
  [6, 6],
  [8, 6],
  [6, 2]
]
```

再逐欄求高度為 3 的最大值：

```text
第 0 欄：[9, 6, 8, 6] -> [9, 8]
第 1 欄：[9, 6, 6, 2] -> [9, 6]
```

把欄結果放回正確座標後得到 `[[9, 9], [8, 6]]`。

### 正確性說明

橫向階段的每個中間值，是原矩陣同一列連續三個元素的最大值。縱向階段再取同一輸出欄位、連續三列中間值的最大值，等價於從原矩陣對應的三列、三欄共九個元素中取最大值。

## 複雜度比較

| 解法 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- |
| `LargestLocal` 固定視窗掃描 | `O((n-2)² x 9) = O(n²)` | `O(1)`，不含輸出 | 直觀、常數小，適合固定 3 x 3 視窗 |
| `LargestLocal2` 兩階段單調佇列 | `O(n²)` | `O(n²)` | 可推廣至較大的固定視窗 |

> [!NOTE]
> 題目的視窗固定為 `3 x 3`，所以兩種解法的大 O 時間複雜度相同。單調佇列版本的價值主要在於展示可推廣到較大視窗的通用技巧，而不是讓固定常數更快。

## 測試設計

`Main` 執行五組固定案例：

| 案例 | 驗證目的 | 預期輸出 |
| --- | --- | --- |
| 官方 4 x 4 範例 | 一般多視窗行為 | `[[9, 9], [8, 6]]` |
| 3 x 3 最小尺寸 | 只有一個合法視窗 | `[[9]]` |
| 全重複值 | 重複元素與佇列淘汰規則 | `[[5, 5], [5, 5]]` |
| 最大值位於視窗邊界 | 確認首尾元素不會被忽略 | `[[9, 8], [7, 6]]` |
| 遞增 5 x 5 多視窗 | 多列多欄滑動與座標映射 | `[[13, 14, 15], [18, 19, 20], [23, 24, 25]]` |

每組案例會檢查兩個解法的輸出，以及兩份獨立輸入在執行後是否保持不變，共 `5 x 4 = 20` 項。任一檢查失敗時，程式會設定非零結束碼。

## 建置與執行

需求：已安裝支援 `net10.0` 的 .NET 10 SDK。從 repository 根目錄執行：

```bash
dotnet restore leetcode_2373/leetcode_2373.csproj
dotnet build leetcode_2373/leetcode_2373.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_2373/leetcode_2373.csproj
```

本專案沒有獨立的自動化測試專案；主控台測試入口、成功／失敗統計與程序結束碼共同構成驗收測試。

## 測試案例與實際輸出

以下內容來自 `dotnet run --no-build --project leetcode_2373/leetcode_2373.csproj` 的實際執行：

```text
Case: 官方 4x4 範例
  LargestLocal
    Expected: [[9, 9], [8, 6]]
    Actual:   [[9, 9], [8, 6]]
    Result:   PASS
  LargestLocal2
    Expected: [[9, 9], [8, 6]]
    Actual:   [[9, 9], [8, 6]]
    Result:   PASS
  LargestLocal input unchanged: PASS
  LargestLocal2 input unchanged: PASS

Case: 3x3 最小尺寸
  LargestLocal
    Expected: [[9]]
    Actual:   [[9]]
    Result:   PASS
  LargestLocal2
    Expected: [[9]]
    Actual:   [[9]]
    Result:   PASS
  LargestLocal input unchanged: PASS
  LargestLocal2 input unchanged: PASS

Case: 全重複值
  LargestLocal
    Expected: [[5, 5], [5, 5]]
    Actual:   [[5, 5], [5, 5]]
    Result:   PASS
  LargestLocal2
    Expected: [[5, 5], [5, 5]]
    Actual:   [[5, 5], [5, 5]]
    Result:   PASS
  LargestLocal input unchanged: PASS
  LargestLocal2 input unchanged: PASS

Case: 最大值位於視窗邊界
  LargestLocal
    Expected: [[9, 8], [7, 6]]
    Actual:   [[9, 8], [7, 6]]
    Result:   PASS
  LargestLocal2
    Expected: [[9, 8], [7, 6]]
    Actual:   [[9, 8], [7, 6]]
    Result:   PASS
  LargestLocal input unchanged: PASS
  LargestLocal2 input unchanged: PASS

Case: 遞增 5x5 多視窗
  LargestLocal
    Expected: [[13, 14, 15], [18, 19, 20], [23, 24, 25]]
    Actual:   [[13, 14, 15], [18, 19, 20], [23, 24, 25]]
    Result:   PASS
  LargestLocal2
    Expected: [[13, 14, 15], [18, 19, 20], [23, 24, 25]]
    Actual:   [[13, 14, 15], [18, 19, 20], [23, 24, 25]]
    Result:   PASS
  LargestLocal input unchanged: PASS
  LargestLocal2 input unchanged: PASS

Summary: 20/20 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2373.sln
└── leetcode_2373/
    ├── leetcode_2373.csproj
    └── Program.cs
```

主要演算法、XML 文件、測試資料與輸出 helper 都集中在 `Program.cs`，以維持此單題主控台專案的簡潔結構。