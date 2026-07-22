# 1685. Sum of Absolute Differences in a Sorted Array／有序陣列中絕對差值之和

- [LeetCode English](https://leetcode.com/problems/sum-of-absolute-differences-in-a-sorted-array/)
- [LeetCode 繁體中文](https://leetcode.cn/problems/sum-of-absolute-differences-in-a-sorted-array/)

## 題意

給定一個依非遞減順序排列的整數陣列 `nums`，建立同長度的 `result`。每個 `result[i]` 是
`nums[i]` 與陣列內所有其他元素之絕對差值總和。

## 限制條件

- `2 <= nums.Length <= 100000`
- `1 <= nums[i] <= 10000`
- `nums` 依非遞減順序排列

## 核心不變量與陷阱

處理索引 `i` 時，`leftSum` 是 `nums[0..i-1]` 的總和，而總和扣除 `leftSum` 與
`nums[i]` 後就是右側總和。排序性使左側差值都能寫成 `nums[i] - nums[j]`，右側差值都能
寫成 `nums[j] - nums[i]`，因此不必逐對呼叫 `Math.Abs`。

常見錯誤是把目前元素重複算進左側或右側、左右元素個數差一，或使用雙層迴圈造成 O(n²)。
本實作不排序、不修改 `nums`；harness 每一項都會比對呼叫前後完整陣列。

## 採用解法：總和與前綴和

`GetSumAbsoluteDifferences(int[] nums)` 先計算全陣列總和，再由左至右掃描。對索引 `i`：

- 左側差值：`nums[i] * i - leftSum`
- 右側差值：`rightSum - nums[i] * (nums.Length - i - 1)`

兩者相加就是 `result[i]`。API 本身沒有 console side effect。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n)` |
| 結果空間 | `O(n)` |
| 輔助空間 | `O(1)` |

### 逐步走查

對 `[2, 3, 5]`，總和為 10：

```plaintext
i = 0：leftSum = 0，左側差值 = 0，右側差值 = 8 - 2 * 2 = 4
i = 1：leftSum = 2，左側差值 = 3 - 2 = 1，右側差值 = 5 - 3 = 2
i = 2：leftSum = 5，左側差值 = 5 * 2 - 5 = 5，右側差值 = 0
結果：[4, 3, 5]
```

## Acceptance Harness

`Main` 有八個確定性案例；每個 PASS 同時要求回傳陣列正確與輸入陣列完全未變。

| # | 案例 | 預期 |
| --- | --- | --- |
| 1 | Official example 1：`[2, 3, 5]` | `[4, 3, 5]` |
| 2 | Official example 2：`[1, 4, 6, 8, 10]` | `[24, 15, 13, 15, 21]` |
| 3 | 最小長度／相等值：`[1, 1]` | `[0, 0]` |
| 4 | 最小與最大值：`[1, 10000]` | `[9999, 9999]` |
| 5 | 重複群組：`[1, 1, 3, 3]` | `[4, 4, 4, 4]` |
| 6 | 偏斜差距回歸：`[1, 2, 10]` | `[10, 9, 17]` |
| 7 | 連續平衡值：`[1, 2, 3, 4, 5]` | `[10, 7, 6, 7, 10]` |
| 8 | 長度 100000：前半為 1、後半為 10000 | 每項皆為 `499950000` |

## 已驗證命令

以下命令從 repository root 執行：

```bash
jq empty leetcode_1685/.vscode/launch.json leetcode_1685/.vscode/tasks.json
dotnet build leetcode_1685/leetcode_1685/leetcode_1685.csproj --nologo
dotnet run --no-build --project leetcode_1685/leetcode_1685/leetcode_1685.csproj
```

若直接開啟 `leetcode_1685/` 作為 VS Code workspace，使用相對巢狀路徑：

```bash
dotnet build leetcode_1685/leetcode_1685.csproj --nologo
dotnet run --no-build --project leetcode_1685/leetcode_1685.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [2, 3, 5]
Expected: [4, 3, 5]
Actual: [4, 3, 5]
Input preserved: YES
Result: PASS

Case: Official example 2
Input: [1, 4, 6, 8, 10]
Expected: [24, 15, 13, 15, 21]
Actual: [24, 15, 13, 15, 21]
Input preserved: YES
Result: PASS

Case: Minimum length / equal values
Input: [1, 1]
Expected: [0, 0]
Actual: [0, 0]
Input preserved: YES
Result: PASS

Case: Minimum and maximum values
Input: [1, 10000]
Expected: [9999, 9999]
Actual: [9999, 9999]
Input preserved: YES
Result: PASS

Case: Duplicate groups
Input: [1, 1, 3, 3]
Expected: [4, 4, 4, 4]
Actual: [4, 4, 4, 4]
Input preserved: YES
Result: PASS

Case: Skewed distances regression
Input: [1, 2, 10]
Expected: [10, 9, 17]
Actual: [10, 9, 17]
Input preserved: YES
Result: PASS

Case: Balanced consecutive values
Input: [1, 2, 3, 4, 5]
Expected: [10, 7, 6, 7, 10]
Actual: [10, 7, 6, 7, 10]
Input preserved: YES
Result: PASS

Case: Maximum length / boundary values
Input: [1, 1, 1, ..., 10000, 10000, 10000] (length 100000)
Expected: [499950000, 499950000, 499950000, ..., 499950000, 499950000, 499950000] (length 100000)
Actual: [499950000, 499950000, 499950000, ..., 499950000, 499950000, 499950000] (length 100000)
Input preserved: YES
Result: PASS

Summary: 8/8 checks passed.
```

## 最終結構

```plaintext
leetcode_1685/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1685/
    ├── Program.cs
    └── leetcode_1685.csproj
```
