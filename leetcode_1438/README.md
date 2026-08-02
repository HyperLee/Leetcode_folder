# LeetCode 1438：絕對差不超過限制的最長連續子陣列

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C%23](https://img.shields.io/badge/C%23-Console-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)

本專案以 .NET 10 console application 示範 LeetCode 1438。`Program.cs` 保留題目原始說明，並提供三種可直接執行、互相核對的解法：暴力法、`SortedSet` 滑動視窗，以及雙單調佇列滑動視窗。

## 題目說明

給定整數陣列 `nums` 與整數 `limit`，請找出最長的非空連續子陣列，使該子陣列中任意兩個元素的絕對差都不超過 `limit`，並回傳這個子陣列的長度。

例如 `nums = [8, 2, 4, 7]`、`limit = 4` 時，`[2, 4]` 與 `[4, 7]` 都是長度 2 的合法子陣列，但更長的範圍都會同時包含 2 與 8，或包含 2 與 7，因此答案是 `2`。

官方題目：[Longest Continuous Subarray With Absolute Diff Less Than or Equal to Limit](https://leetcode.com/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/description/)

## 限制條件

- `1 <= nums.length <= 10^5`
- `1 <= nums[i] <= 10^9`
- `0 <= limit <= 10^9`
- 題目的「任意兩元素最大絕對差」可以改寫成目前範圍的 `最大值 - 最小值`。
- 本地 harness 額外測試空陣列，三種方法在此情況回傳 `0`；空陣列不在原題輸入限制內。

## 解題概念與出發點

對固定的連續範圍 `[left, right]` 而言，任意兩個元素之間最大的絕對差，一定由範圍內的最大值與最小值產生。因此合法條件可簡化為：

```text
windowMax - windowMin <= limit
```

這個條件適合使用滑動視窗：

1. 右界 `right` 逐步向右加入元素，嘗試擴大範圍。
2. 如果 `windowMax - windowMin > limit`，目前範圍不合法，右界保持不動，向右移動左界 `left`。
3. 範圍恢復合法後，用 `right - left + 1` 更新答案。

三種解法的差別只在於「如何快速取得目前視窗的最大值與最小值」：

| 方法 | 主要 API | 維護最大值/最小值的方式 | 時間複雜度 | 額外空間 |
| --- | --- | --- | --- | --- |
| 暴力法 | `LongestSubarrayBruteForce` | 逐步更新目前範圍的 min/max | O(n²) | O(1) |
| 有序集合 | `LongestSubarrayWithSortedSet` | `SortedSet<(Value, Index)>` 的首尾元素 | O(n log n) | O(n) |
| 單調佇列 | `LongestSubarray` | 遞減最大值佇列與遞增最小值佇列 | O(n) | O(n) |

## 解法一：暴力法

### 設計方式

`LongestSubarrayBruteForce` 枚舉每一個可能的左界。對每個 `left`，再讓 `right` 從 `left` 向右延伸，同時更新：

- `currentMin`：目前範圍的最小值。
- `currentMax`：目前範圍的最大值。
- `best`：目前找到的最長合法長度。

當 `currentMax - currentMin > limit` 時，可以立即停止目前左界的搜尋。因為右界再往右只會加入更多元素，min 不會變大、max 不會變小，範圍不可能重新合法。

### 範例演示：`[8, 2, 4, 7]`, `limit = 4`

| 左界 | 右界延伸 | 結果 |
| --- | --- | --- |
| 0 | `[8]` 合法，長度 1；加入 2 後差值為 6，超限，停止 | 最佳 1 |
| 1 | `[2]` 合法；`[2, 4]` 合法，長度 2；加入 7 後差值為 5，停止 | 最佳 2 |
| 2 | `[4]` 合法；`[4, 7]` 合法，長度 2 | 最佳 2 |
| 3 | `[7]` 合法，長度 1 | 最佳 2 |

這個方法最容易直接驗證正確性，但在 `n = 10^5` 時最壞可能檢查 O(n²) 個範圍，因此只適合小型資料與教學對照。

## 解法二：SortedSet 滑動視窗

### 設計方式

`LongestSubarrayWithSortedSet` 使用 `SortedSet<(int Value, int Index)>` 保存目前視窗的所有元素：

- `Value` 讓集合依數值排序。
- `Index` 是第二排序鍵，確保相同數值的不同位置仍會各自保留。
- `window.Min.Value` 是目前最小值。
- `window.Max.Value` 是目前最大值。

每次加入 `nums[right]` 後，檢查 `window.Max.Value - window.Min.Value`。若超過 `limit`，就移除左界項目 `(nums[left], left)` 並右移 `left`，直到範圍重新合法。

### 範例演示：`[10, 1, 2, 4, 7, 2]`, `limit = 5`

| `right` | 目前視窗 | min / max | 動作與目前最佳答案 |
| ---: | --- | --- | --- |
| 0 | `[10]` | 10 / 10 | 合法，最佳 1 |
| 1 | `[10, 1]` | 1 / 10 | 差值 9 超限，移除 10，視窗變成 `[1]` |
| 2 | `[1, 2]` | 1 / 2 | 合法，最佳 2 |
| 3 | `[1, 2, 4]` | 1 / 4 | 合法，最佳 3 |
| 4 | `[1, 2, 4, 7]` | 1 / 7 | 差值 6 超限，移除 1，視窗變成 `[2, 4, 7]` |
| 5 | `[2, 4, 7, 2]` | 2 / 7 | 差值 5 合法，長度 4，答案為 4 |

此方法比暴力法有效率，且資料結構直接表達「取得目前最小值與最大值」的需求；代價是每次插入與刪除都需要 O(log n)。

## 解法三：雙單調佇列滑動視窗

### 設計方式

`LongestSubarray` 是保留的主要 API，使用兩個只保存索引的 `LinkedList<int>`：

- `maxDeque`：對應值由前到後遞減，佇列首端永遠是目前最大值的索引。
- `minDeque`：對應值由前到後遞增，佇列首端永遠是目前最小值的索引。

加入新元素時：

1. 從 `maxDeque` 尾端移除所有小於或等於新值的索引，因為它們不可能再成為視窗最大值。
2. 從 `minDeque` 尾端移除所有大於或等於新值的索引，因為它們不可能再成為視窗最小值。
3. 將新索引加入兩個佇列尾端。

視窗超限時，向右移動 `left`。如果某個佇列首端正好等於離開的左界，就移除該首端索引；其他索引仍可能在下一個視窗繼續提供最大值或最小值。

### 範例演示：`[8, 2, 4, 7]`, `limit = 4`

| `right` | 最大值佇列的值 | 最小值佇列的值 | 視窗處理 |
| ---: | --- | --- | --- |
| 0 | `[8]` | `[8]` | `[8]` 合法，長度 1 |
| 1 | `[8, 2]` | `[2]` | 差值 6 超限，移除索引 0，視窗變成 `[2]` |
| 2 | `[4]` | `[2, 4]` | 2 被新值 4 從最大值佇列淘汰，`[2, 4]` 合法 |
| 3 | `[7]` | `[2, 4, 7]` | 差值 5 超限，移除索引 1，得到 `[4, 7]`，長度 2 |

每個索引最多被加入、從尾端淘汰或因左界移動而從首端移除一次，所以攤銷時間複雜度為 O(n)，是符合題目最大輸入限制的最佳解法。

## 執行方式

請在本 README 所在的專案根目錄執行：

```bash
dotnet restore leetcode_1438/leetcode_1438.csproj
dotnet build leetcode_1438/leetcode_1438.csproj --nologo
dotnet run --project leetcode_1438/leetcode_1438.csproj
```

專案沒有另外建立 automated test project；`Main` 會以固定輸入同時驗證三種解法，任何案例失敗時程序會以非零 exit code 結束。

## 範例執行結果

以下輸出由 `dotnet run --project leetcode_1438/leetcode_1438.csproj` 實際產生：

```text
[Example 1] nums=[8, 2, 4, 7], limit=4; Expected=2; LongestSubarray=2; LongestSubarrayWithSortedSet=2; LongestSubarrayBruteForce=2; PASS
[Example 2] nums=[10, 1, 2, 4, 7, 2], limit=5; Expected=4; LongestSubarray=4; LongestSubarrayWithSortedSet=4; LongestSubarrayBruteForce=4; PASS
[Example 3] nums=[4, 2, 2, 2, 4, 4, 2, 2], limit=0; Expected=3; LongestSubarray=3; LongestSubarrayWithSortedSet=3; LongestSubarrayBruteForce=3; PASS
[Single element] nums=[5], limit=0; Expected=1; LongestSubarray=1; LongestSubarrayWithSortedSet=1; LongestSubarrayBruteForce=1; PASS
[Duplicate values] nums=[2, 2, 2, 2], limit=0; Expected=4; LongestSubarray=4; LongestSubarrayWithSortedSet=4; LongestSubarrayBruteForce=4; PASS
[All values valid] nums=[1, 3, 2, 4], limit=3; Expected=4; LongestSubarray=4; LongestSubarrayWithSortedSet=4; LongestSubarrayBruteForce=4; PASS
[Regression - middle value reconnect] nums=[1, 10, 5], limit=5; Expected=2; LongestSubarray=2; LongestSubarrayWithSortedSet=2; LongestSubarrayBruteForce=2; PASS
[Empty input] nums=[], limit=0; Expected=0; LongestSubarray=0; LongestSubarrayWithSortedSet=0; LongestSubarrayBruteForce=0; PASS
Summary: 8/8 cases passed.
```

## 專案結構

```text
.
├── leetcode_1438.sln
├── leetcode_1438
│   ├── Program.cs
│   └── leetcode_1438.csproj
├── docs
│   └── readme-template.md
└── .vscode
    ├── launch.json
    └── tasks.json
```

`bin/` 與 `obj/` 是 .NET 建置產物，依 `.gitignore` 保持未追蹤。
