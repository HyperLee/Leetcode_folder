# LeetCode 033 - Search in Rotated Sorted Array

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)

這個專案使用 C# console app 實作 LeetCode 33「Search in Rotated Sorted Array」。題目要求在可能被旋轉過的遞增排序陣列中查找目標值，並在二分搜尋解法中維持 `O(log n)` 時間複雜度。

## 題目說明

給定一個原本以遞增順序排列、且元素值不重複的整數陣列 `nums`。在傳入函式前，陣列可能已經在某個未知索引被向左旋轉，例如：

```text
[0, 1, 2, 4, 5, 6, 7] -> [4, 5, 6, 7, 0, 1, 2]
```

請找出 `target` 在旋轉後陣列中的索引；若不存在，回傳 `-1`。

## 限制條件

- `1 <= nums.length <= 5000`
- `-10^4 <= nums[i] <= 10^4`
- `nums` 中的每個值都不重複
- `nums` 原本依遞增順序排序，之後可能被旋轉
- `-10^4 <= target <= 10^4`
- 二分搜尋解法需達到 `O(log n)` 時間複雜度

## 解題概念與出發點

旋轉排序陣列可以視為兩段各自遞增的區間：

```text
[4, 5, 6, 7, 0, 1, 2]
 ^^^^^^^^^^^  ^^^^^^^
 左側遞增段   右側遞增段
```

如果使用線性搜尋，可以直接逐一比對，實作簡單但時間複雜度為 `O(n)`。若要滿足題目的 `O(log n)` 要求，則需要先用二分搜尋找出最小值位置，也就是右側遞增段的起點，再判斷 `target` 應該落在哪一段遞增區間，最後只在該區間內做二分搜尋。

## 解法設計

### 解法一：線性搜尋 `Search`

`Search` 從索引 `0` 開始逐一掃描陣列：

1. 檢查目前元素是否等於 `target`。
2. 如果相等，立即回傳目前索引。
3. 如果掃描完整個陣列仍找不到，回傳 `-1`。

範例流程：

```text
nums = [4, 5, 6, 7, 0, 1, 2], target = 0

index 0 -> nums[0] = 4，不符合
index 1 -> nums[1] = 5，不符合
index 2 -> nums[2] = 6，不符合
index 3 -> nums[3] = 7，不符合
index 4 -> nums[4] = 0，符合，回傳 4
```

### 解法二：旋轉點 + 二分搜尋 `Search2`

`Search2` 由兩個輔助步驟組成：

1. `findMin` 使用二分搜尋找出最小值索引，也就是旋轉後右側遞增段的起點。
2. 透過 `target > nums[n - 1]` 判斷目標值應位於左側或右側遞增段。
3. `lowerBound` 只在選定的遞增段中搜尋第一個大於或等於 `target` 的位置。
4. 若該位置的值等於 `target`，回傳索引；否則回傳 `-1`。

範例流程：

```text
nums = [4, 5, 6, 7, 0, 1, 2], target = 0

findMin 找到最小值 0 的索引為 4
nums[n - 1] = 2
target = 0，不大於 2，因此 target 只可能在右側遞增段 [0, 1, 2]
lowerBound 在索引範圍 [4, 6] 中搜尋 0
找到 nums[4] = 0，回傳 4
```

找不到目標值的流程：

```text
nums = [4, 5, 6, 7, 0, 1, 2], target = 3

findMin 找到最小值 0 的索引為 4
nums[n - 1] = 2
target = 3，大於 2，因此 target 只可能在左側遞增段 [4, 5, 6, 7]
lowerBound 在索引範圍 [0, 3] 中搜尋 3
第一個大於或等於 3 的位置是索引 0，但 nums[0] = 4，不等於 3
回傳 -1
```

## 執行方式

需要 .NET SDK 10.0 或更新版本。

```bash
dotnet build leetcode_033/leetcode_033.csproj
dotnet run --project leetcode_033/leetcode_033.csproj
```

目前專案的範例輸出：

```text
CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.
Example 1: nums = [4, 5, 6, 7, 0, 1, 2], target = 0
  Search  => 4 (expected: 4)
  Search2 => 4 (expected: 4)
Example 2: nums = [4, 5, 6, 7, 0, 1, 2], target = 3
  Search  => -1 (expected: -1)
  Search2 => -1 (expected: -1)
Example 3: nums = [1], target = 0
  Search  => -1 (expected: -1)
  Search2 => -1 (expected: -1)
```

> [!NOTE]
> `CSSM_ModuleLoad()` 是目前 macOS/.NET 執行環境顯示的警告；本專案建置與執行命令仍以 exit code `0` 完成。

## 專案結構

```text
.
├── README.md
├── docs
│   └── readme-template.md
└── leetcode_033
    ├── Program.cs
    └── leetcode_033.csproj
```
