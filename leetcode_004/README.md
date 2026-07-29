# LeetCode 4 — Median of Two Sorted Arrays

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)
[![Difficulty](https://img.shields.io/badge/difficulty-Hard-CB2431)](https://leetcode.com/problems/median-of-two-sorted-arrays/)

以 C# 實作「兩個正序陣列的中位數」，同時保留容易理解的合併排序解法，以及符合題目效率要求的二分搜尋解法。程式內建六組案例，執行後會比較預期值與兩種解法的結果。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：合併後排序](#解法一合併後排序)
- [解法二：二分搜尋分割點](#解法二二分搜尋分割點)
- [執行與偵錯](#執行與偵錯)
- [實際執行結果](#實際執行結果)

## 題目說明

給定兩個由小到大排序的整數陣列 `nums1` 與 `nums2`，找出兩個陣列所有元素組合後的中位數。

- 若元素總數是奇數，中位數是排序後位於正中央的元素。
- 若元素總數是偶數，中位數是中央兩個元素的平均值。
- 題目要求整體時間複雜度應為 `O(log(m+n))`。

題目來源：[LeetCode 4. Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/)

### 官方範例

```text
nums1 = [1, 3], nums2 = [2]
合併後 = [1, 2, 3]
中位數 = 2
```

```text
nums1 = [1, 2], nums2 = [3, 4]
合併後 = [1, 2, 3, 4]
中位數 = (2 + 3) / 2 = 2.5
```

## 限制條件

令 `m = nums1.Length`、`n = nums2.Length`：

- `0 <= m <= 1000`
- `0 <= n <= 1000`
- `1 <= m + n <= 2000`
- `-10^6 <= nums1[i], nums2[i] <= 10^6`
- `nums1` 與 `nums2` 各自皆為遞增排序
- 任一陣列可以是空陣列，但兩者不能同時為空

## 解題概念與出發點

「中位數」只和排序後的中央位置有關，不需要知道每個元素的總和。最直觀的想法是先把兩個陣列合併並排序；這能快速建立正確性基準，但排序全部元素的成本高於題目要求。

更進一步觀察：如果能把兩個已排序陣列各切成左右兩半，並同時滿足下列條件，就不需要真的合併陣列：

1. 左半部的元素數量等於右半部，或只多一個。
2. 左半部所有元素都不大於右半部所有元素。

找到這個分割點後，中位數只會來自分割邊界的最多四個值。因為陣列已排序，可以在較短陣列上使用二分搜尋調整分割點。

| 解法 | 核心策略 | 時間複雜度 | 額外空間 | 符合題目效率要求 |
| --- | --- | --- | --- | --- |
| `FindMedianSortedArrays` | 合併、排序、讀取中央位置 | `O((m+n) log(m+n))` | `O(m+n)` | 否 |
| `FindMedianSortedArrays2` | 在較短陣列二分搜尋合法分割 | `O(log(min(m,n)))` | `O(1)` | 是 |

## 解法一：合併後排序

### 設計說明

1. 使用 `Concat` 將 `nums1` 與 `nums2` 複製到新陣列。
2. 使用 `Array.Sort` 將合併陣列由小到大排序。
3. 若總長度是奇數，回傳索引 `length / 2` 的值。
4. 若總長度是偶數，取索引 `length / 2 - 1` 與 `length / 2` 的平均值。

這個方法不會修改呼叫端提供的兩個原始陣列。它的流程直接對應中位數定義，適合用來理解問題及作為其他解法的結果基準；缺點是必須配置合併陣列，並重新排序本來已經各自有序的資料。

### 範例演示

輸入：

```text
nums1 = [1, 2]
nums2 = [3, 4]
```

流程：

```text
合併： [1, 2] + [3, 4]
結果： [1, 2, 3, 4]
排序： [1, 2, 3, 4]
長度： 4（偶數）
中央索引：1、2
中央數值：2、3
中位數：(2 + 3) / 2 = 2.5
```

## 解法二：二分搜尋分割點

### 設計說明

1. 確保 `nums1` 是較短的陣列；若不是，就交換兩個參數後重新呼叫相同的二分搜尋方法。
2. 在 `nums1` 上選擇分割點 `partitionX`。
3. 依左右元素數量必須平衡的條件，推導另一個分割點：

   ```text
   partitionY = (m + n + 1) / 2 - partitionX
   ```

4. 取得兩個分割點左右的邊界值：

   ```text
   maxLeftX   | minRightX
   maxLeftY   | minRightY
   ```

5. 合法分割必須同時滿足：

   ```text
   maxLeftX <= minRightY
   maxLeftY <= minRightX
   ```

6. 若 `maxLeftX > minRightY`，代表 `nums1` 左半部過大，將分割點向左移；否則向右移。
7. 找到合法分割後：
   - 總長度為奇數：回傳 `max(maxLeftX, maxLeftY)`。
   - 總長度為偶數：回傳左側最大值與右側最小值的平均。

當分割點位於陣列開頭或結尾時，程式使用 `int.MinValue` 或 `int.MaxValue` 表示該側沒有元素，讓空陣列和邊界案例能沿用相同判斷。

### 範例演示

輸入：

```text
nums1 = [1, 3]
nums2 = [2]
```

因為第一個陣列較長，先交換：

```text
X = [2]，長度 1
Y = [1, 3]，長度 2
```

第一次分割：

```text
partitionX = 0
partitionY = 2

X: [-∞ | 2]
Y: [1, 3 | +∞]

maxLeftY = 3 > minRightX = 2
X 的左半部太小，partitionX 向右移。
```

第二次分割：

```text
partitionX = 1
partitionY = 1

X: [2 | +∞]
Y: [1 | 3]

2 <= 3，且 1 <= +∞，分割合法。
總長度 3 為奇數，中位數 = max(2, 1) = 2。
```

## 內建案例

每個案例都會分別執行兩種解法，共進行 12 次檢查。每種解法使用獨立的輸入副本，避免其中一種實作若在未來改為原地操作時影響另一種解法。

| 案例 | `nums1` | `nums2` | 預期中位數 | 驗證重點 |
| --- | --- | --- | ---: | --- |
| 奇數總長度 | `[1, 3]` | `[2]` | `2` | 單一中央元素 |
| 偶數總長度 | `[1, 2]` | `[3, 4]` | `2.5` | 中央兩數平均 |
| 第一個陣列為空 | `[]` | `[1]` | `1` | 空陣列與邊界分割 |
| 第一個陣列較長 | `[1, 2, 9]` | `[3]` | `2.5` | 二分搜尋交換參數 |
| 負數 | `[-5, -3, -1]` | `[-2]` | `-2.5` | 負值排序與平均 |
| 重複值 | `[0, 0]` | `[0, 0]` | `0` | 相等邊界值 |

## 執行與偵錯

### 必要環境

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- 選用：[Visual Studio Code](https://code.visualstudio.com/) 與 C# 偵錯擴充套件

### 終端機

在本專案根目錄執行：

```bash
dotnet build leetcode_004/leetcode_004.csproj
dotnet run --project leetcode_004/leetcode_004.csproj --no-build
```

### Visual Studio Code

專案提供單一預設建置工作與單一偵錯設定：

1. 用 VS Code 開啟本專案根目錄。
2. 開啟 `Run and Debug`。
3. 按下 `F5`。

VS Code 會先建置巢狀專案，再直接啟動 `net10.0` DLL；不需要另外輸入或選擇設定名稱。

## 實際執行結果

以下內容來自 `dotnet run --project leetcode_004/leetcode_004.csproj --no-build`：

```text
4. Median of Two Sorted Arrays
================================
Case 1: Odd total length
  nums1: [1, 3]
  nums2: [2]
  Expected: 2
  Merge and sort: 2 (PASS)
  Binary search: 2 (PASS)

Case 2: Even total length
  nums1: [1, 2]
  nums2: [3, 4]
  Expected: 2.5
  Merge and sort: 2.5 (PASS)
  Binary search: 2.5 (PASS)

Case 3: Empty nums1
  nums1: []
  nums2: [1]
  Expected: 1
  Merge and sort: 1 (PASS)
  Binary search: 1 (PASS)

Case 4: nums1 is longer
  nums1: [1, 2, 9]
  nums2: [3]
  Expected: 2.5
  Merge and sort: 2.5 (PASS)
  Binary search: 2.5 (PASS)

Case 5: Negative values
  nums1: [-5, -3, -1]
  nums2: [-2]
  Expected: -2.5
  Merge and sort: -2.5 (PASS)
  Binary search: -2.5 (PASS)

Case 6: Duplicate values
  nums1: [0, 0]
  nums2: [0, 0]
  Expected: 0
  Merge and sort: 0 (PASS)
  Binary search: 0 (PASS)

Summary: 12/12 checks passed.
```

## 專案結構

```text
.
├── .vscode/
│   ├── launch.json          # 單一 coreclr 偵錯設定
│   └── tasks.json           # 預設 dotnet build 工作
├── docs/
│   └── readme-template.md   # README 建立規範
├── leetcode_004/
│   ├── Program.cs           # 範例入口與兩種中位數解法
│   └── leetcode_004.csproj  # .NET 10 主控台專案
├── leetcode_004.sln
└── README.md
```
