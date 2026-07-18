# 1493. Longest Subarray of 1's After Deleting One Element／刪除一個元素後全為 1 的最長子陣列

使用滑動視窗在一次線性掃描內找出答案，同時正確處理「必須刪除恰好一個元素」的限制。

- [LeetCode English](https://leetcode.com/problems/longest-subarray-of-1s-after-deleting-one-element/)
- [LeetCode 中文](https://leetcode.cn/problems/longest-subarray-of-1s-after-deleting-one-element/)

## 題目說明

給定只包含 `0` 與 `1` 的陣列 `nums`，刪除恰好一個元素後，回傳結果陣列中只包含
`1` 的最長非空連續子陣列長度。若不存在這樣的子陣列，回傳 `0`。

## 限制條件

- `1 <= nums.Length <= 100000`
- `nums[i]` 為 `0` 或 `1`
- 必須刪除恰好一個元素，而不是至多刪除一個元素

## 解法與核心不變量

`LongestSubarray(int[] nums)` 維護一個最多包含一個 `0` 的滑動視窗。右指標逐步擴張；
當視窗出現第二個 `0` 時，左指標向右移動，直到視窗重新只含至多一個 `0`。

有效視窗的元素數量是 `right - left + 1`。由於題目要求必須刪除一個元素，候選答案
固定為 `right - left`：視窗含 `0` 時刪除該 `0`；視窗全為 `1` 時則刪除其中一個 `1`。
這也避免把全為 `1` 的長度 `n` 陣列誤答成 `n`。公開 API 不輸出、不修改輸入陣列。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n)`，每個索引最多被左右指標各造訪一次 |
| 輔助空間 | `O(1)` |
| 結果空間 | `O(1)` |

## 逐步範例

以 `nums = [1, 1, 0, 1]` 為例：

```plaintext
right = 0..1：視窗為 [1, 1]，刪除一個元素後候選長度為 1
right = 2：視窗為 [1, 1, 0]，刪除 0 後候選長度為 2
right = 3：視窗為 [1, 1, 0, 1]，刪除 0 後候選長度為 3
答案：3
```

## Acceptance Harness

`Main` 提供十個確定性案例；每個 PASS 同時要求回傳值正確及輸入陣列完全未變。

| # | 案例 | 預期 |
| --- | --- | ---: |
| 1 | 官方案例 `[1, 1, 0, 1]` | 3 |
| 2 | 官方案例 `[0, 1, 1, 1, 0, 1, 1, 0, 1]` | 5 |
| 3 | 官方案例／全為 `1` | 2 |
| 4 | 最小輸入 `[0]` | 0 |
| 5 | 最小輸入 `[1]` | 0 |
| 6 | 全為 `0` | 0 |
| 7 | `0` 位於左邊界 | 3 |
| 8 | 刪除中間 `0` 合併兩段 `1` | 5 |
| 9 | 連續兩個 `0` 觸發視窗收縮 | 3 |
| 10 | 長度 100000、單一中間 `0` | 99999 |

本專案沒有正式測試專案；`Main` 是可重複執行的驗收機制。任何案例失敗時，程式會將
process exit code 設為 1。

## 建置與執行

從 repository 根目錄執行：

```bash
dotnet build leetcode_1493/leetcode_1493/leetcode_1493.csproj --nologo
dotnet run --no-build --project leetcode_1493/leetcode_1493/leetcode_1493.csproj
```

若直接以 `leetcode_1493/` 作為 workspace，從題目根目錄執行：

```bash
dotnet build leetcode_1493/leetcode_1493.csproj --nologo
dotnet run --no-build --project leetcode_1493/leetcode_1493.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Input: nums = [1, 1, 0, 1]
Expected: 3
Actual: 3
Result: PASS

Case: Official example 2
Input: nums = [0, 1, 1, 1, 0, 1, 1, 0, 1]
Expected: 5
Actual: 5
Result: PASS

Case: Official example 3 / all ones
Input: nums = [1, 1, 1]
Expected: 2
Actual: 2
Result: PASS

Case: Minimum input / zero
Input: nums = [0]
Expected: 0
Actual: 0
Result: PASS

Case: Minimum input / one
Input: nums = [1]
Expected: 0
Actual: 0
Result: PASS

Case: All zeros
Input: nums = [0, 0, 0]
Expected: 0
Actual: 0
Result: PASS

Case: Zero at the left boundary
Input: nums = [0, 1, 1, 1]
Expected: 3
Actual: 3
Result: PASS

Case: Bridge two runs of ones
Input: nums = [1, 1, 0, 1, 1, 1, 0, 1, 1]
Expected: 5
Actual: 5
Result: PASS

Case: Shrink across consecutive zeros
Input: nums = [1, 0, 0, 1, 1, 1]
Expected: 3
Actual: 3
Result: PASS

Case: Maximum length / one middle zero
Input: nums = [50000 ones, 0, 49999 ones]
Expected: 99999
Actual: 99999
Result: PASS

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
leetcode_1493/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/2026-07-18-leetcode-1493-net10-migration.md
│       └── specs/2026-07-18-leetcode-1493-net10-migration-design.md
└── leetcode_1493/
    ├── Program.cs
    └── leetcode_1493.csproj
```
