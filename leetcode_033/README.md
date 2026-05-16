# LeetCode 033 - Search in Rotated Sorted Array

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)

這個專案使用 C# console app 實作 LeetCode 33「Search in Rotated Sorted Array」。題目要求在可能被旋轉過的遞增排序陣列中查找目標值，並示範線性搜尋與兩種維持 `O(log n)` 時間複雜度的二分搜尋寫法。

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

如果使用線性搜尋，可以直接逐一比對，實作簡單但時間複雜度為 `O(n)`。若要滿足題目的 `O(log n)` 要求，可以使用兩種二分搜尋設計：

- 先用二分搜尋找出最小值位置，也就是右側遞增段的起點，再判斷 `target` 應該落在哪一段遞增區間。
- 每一輪都以 `mid` 作為分割點，判斷目前切出的左右兩部分哪一側仍然有序，再用有序區間判斷 `target` 是否位於其中。

## 三種解法比較

| 解法 | 核心概念 | 時間複雜度 | 空間複雜度 | 優點 | 缺點 | 推薦程度 |
| --- | --- | --- | --- | --- | --- | --- |
| `Search` | 從頭到尾逐一比對 `target` | `O(n)` | `O(1)` | 實作最直覺，容易驗證正確性 | 不符合題目要求的 `O(log n)` | 適合入門理解，不建議提交 |
| `Search2` | 先找旋轉點，再於對應遞增段做 `LowerBound` 二分搜尋 | `O(log n)` | `O(1)` | 搜尋區間切分明確，輔助方法可重用 | 需要先找最小值，流程與邊界判斷較多 | 可提交，但維護成本較高 |
| `Search3` | 每輪判斷哪一半有序，直接排除不可能區間 | `O(log n)` | `O(1)` | 單次二分搜尋即可完成，符合題目效率要求且實作集中 | 條件判斷需要小心處理旋轉區間邊界 | 最推薦作為提交解法 |

綜合可讀性、效率與題目限制，`Search3` 是最推薦的版本；`Search2` 同樣符合 `O(log n)`，但比較適合用來練習「找旋轉點 + 標準二分」的拆解思路。`Search` 可作為基準解法與範例驗證，但不符合正式提交所需的時間複雜度。

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

1. `FindMin` 使用二分搜尋找出最小值索引，也就是旋轉後右側遞增段的起點。
2. 透過 `target > nums[n - 1]` 判斷目標值應位於左側或右側遞增段。
3. `LowerBound` 只在選定的遞增段中搜尋第一個大於或等於 `target` 的位置。
4. 若該位置的值等於 `target`，回傳索引；否則回傳 `-1`。

範例流程：

```text
nums = [4, 5, 6, 7, 0, 1, 2], target = 0

FindMin 找到最小值 0 的索引為 4
nums[n - 1] = 2
target = 0，不大於 2，因此 target 只可能在右側遞增段 [0, 1, 2]
LowerBound 在索引範圍 [4, 6] 中搜尋 0
找到 nums[4] = 0，回傳 4
```

找不到目標值的流程：

```text
nums = [4, 5, 6, 7, 0, 1, 2], target = 3

FindMin 找到最小值 0 的索引為 4
nums[n - 1] = 2
target = 3，大於 2，因此 target 只可能在左側遞增段 [4, 5, 6, 7]
LowerBound 在索引範圍 [0, 3] 中搜尋 3
第一個大於或等於 3 的位置是索引 0，但 nums[0] = 4，不等於 3
回傳 -1
```

### 解法三：判斷有序半邊的二分搜尋 `Search3`

`Search3` 不先找旋轉點，而是在每一輪二分搜尋中直接判斷哪一側仍然有序：

1. 先檢查 `nums[mid]` 是否等於 `target`，若相等就直接回傳 `mid`。
2. 若 `nums[0] <= nums[mid]`，代表 `mid` 位於左側遞增段；此時可用 `[nums[0], nums[mid])` 判斷 `target` 是否在左半邊。
3. 若 `target` 落在左側有序區間，將右界縮到 `mid - 1`；否則改往右半邊搜尋。
4. 若 `nums[0] > nums[mid]`，代表 `mid` 位於右側遞增段；此時可用 `(nums[mid], nums[n - 1]]` 判斷 `target` 是否在右半邊。
5. 若 `target` 落在右側有序區間，將左界縮到 `mid + 1`；否則改往左半邊搜尋。

範例流程：

```text
nums = [4, 5, 6, 7, 0, 1, 2], target = 0

l = 0, r = 6, mid = 3, nums[mid] = 7
nums[0] <= nums[mid]，左側 [4, 5, 6, 7] 有序
target = 0 不在 [4, 7)，往右半邊搜尋

l = 4, r = 6, mid = 5, nums[mid] = 1
nums[0] > nums[mid]，右側 [1, 2] 有序
target = 0 不在 (1, 2]，往左半邊搜尋

l = 4, r = 4, mid = 4, nums[mid] = 0
找到 target，回傳 4
```

這個方法每一輪都能排除一半搜尋範圍，時間複雜度為 `O(log n)`，只使用固定數量變數，空間複雜度為 `O(1)`。

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
  Search3 => 4 (expected: 4)
Example 2: nums = [4, 5, 6, 7, 0, 1, 2], target = 3
  Search  => -1 (expected: -1)
  Search2 => -1 (expected: -1)
  Search3 => -1 (expected: -1)
Example 3: nums = [1], target = 0
  Search  => -1 (expected: -1)
  Search2 => -1 (expected: -1)
  Search3 => -1 (expected: -1)
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
